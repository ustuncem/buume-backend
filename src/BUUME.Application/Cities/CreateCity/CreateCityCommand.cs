using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Cities.CreateCity;

public record CreateCityCommand(string Name, string Code, Guid CountryId, Guid? RegionId) : ICommand<Guid>;