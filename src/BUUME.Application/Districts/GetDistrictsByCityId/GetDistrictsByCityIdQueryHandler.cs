using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Districts.GetDistrictsByCityId;

internal sealed class GetDistrictsByCityIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetDistrictsByCityIdQuery, IReadOnlyList<DistrictResponse>>
{
    public async Task<Result<IReadOnlyList<DistrictResponse>>> Handle(GetDistrictsByCityIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                d.id AS Id,
                d.name AS Name
            FROM "districts" d
            WHERE d.deleted_at IS NULL
            AND d.name ILIKE @SearchTerm 
            AND d.city_id = @CityId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var districts = await connection.QueryAsync<DistrictResponse>(
            sql,
            new { SearchTerm = $"%{query.SearchTerm}%", query.CityId}
            );

        return districts.ToList();
    }
}