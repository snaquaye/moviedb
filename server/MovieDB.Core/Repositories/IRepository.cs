namespace MovieDB.Core.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync(int page, int limit, CancellationToken ct);
    
    public Task<TEntity?> GetAsync(Guid id, CancellationToken ct);
    
    public Task AddAsync(TEntity entity, CancellationToken ct);
    
    public void Update(TEntity entity);
    
    public void Remove(TEntity entity);
}