using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.DeleteMe;

public sealed record DeleteMeCommand() : IQuery<bool>;