namespace MovieDB.Services.Abstractions;

public interface IRedisCacheService
{
    public Task<T?> GetCachedData<T>(string key) where T : class;
    
    public Task SetCachedData<T>(string key, T value, TimeSpan cacheDuration) where T : class;
}