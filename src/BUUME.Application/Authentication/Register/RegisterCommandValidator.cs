using FluentValidation;

namespace BUUME.Application.Authentication.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(AuthenticationErrorCodes.MissingPhoneNumber)
            .Matches(@"^(?:\+90|0)?5\d{9}$")
            .WithErrorCode(AuthenticationErrorCodes.InvalidPhoneNumber);
    }
}