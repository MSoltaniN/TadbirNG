using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// تنظیمات پایه مورد نیاز برای پیکربندی سرویس های وب تدبیر را تعریف می کند
    /// </summary>
    public class AppSettingsModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AppSettingsModel()
        {
            Logging = new LoggingModel()
            {
                LogLevel = new LogLevelModel()
                {
                    Default = "Information",
                    Microsoft = "Warning"
                }
            };
            AllowedHosts = "*";
        }

        /// <summary>
        /// تنظیمات رشته های اتصال مورد نیاز برای انجام عملیات دیتابیسی
        /// </summary>
        public ConnectionStringsModel ConnectionStrings { get; set; }

        /// <summary>
        /// تنظیمات ایجاد لاگ در لایه های مختلف زیرساخت سرویس
        /// </summary>
        public LoggingModel Logging { get; set; }

        /// <summary>
        /// تنظیمات مرتبط با اتصال به سرویس
        /// </summary>
        public string AllowedHosts { get; set; }
    }
}
