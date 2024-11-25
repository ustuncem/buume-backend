using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Countries;

public interface ICountryRepository : IRepository<Country>
{
    Task<IReadOnlyList<Country>> GetAllAsync();
}