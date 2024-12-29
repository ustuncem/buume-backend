using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Districts;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Districts.GetDistrictById;

internal sealed class GetDistrictByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetDistrictByIdQuery, DistrictResponse>
{
    public async Task<Result<DistrictResponse>> Handle(GetDistrictByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                d.id AS Id,
                d.name AS Name,
                d.code AS Code,
                c.name AS Name,
                c.code AS Code
            FROM "districts" d
            LEFT JOIN "cities" c ON d.city_id = c.id
            WHERE d.id = @DistrictId AND d.deleted_at IS NULL;
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var sqlResponse = await connection.QueryAsync<DistrictResponse, MinimizedCity, DistrictResponse>(
            sql,
            (district, city) =>
            {
                district.City = city;
                return district;
            },
            query,
            splitOn: "Name");
        
        var city = sqlResponse.FirstOrDefault();

        if (city is null)
        {
            return Result.Failure<DistrictResponse>(DistrictErrors.NotFound(query.DistrictId));
        }

        return city;
    }
}
