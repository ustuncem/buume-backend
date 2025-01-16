using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.Data;
using BUUME.Domain.PairDevice;
using BUUME.SharedKernel;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class CreatePairDeviceCommandHandler(
    IPairDeviceRepository pairDeviceRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePairDeviceCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreatePairDeviceCommand request, CancellationToken cancellationToken)
    {
        var deviceName = new DeviceName(request.DeviceName);
        var fcmToken = new FcmToken(request.FcmToken);
        var userId = request.UserId;
        var operatingSystem = Enum.Parse<Domain.PairDevice.OperatingSystem>(request.OperatingSystem);
        var isActive = new IsActive(request.IsActive);
        var hasUserEnabledNotifications = request.HasUserEnabledNotifications;

        var pairDevice = PairDevice.Create(deviceName, fcmToken, userId, operatingSystem, isActive, hasUserEnabledNotifications);

        pairDeviceRepository.Add(pairDevice);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return pairDevice.Id;
    }
}