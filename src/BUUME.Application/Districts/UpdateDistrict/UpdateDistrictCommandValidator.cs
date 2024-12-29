using FluentValidation;

namespace BUUME.Application.Districts.UpdateDistrict;

internal sealed class UpdateDistrictCommandValidator : AbstractValidator<UpdateDistrictCommand>
{
    public UpdateDistrictCommandValidator()
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