using System;
using System.ComponentModel.DataAnnotations.Schema;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// مشخصات مورد نیاز برای یک موجودیت محدود به دوره مالی و شعبه را نگهداری می کند
    /// </summary>
    public class PFiscalEntity : PCoreEntity, IFiscalEntity
    {
        /// <summary>
        /// شناسه یکتای دوره مالی که این موجودیت در آن تعریف می شود
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه یکتای شعبه سازمانی که این موجودیت در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// دوره مالی که این موجودیت در آن تعریف شده است
        /// </summary>
        public FiscalPeriod FiscalPeriod { get; set; }

        /// <summary>
        /// شعبه سازمانی که این موجودیت در آن تعریف شده است
        /// </summary>
        public Branch Branch { get; set; }
    }
}
