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
        /// عدم بروز خطا
        /// </summary>
        NoError = 0,

        /// <summary>
        /// بروز خطای اعتبارسنجی
        /// </summary>
        ValidationError = 1,

        /// <summary>
        /// بروز خطای زمان اجرا
        /// </summary>
        RuntimeException = 2,

        /// <summary>
        /// منقضی شدن تیکت امنیتی
        /// </summary>
        ExpiredSession = 3
    }
}
