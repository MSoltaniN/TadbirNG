using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class AccountViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی حساب والد این حساب در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        public int CompanyId { get; set; }

        /// <summary>
        /// تعداد حساب های زیرمجموعه این حساب در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// یک رشته متنی برای این آبجکت برمی گرداند
        /// </summary>
        /// <returns>یک رشته متنی برای این آبجکت</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
