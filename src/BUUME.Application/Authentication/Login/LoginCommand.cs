using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Authentication.Login;

public record LoginCommand(string PhoneNumber) : ICommand<string>;