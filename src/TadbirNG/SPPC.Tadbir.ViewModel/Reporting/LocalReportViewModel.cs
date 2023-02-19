using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class LocalReportViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی گزارش ذخیره شده مرتبط
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی زبان مورد استفاده در گزارش محلی شده
        /// </summary>
        public int LocaleId { get; set; }

        /// <summary>
        /// وضعیت این گزارش محلی شده در جدول دیتابیسی مربوطه - مورد استفاده در ابزار پروژه
        /// </summary>
        public RecordState State { get; set; }
    }
}
