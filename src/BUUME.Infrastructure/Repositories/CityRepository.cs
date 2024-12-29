using BUUME.Domain.Cities;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class CityRepository(ApplicationDbContext dbContext) : 
    Repository<City>(dbContext), ICityRepository
{
}