using FluentValidation;

namespace BUUME.Application.Authentication.RefreshToken;

internal sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithErrorCode(AuthenticationErrorCodes.MissingPhoneNumber);

        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithErrorCode(AuthenticationErrorCodes.MissingCode);
    }
}