using MovieDB.Core.Repositories;

namespace MovieDB.Infrastructure.Data;

public sealed class UnitOfWork(AppDbContext context, bool disposed = true) : IUnitOfWork
{
    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }

        disposing = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<bool> CommitAsync(CancellationToken ct)
    {
        var returnValue = true;

        try
        {
            await context.SaveChangesAsync(ct);
        }
        catch (Exception)
        {
            returnValue = false;
        }

        return returnValue;
    }
}