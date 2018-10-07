using System;
using System.Collections.Generic;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CostCenterViewModel : IFiscalEntityView, ITreeEntityView
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

        /// <summary>
        /// شناسه دیتابیسی شرکتی که اطلاعات مرکز هزینه در آن تعریف شده است
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// تعداد مراکز هزینه زیرمجموعه این مرکز هزینه در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }
    }
}
