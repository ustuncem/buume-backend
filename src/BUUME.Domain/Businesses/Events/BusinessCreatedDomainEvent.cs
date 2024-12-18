using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses.Events;

public record BusinessCreatedDomainEvent(Guid BusinessId) : IDomainEvent;