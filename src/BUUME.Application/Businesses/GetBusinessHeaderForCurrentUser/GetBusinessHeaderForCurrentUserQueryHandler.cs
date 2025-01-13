using System.Data;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Businesses;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Businesses.GetBusinessHeaderForCurrentUser;

internal sealed class GetBusinessHeaderForCurrentUserQueryHandler(
    IDbConnectionFactory factory, 
    IUserRepository userRepository,
    IUserContext userContext)
    : IQueryHandler<GetBusinessHeaderForCurrentUserQuery, BusinessHeaderResponse>
{
    public async Task<Result<BusinessHeaderResponse>> Handle(GetBusinessHeaderForCurrentUserQuery query, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(currentUserId, cancellationToken);
        if (user == null) return Result.Failure<BusinessHeaderResponse>(UserErrors.NotFound);
        
        const string sql =
            """
            SELECT 
                b.base_info_name AS Name,
                f.path as Logo
            FROM businesses b
            LEFT JOIN files f ON b.logo_id = f.id
            WHERE b.owner_id = @OwnerId;
            """;

        using IDbConnection connection = factory.GetOpenConnection();
        
        IEnumerable<BusinessHeaderResponse> businessList = await connection.QueryAsync<BusinessHeaderResponse>(
            sql,
            new {OwnerId = user.Id});
        
        var business = businessList.FirstOrDefault();
        
        if (business == null) return Result.Failure<BusinessHeaderResponse>(BusinessErrors.NotFound);
        
        return business;
    }
}
