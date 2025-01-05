using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Authentication.Register;

public record RegisterCommand(string PhoneNumber) : ICommand<string>;