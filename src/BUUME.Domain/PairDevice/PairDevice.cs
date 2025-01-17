using BUUME.Domain.PairDevice.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.PairDevice;

public sealed class PairDevice : Entity
{
    private PairDevice(Guid id, DeviceName deviceName, FcmToken fcmToken, Guid userId, OperatingSystem operatingSystem) : base(id)
    {
        DeviceName = deviceName;
        FcmToken = fcmToken;
        UserId = userId;
        OperatingSystem = operatingSystem;
        IsActive = new IsActive(true);
    }
    public DeviceName DeviceName { get; private set; }
    public FcmToken FcmToken { get; private set; }
    public Guid UserId { get; private set; }
    public OperatingSystem OperatingSystem { get; private set; }
    public IsActive IsActive { get; private set; }

    public static PairDevice Create(DeviceName deviceName, FcmToken fcmToken, Guid userId, OperatingSystem operatingSystem, bool hasUserEnabledNotifications)
    {
        var pairDevice = new PairDevice(Guid.NewGuid(), deviceName, fcmToken, userId, operatingSystem);
        pairDevice.RaiseDomainEvent(new PairDeviceCreatedDomainEvent(userId, hasUserEnabledNotifications));
        return pairDevice;
    }

    public void Update(DeviceName deviceName, FcmToken fcmToken, OperatingSystem operatingSystem)
    {
        DeviceName = deviceName;
        FcmToken = fcmToken;
        OperatingSystem = operatingSystem;
        IsActive = new IsActive(true);
        
        RaiseDomainEvent(new PairDeviceUpdatedDomainEvent(Id));
    }
}