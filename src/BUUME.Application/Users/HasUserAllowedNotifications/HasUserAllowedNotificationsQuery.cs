using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.HasUserAllowedNotifications;

public sealed record HasUserAllowedNotificationsQuery() : IQuery<bool>;