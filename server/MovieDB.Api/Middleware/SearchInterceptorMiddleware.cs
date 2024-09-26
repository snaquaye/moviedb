using MovieDB.Core.Entities;
using MovieDB.Core.Repositories;
using MovieDB.Services.Abstractions;

namespace MovieDB.Api.Middleware;

public class SearchInterceptorMiddleware (RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var searchHistoryService = httpContext.RequestServices.GetRequiredService<ISearchHistoryService>();
        var unitOfWork = httpContext.RequestServices.GetRequiredService<IUnitOfWork>();
        var query = httpContext.Request.Query["query"].ToString();
        if (query.Length > 0)
        {
            var searchHistory = new SearchHistory
            {
                SearchTerm = query
            };
            await searchHistoryService.AddAsync(searchHistory, CancellationToken.None);
            await unitOfWork.CommitAsync(CancellationToken.None);
        }

        await next(httpContext);
    }
}