using FluentValidation;

namespace BUUME.Application.Authentication.ValidatePhoneNumber;

internal sealed class ValidatePhoneNumberCommandValidator : AbstractValidator<ValidatePhoneNumberCommand>
{
    public ValidatePhoneNumberCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(AuthenticationErrorCodes.MissingPhoneNumber)
            .Matches(@"^(?:\+90|0)?5\d{9}$")
            .WithErrorCode(AuthenticationErrorCodes.InvalidPhoneNumber);
        
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithErrorCode(AuthenticationErrorCodes.MissingCode)
            .MaximumLength(6)
            .WithErrorCode(AuthenticationErrorCodes.InvalidCode);
    }
}