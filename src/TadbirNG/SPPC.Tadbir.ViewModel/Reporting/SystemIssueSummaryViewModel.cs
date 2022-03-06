using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات خلاصه انواع اشکالات سیستمی را نگهداری می کند
    /// </summary>
    public class SystemIssueSummaryViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی اشکال سیستمی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شاخه والد اشکال سیستمی در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// تعداد موارد موجود برای اشکال سیستمی
        /// </summary>
        public int? ItemCount { get; set; }
    }
}
