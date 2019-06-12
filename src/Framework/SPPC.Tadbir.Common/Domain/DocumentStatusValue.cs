using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// این نوع داده شمارشی مقادیر سیستمی را برای وضعیت ثبتی یک مستند اداری تعریف می کند
    /// </summary>
    public enum DocumentStatusValue
    {
        /// <summary>
        /// وضعیت نامشخص برای مستند اداری
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// وضعیت پیش نویس برای مستند اداری
        /// </summary>
        Draft = 1,

        /// <summary>
        /// وضعیت ثبت عادی برای مستند اداری
        /// </summary>
        Checked = 2,

        /// <summary>
        /// وضعیت ثبت قطعی برای مستند اداری
        /// </summary>
        Finalized = 3
    }
}
