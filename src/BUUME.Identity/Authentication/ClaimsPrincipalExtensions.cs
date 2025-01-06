using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BUUME.Identity.Authentication;

internal static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.Identity.Name;

        return userId ?? throw new ApplicationException("User identity is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ??
               throw new ApplicationException("User identity is unavailable");
    }
}
