using BUUME.Domain.Districts;

namespace BUUME.Infrastructure.Repositories;

internal sealed class DistrictRepository(ApplicationDbContext dbContext) : 
    Repository<District>(dbContext), IDistrictRepository
{
}