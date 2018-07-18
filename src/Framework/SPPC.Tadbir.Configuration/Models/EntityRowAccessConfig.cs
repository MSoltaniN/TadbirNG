using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// اطلاعات تنظیمات مورد استفاده در محدودسازی سطرهای اطلاعاتی قابل دسترسی را نگهداری می کند
    /// </summary>
    public class EntityRowAccessConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public EntityRowAccessConfig()
        {
            AccessMode = RowAccessOptions.Default;
            Items = new List<int>();
        }

        /// <summary>
        /// روش محدودسازی سطرهای اطلاعاتی قابل دسترسی
        /// </summary>
        /// <remarks>
        /// روش های متعددی برای این کار پیش بینی شده که در آینده و بنا بر نیاز کاربران قابل توسعه هستند
        /// </remarks>
        public string AccessMode { get; set; }

        /// <summary>
        /// یک مقدار عددی صحیح یا اعشاری که برای محدود کردن بر اساس تعداد یا مبلغ قابل استفاده است
        /// </summary>
        public decimal? Value { get; set; }

        /// <summary>
        /// مقدار عددی صحیح یا اعشاری اضافی برای تعیین سقف عددی
        /// </summary>
        public decimal? Value2 { get; set; }

        /// <summary>
        /// یک متن آزاد که برای محدود کردن بر اساس رفرنس های مورد استفاده در سطرهای عملیاتی قابل استفاده است
        /// </summary>
        public string TextValue { get; set; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی که برای تعریف دقیق سطرهای محدود اطلاعاتی قابل دسترسی استفاده می شود
        /// </summary>
        public IList<int> Items { get; private set; }
    }
}
