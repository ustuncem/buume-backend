using System.Data;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Users.HasBusiness;

internal sealed class HasBusinessQueryHandler(
    IDbConnectionFactory factory,
    IUserContext userContext,
    IUserRepository userRepository) : IQueryHandler<HasBusinessQuery, bool>
{
    public async Task<Result<bool>> Handle(HasBusinessQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<bool>(UserErrors.NotFound);

        const string sql = """SELECT COUNT(owner_id) FROM businesses WHERE owner_id = @UserId""";
        
        using IDbConnection connection = factory.GetOpenConnection();
        
        var result = await connection.QueryFirstOrDefaultAsync<int>(sql, new { UserId = user.Id });
        
        if (result == 0) return Result.Failure<bool>(UserErrors.NoBusinessFound);

        return true;
    }
}