using System.Data;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Users.Me;

internal sealed class MeQueryHandler(IUserContext userContext, IDbConnectionFactory factory) : IQueryHandler<MeQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(MeQuery request, CancellationToken cancellationToken)
    {
        var userPhoneNumber = userContext.UserId;

        const string sql =
            """
            SELECT
                u.first_name AS FirstName,
                u.last_name AS LastName,
                u.email AS Email,
                u.phone_number AS PhoneNumber,
                u.is_phone_number_verified AS IsPhoneNumberVerified,
                u.birth_date AS BirthDate,
                u.gender AS Gender
            FROM users u
            WHERE u.phone_number = @PhoneNumber;
            """;

        using IDbConnection connection = factory.GetOpenConnection();
        
        var user = await connection.QueryFirstOrDefaultAsync<UserResponse>(sql, new{PhoneNumber = userPhoneNumber});
        
        if (user == null) Result.Failure<UserResponse>(UserErrors.NotFound);
        return user;
    }
}