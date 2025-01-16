using FluentValidation;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class CreatePairDeviceCommandValidator : AbstractValidator<CreatePairDeviceCommand>
{
    public CreatePairDeviceCommandValidator()
    {
        //RuleFor(x => x.Name)
        //    .NotEmpty()
        //    .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.MissingName)
        //    .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
        //    .WithErrorCode(PairDeviceErrorCodes.CreatePairDevice.InvalidName);
    }
}