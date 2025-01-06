using BUUME.SharedKernel;

namespace BUUME.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<string>> RegisterUserAsync(string phoneNumber);
    Task<Result<string>> LoginAsync(string phoneNumber);
    Task<Result<TokenResponse>> ValidateTokenAsync(string phoneNumber, string token);
    Task<Result<TokenResponse>> RefreshAccessToken(string accessToken, string refreshToken);
}