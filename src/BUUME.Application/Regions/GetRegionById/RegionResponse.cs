using BUUME.Application.Countries.GetCountryById;

namespace BUUME.Application.Regions.GetRegionById;

public sealed record RegionResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
    
    public CountryResponse Country { get; set; } = default!;
}