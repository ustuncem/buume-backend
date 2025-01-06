using BUUME.SharedKernel;

namespace BUUME.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default);
}