using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BUUME.Application.Abstractions.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BUUME.Identity.Jwt;

internal sealed class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
{
    public string GenerateAccessToken(List<Claim> claims, bool isTemporary = false) 
    {
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));

        var tokenObject = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            expires: DateTime.Now.AddMinutes(isTemporary ? jwtOptions.Value.TemporaryTokenDurationInMinutes : jwtOptions.Value.DurationInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
        );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret)
    {
        throw new NotImplementedException();
    }
}