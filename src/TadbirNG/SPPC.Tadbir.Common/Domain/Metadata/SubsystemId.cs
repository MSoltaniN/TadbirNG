using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// نوع داده شمارشی برای شناسه‌ی زیرسیستم های تعریف شده در برنامه
    /// </summary>
    public enum SubsystemId
    {
        /// <summary>
        /// مشخص نبودن زیر سیستم، مورد استفاده برای امکانات مستقل از زیرسیستم
        /// </summary>
        None = 0,

        /// <summary>
        /// زیرسیستم حسابداری
        /// </summary>
        Accounting = 1,

        /// <summary>
        /// زیرسیستم خزانه‌داری
        /// </summary>
        Treasury = 2
    }
}
