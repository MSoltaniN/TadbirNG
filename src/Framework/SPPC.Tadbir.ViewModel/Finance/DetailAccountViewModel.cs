using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class DetailAccountViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی تفصیلی شناور والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این تفصیلی شناور در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این تفصیلی شناور در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }
    }
}
