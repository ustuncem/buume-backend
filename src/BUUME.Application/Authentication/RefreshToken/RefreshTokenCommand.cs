using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Authentication.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<TokenResponse>;