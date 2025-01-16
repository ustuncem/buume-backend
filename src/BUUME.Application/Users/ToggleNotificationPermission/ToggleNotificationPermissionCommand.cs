using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.ToggleNotificationPermission;

public sealed record ToggleNotificationPermissionCommand() : ICommand<bool>;