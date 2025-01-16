using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.PairDevices.CreatePairDevice;

public record CreatePairDeviceCommand(string DeviceName, string FcmToken, Guid UserId, string OperatingSystem, bool IsActive, bool HasUserEnabledNotifications) : ICommand<Guid>;