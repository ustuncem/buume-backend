using BUUME.Domain.Abstractions;
using BUUME.Domain.Users.Events;

namespace BUUME.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Name name, Email email, PhoneNumber phoneNumber,
        DateTime birthDate, Gender gender) : base(id)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        IsPhoneNumberVerified = new IsPhoneNumberVerified(false);
        BirthDate = birthDate;
        Gender = gender;
    }
    
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IsPhoneNumberVerified IsPhoneNumberVerified { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }

    public static User Create(Name name, Email email, PhoneNumber phoneNumber,
        DateTime birthDate, Gender gender)
    {
        var user = new User(Guid.NewGuid(), name, email, phoneNumber, birthDate, gender);
        
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void Update(Name name, Email email, PhoneNumber phoneNumber, DateTime birthDate, Gender gender)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Gender = gender;
        UpdatedAt = DateTime.UtcNow;
        
        // If phoneNumber changes revalidate user.
        if(phoneNumber != PhoneNumber) IsPhoneNumberVerified = new IsPhoneNumberVerified(false);
        
        RaiseDomainEvent(new UserUpdatedDomainEvent(Id));
    }
    
    public void ValidatePhoneNumber()
    {
        if (IsPhoneNumberVerified.Value)
            throw new InvalidOperationException("Phone number is already validated.");

        IsPhoneNumberVerified = new IsPhoneNumberVerified(true);
    }
}

