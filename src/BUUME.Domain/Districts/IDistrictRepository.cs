using BUUME.SharedKernel;

namespace BUUME.Domain.Districts;

public interface IDistrictRepository : IRepository<District>
{
    Task<IReadOnlyList<District>> GetAllByCityIdAsync(Guid cityId);
}