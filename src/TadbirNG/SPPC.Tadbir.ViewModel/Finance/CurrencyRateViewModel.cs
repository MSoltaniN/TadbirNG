using System;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CurrencyRateViewModel : ViewModelBase, IFiscalEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که نرخ ارز در آن تعریف شده است - این فیلد فعلاً استفاده نمی شود
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی ارز مرتبط با این نرخ
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این نرخ ارز در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه سازمانی که این نرخ ارز در آن تعریف می شود
        /// </summary>
        public string BranchName { get; set; }
    }
}
