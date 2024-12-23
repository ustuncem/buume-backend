using BUUME.SharedKernel;

namespace BUUME.Domain.BusinessCategories;

public static class BusinessCategoryErrors
{
    public static Error NotFound(Guid businessCategoryId) => Error.NotFound(
        "BusinessCategories.NotFound",
        $"The business category with the Id = '{businessCategoryId}' was not found");
}
