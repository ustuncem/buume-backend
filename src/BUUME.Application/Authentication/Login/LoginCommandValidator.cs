using FluentValidation;

namespace BUUME.Application.Authentication.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(AuthenticationErrorCodes.MissingPhoneNumber)
            .Matches(@"^(?:\+90|0)?5\d{9}$")
            .WithErrorCode(AuthenticationErrorCodes.InvalidPhoneNumber);
    }
}