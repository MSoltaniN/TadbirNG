using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ExceptionHandling
{
    /// <summary>
    /// متن های غیرمحلی را برای انواع خطاهای پیش بینی شده در سرویس وب تعریف می کند
    /// </summary>
    public sealed class ErrorMessage
    {
        private ErrorMessage()
        {
        }

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطا در سریال کردن آبجکت ها با فرمت سازگار با جاواسکریپت
        /// </summary>
        public const string JsonSerializerError = "A required JSON conversion failed.";

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطا در پیکربندی تبدیل های مدل به مدل نمایشی و بالعکس
        /// </summary>
        public const string AutoMapperConfigurationError = "A required domain mapping is missing.";

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطای عملیاتی هنگام تبدیل های مدل به مدل نمایشی و بالعکس
        /// </summary>
        public const string AutoMapperMappingError = "A required domain mapping could not be completed.";

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطا هنگام اجرای عملیات دیتابیسی توسط کامپوننت مربوطه
        /// </summary>
        public const string OrmMappingError = "A required entity mapping could not be completed.";

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطای داخلی هنگام ارتباط مستقیم با سرور دیتابیس
        /// </summary>
        public const string DataProviderError = "A required database operation could not be completed.";

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطا در ایجاد یک آبجکت از یک اینترفیس
        /// </summary>
        public const string TypeResolutionError = "A required type resolution could not be completed.";

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطای زمان اجرا در محیط عملیاتی سرویس وب
        /// </summary>
        public const string WebApiRuntimeError = "An error was reported by ASP.NET Core runtime.";

        /// <summary>
        /// متن خطای مورد استفاده در صورت بروز خطای پیش بینی نشده یا ناشناخته
        /// </summary>
        public const string UnknownRuntimeError = "An error was reported by .NET Core runtime.";
    }
}
