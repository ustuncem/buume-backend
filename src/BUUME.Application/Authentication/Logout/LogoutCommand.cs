using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Authentication.Logout;

public record LogoutCommand() : ICommand<bool>;