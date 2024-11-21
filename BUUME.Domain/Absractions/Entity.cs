namespace BUUME.Domain.Absractions;

public abstract class Entity(Guid id)
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public Guid Id { get; init; } = id;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    
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