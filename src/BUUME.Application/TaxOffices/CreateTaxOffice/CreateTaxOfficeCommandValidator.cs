using FluentValidation;

namespace BUUME.Application.TaxOffices.CreateTaxOffice;

internal sealed class CreateTaxOfficeCommandValidator : AbstractValidator<CreateTaxOfficeCommand>
{
    public CreateTaxOfficeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(TaxOfficeErrorCodes.CreateTaxOffice.MissingName)
            .Matches("^[a-zA-Z]+$")
            .WithErrorCode(TaxOfficeErrorCodes.CreateTaxOffice.InvalidName);
    }
}