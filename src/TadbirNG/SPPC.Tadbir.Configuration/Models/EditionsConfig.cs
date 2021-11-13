using System;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات کلیه ویرایش های تعریف شده برای برنامه را نگهداری می کند
    /// </summary>
    public class EditionsConfig
    {
        /// <summary>
        /// تنظیمات ویرایش استاندارد
        /// </summary>
        public EditionConfig Standard { get; set; }

        /// <summary>
        /// تنظیمات ویرایش حرفه ای
        /// </summary>
        public EditionConfig Professional { get; set; }

        /// <summary>
        /// تنظیمات ویرایش سازمانی
        /// </summary>
        public EditionConfig Enterprise { get; set; }
    }
}
