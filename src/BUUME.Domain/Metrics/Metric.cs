using BUUME.SharedKernel;

namespace BUUME.Domain.Metrics;

public sealed class Metric(Guid id, EntityType entityType, Guid entityId, MetricInfo info) : Entity(id)
{
    public EntityType EntityType { get; private set; } = entityType;
    public Guid EntityId { get; private set; } = entityId;
    public MetricInfo Info { get; private set; } = info;
}