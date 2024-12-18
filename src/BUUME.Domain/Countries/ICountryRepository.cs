using BUUME.SharedKernel;

namespace BUUME.Domain.Countries;

public interface ICountryRepository : IRepository<Country>
{
    Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default);
}