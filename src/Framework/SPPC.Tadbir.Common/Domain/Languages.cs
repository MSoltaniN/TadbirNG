using System;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// نمادهای استاندارد زبان های مورد پشتیبانی برنامه را به صورت رشته های متنی ثابت تعریف می کند
    /// </summary>
    public sealed class Languages
    {
        private Languages()
        {
        }

        /// <summary>
        /// نماد عمومی زبان فارسی
        /// </summary>
        public const string Persian = "fa";

        /// <summary>
        /// نماد زبان فارسی با نگارش مورد استفاده در ایران
        /// </summary>
        public const string PersianIran = "fa-IR";

        /// <summary>
        /// نماد عمومی زبان انگلیسی
        /// </summary>
        public const string English = "en";

        /// <summary>
        /// نماد زبان انگلیسی با نگارش مورد استفاده در ایالات متحده آمریکا
        /// </summary>
        public const string EnglishUS = "en-US";

        /// <summary>
        /// نماد عمومی زبان عربی
        /// </summary>
        public const string Arabic = "ar";

        /// <summary>
        /// نماد عمومی زبان فرانسوی
        /// </summary>
        public const string French = "fr";
    }
}
