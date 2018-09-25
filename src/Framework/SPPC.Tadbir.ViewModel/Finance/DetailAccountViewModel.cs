using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class DetailAccountViewModel : IFiscalEntityView
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

        /// <summary>
        /// شناسه دیتابیسی شرکتی که اطلاعات تفصیلی شناور در آن تعریف شده است
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// تعداد شناورهای زیرمجموعه این تفصیلی شناور در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }
    }
}
