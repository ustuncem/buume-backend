using BUUME.SharedKernel.Events;

namespace BUUME.SharedKernel;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
    }
    
    protected Entity()
    {
        
    }
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public void Delete<T>() where T : Entity
    {
        DeletedAt = DateTime.UtcNow;
        RaiseDomainEvent(new EntityDeletedDomainEvent<T>(Id));
    }
    
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}