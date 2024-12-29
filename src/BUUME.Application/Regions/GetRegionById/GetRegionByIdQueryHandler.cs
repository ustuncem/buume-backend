using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Countries.GetCountryById;
using BUUME.Domain.Regions;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Regions.GetRegionById;

internal sealed class GetRegionByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetRegionByIdQuery, RegionResponse>
{
    public async Task<Result<RegionResponse>> Handle(GetRegionByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                r.id AS Id,
                r.name AS Name,
                c.id AS Id,
                c.name AS Name,
                c.code AS Code,
                c.has_region AS HasRegion
            FROM regions r
            LEFT JOIN countries c ON r.country_id = c.id
            WHERE r.id = @RegionId;
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var sqlResponse = await connection.QueryAsync<RegionResponse, CountryResponse, RegionResponse>(
            sql,
            (region, country) =>
            {
                region.Country = country;
                return region;
            },
            query,
            splitOn: "Id");
        
        var region = sqlResponse.FirstOrDefault();

        if (region is null)
        {
            return Result.Failure<RegionResponse>(RegionErrors.NotFound(query.RegionId));
        }

        return region;
    }
}
