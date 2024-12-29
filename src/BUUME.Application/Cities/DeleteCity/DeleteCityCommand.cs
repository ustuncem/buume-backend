using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Cities.DeleteCity;

public record DeleteCityCommand(Guid Id) : ICommand<bool>;