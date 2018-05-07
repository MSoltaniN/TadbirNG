using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CostCenterViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این مرکز هزینه در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این مرکز هزینه در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }
    }
}
