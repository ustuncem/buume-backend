using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Countries.GetAllCountries;

public sealed record GetAllCountriesQuery() : IQuery<IReadOnlyList<CountryResponse>>
{
}
