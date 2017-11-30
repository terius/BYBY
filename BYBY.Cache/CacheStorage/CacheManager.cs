namespace BYBY.Cache.CacheStorage
{
    public class CacheManager
    {
        private static ICacheService _cache;

        public static void InitializeCacheFactory(ICacheService cache)
        {
            _cache = cache;
        }

        public static ICacheService GetCacheService()
        {
            return _cache;
        }
    }
}
