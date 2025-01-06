using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;

namespace BUUME.Application.Users.Me;

public record MeQuery() : IQuery<UserResponse>;