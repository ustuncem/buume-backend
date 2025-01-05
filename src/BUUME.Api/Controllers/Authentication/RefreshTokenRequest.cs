namespace BUUME.Api.Controllers.Authentication;

public sealed class RefreshTokenRequest
{
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}