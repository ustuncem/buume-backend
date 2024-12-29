using BUUME.Application.Countries.GetCountryById;

namespace BUUME.Application.Cities.GetCityById;

public sealed record CityResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
    
    public CountryResponse Country { get; set; } = default!;
}