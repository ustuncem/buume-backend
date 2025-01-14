using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Users.HasUserSwitchedToBusiness;

internal sealed class HasUserSwitchedToBusinessQueryHandler(
    IUserRepository userRepository, 
    IUserContext userContext) : IQueryHandler<HasUserSwitchedToBusinessQuery, bool>
{
    public async Task<Result<bool>> Handle(HasUserSwitchedToBusinessQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(userId, cancellationToken);
        if (user == null) return Result.Failure<bool>(UserErrors.NotFound);

        return user.SwitchedToBusiness.Value;
    }
}