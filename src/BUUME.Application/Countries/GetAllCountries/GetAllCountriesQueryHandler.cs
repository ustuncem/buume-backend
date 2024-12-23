using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.Countries.GetAllCountries;

internal sealed class GetAllTaxOfficeQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetAllCountriesQuery, IReadOnlyList<CountryResponse>>
{
    public async Task<Result<IReadOnlyList<CountryResponse>>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                country.id AS Id,
                country.name AS Name,
                country.code AS Code,
                country.has_region AS HasRegion
            FROM "countries" country
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var countries = await connection.QueryAsync<CountryResponse>(
            sql,
            query);

        return countries.ToList();
    }
}