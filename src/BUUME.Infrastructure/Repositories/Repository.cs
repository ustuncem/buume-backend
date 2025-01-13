using BUUME.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal abstract class Repository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext DbContext = dbContext;
    
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ?? null;
    }
    public void Add(T entity)
    {
        DbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        DbContext.Set<T>().Update(entity);
    }

    public void HardDelete(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }
}