using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    public partial class CashRegisterViewModel : IFiscalEntity, IBaseEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که صندوق در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که صندوق در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه ای که صندوق در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// نام دوره مالی که صندوق در آن تعریف شده است
        /// </summary>
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// وضعیت فعال یا غیر فعال برای این سطر اطلاعاتی
        /// </summary>
        public string State { get; set; }
    }
}
