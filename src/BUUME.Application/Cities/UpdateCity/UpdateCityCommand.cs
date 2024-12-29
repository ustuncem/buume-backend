using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Cities.UpdateCity;

public record UpdateCityCommand(Guid Id, string Name, string Code, Guid CountryId, Guid? RegionId) : ICommand<Guid>;