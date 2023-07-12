using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    public partial class PayReceiveCashAccountViewModel
    {
        /// <summary>
        /// بردار حساب مورد استفاده برای حساب نقد
        /// </summary>
        public FullAccountViewModel FullAccount { get; set; }

        /// <summary>
        /// شناسه فرم دریافت/پرداخت اصلی
        /// </summary>
        public int PayReceiveId { get; set; }

        /// <summary>
        /// شناسه منبع یا مصرف مورد استفاده
        /// </summary>
        public int? SourceAppId { get; set; }
    }
}
