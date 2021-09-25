using System;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات سرفصل حسابداری غیرفعال شده در یک دوره مالی را نگهداری می کند
    /// </summary>
    public partial class InactiveAccount : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public InactiveAccount()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری غیرفعال
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که حساب مورد نظر در آن غیرفعال شده
        /// </summary>
        public virtual int FiscalPeriodId { get; set; }

        /// <summary>
        /// نمونه سرفصل حسابداری غیرفعال متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// نمونه دوره مالی متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual FiscalPeriod FiscalPeriod { get; set; }
    }
}
