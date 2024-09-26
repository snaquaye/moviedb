using MovieDB.Core.Entities;

namespace MovieDB.Core.Repositories;

public interface ISearchHistoryRepository : IRepository<SearchHistory>
{
    public IEnumerable<string?> GetRecentSearchTerm(int page, int limit, CancellationToken ct);
}