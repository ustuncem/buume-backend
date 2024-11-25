using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Roles.Events;

public sealed record RoleCreatedDomainEvent(Guid RoleId) : IDomainEvent;