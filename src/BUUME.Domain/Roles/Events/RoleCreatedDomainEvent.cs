using BUUME.SharedKernel;

namespace BUUME.Domain.Roles.Events;

public sealed record RoleCreatedDomainEvent(Guid RoleId) : IDomainEvent;