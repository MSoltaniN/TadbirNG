using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class ProjectViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این پروژه در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این پروژه در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }
    }
}
