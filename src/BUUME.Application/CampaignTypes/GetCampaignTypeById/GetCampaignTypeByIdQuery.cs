using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.CampaignTypes.GetCampaignTypeById;

public sealed record GetCampaignTypeByIdQuery(Guid CampaignTypeId) : IQuery<CampaignTypeResponse>
{
}
