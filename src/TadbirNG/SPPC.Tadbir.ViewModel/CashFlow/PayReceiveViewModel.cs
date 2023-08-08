using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    public partial class PayReceiveViewModel : IFiscalEntity
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که فرم دریافت یا پرداخت در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که فرم دریافت یا پرداخت در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه ارز
        /// </summary>
        public int? CurrencyId { get; set; }

        /// <summary>
        /// مشخص می کند که آیا فرم دریافت/پرداخت مورد نظر تایید شده است یا نه؟
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// مشخص می کند که آیا فرم دریافت/پرداخت مورد نظر تصویب شده است یا نه؟
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// مشخص می کند که آیا فرم دریافت/پرداخت مورد نظر ثبت مالی شده است یا خیر؟
        /// </summary>
        public bool IsRegistered { get; set; }

        /// <summary>
        /// مشخص می کند که شماره فرمی بعد از این فرم دریافت/پرداخت وجود دارد یا نه
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// مشخص می کند که شماره فرمی قبل از این فرم دریافت/پرداخت وجود دارد یا نه
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// مجموع مبلغ‌های طرف حساب
        /// </summary>
        public decimal AccountAmountsSum { get; set; }

        /// <summary>
        /// مجموع مبلغ‌های حساب نقدی
        /// </summary>
        public decimal CashAmountsSum { get; set; }
    }
}
