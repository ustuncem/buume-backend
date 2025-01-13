using System.Data;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Users.GetMeHeader;

internal sealed class GetMeHeaderQueryHandler(
    IDbConnectionFactory factory, 
    IUserRepository userRepository,
    IUserContext userContext)
    : IQueryHandler<GetMeHeaderQuery, MeHeaderResponse>
{
    public async Task<Result<MeHeaderResponse>> Handle(GetMeHeaderQuery query, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<MeHeaderResponse>(UserErrors.NotFound);
        
        const string sql =
            """
            SELECT 
                u.first_name AS FirstName,
                u.last_name AS LastName,
                f.path as ProfilePhoto
            FROM users u
            LEFT JOIN files f ON u.profile_photo_id = f.id
            WHERE u.id = @UserId;
            """;

        using IDbConnection connection = factory.GetOpenConnection();
        
        var meHeader = await connection.QueryFirstOrDefaultAsync<MeHeaderResponse>(
            sql,
            new {UserId = user.Id});
        
        if (meHeader == null) return Result.Failure<MeHeaderResponse>(UserErrors.NotFound);
        
        return meHeader;
    }
}
