using BUUME.Domain.PairDevice.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.PairDevice;

public sealed class PairDevice : Entity
{
    private PairDevice(Guid id, DeviceName deviceName, FcmToken fcmToken, Guid userId, OperatingSystem operatingSystem, IsActive isActive) : base(id)
    {
        DeviceName = deviceName;
        FcmToken = fcmToken;
        UserId = userId;
        OperatingSystem = operatingSystem;
        IsActive = isActive;
    }
    public DeviceName DeviceName { get; private set; }
    public FcmToken FcmToken { get; private set; }
    public Guid UserId { get; private set; }
    public OperatingSystem OperatingSystem { get; private set; }
    public IsActive IsActive { get; private set; }

    public static PairDevice Create(DeviceName deviceName, FcmToken fcmToken, Guid userId, OperatingSystem operatingSystem, IsActive isActive)
    {
        var pairDevice = new PairDevice(Guid.NewGuid(), deviceName, fcmToken, userId, operatingSystem, isActive);
        pairDevice.RaiseDomainEvent(new PairDeviceCreatedDomainEvent(pairDevice.Id));
        return pairDevice;
    }

    public void Update(DeviceName deviceName, FcmToken fcmToken, OperatingSystem operatingSystem, IsActive isActive)
    {
        DeviceName = deviceName;
        FcmToken = fcmToken;
        OperatingSystem = operatingSystem;
        IsActive = isActive;
        
        RaiseDomainEvent(new PairDeviceUpdatedDomainEvent(Id));
    }
}