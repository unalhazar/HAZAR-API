using Application.Abstraction;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.AppServices.CacheService
{
    public class CacheService(IDistributedCache cache, ConnectionMultiplexer connectionMultiplexer)
        : ICacheService
    {
        public async Task<T> GetCachedDataAsync<T>(string cacheKey)
        {
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(cachedData))
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        public async Task SetCacheDataAsync<T>(string cacheKey, T data, TimeSpan timeToLive = default)
        {
            if (timeToLive == default) timeToLive = TimeSpan.FromMinutes(30); // Varsayılan süre 30 dakika 

            var serializedData = JsonConvert.SerializeObject(data);
            await cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }


        public async Task ClearCacheByPrefixAsync(string prefix)
        {
            var cacheKeys = await GetCacheKeysAsync();
            foreach (var key in cacheKeys)
            {
                if (key.StartsWith(prefix))
                {
                    var db = connectionMultiplexer.GetDatabase();
                    var keyType = await db.KeyTypeAsync(key);

                    if (keyType == RedisType.String)
                    {
                        await cache.RemoveAsync(key); 
                    }
                    else
                    {
                        await db.KeyDeleteAsync(key); // Diğer türlerdeki (örneğin hash) anahtarları sil
                    }
                }
            }
        }

        private Task<IEnumerable<string>> GetCacheKeysAsync()
        {
            var server = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints().First());
            var keys = server.Keys(database: 0, pattern: "*");
            return Task.FromResult(keys.Select(k => k.ToString()));
        }
    }
}
