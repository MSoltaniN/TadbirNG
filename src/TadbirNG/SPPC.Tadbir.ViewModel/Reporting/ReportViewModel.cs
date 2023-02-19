using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class ReportViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی گروه بندی گزارش در ساختمان درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی فرمی که به عنوان منبع داده اصلی این گزارش در برنامه شناخته می شود
        /// </summary>
        public int? ViewId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر ایجادکننده گزارش
        /// </summary>
        public int CreatedById { get; set; }

        /// <summary>
        /// یک دیکشنری از متن های چند زبانه مورد نیاز در گزارش
        /// </summary>
        public Dictionary<string, string> ResourceMap { get; private set; }

        /// <summary>
        /// وضعیت این گزارش چاپی در جدول دیتابیسی مربوطه - مورد استفاده در ابزار پروژه
        /// </summary>
        public RecordState State { get; set; }
    }
}
