using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class ParameterViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی گزارشی که این پارامتر برای آن تعریف شده است
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// وضعیت این پارامتر در جدول دیتابیسی مربوطه - مورد استفاده در ابزار پروژه
        /// </summary>
        public RecordState State { get; set; }
    }
}
