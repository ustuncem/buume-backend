namespace BUUME.Domain.Abstractions.Events;

public sealed record EntityDeletedDomainEvent<T>(Guid EntityId) : IDomainEvent
    where T : Entity;