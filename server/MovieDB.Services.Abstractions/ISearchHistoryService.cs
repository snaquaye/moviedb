using MovieDB.Core.Entities;
using MovieDB.Shared.Response;

namespace MovieDB.Services.Abstractions;

public interface ISearchHistoryService
{
    Task AddAsync(SearchHistory searchHistory, CancellationToken ct);
    IEnumerable<string?> GetAll(int page, int limit, CancellationToken ct);
}