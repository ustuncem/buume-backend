namespace BUUME.Application.Users;

public static class UserErrorCodes
{
    public static class UpdateMe
    {
        public const string InvalidFirstName = nameof(InvalidFirstName);
        public const string InvalidLastName = nameof(InvalidLastName);
        public const string InvalidEmail = nameof(InvalidEmail);
        public const string MissingPhoneNumber = nameof(MissingPhoneNumber);
        public const string InvalidPhoneNumber = nameof(InvalidPhoneNumber);
        public const string InvalidBirthDate = nameof(InvalidBirthDate);
        public const string InvalidGender = nameof(InvalidGender);
    }
}