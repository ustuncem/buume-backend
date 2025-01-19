using FluentValidation;

namespace BUUME.Application.Businesses.CreateBusiness;

internal sealed class CreateBusinessCommandValidator : AbstractValidator<CreateBusinessCommand>
{
    public CreateBusinessCommandValidator()
    {
        RuleFor(x => x.Logo)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingLogo);
        
        RuleFor(x => x.Logo)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingTaxDocument);
        
        RuleFor(x => x.CountryId)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingCountry);
        
        RuleFor(x => x.CityId)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingCity);
        
        RuleFor(x => x.DistrictId)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingDistrict);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingName);
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithErrorCode(BusinessErrorCodes.InvalidEmail);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingPhone);

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingAddress);
        
        RuleFor(x => x.Latitude)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingLatitude);
        
        RuleFor(x => x.Longitude)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingLongitude);
        
        RuleFor(x => x.IsKvkkApproved)
            .NotEmpty()
            .WithErrorCode(BusinessErrorCodes.MissingKvkk);
    }
}