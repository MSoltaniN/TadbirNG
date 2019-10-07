﻿
namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات مربوط به پیکربندی سیستم را نگهداری میکند
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public SystemConfig()
        {
        }

        /// <summary>
        /// کد یکتای ارز پیش فرض
        /// </summary>
        public string DefaultCurrencyNameKey { get; set; }

        /// <summary>
        /// تعداد اعشار ارز پیش فرض
        /// </summary>
        public int DefaultDecimalCount { get; set; }

        /// <summary>
        /// تقویم پیش فرض
        /// </summary>
        public int DefaultCalendar { get; set; }

        /// <summary>
        /// آیا از کدینگ پیش فرض استفاده شود؟
        /// </summary>
        public bool IsUseDefaultCoding { get; set; }
    }
}
