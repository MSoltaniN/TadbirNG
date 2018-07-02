using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ExceptionHandling
{
    /// <summary>
    /// اجزاء نرم افزاری اصلی مورد استفاده را به عنوان منابع بالقوه بروز خطا تعریف می کند
    /// </summary>
    public sealed class ErrorSource
    {
        private ErrorSource()
        {
        }

        /// <summary>
        /// کامپوننت استاندارد مورد استفاده برای سریال کردن آبجکت ها با فرمت سازگار با جاواسکریپت
        /// </summary>
        public const string JsonSerializer = "Newtonsoft.Json";

        /// <summary>
        /// کامپوننت مورد استفاده برای نگاشت مدل ها به مدل های نمایشی و بالعکس
        /// </summary>
        public const string AutoMapper = "AutoMapper";

        /// <summary>
        /// کامپوننت استاندارد مایکروسافت برای پیاده سازی عملیات دیتابیسی
        /// </summary>
        public const string EntityFramework = "EF Core";

        /// <summary>
        /// کامپوننت مورد استفاده برای کار با بانک های اطلاعاتی SQL Server
        /// </summary>
        public const string SqlServerAdoNet = "ADO.NET Provider for SQL Server";

        /// <summary>
        /// کامپوننت مورد استفاده برای ایجاد آبجکت ها از اینترفیس ها
        /// </summary>
        public const string IocContainer = ".NET Core Ioc";

        /// <summary>
        /// کامپوننت مورد استفاده برای پیاده سازی سرویس وب
        /// </summary>
        public const string AspNetCoreRuntime = "ASP.NET Core";

        /// <summary>
        /// محیط اجرایی اصلی سرویس وب
        /// </summary>
        public const string DotNetRuntime = ".NET Core Runtime";
    }
}
