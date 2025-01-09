using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses;

public interface IBusinessRepository : IRepository<Business>
{
    Task<Business?> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
}