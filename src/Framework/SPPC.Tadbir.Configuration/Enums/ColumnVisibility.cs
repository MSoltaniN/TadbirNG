using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// گزینه های موجود برای وضعیت نمایشی یک ستون در نمای لیستی را تعریف می کند
    /// </summary>
    public class ColumnVisibility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ColumnVisibility()
        {
        }

        /// <summary>
        /// تنظیم پیش فرض، به معنی تنظیماتی که توسط برنامه انجام شده
        /// </summary>
        public const string Default = "Default";

        /// <summary>
        /// نمایش دائمی ستون در نمای لیستی
        /// </summary>
        /// <remarks>
        /// این وضعیت برای ستون های نام یا عنوان مناسب است و برای قابل استفاده بودن نمای لیستی،
        /// لازم است حداقل یک ستون با این وضعیت نمایشی تنظیم شده باشد. ستون های دارای این وضعیت
        /// قابل پنهان کردن نخواهند بود
        /// </remarks>
        public const string AlwaysVisible = "";

        /// <summary>
        /// عدم نمایش دائمی ستون در نمای لیستی
        /// </summary>
        /// <remarks>
        /// این وضعیت برای ستون های مربوط به شناسه های دیتابیسی مناسب است.
        /// ستون های دارای این وضعیت همواره در نمای لیستی مخفی خواهند بود
        /// </remarks>
        public const string AlwaysHidden = "";

        /// <summary>
        /// نمایش ستون در نمای لیستی
        /// </summary>
        public const string Visible = "Visible";

        /// <summary>
        /// عدم نمایش ستون در نمای لیستی
        /// </summary>
        public const string Hidden = "Hidden";
    }
}
