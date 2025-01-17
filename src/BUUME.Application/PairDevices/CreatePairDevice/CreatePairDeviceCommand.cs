using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.PairDevices.CreatePairDevice;

public record CreatePairDeviceCommand(string DeviceName, string FcmToken, int OperatingSystem, bool HasUserEnabledNotifications) : ICommand<Guid>;