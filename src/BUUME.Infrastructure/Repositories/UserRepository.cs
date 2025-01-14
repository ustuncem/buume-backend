using BUUME.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : 
    Repository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<User>()
            .FirstOrDefaultAsync(user => user.PhoneNumber == new PhoneNumber(phoneNumber), cancellationToken);
    }
}