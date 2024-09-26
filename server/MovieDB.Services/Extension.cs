using Microsoft.Extensions.DependencyInjection;
using MovieDB.Services.Abstractions;

namespace MovieDB.Services;

public static class Extension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISearchHistoryService, SearchHistoryService>();
    }
}