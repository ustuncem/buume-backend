using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Country;

public interface ICountryRepository : IRepository<Country>
{
    Task<IReadOnlyList<Country>> GetAllAsync();
}