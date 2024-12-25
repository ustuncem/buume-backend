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
            WHERE c.deleted_at IS NULL
            AND (
                c.name ILIKE @SearchTerm OR
                c.Code ILIKE @SearchTerm
                )
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var countries = await connection.QueryAsync<CountryResponse>(
            sql,
            new { SearchTerm = $"%{query.SearchTerm}%"}
            );

        return countries.ToList();
    }
}