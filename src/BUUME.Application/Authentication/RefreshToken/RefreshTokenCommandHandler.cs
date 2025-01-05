using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;

namespace BUUME.Application.Authentication.RefreshToken;

internal sealed class RefreshTokenCommandHandler(
    IAuthenticationService authenticationService)
    : ICommandHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var response = await authenticationService.RefreshAccessToken(request.AccessToken, request.RefreshToken);
        return response;
    }
}