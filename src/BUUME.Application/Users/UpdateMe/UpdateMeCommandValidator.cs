using System.Globalization;
using BUUME.Domain.Users;
using FluentValidation;

namespace BUUME.Application.Users.UpdateMe;

internal sealed class UpdateMeCommandValidator : AbstractValidator<UpdateMeCommand>
{
    public UpdateMeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(UserErrorCodes.UpdateMe.InvalidFirstName);
        
        RuleFor(x => x.LastName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(UserErrorCodes.UpdateMe.InvalidLastName);

        RuleFor(x => x.Email)
            .EmailAddress().WithErrorCode(UserErrorCodes.UpdateMe.InvalidEmail);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(UserErrorCodes.UpdateMe.MissingPhoneNumber)
            .Matches(@"^(?:\+90|0)?5\d{9}$")
            .WithErrorCode(UserErrorCodes.UpdateMe.InvalidPhoneNumber);

        RuleFor(person => person.BirthDate)
            .Must(BeAValidBirthDate).WithErrorCode(UserErrorCodes.UpdateMe.InvalidBirthDate);
        
        RuleFor(user => user.Gender)
            .Must(BeAValidGender).WithErrorCode(UserErrorCodes.UpdateMe.InvalidGender);
    }
    
    private static bool BeAValidBirthDate(string? date)
    {
        if(string.IsNullOrWhiteSpace(date)) return true;
        
        if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate < DateTime.Now;
        }
        return false;
    }
    private static bool BeAValidGender(int? gender)
    {
        if(gender == null) return true;
        
        return Enum.IsDefined(typeof(Gender), gender);
    }
}