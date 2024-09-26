using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieDB.Services.Abstractions;

namespace MovieDb.HttpClient;

public static class Extension
{
    public static void AddOmDbClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOmDbClient, OmDbClient>();
        services.AddHttpClient<IOmDbClient, OmDbClient>(builder =>
        {
            var apiKey = configuration["OmDb:ApiKey"];
            var baseAddress = configuration["OmDb:BaseAddress"];
            builder.BaseAddress = new Uri($"{baseAddress}/");
            builder.DefaultRequestHeaders.Add("Accept", "application/json");
        });
    }
}