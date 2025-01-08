using BUUME.Domain.Users.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id,  PhoneNumber phoneNumber, Name? firstName = null, Name? lastName = null, Email? email = null,
        DateTime? birthDate = null, Gender? gender = null) : base(id)
    {
        FirstName = FirstName;
        LastName = LastName;
        Email = email;
        PhoneNumber = phoneNumber;
        IsPhoneNumberVerified = new IsPhoneNumberVerified(false);
        BirthDate = birthDate;
        Gender = gender;
    }
    
    public Name? FirstName { get; private set; }
    public Name? LastName { get; private set; }
    public Email? Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IsPhoneNumberVerified? IsPhoneNumberVerified { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public Gender? Gender { get; private set; }
    
    public Guid? ProfilePhotoId { get; private set; }

    public static User Create(PhoneNumber phoneNumber, string validationToken)
    {
        var user = new User(Guid.NewGuid(), phoneNumber);
        
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, validationToken));

        return user;
    }

    public Result Update(
        Name? firstName, 
        Name? lastName, 
        Email? email, 
        PhoneNumber phoneNumber, 
        DateTime? birthDate, 
        Gender? gender, 
        Guid? profilePhotoId = null)
    {
        var isUserInValidAge = AgeCheckService.IsValidAge(birthDate);

        if (!isUserInValidAge) return Result.Failure(UserErrors.NotInValidAge);
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Gender = gender;
        ProfilePhotoId = profilePhotoId;
        UpdatedAt = DateTime.UtcNow;
        
        if(phoneNumber != PhoneNumber) IsPhoneNumberVerified = new IsPhoneNumberVerified(false);
        
        RaiseDomainEvent(new UserUpdatedDomainEvent(Id));

        return Result.Success();
    }

    public void UpdatePhoneNumberValidState(bool isPhoneNumberVerified)
    {
        IsPhoneNumberVerified = new IsPhoneNumberVerified(isPhoneNumberVerified);
    }
    
    public void ValidatePhoneNumber()
    {
        if (IsPhoneNumberVerified.Value)
            throw new InvalidOperationException("Phone number is already validated.");

        IsPhoneNumberVerified = new IsPhoneNumberVerified(true);
    }
}

