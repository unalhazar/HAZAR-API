using Application.Abstraction;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.AppServices.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public CacheService(IDistributedCache cache, ConnectionMultiplexer connectionMultiplexer)
        {
            _cache = cache;
            _connectionMultiplexer = connectionMultiplexer;
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

        public async Task ClearCacheByPrefixAsync(string prefix)
        {
            var cacheKeys = await GetCacheKeysAsync();
            foreach (var key in cacheKeys)
            {
                if (key.StartsWith(prefix))
                {
                    // Anahtarın silinmeye çalışıldığını log
                    Console.WriteLine($"Cache anahtarı siliniyor: {key}");

                    // Anahtarın türünü kontrol edin ve uygun şekilde sil
                    var db = _connectionMultiplexer.GetDatabase();
                    var keyType = await db.KeyTypeAsync(key);

                    if (keyType == RedisType.String)
                    {
                        await _cache.RemoveAsync(key); // String türündeki anahtarı sil
                    }
                    else
                    {
                        await db.KeyDeleteAsync(key); // Diğer türlerdeki (örneğin hash) anahtarları sil
                    }
                }
            }
        }

        private async Task<IEnumerable<string>> GetCacheKeysAsync()
        {
            var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
            var keys = server.Keys(database: 0, pattern: "*");
            return keys.Select(k => k.ToString());
        }
    }
}
