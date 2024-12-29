using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Cities.GetCitiesByCountryId;

internal sealed class GetCitiesByCountryIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetCitiesByCountryIdQuery, IReadOnlyList<CityResponse>>
{
    public async Task<Result<IReadOnlyList<CityResponse>>> Handle(GetCitiesByCountryIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                c.id AS Id,
                c.name AS Name
            FROM "cities" c
            WHERE c.deleted_at IS NULL
            AND c.name ILIKE @SearchTerm 
            AND c.country_id = @CountryId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var cities = await connection.QueryAsync<CityResponse>(
            sql,
            new { SearchTerm = $"%{query.SearchTerm}%", query.CountryId}
            );

        return cities.ToList();
    }
}