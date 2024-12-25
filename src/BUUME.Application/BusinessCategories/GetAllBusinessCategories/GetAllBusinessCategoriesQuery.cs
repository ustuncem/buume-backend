using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.Search;

namespace BUUME.Application.BusinessCategories.GetAllBusinessCategories;

public sealed record GetAllBusinessCategoriesQuery(string? SearchTerm) : ISearchableQuery<IReadOnlyList<BusinessCategoryResponse>>
{
}
