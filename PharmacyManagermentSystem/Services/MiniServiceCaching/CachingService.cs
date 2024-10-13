using Microsoft.Extensions.Caching.Memory;
namespace PharmacyManagermentSystem.Services.MiniServiceCaching
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;
        public CachingService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T? Get<T>(string cacheKey)
        {
            _memoryCache.TryGetValue(cacheKey, out T? value);
            return value;
        }

        public void Set<T>(string cacheKey, T value, TimeSpan time)
        {
            _memoryCache.Set(cacheKey, value, time);
        }

        public void Set<T>(string cacheKey, T value, MemoryCacheEntryOptions options)
        {
            _memoryCache.Set(cacheKey, value, options);
        }

        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
    }
}
