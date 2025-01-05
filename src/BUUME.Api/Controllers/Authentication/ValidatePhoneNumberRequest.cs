namespace BUUME.Api.Controllers.Authentication;

public sealed class ValidatePhoneNumberRequest
{
    public string phoneNumber { get; set; }
    public string code { get; set; }
}