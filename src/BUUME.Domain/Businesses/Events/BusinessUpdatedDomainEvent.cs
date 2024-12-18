using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses.Events;

public record BusinessUpdatedDomainEvent(Guid BusinessId) : IDomainEvent;