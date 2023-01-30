using System;

namespace SPPC.Tadbir.ViewModel.Config
{
    public partial class LogSettingViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی زیرسیستم مرتبط با فرم یا موجودیت
        /// </summary>
        public int SubsystemId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نوع فرم یا موجوریت
        /// </summary>
        public int SourceTypeId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی فرم عملیاتی مورد استفاده
        /// </summary>
        public int? SourceId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیت مورد استفاده در عملیات
        /// </summary>
        public int? EntityTypeId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی عملیات انجام شده
        /// </summary>
        public int OperationId { get; set; }

        /// <summary>
        /// نام محلی شده برای عملیات
        /// </summary>
        public string OperationName { get; set; }
    }
}
