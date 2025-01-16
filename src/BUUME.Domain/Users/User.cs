using BUUME.Domain.Users.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, PhoneNumber phoneNumber, FirstName? firstName = null, LastName? lastName = null, Email? email = null,
        DateTime? birthDate = null, Gender? gender = null) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        IsPhoneNumberVerified = new IsPhoneNumberVerified(false);
        BirthDate = birthDate;
        Gender = gender;
        SwitchedToBusiness = new SwitchedToBusiness(false);
        HasAllowedNotifications = new HasAllowedNotifications(false);
    }

    public FirstName? FirstName { get; private set; }
    public LastName? LastName { get; private set; }
    public Email? Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IsPhoneNumberVerified? IsPhoneNumberVerified { get; private set; }
    public SwitchedToBusiness SwitchedToBusiness { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public Gender? Gender { get; private set; }
    public Guid? ProfilePhotoId { get; private set; }
    public HasAllowedNotifications HasAllowedNotifications { get; private set; }

    public static User Create(PhoneNumber phoneNumber, string validationToken)
    {
        var user = new User(Guid.NewGuid(), phoneNumber);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, validationToken));

        return user;
    }

    public Result Update(
        FirstName? firstName,
        LastName? lastName,
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

        if (phoneNumber != PhoneNumber) IsPhoneNumberVerified = new IsPhoneNumberVerified(false);

        RaiseDomainEvent(new UserUpdatedDomainEvent(Id));

        return Result.Success();
    }

    public void UpdatePhoneNumberValidState(bool isPhoneNumberVerified)
    {
        IsPhoneNumberVerified = new IsPhoneNumberVerified(isPhoneNumberVerified);
    }

    public void ToggleBusinessSwitch()
    {
        SwitchedToBusiness = new SwitchedToBusiness(!SwitchedToBusiness.Value);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ValidatePhoneNumber()
    {
        if (IsPhoneNumberVerified.Value)
            throw new InvalidOperationException("Phone number is already validated.");

        IsPhoneNumberVerified = new IsPhoneNumberVerified(true);
    }

    public void ToggleNotificationPermission()
    {
        HasAllowedNotifications = new HasAllowedNotifications(!HasAllowedNotifications.Value);
        UpdatedAt = DateTime.UtcNow;
    }
}

