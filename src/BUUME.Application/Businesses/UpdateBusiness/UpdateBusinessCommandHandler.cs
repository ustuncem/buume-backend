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
        
        if (!request.IsKvkkApproved) return Result.Failure<Guid>(BusinessErrors.KvkkNotApproved);
        
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
        
        storedBusiness.Update(countryId, cityId, districtId, baseInfo, addressInfo, location, isKvkkApproved, workingHours);
        
        businessRepository.Update(storedBusiness);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await HandleCategories(request.BusinessCategoryIds, storedBusiness.Id);
        
        return Guid.NewGuid();
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
}