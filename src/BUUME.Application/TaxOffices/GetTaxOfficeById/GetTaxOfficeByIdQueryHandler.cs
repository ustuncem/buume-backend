using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.TaxOffices;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.TaxOffices.GetTaxOfficeById;

internal sealed class GetTaxOfficeByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetTaxOfficeByIdQuery, TaxOfficeResponse>
{
    public async Task<Result<TaxOfficeResponse>> Handle(GetTaxOfficeByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                tax.id AS Id,
                tax.name AS Name
            FROM "tax_offices" tax
            WHERE tax.id = @TaxOfficeId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        TaxOfficeResponse? taxOffice = await connection.QueryFirstOrDefaultAsync<TaxOfficeResponse>(
            sql,
            query);

        if (taxOffice is null)
        {
            return Result.Failure<TaxOfficeResponse>(TaxOfficeErrors.NotFound(query.TaxOfficeId));
        }

        return taxOffice;
    }
}
