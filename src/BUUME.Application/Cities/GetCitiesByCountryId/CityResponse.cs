namespace BUUME.Application.Cities.GetCitiesByCountryId;

public sealed record CityResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}