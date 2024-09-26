using MovieDB.Core.Entities;
using MovieDB.Core.Repositories;
using MovieDB.Services.Abstractions;
using MovieDB.Shared.Response;

namespace MovieDB.Services;

public class SearchHistoryService (ISearchHistoryRepository searchHistoryRepository, IUnitOfWork unitOfWork) : ISearchHistoryService
{
    public async Task AddAsync(SearchHistory searchHistory, CancellationToken ct)
    {
        await searchHistoryRepository.AddAsync(searchHistory, ct);
        await unitOfWork.CommitAsync(ct);
    }

    public IEnumerable<string?> GetAll(int page, int limit, CancellationToken ct)
    {
        var searchHistories = searchHistoryRepository.GetRecentSearchTerm(page, limit, ct);

        return searchHistories;
    }
}