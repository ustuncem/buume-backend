using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Countries.GetCountryById;

public sealed record GetCountryByIdQuery(Guid CountryId) : IQuery<CountryResponse>
{
}
