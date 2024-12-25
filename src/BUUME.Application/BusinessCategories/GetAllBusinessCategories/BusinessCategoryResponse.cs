namespace BUUME.Application.BusinessCategories.GetAllBusinessCategories;

public sealed record BusinessCategoryResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}
