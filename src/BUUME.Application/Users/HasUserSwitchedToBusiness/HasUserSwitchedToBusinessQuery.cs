using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.HasUserSwitchedToBusiness;

public sealed record HasUserSwitchedToBusinessQuery() : IQuery<bool>;