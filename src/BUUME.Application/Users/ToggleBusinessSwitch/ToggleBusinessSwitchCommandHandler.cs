using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Users.ToggleBusinessSwitch;

internal sealed class ToggleBusinessSwitchCommandHandler(
    IUserRepository userRepository,
    IUserContext userContext,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<ToggleBusinessSwitchCommand, bool>
{
    public async Task<Result<bool>> Handle(ToggleBusinessSwitchCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(userId, cancellationToken);
        if (user == null) Result.Failure<bool>(UserErrors.NotFound);
        
        user.ToggleBusinessSwitch();
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}