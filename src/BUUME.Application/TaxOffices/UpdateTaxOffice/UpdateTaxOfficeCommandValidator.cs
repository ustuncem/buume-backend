using FluentValidation;
    
namespace BUUME.Application.TaxOffices.UpdateTaxOffice;

internal sealed class UpdateTaxOfficeCommandValidator : AbstractValidator<UpdateTaxOfficeCommand>
{
    public UpdateTaxOfficeCommandValidator()
    {
        RuleFor(to => to.Name).NotEmpty().Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(TaxOfficeErrorCodes.CreateTaxOffice.InvalidName);
    }
}