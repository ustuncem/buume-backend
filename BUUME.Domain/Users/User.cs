using BUUME.Domain.Absractions;

namespace BUUME.Domain.Users;

public sealed class User : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public bool IsPhoneNumberVerified { get; private set; }
    public DateTime BirthDate { get; private set; }
    public Gender Gender { get; private set; }
}

