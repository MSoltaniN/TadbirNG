using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// مدل اطلاعاتی برای تنظیمات ایجاد لاگ
    /// </summary>
    public class LoggingModel
    {
        /// <summary>
        /// سطح مورد نیاز برای ایجاد لاگ - مانند لاگ های اطلاعاتی، لاگ های هشدار و غیره
        /// </summary>
        public LogLevelModel LogLevel { get; set; }
    }
}
