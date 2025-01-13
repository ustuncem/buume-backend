namespace BUUME.SharedKernel;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void HardDelete(T entity);
}