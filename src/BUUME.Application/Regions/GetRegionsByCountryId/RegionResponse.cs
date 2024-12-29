namespace BUUME.Application.Regions.GetRegionsByCountryId;

public sealed record RegionResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}