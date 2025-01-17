using FluentValidation;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class CreatePairDeviceCommandValidator : AbstractValidator<CreatePairDeviceCommand>
{
    public CreatePairDeviceCommandValidator()
    {
        RuleFor(x => x.DeviceName)
            .NotEmpty()
            .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.MissingDeviceName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü0-9]+( [a-zA-ZÇçĞğİıÖöŞşÜü0-9]+)*$")
            .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.InvalidDeviceName);

        RuleFor(x => x.FcmToken)
            .NotEmpty()
            .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.MissingFcmToken);

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.MissingUserId);

        RuleFor(x => x.OperatingSystem)
            .IsInEnum()
            .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.InvalidOperatingSystem);

        RuleFor(x => x.IsActive)
            .NotNull()
            .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.MissingIsActive);
    }
}
