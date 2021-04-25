using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    /// <summary>
    /// انواع کلی خطاهای ایجاد شده سمت سرویس را تعریف می کند
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// عدم بروز خطا - مورد استفاده برای پیغام های اطلاعاتی
        /// </summary>
        Info = 0,

        /// <summary>
        /// عدم بروز خطا - مورد استفاده برای پیغام های هشداری
        /// </summary>
        Warning = 1,

        /// <summary>
        /// بروز خطای اعتبارسنجی
        /// </summary>
        ValidationError = 2,

        /// <summary>
        /// بروز خطای زمان اجرا
        /// </summary>
        RuntimeException = 3
    }
}
