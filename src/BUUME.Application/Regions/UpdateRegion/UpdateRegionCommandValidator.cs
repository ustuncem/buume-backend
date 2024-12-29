using BUUME.Application.Countries.UpdateCountry;
using FluentValidation;

namespace BUUME.Application.Regions.UpdateRegion;

internal sealed class UpdateRegionCommandValidator : AbstractValidator<UpdateRegionCommand>
{
    public UpdateRegionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(RegionErrorCodes.CreateRegion.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(RegionErrorCodes.CreateRegion.InvalidName);

        RuleFor(x => x.CountryId)
            .NotEmpty().WithErrorCode(RegionErrorCodes.CreateRegion.MissingCountryId);
    }
}