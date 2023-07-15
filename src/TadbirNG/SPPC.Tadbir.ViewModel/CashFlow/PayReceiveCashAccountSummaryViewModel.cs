using System;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    /// <summary>
    /// خلاصه اطلاعات مورد نیاز از حساب نقدی سرویس دریافت/پرداخت
    /// </summary>
    public class PayReceiveCashAccountSummaryViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceiveCashAccountSummaryViewModel()
        {

        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مبلغ آرتیکل حساب نقدی
        /// </summary>
        public decimal Amount { get; set; }
    }
}
