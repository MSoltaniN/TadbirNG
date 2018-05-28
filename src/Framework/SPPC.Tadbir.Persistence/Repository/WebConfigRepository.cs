using System;
using System.Collections.Generic;
using SPPC.Tadbir.Configuration;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات برنامه را با استفاده از فایل تنظیمات پیاده سازی می کند
    /// </summary>
    public class WebConfigRepository : IConfigRepository
    {
        /// <summary>
        /// تنظیمات موجود برای مدیریت ارتباطات بین مولفه های بردار حساب را از فایل تنظیمات خوانده و برمی گرداند
        /// </summary>
        /// <returns></returns>
        public RelationsConfig GetRelationsConfig()
        {
            return new RelationsConfig();
        }
    }
}
