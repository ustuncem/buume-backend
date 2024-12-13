using BUUME.Application.TaxOffices.CreateTaxOffice;
using FluentValidation;

namespace BUUME.Application.TaxOffices.CreateTaxOffice;

public class CreateTaxOfficeCommandValidator : AbstractValidator<CreateTaxOfficeCommand>
{
    public CreateTaxOfficeCommandValidator()
    {
        RuleFor(to => to.Name).NotEmpty();
    }
}