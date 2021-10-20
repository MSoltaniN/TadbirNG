using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات خلاصه را برای یکی از گزارش های موجود نگهداری می کند
    /// </summary>
    public class ReportSummaryViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی گزارش
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر سیستمی است یا نه؟
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر پیش فرض هست یا نه؟
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
