using BUUME.SharedKernel;

namespace BUUME.Domain.PairDevice.Events;

public sealed record PairDeviceUpdatedDomainEvent(Guid PairDeviceId) : IDomainEvent;