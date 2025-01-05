using System.Security.Claims;

namespace BUUME.Application.Abstractions.Authentication;

public interface IJwtService
{
    string GenerateAccessToken(List<Claim> claims, bool isTemporary = false);
    string GenerateRefreshToken();
    string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret);
}