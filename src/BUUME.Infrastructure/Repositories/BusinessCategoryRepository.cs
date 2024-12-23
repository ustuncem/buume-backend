using BUUME.Domain.BusinessCategories;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class BusinessCategoryRepository(ApplicationDbContext dbContext) : 
    Repository<BusinessCategory>(dbContext), IBusinessCategoryRepository
{
    public async Task<IReadOnlyList<BusinessCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<BusinessCategory>().ToListAsync(cancellationToken);
        return result;
    }
}