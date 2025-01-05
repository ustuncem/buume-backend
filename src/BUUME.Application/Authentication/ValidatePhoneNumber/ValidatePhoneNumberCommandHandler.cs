using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;

namespace BUUME.Application.Authentication.ValidatePhoneNumber;

internal sealed class ValidatePhoneNumberCommandHandler(
    IAuthenticationService authenticationService)
    : ICommandHandler<ValidatePhoneNumberCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(ValidatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var response = await authenticationService.ValidateTokenAsync(request.PhoneNumber, request.Code);
        
        if (!response.IsSuccess) return Result.Failure<TokenResponse>(response.Error);

        return response;
    }
}