using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class LocalReport
    {
        /// <summary>
        /// شناسه دیتابیسی زبان گزارش محلی شده
        /// </summary>
        public int LocaleId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی گزارش سیستمی یا کاربری مرتبط
        /// </summary>
        public int ReportId { get; set; }
    }
}
