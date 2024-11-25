using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;