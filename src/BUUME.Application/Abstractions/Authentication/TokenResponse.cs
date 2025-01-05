namespace BUUME.Application.Abstractions.Authentication;

public sealed class TokenResponse
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public DateTime ExpiresAt { get; init; }
    public int ExpiresIn { get; init; }

    private TokenResponse(string accessToken, string refreshToken, int expiresIn, DateTime expiresAt)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
        ExpiresIn = expiresIn;
    }

    public static TokenResponse Create(string accessToken, string refreshToken, int expiresIn)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(expiresIn);
        var tokenResponse = new TokenResponse(accessToken, refreshToken, expiresIn, expiresAt);
        
        return tokenResponse;
    }
}