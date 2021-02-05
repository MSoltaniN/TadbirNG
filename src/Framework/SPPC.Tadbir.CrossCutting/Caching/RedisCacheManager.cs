using ServiceStack.Redis;

namespace SPPC.Tadbir.CrossCutting.Redis
{
    /// <summary>
    /// این کلاس مدیریت کش را به عهده دارد
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        /// <summary>
        /// اطلاعات مشخص شده با کلید متنی را در حافظه کش اضافه یا به روزرسانی می کند
        /// </summary>
        /// <typeparam name="T">نوع اطلاعات مورد نظر برای ذخیره یا به روزرسانی</typeparam>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        /// <param name="value">اطلاعات مورد نظر برای ذخیره یا به روزرسانی</param>
        public void Set<T>(string key, T value)
        {
            using (var manager = new RedisManagerPool(_hostUrl))
            {
                using (var client = manager.GetClient())
                {
                    client.Set(key, value);
                }
            }
        }

        /// <summary>
        /// اطلاعات مشخص شده با کلید متنی را از حافظه کش بازیابی می کند
        /// </summary>
        /// <typeparam name="T">نوع اطلاعات مورد نظر برای بازیابی</typeparam>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        /// <returns>اطلاعات بازیابی شده از حافظه کش</returns>
        public T Get<T>(string key)
        {
            using (var manager = new RedisManagerPool(_hostUrl))
            {
                using (var client = manager.GetClient())
                {
                    var item = client.Get<T>(key);
                    return item;
                }
            }
        }

        /// <summary>
        /// اطلاعات مشخص شده با کلید متنی را از حافظه کش پاک می کند
        /// </summary>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        public void Delete(string key)
        {
            using (var manager = new RedisManagerPool(_hostUrl))
            {
                using (var client = manager.GetClient())
                {
                    if (client.ContainsKey(key))
                    {
                        client.Remove(key);
                    }
                }
            }
        }

        /// <summary>
        /// مشخص می کند که اطلاعاتی با کلید متنی مشخص شده در حافظه کش وجود دارد یا نه
        /// </summary>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        /// <returns>در صورت وجود اطلاعات در حافظه کش مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool ContainsKey(string key)
        {
            using (var manager = new RedisManagerPool(_hostUrl))
            {
                using (var client = manager.GetClient())
                {
                    return client.ContainsKey(key);
                }
            }
        }

        private const string _hostUrl = "localhost:6379";
    }
}
