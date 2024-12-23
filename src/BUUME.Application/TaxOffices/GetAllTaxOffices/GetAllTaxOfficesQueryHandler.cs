using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.TaxOffices.GetAllTaxOffices;

internal sealed class GetAllTaxOfficeQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetAllTaxOfficesQuery, IReadOnlyList<TaxOfficeResponse>>
{
    public async Task<Result<IReadOnlyList<TaxOfficeResponse>>> Handle(GetAllTaxOfficesQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                tax.id AS Id,
                tax.name AS Name
            FROM "tax_offices" tax
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var taxOffice = await connection.QueryAsync<TaxOfficeResponse>(
            sql,
            query);

        return taxOffice.ToList();
    }
}
