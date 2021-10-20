using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات مربوط به تقویم پیش فرض را با در نظر گرفتن زبان جاری برنامه نگهداری می کند
    /// </summary>
    public class DefaultCalendarConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس با مقادیر پیش فرض می سازد
        /// </summary>
        public DefaultCalendarConfig()
        {
            Language = Languages.Persian;
            Calendar = (int)CalendarType.Jalali;
        }

        /// <summary>
        /// زبان مورد نظر برای تنظیم تقویم پیش فرض
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// تقویم پیش فرض مورد نظر
        /// </summary>
        public int Calendar { get; set; }
    }
}
