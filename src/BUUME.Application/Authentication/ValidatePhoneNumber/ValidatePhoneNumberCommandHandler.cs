using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Authentication.ValidatePhoneNumber;

internal sealed class ValidatePhoneNumberCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IAuthenticationService authenticationService)
    : ICommandHandler<ValidatePhoneNumberCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(ValidatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var response = await authenticationService.ValidateTokenAsync(request.PhoneNumber, request.Code);
        
        if (!response.IsSuccess) return Result.Failure<TokenResponse>(response.Error);
        
        var user = await userRepository.GetByPhoneNumberAsync(request.PhoneNumber);
        
        if (user == null) return Result.Failure<TokenResponse>(UserErrors.NotFound);
        
        user.UpdatePhoneNumberValidState(true);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return response;
    }
}