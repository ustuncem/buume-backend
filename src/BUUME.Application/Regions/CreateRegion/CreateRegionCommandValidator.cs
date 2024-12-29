using FluentValidation;

namespace BUUME.Application.Regions.CreateRegion;

internal sealed class CreateRegionCommandValidator : AbstractValidator<CreateRegionCommand>
{
    public CreateRegionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(RegionErrorCodes.CreateRegion.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(RegionErrorCodes.CreateRegion.InvalidName);

        RuleFor(x => x.CountryId)
            .NotEmpty()
            .WithErrorCode(RegionErrorCodes.CreateRegion.MissingCountryId);
    }
}