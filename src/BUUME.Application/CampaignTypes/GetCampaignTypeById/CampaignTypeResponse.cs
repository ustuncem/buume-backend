namespace BUUME.Application.CampaignTypes.GetCampaignTypeById;

public sealed record CampaignTypeResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string Code { get; init; } = default!;

    public string Description { get; init; } = default!;
}