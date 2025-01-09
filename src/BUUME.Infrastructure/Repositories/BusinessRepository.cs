using BUUME.Domain.Businesses;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class BusinessRepository(ApplicationDbContext dbContext) : 
    Repository<Business>(dbContext), IBusinessRepository
{
    public async Task<Business?> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Business>().FirstOrDefaultAsync(business => business.OwnerId == ownerId, cancellationToken);
    }
}