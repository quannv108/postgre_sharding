using Core.Models.Base;

namespace Core.Dal.Base;

public interface IBaseRepository<T> where T : class, IEntity
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<List<T>> GetPaginatedAsync(int page, int size);
    Task<List<T>> GetAllAsync();

    IQueryable<T> Queryable();
    Task SaveChangeAsync();
}