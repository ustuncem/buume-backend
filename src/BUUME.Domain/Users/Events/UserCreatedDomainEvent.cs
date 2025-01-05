using BUUME.SharedKernel;

namespace BUUME.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId, string ValidationToken) : IDomainEvent;