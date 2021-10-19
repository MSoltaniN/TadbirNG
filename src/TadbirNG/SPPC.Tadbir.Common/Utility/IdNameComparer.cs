using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// امکان مقایسه دو زوج اطلاعاتی شامل شناسه عددی یکتا و نام را فراهم می کند
    /// </summary>
    public class IdNameComparer : IEqualityComparer<KeyValuePair<int, string>>
    {
        /// <summary>
        /// مشخص می کند که دو نمونه داده شده با هم برابرند یا نه
        /// </summary>
        /// <param name="x">اولین نمونه داده شده برای مقایسه</param>
        /// <param name="y">دومین نمونه داده شده برای مقایسه</param>
        /// <returns>در صورت برابر بودن دو نمونه مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool Equals(KeyValuePair<int, string> x, KeyValuePair<int, string> y)
        {
            return x.Key.Equals(y.Key);
        }

        /// <summary>
        /// برای نمونه داده شده یک کد درهم سازی محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="obj">نمونه مورد نظر برای محاسبه کد درهم سازی</param>
        /// <returns>کد درهم سازی محاسبه شده برای نمونه مورد نظر</returns>
        public int GetHashCode(KeyValuePair<int, string> obj)
        {
            return obj.Key.GetHashCode() | obj.Value.GetHashCode();
        }
    }
}
