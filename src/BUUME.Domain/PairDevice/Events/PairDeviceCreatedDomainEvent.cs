using BUUME.SharedKernel;

namespace BUUME.Domain.PairDevice.Events;

public sealed record PairDeviceCreatedDomainEvent(Guid PairDeviceId, Guid UserId, bool HasUserEnabledNotifications) : IDomainEvent;