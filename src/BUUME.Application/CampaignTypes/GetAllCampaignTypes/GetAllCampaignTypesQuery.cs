using BUUME.Application.Abstractions.Search;

namespace BUUME.Application.CampaignTypes.GetAllCampaignTypes;

public sealed record GetAllCampaignTypesQuery(string? SearchTerm) : ISearchableQuery<IReadOnlyList<CampaignTypeResponse>>
{
}
