using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Districts.GetDistrictsByCityId;

internal sealed class GetCitiesByCountryIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetDistrictsByCityIdQuery, IReadOnlyList<DistrictResponse>>
{
    public async Task<Result<IReadOnlyList<DistrictResponse>>> Handle(GetDistrictsByCityIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                c.id AS Id,
                c.name AS Name
            FROM "districts" c
            WHERE c.deleted_at IS NULL
            AND c.name ILIKE @SearchTerm 
            AND c.country_id = @CityId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var districts = await connection.QueryAsync<DistrictResponse>(
            sql,
            new { SearchTerm = $"%{query.SearchTerm}%", query.CityId}
            );

        return districts.ToList();
    }
}