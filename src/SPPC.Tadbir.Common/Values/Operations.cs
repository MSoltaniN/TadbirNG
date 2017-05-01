using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی برای عملیات برنامه را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    public sealed class Operations
    {
        private Operations()
        {
        }

        /// <summary>
        /// متن فارسی برای عمل تنظیم سند در فرآیند ثبت سند مالی
        /// </summary>
        public const string Prepare = "تنظیم";

        /// <summary>
        /// متن فارسی برای عمل بررسی سند در فرآیند ثبت سند مالی
        /// </summary>
        public const string Review = "بررسی";

        /// <summary>
        /// متن فارسی برای عمل تایید سند در فرآیند ثبت سند مالی
        /// </summary>
        public const string Confirm = "تایید";

        /// <summary>
        /// متن فارسی برای عمل تصویب سند در فرآیند ثبت سند مالی
        /// </summary>
        public const string Approve = "تصویب";
    }
}
