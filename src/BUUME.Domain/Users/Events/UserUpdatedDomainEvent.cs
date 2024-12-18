using BUUME.SharedKernel;

namespace BUUME.Domain.Users.Events;

public sealed record UserUpdatedDomainEvent(Guid UserId) : IDomainEvent;