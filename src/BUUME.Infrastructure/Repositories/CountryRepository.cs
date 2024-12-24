using BUUME.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class CountryRepository(ApplicationDbContext dbContext) : 
    Repository<Country>(dbContext), ICountryRepository
{
    public async Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<Country>().ToListAsync(cancellationToken);
        return result;
    }
}