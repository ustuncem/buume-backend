using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Authentication.Register;

internal sealed class RegisterCommandHandler(
    IUserRepository userRepository,
    IAuthenticationService authenticationService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterCommand, string>
{

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await authenticationService.RegisterUserAsync(request.PhoneNumber);
        if (!response.IsSuccess) return response;
        
        var phoneNumber = new PhoneNumber(request.PhoneNumber);
        var user = User.Create(phoneNumber, response.Value);
        
        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return "true";
    }
}