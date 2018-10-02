using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// مقادیر ثابت طراحی شده برای تنظیمات برنامه را تعریف می کند
    /// </summary>
    public static class ConfigConstants
    {
        /// <summary>
        /// مقدار تعریف شده در سیستم برای حداکثر عمق قابل تعیین برای ساختارهای اطلاعاتی درختی
        /// </summary>
        public const short MaxTreeDepth = 16;

        /// <summary>
        /// حداکثر عمق پیش فرض که توسط برنامه برای یک ساختار درختی پیشنهاد می شود.
        /// </summary>
        public const short DefaultTreeDepth = 4;

        /// <summary>
        /// مقدار تعریف شده در سیستم برای حداکثر طول کد قابل تعیین برای یک سطح از ساختار اطلاعاتی درختی
        /// </summary>
        public const short MaxCodeLength = 16;

        /// <summary>
        /// طول کد پیش فرض که توسط برنامه برای یکی از سطوح ساختار درختی پیشنهاد می شود
        /// </summary>
        public const short DefaultCodeLength = 4;

        /// <summary>
        /// نام پیش فرض که برای نمایش یک سطح از ساختار درختی پیشنهاد می شود
        /// </summary>
        public const string DefaultLevelNameKey = "TreeLevelX";
    }
}
