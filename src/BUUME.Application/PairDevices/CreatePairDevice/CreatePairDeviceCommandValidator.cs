using FluentValidation;
using OperatingSystem = BUUME.Domain.PairDevice.OperatingSystem;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class CreatePairDeviceCommandValidator : AbstractValidator<CreatePairDeviceCommand>
{
    public CreatePairDeviceCommandValidator()
    {
        RuleFor(x => x.DeviceName)
            .NotEmpty()
            .WithErrorCode(PairDeviceErrorCodes.MissingDeviceName);

        RuleFor(x => x.FcmToken)
            .NotEmpty()
            .WithErrorCode(PairDeviceErrorCodes.MissingFcmToken);

        RuleFor(x => x.OperatingSystem)
            .Must(ValidNotificationStatus)
            .WithErrorCode(PairDeviceErrorCodes.InvalidOperatingSystem);
    }
    
    private static bool ValidNotificationStatus(int hasAllowedNotifications)
    {
        return Enum.IsDefined(typeof(OperatingSystem), hasAllowedNotifications);
    }
}
