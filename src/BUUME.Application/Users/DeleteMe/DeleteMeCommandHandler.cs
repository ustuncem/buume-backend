using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Users.DeleteMe;

internal sealed class DeleteMeCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IUserContext userContext)
    : IQueryHandler<DeleteMeCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteMeCommand command, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<bool>(UserErrors.NotFound);
        
        var result = await authenticationService.DeleteAccountAsync(user.PhoneNumber.Value);
        
        if(!result.IsSuccess) return Result.Failure<bool>(result.Error);
        
        userRepository.HardDelete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
