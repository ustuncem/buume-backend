using FluentValidation;

namespace BUUME.Application.Countries.CreateCountry;

internal sealed class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(CountryErrorCodes.CreateCountry.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(CountryErrorCodes.CreateCountry.InvalidName);
    }
}