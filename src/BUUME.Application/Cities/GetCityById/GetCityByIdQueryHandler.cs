using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Countries.GetCountryById;
using BUUME.Domain.Cities;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Cities.GetCityById;

internal sealed class GetCityByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetCityByIdQuery, CityResponse>
{
    public async Task<Result<CityResponse>> Handle(GetCityByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                ci.id AS Id,
                ci.name AS Name,
                ci.code AS Code,
                c.id AS Id,
                c.name AS Name,
                c.code AS Code,
                c.has_region AS HasRegion
            FROM "cities" ci
            LEFT JOIN "countries" c ON ci.country_id = c.id
            WHERE ci.id = @CityId;
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var sqlResponse = await connection.QueryAsync<CityResponse, CountryResponse, CityResponse>(
            sql,
            (city, country) =>
            {
                city.Country = country;
                return city;
            },
            query,
            splitOn: "Id");
        
        var city = sqlResponse.FirstOrDefault();

        if (city is null)
        {
            return Result.Failure<CityResponse>(CityErrors.NotFound(query.CityId));
        }

        return city;
    }
}
