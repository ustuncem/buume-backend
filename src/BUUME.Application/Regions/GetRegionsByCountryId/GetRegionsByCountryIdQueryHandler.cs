using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Regions.GetRegionsByCountryId;

internal sealed class GetAllTaxOfficeQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetRegionsByCountryIdQuery, IReadOnlyList<RegionResponse>>
{
    public async Task<Result<IReadOnlyList<RegionResponse>>> Handle(GetRegionsByCountryIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                c.id AS Id,
                c.name AS Name
            FROM "regions" c
            WHERE c.deleted_at IS NULL
            AND c.name ILIKE @SearchTerm 
            AND c.country_id = @CountryId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var countries = await connection.QueryAsync<RegionResponse>(
            sql,
            new { SearchTerm = $"%{query.SearchTerm}%", query.CountryId}
            );

        return countries.ToList();
    }
}