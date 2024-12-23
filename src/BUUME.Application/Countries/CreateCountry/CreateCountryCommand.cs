using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Countries.CreateCountry;

public record CreateCountryCommand(string Name, string Code, bool HasRegion) : ICommand<Guid>;