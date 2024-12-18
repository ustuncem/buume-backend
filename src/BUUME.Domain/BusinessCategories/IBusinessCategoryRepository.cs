using BUUME.SharedKernel;

namespace BUUME.Domain.BusinessCategories;

public interface IBusinessCategoryRepository : IRepository<BusinessCategory>
{
    Task<IReadOnlyList<BusinessCategory>> GetAllAsync();
}