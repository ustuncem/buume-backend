using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.Search;

namespace BUUME.Application.Countries.GetAllCountries;

public sealed record GetAllCountriesQuery(string? SearchTerm) : ISearchableQuery<IReadOnlyList<CountryResponse>>
{
}
