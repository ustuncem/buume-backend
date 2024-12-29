using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Regions.CreateRegion;

public record CreateRegionCommand(string Name, Guid CountryId) : ICommand<Guid>;