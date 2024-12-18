using BUUME.SharedKernel;

namespace BUUME.Domain.TaxOffices;

public interface ITaxOfficeRepository : IRepository<TaxOffice>
{
    public Task<IReadOnlyList<TaxOffice>> GetAllAsync(CancellationToken cancellationToken = default);
}