namespace BUUME.Application.Authentication;

public static class AuthenticationErrorCodes
{ 
    public const string MissingPhoneNumber = nameof(MissingPhoneNumber); 
    public const string InvalidPhoneNumber = nameof(InvalidPhoneNumber);
    public const string InvalidCode = nameof(InvalidCode);
    public const string MissingCode = nameof(MissingCode);
}