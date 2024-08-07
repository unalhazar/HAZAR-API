using Application.Abstraction;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.AppServices.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetCachedDataAsync<T>(string cacheKey)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(cachedData))
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        public async Task SetCacheDataAsync<T>(string cacheKey, T data, TimeSpan timeToLive)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }
    }
}
