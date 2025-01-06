using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.SMS;
using BUUME.SharedKernel;

namespace BUUME.Application.Authentication.Login;

internal sealed class LoginCommandHandler(
    ISmsService smsService,
    IAuthenticationService authenticationService)
    : ICommandHandler<LoginCommand, string>
{

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await authenticationService.LoginAsync(request.PhoneNumber);
        if (!response.IsSuccess) return response;
        
        // send sms here!
        
        return "true";
    }
}