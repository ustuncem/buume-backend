using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Regions.DeleteRegion;

public record DeleteRegionCommand(Guid Id) : ICommand<bool>;