using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// اطلاعات کلی یکی از انواع تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class TestBalanceModeInfo
    {
        /// <summary>
        /// شناسه نوع تراز
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام محلی شده نوع تراز
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// شماره سطح مورد استفاده برای محاسبه اطلاعات تراز
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// مشخص می کند که آیا تراز از نوع زیرمجموعه است یا نه؟
        /// </summary>
        public bool IsDetail { get; set; }
    }
}
