using System;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات ارتباط بین یک سرفصل حسابداری و یک تفصیلی شناور را نگهداری می کند
    /// </summary>
    public class AccountDetailAccount : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountDetailAccount()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری در این ارتباط
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تفصیلی شناور در این ارتباط
        /// </summary>
        public virtual int DetailAccountId { get; set; }

        /// <summary>
        /// نمونه سرفصل حسابداری متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// نمونه تفصیلی شناور متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual DetailAccount DetailAccount { get; set; }
    }
}
