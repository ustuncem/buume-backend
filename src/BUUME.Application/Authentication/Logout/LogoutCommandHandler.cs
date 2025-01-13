using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.SMS;
using BUUME.Application.Authentication.Login;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Authentication.Logout;

internal sealed class LogoutCommandHandler(
    IUserContext userContext,
    IUserRepository userRepository,
    IAuthenticationService authenticationService)
    : ICommandHandler<LogoutCommand, bool>
{

    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<bool>(UserErrors.NotFound);
        
        var response = await authenticationService.LogoutAsync(user.PhoneNumber.Value);
        if (!response.IsSuccess) return response;
        
        return true;
    }
}