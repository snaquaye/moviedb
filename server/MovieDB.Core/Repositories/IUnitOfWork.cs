namespace MovieDB.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    public Task<bool> CommitAsync(CancellationToken ct);
}