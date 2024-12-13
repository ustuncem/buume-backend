using BUUME.Domain.TaxOffices;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class TaxOfficeRepository(ApplicationDbContext dbContext) : 
    Repository<TaxOffice>(dbContext), ITaxOfficeRepository
{
    public async Task<IReadOnlyList<TaxOffice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TaxOffice>().ToListAsync(cancellationToken);
        return result;
    }
}