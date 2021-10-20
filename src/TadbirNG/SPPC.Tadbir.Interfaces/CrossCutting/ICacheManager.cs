using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.CrossCutting
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت کش اطلاعاتی را تعریف می کند
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// اطلاعات مشخص شده با کلید متنی را در حافظه کش اضافه یا به روزرسانی می کند
        /// </summary>
        /// <typeparam name="T">نوع اطلاعات مورد نظر برای ذخیره یا به روزرسانی</typeparam>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        /// <param name="value">اطلاعات مورد نظر برای ذخیره یا به روزرسانی</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// اطلاعات مشخص شده با کلید متنی را از حافظه کش بازیابی می کند
        /// </summary>
        /// <typeparam name="T">نوع اطلاعات مورد نظر برای بازیابی</typeparam>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        /// <returns>اطلاعات بازیابی شده از حافظه کش</returns>
        T Get<T>(string key);

        /// <summary>
        /// اطلاعات مشخص شده با کلید متنی را از حافظه کش پاک می کند
        /// </summary>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        void Delete(string key);

        /// <summary>
        /// مشخص می کند که اطلاعاتی با کلید متنی مشخص شده در حافظه کش وجود دارد یا نه
        /// </summary>
        /// <param name="key">کلید متنی برای مشخص کردن اطلاعات در حافظه کش</param>
        /// <returns>در صورت وجود اطلاعات در حافظه کش مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        bool ContainsKey(string key);
    }
}
