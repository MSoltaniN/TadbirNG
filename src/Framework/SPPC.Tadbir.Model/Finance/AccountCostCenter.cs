using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات ارتباط بین یک سرفصل حسابداری و یک مرکز هزینه را نگهداری می کند
    /// </summary>
    public class AccountCostCenter : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountCostCenter()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری در این ارتباط
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه در این ارتباط
        /// </summary>
        public virtual int CostCenterId { get; set; }

        /// <summary>
        /// نمونه سرفصل حسابداری متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// نمونه مرکز هزینه متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual CostCenter CostCenter { get; set; }
    }
}
