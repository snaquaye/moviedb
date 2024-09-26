using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using MovieDB.Services.Abstractions;

namespace MovieDB.Caching.Services;

public class RedisCacheService(IDistributedCache cache) : IRedisCacheService
{
    public async Task<T?> GetCachedData<T>(string key) where T : class
    {
        var jsonData = await cache.GetStringAsync(key);
        
        if (jsonData is null)
        {
            return default(T);
        }
        
        return JsonSerializer.Deserialize<T>(jsonData);
    }
    
    public async Task SetCachedData<T>(string key, T value, TimeSpan cacheDuration) where T : class
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = cacheDuration
        };
        
        var jsonData = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, jsonData, options);
    }
}