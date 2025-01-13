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
        if (!result.Succeeded) return Result.Failure<string>(AuthenticationErrors.UnknownError);
        
        var phoneNumberValidationToken = await userManager.GenerateTwoFactorTokenAsync(newUser, TokenOptions.DefaultPhoneProvider);
        
        return phoneNumberValidationToken;
    }

    public async Task<Result<string>> LoginAsync(string phoneNumber)
    {
        var user = userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        if (user == null) return Result.Failure<string>(AuthenticationErrors.NotFound(phoneNumber));

        var phoneNumberValidationToken =
            await userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
        
        return phoneNumberValidationToken;
    }

    public async Task<Result<TokenResponse>> ValidateTokenAsync(string phoneNumber, string token)
    {
        var user = userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        if (user == null) return Result.Failure<TokenResponse>(AuthenticationErrors.NotFound(phoneNumber));

        var tokenValidationResponse = await userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, token);
        
        if(!tokenValidationResponse) return Result.Failure<TokenResponse>(AuthenticationErrors.WrongToken);
        
        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Name, user.PhoneNumber),
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
        
        if (!result.Succeeded) return Result.Failure<TokenResponse>(AuthenticationErrors.UnknownError);
        
        var tokenResponse = TokenResponse.Create(accessToken, refreshToken, jwtOptions.Value.DurationInMinutes);
        
        await signInManager.SignInAsync(user, true);
        
        return tokenResponse;
    }

    public async Task<Result<TokenResponse>> RefreshAccessToken(string accessToken, string refreshToken)
    {
        var principal = jwtService.GetTokenPrincipal(accessToken);
        var user = userManager.Users.SingleOrDefault(u => u.PhoneNumber == principal.Identity.Name);
        
        if (user == null) return Result.Failure<TokenResponse>(AuthenticationErrors.NotFound(""));

        if (DateTime.UtcNow >= user.RefreshTokenExpiration || refreshToken != user.RefreshToken)
            return Result.Failure<TokenResponse>(AuthenticationErrors.CantRequestNewToken);

        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Name, user.PhoneNumber),
            new (ClaimTypes.MobilePhone, user.PhoneNumber),
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new ("JWTID", Guid.NewGuid().ToString()),
            new ("UserName", user.UserName),
        };
        
        var newAccessToken = jwtService.GenerateAccessToken(authClaims);
        var newRefreshToken = jwtService.GenerateRefreshToken();
        
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenDurationInDays);
        
        var result = await userManager.UpdateAsync(user);
        
        if (!result.Succeeded) return Result.Failure<TokenResponse>(AuthenticationErrors.UnknownError);
        
        var tokenResponse = TokenResponse.Create(newAccessToken, newRefreshToken, jwtOptions.Value.DurationInMinutes);
        
        await signInManager.SignInAsync(user, true);
        
        return tokenResponse;
    }

    public async Task<Result<bool>> LogoutAsync(string phoneNumber)
    {
        var user = userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        if (user == null) return Result.Failure<bool>(AuthenticationErrors.NotFound(phoneNumber));
        
        await signInManager.SignOutAsync();
        return true;
    }

    public async Task<Result<bool>> DeleteAccountAsync(string phoneNumber)
    {
        var user = userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        if (user == null) return Result.Failure<bool>(AuthenticationErrors.NotFound(phoneNumber));
        
        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded) return Result.Failure<bool>(AuthenticationErrors.UnknownError);
        
        return true;
    }
}