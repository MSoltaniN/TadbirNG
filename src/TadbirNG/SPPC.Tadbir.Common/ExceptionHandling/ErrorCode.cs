using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ExceptionHandling
{
    /// <summary>
    /// کدهای عددی را برای انواع خطاهای پیش بینی شده در سرویس وب تعریف می کند
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// کد عددی خاص برای عدم بروز خطا
        /// </summary>
        NoError = 0x0,

        /// <summary>
        /// کد خطا برای بروز خطا در سریال کردن آبجکت ها با فرمت سازگار با جاواسکریپت
        /// </summary>
        JsonSerializerError = 0x1000,

        /// <summary>
        /// کد خطا برای بروز خطا در پیکربندی تبدیل های مدل به مدل نمایشی و بالعکس
        /// </summary>
        AutoMapperConfigurationError = 0x2000,

        /// <summary>
        /// کد خطا برای بروز خطای عملیاتی هنگام تبدیل های مدل به مدل نمایشی و بالعکس
        /// </summary>
        AutoMapperMappingError = 0x2001,

        /// <summary>
        /// کد خطا برای بروز خطا هنگام اجرای عملیات دیتابیسی توسط کامپوننت مربوطه
        /// </summary>
        OrmMappingError = 0x3000,

        /// <summary>
        /// کد خطا برای بروز خطای داخلی هنگام ارتباط مستقیم با سرور دیتابیس
        /// </summary>
        DataProviderError = 0x4000,

        /// <summary>
        /// کد خطا برای بروز خطا در ایجاد یک آبجکت از یک اینترفیس
        /// </summary>
        TypeResolutionError = 0x5000,

        /// <summary>
        /// کد خطا برای بروز خطای زمان اجرا در محیط عملیاتی سرویس وب
        /// </summary>
        WebApiRuntimeError = 0x6000,

        /// <summary>
        /// کد خطا برای بروز خطای پیش بینی نشده یا ناشناخته
        /// </summary>
        UnknownRuntimeError = 0xf000
    }
}
