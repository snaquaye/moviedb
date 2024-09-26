using MovieDB.Core.Entities;
using MovieDB.Core.Repositories;
using MovieDB.Infrastructure.Data;

namespace MovieDB.Infrastructure.Repository;

public class SearchHistoryRepository(AppDbContext context)
    : AbstractRepository<SearchHistory>(context), ISearchHistoryRepository
{
    public IEnumerable<string?> GetRecentSearchTerm(int page, int limit, CancellationToken ct)
    {
        var skip = (page - 1) * limit;
        var searchHistories = context.SearchHistories
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => s.SearchTerm)
            .Distinct()
            .Skip(skip)
            .Take(limit)
            .ToList();

        return searchHistories;
    }
}