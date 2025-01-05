using BUUME.Domain.Users;

namespace BUUME.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext) : 
    Repository<User>(dbContext), IUserRepository
{
}