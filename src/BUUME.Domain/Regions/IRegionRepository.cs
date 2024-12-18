using BUUME.SharedKernel;

namespace BUUME.Domain.Regions;

public interface IRegionRepository : IRepository<Region>
{
    Task<List<Region>> GetAllRegionsByCountryIdAsync(Guid countryId);
}