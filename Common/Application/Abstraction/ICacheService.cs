namespace Application.Abstraction
{
    public interface ICacheService
    {
        Task<T> GetCachedDataAsync<T>(string cacheKey);
        Task SetCacheDataAsync<T>(string cacheKey, T data, TimeSpan timeToLive);
    }
}
