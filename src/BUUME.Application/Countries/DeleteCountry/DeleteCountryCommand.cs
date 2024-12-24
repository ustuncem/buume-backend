using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Countries.DeleteCountry;

public record DeleteCountryCommand(Guid Id) : ICommand<bool>;