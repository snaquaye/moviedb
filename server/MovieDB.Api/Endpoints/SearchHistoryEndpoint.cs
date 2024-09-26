using Microsoft.AspNetCore.Mvc;
using MovieDB.Api.Contracts;
using MovieDB.Services.Abstractions;
using MovieDB.Shared.Response;

namespace MovieDB.Api.Endpoints;

public class SearchHistoryEndpoint() : IEndpoint
{
    public void AddEndpoints(WebApplication app)
    {
        app.MapGet("/api/v1/search-histories", GetSearchHistoriesAsync);
    }

    private SearchHistoryResponse GetSearchHistoriesAsync(int page, int limit, [FromServices] ISearchHistoryService searchHistoryService, CancellationToken ct)
    {
        var searchHistories = searchHistoryService.GetAll(page, limit, ct);

        return new SearchHistoryResponse
        {
            SearchTerms = searchHistories
        };
    }
}