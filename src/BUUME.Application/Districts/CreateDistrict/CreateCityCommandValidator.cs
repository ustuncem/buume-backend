using FluentValidation;

namespace BUUME.Application.Districts.CreateDistrict;

internal sealed class CreateDistrictCommandValidator : AbstractValidator<CreateDistrictCommand>
{
    public CreateDistrictCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(DistrictErrorCodes.CreateDistrict.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(DistrictErrorCodes.CreateDistrict.InvalidName);

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithErrorCode(DistrictErrorCodes.CreateDistrict.MissingCode);

        RuleFor(x => x.CityId)
            .NotEmpty()
            .WithErrorCode(DistrictErrorCodes.CreateDistrict.MissingCityId);
    }
}