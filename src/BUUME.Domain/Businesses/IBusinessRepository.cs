using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses;

public interface IBusinessRepository : IRepository<Business>
{
    Task<IReadOnlyList<Business>> GetAllAsync(CancellationToken cancellationToken);
}