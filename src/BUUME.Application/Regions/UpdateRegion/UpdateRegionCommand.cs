using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Regions.UpdateRegion;

public record UpdateRegionCommand(Guid Id, string Name, Guid CountryId) : ICommand<Guid>;