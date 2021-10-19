using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// گزینه های موجود برای محدودسازی سطرهای اطلاعاتی قابل دسترسی را تعریف می کند
    /// </summary>
    public sealed class RowAccessOptions
    {
        private RowAccessOptions()
        {
        }

        /// <summary>
        /// روش پیش فرض برای محدودسازی
        /// </summary>
        /// <remarks>
        /// در صورتی که راهبر سیستم دسترسی به موجودیت را نداده باشد، این گزینه معادل عدم دسترسی کامل است و
        /// در صورتی که دسترسی مشاهده موجودیت را داده باشد، این گزینه معادل دسترسی کامل خواهد بود
        /// </remarks>
        public const string Default = "Default";

        /// <summary>
        /// دسترسی کامل به تمام سطرهای اطلاعاتی یک موجودیت
        /// </summary>
        public const string AllRecords = "AllRecords";

        /// <summary>
        /// دسترسی به تمام سطرهای اطلاعاتی ایجاد شده توسط کاربر
        /// </summary>
        public const string AllRecordsCreatedByUser = "AllRecordsCreatedByUser";

        /// <summary>
        /// دسترسی به تعداد محدودی از سطرهای اطلاعاتی یک موجودیت
        /// </summary>
        public const string SpecificRecords = "SpecificRecords";

        /// <summary>
        /// دسترسی به تمام سطرهای اطلاعاتی یک موجودیت بجز تعداد محدودی از آنها
        /// </summary>
        public const string AllExceptSpecificRecords = "AllExceptSpecificRecords";

        /// <summary>
        /// محدودسازی بر مبنای یک سقف تعیین شده برای مقادیر پولی و ارزی
        /// </summary>
        public const string MaxMoneyValue = "MaxMoneyValue";

        /// <summary>
        /// محدودسازی بر مبنای یک سقف تعیین شده برای فیلدهای تعداد
        /// </summary>
        public const string MaxQuantityValue = "MaxQuantityValue";

        /// <summary>
        /// دسترسی به تمام سطرهای اطلاعاتی دارای یک متن در فیلد رفرنس
        /// </summary>
        public const string SpecificReference = "SpecificReference";

        /// <summary>
        /// دسترسی به تمام سطرهای اطلاعاتی بجز سطرهای دارای یک متن در فیلد رفرنس
        /// </summary>
        public const string AllExceptSpecificReference = "AllExceptSpecificReference";

        /// <summary>
        /// عدم دسترسی کامل به سطرهای اطلاعاتی یک موجودیت
        /// </summary>
        public const string NoRecords = "NoRecords";
    }
}
