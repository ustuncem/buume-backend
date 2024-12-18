using BUUME.SharedKernel;

namespace BUUME.Domain.Users.Events;

public sealed record UserDeletedDomainEvent(Guid UserId) : IDomainEvent;