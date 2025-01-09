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

namespace BUUME.Application.Businesses.UpdateBusiness;

internal sealed class UpdateBusinessCommandHandler(
    IUserRepository userRepository,
    IDbConnectionFactory factory,
    IFileRepository fileRepository,
    IFileUploader fileUploader,
    IUserContext userContext,
    IBusinessRepository businessRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateBusinessCommand, Guid>
{

    public async Task<Result<Guid>> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        // Fetch the wanna be executive.
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<Guid>(UserErrors.NotFound);

        var storedBusiness = await businessRepository.GetByOwnerIdAsync(user.Id, cancellationToken);
        
        if (storedBusiness == null) return Result.Failure<Guid>(BusinessErrors.NotFound);
        
        // Handle logo and files
        var imageIds = await UploadLogoAndBusinessImagesIfAnyAsync(request.Logo, request.BusinessPhotos);
        
        if (!request.IsKvkkApproved) return Result.Failure<Guid>(BusinessErrors.KvkkNotApproved);
        
        var logoId = imageIds.Count > 0 ? imageIds[0] : storedBusiness.LogoId;
        var ownerId = user.Id;
        var countryId = request.CountryId;
        var cityId = request.CityId;
        var districtId = request.DistrictId;
        var taxOfficeId = request.TaxOfficeId;
        var baseInfo = new BaseInfo(request.Name, request.Email, request.PhoneNumber, request.Description,
            request.OnlineOrderLink, request.MenuLink, request.WebsiteLink);
        var addressInfo = new AddressInfo(request.Address, request.Latitude, request.Longitude);
        var taxInfo = new TaxInfo(request.TradeName, request.Vkn);
        var isKvkkApproved = new IsKvkkApproved(request.IsKvkkApproved);
        
        // Handle working hours if any
        TimeSpan? openingTime = !string.IsNullOrEmpty(request.StartTime) ? TimeSpan.Parse(request.StartTime) : null;
        TimeSpan? closingTime = !string.IsNullOrEmpty(request.EndTime) ? TimeSpan.Parse(request.EndTime) : null;
        var workingHours = openingTime != null && closingTime != null ? WorkingHours.Create(openingTime.Value, closingTime.Value) : null;
        
        storedBusiness.Update(logoId, ownerId, countryId, cityId, districtId, taxOfficeId, baseInfo, addressInfo, taxInfo, isKvkkApproved, workingHours);
        
        businessRepository.Update(storedBusiness);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await HandleCategories(request.BusinessCategoryIds, storedBusiness.Id);
        await HandleImages(imageIds.ToArray(), storedBusiness.Id);
        
        return Guid.NewGuid();
    }

    private async Task<List<Guid>> UploadLogoAndBusinessImagesIfAnyAsync(string? logo64, string[]? businessImages64 = null)
    {
        List<Guid> imageIds = [];
        
        if (businessImages64 == null) return imageIds; 
        
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
        
        const string deleteSql = 
            """
            DELETE FROM business_business_category WHERE business_id = @BusinessId
            """;
        
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
            
            await connection.QueryAsync(deleteSql, new { BusinessId = businessId }, transaction);
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

        const string deleteSql = 
            """
            DELETE FROM business_file WHERE business_id = @BusinessId
            """;
        
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
            
            await connection.QueryAsync(deleteSql, new { BusinessId = businessId }, transaction);

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