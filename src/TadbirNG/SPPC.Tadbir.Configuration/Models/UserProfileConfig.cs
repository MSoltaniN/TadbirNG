using System;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات کاربری مورد استفاده در زیرسیستم های مختلف را نگهداری می کند
    /// </summary>
    public class UserProfileConfig
    {
        /// <summary>
        /// نمونه جدید از این کلاس می سازد
        /// </summary>
        public UserProfileConfig()
        {
            ShowDashboardAtStartup = true;
        }

        /// <summary>
        /// مشخص می کند که داشبورد کاربر پس از ورود به شرکت نمایش داده شود یا نه
        /// </summary>
        public bool ShowDashboardAtStartup { get; set; }
    }
}
