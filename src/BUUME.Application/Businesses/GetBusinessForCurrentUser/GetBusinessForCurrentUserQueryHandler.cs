using System.Data;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Businesses;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Businesses.GetBusinessForCurrentUser;

internal sealed class GetBusinessForCurrentUserQueryHandler(
    IDbConnectionFactory factory, 
    IUserRepository userRepository,
    IUserContext userContext)
    : IQueryHandler<GetBusinessForCurrentUserQuery, BusinessResponse>
{
    public async Task<Result<BusinessResponse>> Handle(GetBusinessForCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<BusinessResponse>(UserErrors.NotFound);
        
        const string sql =
            """
            SELECT 
                b.id as BusinessId,
                b.base_info_name AS Name,
                b.base_info_email AS Email,
                b.base_info_phone_number AS PhoneNumber,
                b.address AS Address,
                ST_X(location::geometry) AS longitude,
                ST_Y(location::geometry) AS latitude,
                b.is_kvkk_approved AS IsKvkkApproved,
                TO_CHAR(b.working_hours_start_time, 'HH24:MI') AS StartTime,
                TO_CHAR(b.working_hours_end_time, 'HH24:MI') AS EndTime,
                b.base_info_description AS Description,
                b.base_info_online_order_link AS OnlineOrderLink,
                b.base_info_menu_link AS MenuLink,
                b.base_info_website_link AS WebsiteLink,
                f.id as LogoId,
                f.path as Logo,
                td.path as TaxDocument,
                c.id AS CountryId,
                c.name AS CountryName,
                ci.id AS CityId,
                ci.name AS CityName,
                d.id AS DistrictId,
                d.name AS DistrictName
            FROM businesses b
            LEFT JOIN files f ON b.logo_id = f.id
            LEFT JOIN files td ON b.tax_document_id = td.id
            LEFT JOIN countries c ON b.country_id = c.id
            LEFT JOIN cities ci ON b.city_id = ci.id
            LEFT JOIN districts d ON b.district_id = d.id
            WHERE b.owner_id = @OwnerId;
            """;

        const string imagesAndCategoriesSql =
            """
            SELECT
                bc.id AS BusinessCategoryId,
                bc.name AS BusinessCategoryName
            FROM business_business_category bbc
            JOIN business_categories bc ON bc.id = bbc.business_category_id
            WHERE
                bbc.business_id = @BusinessId;
            SELECT
                f.id AS BusinessFileId,
                f.path AS BusinessFilePath
            FROM business_file bf
            JOIN files f ON bf.file_id = f.id
            WHERE 
                bf.business_id = @BusinessId AND f.path != @LogoPath;
            """;


        using IDbConnection connection = factory.GetOpenConnection();
        
        IEnumerable<BusinessResponse> businessList = await connection.QueryAsync<
            BusinessResponse, 
            CountryResponse, 
            CityResponse, 
            DistrictResponse,
            BusinessResponse>(
            sql,
            (business, country, city, district) =>
            {
                business.Country = country;
                business.City = city;
                business.District = district;
                return business;
            },
            new {OwnerId = user.Id},
            splitOn: "CountryId,CityId,DistrictId");
        
        var business = businessList.FirstOrDefault();
        
        if (business == null) return Result.Failure<BusinessResponse>(BusinessErrors.NotFound);
        
        var reader = await connection.QueryMultipleAsync(imagesAndCategoriesSql, new{business.BusinessId, LogoPath = business.Logo});
        
        var categories = await reader.ReadAsync<BusinessCategoryResponse>();
        var images = await reader.ReadAsync<BusinessFileResponse>();
        
        business.BusinessCategories = categories.ToArray();
        business.BusinessPhotos = images.ToArray();
        
        return business;
    }
}
