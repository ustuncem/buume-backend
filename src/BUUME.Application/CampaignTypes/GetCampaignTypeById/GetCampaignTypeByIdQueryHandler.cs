using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.CampaignTypes;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.CampaignTypes.GetCampaignTypeById;

internal sealed class GetCampaignTypeByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetCampaignTypeByIdQuery, CampaignTypeResponse>
{
    public async Task<Result<CampaignTypeResponse>> Handle(GetCampaignTypeByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                c.id AS Id,
                c.name AS Name,
                c.code AS Code,
                c.description AS Description
            FROM "campaign_types" c
            WHERE c.id = @CampaignTypeId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        CampaignTypeResponse? campaignType = await connection.QueryFirstOrDefaultAsync<CampaignTypeResponse>(
            sql,
            query);

        if (campaignType is null)
        {
            return Result.Failure<CampaignTypeResponse>(CampaignTypeErrors.NotFound(query.CampaignTypeId));
        }

        return campaignType;
    }
}
