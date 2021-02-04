using ServiceStack.Redis;

namespace SPPC.Tadbir.CrossCutting.Caching
{
    /// <summary>
    /// این کلاس مدیریت کش را به عهده دارد
    /// </summary>
    public class RedisCacheManager
    {
        /// <summary>
        /// نمونه ای از کش منیجر
        /// </summary>
        public RedisManagerPool CacheManager
        {
            get
            {
                var manager = new RedisManagerPool("localhost:6379");
                return manager;
            }
        }

        /// <summary>
        /// اضافه کردن به کش
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">کلید اصلی کش</param>
        /// <param name="value">مقدار کش</param>
        public void Set<T>(string key, T value)
        {
            using (var client = CacheManager.GetClient())
            {
                client.Set<T>(key, value);
            }
        }

        /// <summary>
        /// خواندن از کش
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">کلید اصلی کش</param>
        public T Get<T>(string key)
        {
            using (var client = CacheManager.GetClient())
            {
                var item = client.Get<T>(key);
                return item;
            }
        }

        /// <summary>
        /// حذف یک مورد از کش
        /// </summary>
        /// <param name="key">کلید اصلی کش</param>
        public void Delete(string key)
        {
            using (var client = CacheManager.GetClient())
            {
                if (client.ContainsKey(key))
                {
                    client.Remove(key);
                }
            }
        }

        /// <summary>
        /// بررسی وجود آیتم در کش
        /// </summary>
        /// <param name="key"></param>
        public bool ContainKey(string key)
        {
            using (var client = CacheManager.GetClient())
            {
                return client.ContainsKey(key);
            }
        }
    }
}
