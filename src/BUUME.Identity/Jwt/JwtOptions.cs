namespace BUUME.Identity.Jwt;

public sealed class JwtOptions
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public int RefreshTokenDurationInDays { get; set; }
    public int TemporaryTokenDurationInMinutes { get; set; }
}