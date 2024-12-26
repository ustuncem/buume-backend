namespace BUUME.Application.CampaignTypes.GetAllCampaignTypes;

public sealed record CampaignTypeResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}