using BUUME.SharedKernel;

namespace BUUME.Domain.PairDevice.Events;

public sealed record PairDeviceCreatedDomainEvent(Guid UserId, bool HasUserEnabledNotifications) : IDomainEvent;