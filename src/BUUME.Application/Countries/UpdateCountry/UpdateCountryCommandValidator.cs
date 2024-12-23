using FluentValidation;
    
namespace BUUME.Application.Countries.UpdateCountry;

internal sealed class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(CountryErrorCodes.CreateCountry.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(CountryErrorCodes.CreateCountry.InvalidName);
    }
}