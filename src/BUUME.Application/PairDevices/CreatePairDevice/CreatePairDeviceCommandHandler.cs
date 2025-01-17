using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.Data;
using BUUME.Domain.PairDevice;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using OperatingSystem = BUUME.Domain.PairDevice.OperatingSystem;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class CreatePairDeviceCommandHandler(
    IPairDeviceRepository pairDeviceRepository,
    IUserRepository userRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePairDeviceCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreatePairDeviceCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(userId, cancellationToken);
        if(user == null) Result.Failure<Guid>(UserErrors.NotFound);
        
        var deviceName = new DeviceName(request.DeviceName);
        var fcmToken = new FcmToken(request.FcmToken);
        var operatingSystem = Enum.Parse<OperatingSystem>(request.OperatingSystem.ToString());
        
        var pairDevice = PairDevice.Create(deviceName, fcmToken, user.Id, operatingSystem, request.HasUserEnabledNotifications);
        
        pairDeviceRepository.Add(pairDevice);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return pairDevice.Id;
    }
}