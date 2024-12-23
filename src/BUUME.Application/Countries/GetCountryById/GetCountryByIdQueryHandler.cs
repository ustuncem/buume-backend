using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Countries;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Countries.GetCountryById;

internal sealed class GetCountryByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetCountryByIdQuery, CountryResponse>
{
    public async Task<Result<CountryResponse>> Handle(GetCountryByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                country.id AS Id,
                country.name AS Name,
                country.code AS Code,
                country.has_region AS HasRegion
            FROM "countries" country
            WHERE country.id = @CountryId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        CountryResponse? country = await connection.QueryFirstOrDefaultAsync<CountryResponse>(
            sql,
            query);

        if (country is null)
        {
            return Result.Failure<CountryResponse>(CountryErrors.NotFound(query.CountryId));
        }

        return country;
    }
}
