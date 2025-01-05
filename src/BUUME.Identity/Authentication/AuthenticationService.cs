using System.Security.Claims;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Identity.Data;
using BUUME.Identity.Jwt;
using BUUME.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BUUME.Identity.Authentication;

internal sealed class AuthenticationService(
    UserManager<ApplicationUser> userManager,
    IJwtService jwtService,
    IOptions<JwtOptions> jwtOptions,
    SignInManager<ApplicationUser> signInManager) : IAuthenticationService
{
    public async Task<Result<string>> RegisterUserAsync(string phoneNumber)
    {
        var user = userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        if (user != null) return Result.Failure<string>(AuthenticationErrors.UserAlreadyExist(phoneNumber));
        
        var newUser = new ApplicationUser(phoneNumber)
        {
            PhoneNumber = phoneNumber
        };
        
        var result = await userManager.CreateAsync(newUser);
        if (!result.Succeeded) return Result.Failure<string>(AuthenticationErrors.UnknownError());
        
        var phoneNumberValidationToken = await userManager.GenerateTwoFactorTokenAsync(newUser, TokenOptions.DefaultPhoneProvider);
        
        return phoneNumberValidationToken;
    }

    public async Task<Result<TokenResponse>> ValidateTokenAsync(string phoneNumber, string token)
    {
        var user = userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        if (user == null) return Result.Failure<TokenResponse>(AuthenticationErrors.NotFound(phoneNumber));

        var tokenValidationResponse = await userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, token);
        
        var authClaims = new List<Claim>
        {
            new (ClaimTypes.MobilePhone, user.PhoneNumber),
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new ("JWTID", Guid.NewGuid().ToString()),
            new ("UserName", user.UserName),
        };
        
        var accessToken = jwtService.GenerateAccessToken(authClaims);
        var refreshToken = jwtService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenDurationInDays);
        
        var result = await userManager.UpdateAsync(user);
        
        if (!result.Succeeded) return Result.Failure<TokenResponse>(AuthenticationErrors.UnknownError());
        
        var tokenResponse = TokenResponse.Create(accessToken, refreshToken, jwtOptions.Value.DurationInMinutes);
        
        await signInManager.SignInAsync(user, true);
        
        return tokenResponse;
    }
}