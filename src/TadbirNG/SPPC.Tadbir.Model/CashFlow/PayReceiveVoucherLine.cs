using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Model.CashFlow
{
     /// <summary>
    /// اطلاعات ارتباط بین فرم دریافت/پرداخت و آرتیکل مالی را نگهداری می کند
    /// </summary>
    public class PayReceiveVoucherLine : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public PayReceiveVoucherLine()
        {
            
        }

        /// <summary>
        /// شناسه دیتابیسی فرم دریافت/پرداخت
        /// </summary>
        public virtual int PayReceiveId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی آرتیکل مالی
        /// </summary>
        public virtual int VoucherLineId { get; set; }

        /// <summary>
        /// نمونه فرم دریافت/پرداخت متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual PayReceive PayReceive { get; set; }

        /// <summary>
        /// نمونه آرتیکل مالی متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual VoucherLine VoucherLine { get; set; }
    }
}
