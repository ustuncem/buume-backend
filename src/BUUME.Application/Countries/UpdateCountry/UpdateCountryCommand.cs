using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Countries.UpdateCountry;

public record UpdateCountryCommand(Guid Id, string Name, string Code) : ICommand<Guid>;