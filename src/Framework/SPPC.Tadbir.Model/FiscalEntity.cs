using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Model
{
    public class FiscalEntity : IFiscalEntity
    {
        /// <summary>
        /// شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public Branch Branch { get; set; }

        /// <summary>
        /// شناسه یکتای دوره مالی که این موجودیت در آن تعریف می شود
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه یکتای شعبه سازمانی که این موجودیت در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
