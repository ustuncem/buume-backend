using FluentValidation;

namespace BUUME.Application.Cities.CreateCity;

internal sealed class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(CityErrorCodes.CreateCity.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(CityErrorCodes.CreateCity.InvalidName);

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithErrorCode(CityErrorCodes.CreateCity.MissingCode);

        RuleFor(x => x.CountryId)
            .NotEmpty()
            .WithErrorCode(CityErrorCodes.CreateCity.MissingCountryId);
    }
}