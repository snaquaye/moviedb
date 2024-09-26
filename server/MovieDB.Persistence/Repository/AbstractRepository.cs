using Microsoft.EntityFrameworkCore;
using MovieDB.Core.Entities;
using MovieDB.Core.Repositories;

namespace MovieDB.Infrastructure.Repository;

public abstract class AbstractRepository<TEntity>(DbContext context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    public async Task<IEnumerable<TEntity>> GetAllAsync(int page, int limit, CancellationToken ct)
    {
        var startIndex = (page - 1) * limit;
        var entities = await context.Set<TEntity>()
            .OrderBy(e => e.CreatedAt)
            .Skip(startIndex)
            .Take(limit)
            .ToListAsync(ct);

        return entities;
    }

    public async Task<TEntity?> GetAsync(Guid id, CancellationToken ct)
    {
        var entity = await context.Set<TEntity>()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(ct);

        return entity;
    }

    public async Task AddAsync(TEntity entity, CancellationToken ct)
    {
        await context.Set<TEntity>().AddAsync(entity, ct);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }
}