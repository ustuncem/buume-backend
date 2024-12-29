using BUUME.Application.Abstractions.Search;

namespace BUUME.Application.Cities.GetCitiesByCountryId;

public sealed record GetCitiesByCountryIdQuery(Guid CountryId, string? SearchTerm) : ISearchableQuery<IReadOnlyList<CityResponse>>
{
}
