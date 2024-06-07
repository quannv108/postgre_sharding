using Core.Dal.Base;
using Core.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.Dal.EF.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
{
    protected AppDbContext DbContext { get; }

    public BaseRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task<List<T>> GetPaginatedAsync(int page, int size)
    {
        return DbContext.Set<T>().AsNoTrackingWithIdentityResolution()
            .Skip((page-1) * size)
            .Take(size)
            .ToListAsync();
    }

    public Task<List<T>> GetAllAsync()
    {
        return DbContext.Set<T>().AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public IQueryable<T> Queryable()
    {
        return DbContext.Set<T>();
    }

    public async Task SaveChangeAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}