using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieDB.Caching.Services;
using MovieDB.Services.Abstractions;

namespace MovieDB.Caching;

public static class Extension
{
    public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
            options.InstanceName = configuration.GetValue<string>("RedisInstanceName") ?? "MovieDB_Caching";
        });
        services.AddScoped<IRedisCacheService, RedisCacheService>();
    }
}