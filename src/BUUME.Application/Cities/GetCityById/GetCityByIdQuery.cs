using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Cities.GetCityById;

public sealed record GetCityByIdQuery(Guid CityId) : IQuery<CityResponse>
{
}
