using System;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    /// <summary>
    /// خلاصه اطلاعات مورد نیاز از طرف حساب سرویس دریافت/پرداخت
    /// </summary>
    public class PayReceiveAccountSummaryViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceiveAccountSummaryViewModel()
        {

        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مبلغ آرتیکل طرف حساب
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// ملاحظات آرتیکل طرف حساب
        /// </summary>
        public string Description { get; set; }
    }
}
