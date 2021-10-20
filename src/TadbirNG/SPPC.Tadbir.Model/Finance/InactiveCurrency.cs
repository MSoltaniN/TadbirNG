using System;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات ارز غیرفعال شده در یک دوره مالی را نگهداری می کند
    /// </summary>
    public partial class InactiveCurrency : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public InactiveCurrency()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی ارز غیرفعال
        /// </summary>
        public virtual int CurrencyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که ارز مورد نظر در آن غیرفعال شده
        /// </summary>
        public virtual int FiscalPeriodId { get; set; }

        /// <summary>
        /// نمونه ارز غیرفعال متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// نمونه دوره مالی متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual FiscalPeriod FiscalPeriod { get; set; }
    }
}
