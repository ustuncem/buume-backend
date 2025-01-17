using System.Data;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Files;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Businesses;
using BUUME.Domain.Files;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using Dapper;
using File = BUUME.Domain.Files.File;

namespace BUUME.Application.Businesses.CreateBusiness;

internal sealed class CreateBusinessCommandHandler(
    IUserRepository userRepository,
    IDbConnectionFactory factory,
    IFileRepository fileRepository,
    IFileUploader fileUploader,
    IUserContext userContext,
    IBusinessRepository businessRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateBusinessCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {
        // Fetch the wanna be executive.
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<Guid>(UserErrors.NotFound);
        
        // Handle logo and files
        var imageIds = await UploadLogoAndBusinessImagesIfAnyAsync(request.Logo, request.BusinessPhotos);
        
        if(imageIds.Count == 0) return Result.Failure<Guid>(BusinessErrors.NoLogoUploaded);

        if (!request.IsKvkkApproved) return Result.Failure<Guid>(BusinessErrors.KvkkNotApproved);
        
        var logoId = imageIds[0];
        var ownerId = user.Id;
        var countryId = request.CountryId;
        var cityId = request.CityId;
        var districtId = request.DistrictId;
        var baseInfo = new BaseInfo(request.Name, request.Email, request.PhoneNumber, request.Description,
            request.OnlineOrderLink, request.MenuLink, request.WebsiteLink);
        var addressInfo = new Address(request.Address);
        var location = Location.Create(request.Latitude, request.Longitude);
        var isKvkkApproved = new IsKvkkApproved(request.IsKvkkApproved);
        
        // Handle working hours if any
        TimeSpan? openingTime = !string.IsNullOrEmpty(request.StartTime) ? TimeSpan.Parse(request.StartTime) : null;
        TimeSpan? closingTime = !string.IsNullOrEmpty(request.EndTime) ? TimeSpan.Parse(request.EndTime) : null;
        var workingHours = openingTime != null && closingTime != null ? WorkingHours.Create(openingTime.Value, closingTime.Value) : null;
        
        var business = Business.Create(logoId, ownerId, countryId, cityId, districtId, baseInfo, addressInfo, location, isKvkkApproved, workingHours);
        
        user.ToggleBusinessSwitch();
        userRepository.Update(user);
        businessRepository.Add(business);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await HandleCategories(request.BusinessCategoryIds, business.Id);
        await HandleImages(imageIds.ToArray(), business.Id);
        
        return Guid.NewGuid();
    }

    private async Task<List<Guid>> UploadLogoAndBusinessImagesIfAnyAsync(string logo64, string[]? businessImages64 = null)
    {
        List<Guid> imageIds = [];
        var logoUploadResult = await fileUploader.UploadImageFromBase64Async(logo64);
        
        if (!logoUploadResult.IsSuccess && !string.IsNullOrWhiteSpace(logo64)) return imageIds;
        
        var logo = File.Create(
            logoUploadResult.Value.Size, 
            logoUploadResult.Value.Name, 
            logoUploadResult.Value.Path,
            logoUploadResult.Value.Type);
            
        fileRepository.Add(logo);
        imageIds.Add(logo.Id);

        if (businessImages64 != null && businessImages64.Length > 0)
        {
            foreach (var businessImage in businessImages64)
            {
                var businessImageUploadResult = await fileUploader.UploadImageFromBase64Async(businessImage);
                if (!businessImageUploadResult.IsSuccess) break;
                
                var businessImageFile = File.Create(
                    businessImageUploadResult.Value.Size, 
                    businessImageUploadResult.Value.Name, 
                    businessImageUploadResult.Value.Path,
                    businessImageUploadResult.Value.Type);
            
                fileRepository.Add(businessImageFile);
                imageIds.Add(businessImageFile.Id);
            }
        }
        
        return imageIds;
    }

    private async Task HandleCategories(Guid[]? guids, Guid businessId)
    {
        if (guids == null || guids.Length == 0) return;
        
        const string sql =
            """
            INSERT INTO business_business_category (business_category_id, business_id) VALUES (@CategoryId, @BusinessId)
            """;
        
        using IDbConnection connection = factory.GetOpenConnection();
        
        using var transaction = connection.BeginTransaction();

        try
        {
            var parameters = guids.Select(categoryId => new
            {
                CategoryId = categoryId,
                BusinessId = businessId
            });

            await connection.ExecuteAsync(sql, parameters, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
    
    private async Task HandleImages(Guid[]? guids, Guid businessId)
    {
        if (guids == null || guids.Length == 0) return;
        
        const string sql =
            """
            INSERT INTO business_file (business_id, file_id) VALUES (@BusinessId, @FileId)
            """;
        
        using IDbConnection connection = factory.GetOpenConnection();
        
        using var transaction = connection.BeginTransaction();

        try
        {
            var parameters = guids.Select(fileId => new
            {
                FileId = fileId,
                BusinessId = businessId
            });

            await connection.ExecuteAsync(sql, parameters, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}