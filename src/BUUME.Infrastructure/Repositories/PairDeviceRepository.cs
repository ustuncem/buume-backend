using BUUME.Domain.PairDevice;

namespace BUUME.Infrastructure.Repositories;

internal sealed class PairDeviceRepository(ApplicationDbContext dbContext) : 
    Repository<PairDevice>(dbContext), IPairDeviceRepository
{
}