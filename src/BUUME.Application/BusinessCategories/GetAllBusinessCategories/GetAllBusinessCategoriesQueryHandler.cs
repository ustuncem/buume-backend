using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.SharedKernel;
using Dapper;

namespace BUUME.Application.BusinessCategories.GetAllBusinessCategories;

internal sealed class GetAllBusinessCategoriesQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetAllBusinessCategoriesQuery, IReadOnlyList<BusinessCategoryResponse>>
{
    public async Task<Result<IReadOnlyList<BusinessCategoryResponse>>> Handle(GetAllBusinessCategoriesQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                bc.id AS Id,
                bc.name AS Name
            FROM "business_categories" bc
            WHERE bc.deleted_at IS NULL
            AND bc.name ILIKE @SearchTerm
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        var businessCategory = await connection.QueryAsync<BusinessCategoryResponse>(
            sql,
            new { SearchTerm = $"%{query.SearchTerm}%"}
            );

        return businessCategory.ToList();
    }
}
