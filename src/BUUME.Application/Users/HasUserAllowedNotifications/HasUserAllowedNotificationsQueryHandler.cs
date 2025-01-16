using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Users.HasUserAllowedNotifications;

internal sealed class HasUserAllowedNotificationsQueryHandler(
    IUserRepository userRepository, 
    IUserContext userContext) : IQueryHandler<HasUserAllowedNotificationsQuery, bool>
{
    public async Task<Result<bool>> Handle(HasUserAllowedNotificationsQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(userId, cancellationToken);
        if (user == null) return Result.Failure<bool>(UserErrors.NotFound);

        return user.HasAllowedNotifications.Value;
    }
}