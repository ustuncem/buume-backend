using FluentValidation;

namespace BUUME.Application.Businesses.UpdateBusiness;

internal sealed class UpdateBusinessCommandValidator : AbstractValidator<UpdateBusinessCommand>
{
    public UpdateBusinessCommandValidator()
    {
        RuleFor(x => x.CountryId)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingCountry);
        
        RuleFor(x => x.CityId)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingCity);
        
        RuleFor(x => x.DistrictId)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingDistrict);
        
        RuleFor(x => x.TaxOfficeId)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingTaxOffice);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(BusinessErrorCodes.InvalidName);
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithErrorCode(BusinessErrorCodes.InvalidEmail);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingPhone)
            .Matches(@"^(?:\+90|0)?5\d{9}$")
            .WithErrorCode(BusinessErrorCodes.InvalidPhone);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingAddress);
        
        RuleFor(x => x.Latitude)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingLatitude);
        
        RuleFor(x => x.Longitude)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingLongitude);
        
        RuleFor(x => x.TradeName)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingTradeName);
        
        RuleFor(x => x.Vkn)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingVkn);
        
        RuleFor(x => x.IsKvkkApproved)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingKvkk);
    }
}