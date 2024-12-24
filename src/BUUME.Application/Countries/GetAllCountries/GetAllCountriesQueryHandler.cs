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
                c.id AS Id,
                c.name AS Name
            FROM "countries" c
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var countries = await connection.QueryAsync<CountryResponse>(
            sql,
            query);

        return countries.ToList();
    }
}