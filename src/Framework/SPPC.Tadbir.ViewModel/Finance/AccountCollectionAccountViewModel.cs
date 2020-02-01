using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class AccountCollectionAccountViewModel : ViewModelBase, IFiscalEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری در این ارتباط
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی در این ارتباط
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه در این ارتباط
        /// </summary>
        public int BranchId { get; set; }
    }
}
