using FluentValidation;

namespace BUUME.Application.TaxOffices.CreateTaxOffice;

internal sealed class CreateTaxOfficeCommandValidator : AbstractValidator<CreateTaxOfficeCommand>
{
    public CreateTaxOfficeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(TaxOfficeErrorCodes.CreateTaxOffice.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(TaxOfficeErrorCodes.CreateTaxOffice.InvalidName);
    }
}