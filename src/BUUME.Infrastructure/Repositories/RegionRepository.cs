using BUUME.Domain.Regions;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class RegionRepository(ApplicationDbContext dbContext) : 
    Repository<Region>(dbContext), IRegionRepository
{
}