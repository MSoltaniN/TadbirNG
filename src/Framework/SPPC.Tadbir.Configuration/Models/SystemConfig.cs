using System.Collections.Generic;

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
            DefaultCalendars = new List<DefaultCalendarConfig>();
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
        /// تقویم پیش فرض برای زبان جاری برنامه
        /// </summary>
        public int DefaultCalendar { get; set; }

        /// <summary>
        /// تنظیمات تقویم پیش فرض به تفکیک زبان
        /// </summary>
        public IEnumerable<DefaultCalendarConfig> DefaultCalendars { get; }

        /// <summary>
        /// آیا از کدینگ پیش فرض استفاده شود؟
        /// </summary>
        public bool UsesDefaultCoding { get; set; }
    }
}
