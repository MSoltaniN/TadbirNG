using System;

namespace SPPC.Tadbir.Model.Config
{
    public partial class SysLogSetting
    {
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
    }
}
