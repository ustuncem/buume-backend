using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.CampaignTypes;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.CampaignTypes.GetAllCampaignTypes;

internal sealed class GetAllCampaignTypesQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetAllCampaignTypesQuery, IReadOnlyList<CampaignTypeResponse>>
{
    public async Task<Result<IReadOnlyList<CampaignTypeResponse>>> Handle(GetAllCampaignTypesQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                c.id AS Id,
                c.name AS Name
            FROM "campaign_types" c
            WHERE c.deleted_at IS NULL
            AND (
                c.name ILIKE @SearchTerm OR
                c.code ILIKE @SearchTerm
                )
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var countries = await connection.QueryAsync<CampaignTypeResponse>(
            sql,
            new { SearchTerm = $"%{query.SearchTerm}%"}
            );

        return countries.ToList();
    }
}