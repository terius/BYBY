using System.Web;

namespace BYBY.Cache.CacheStorage
{
    public class HttpContextCacheAdapter : ICacheStorage
    {        
        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);   
        }

        public void Store(string key, object data)
        {
            HttpRuntime.Cache.Insert(key, data);    
        }

        public T Retrieve<T>(string key)
        {
            T itemStored = (T)HttpRuntime.Cache.Get(key);
            if (itemStored == null)
                itemStored = default(T);

            return itemStored;       
        }       
    }
}
