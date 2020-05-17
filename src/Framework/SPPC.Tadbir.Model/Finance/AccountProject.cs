using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات ارتباط بین یک سرفصل حسابداری و یک پروژه را نگهداری می کند
    /// </summary>
    public class AccountProject : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountProject()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری در این ارتباط
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه در این ارتباط
        /// </summary>
        public virtual int ProjectId { get; set; }

        /// <summary>
        /// نمونه سرفصل حسابداری متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// نمونه پروژه متناظر با شناسه دیتابیسی موجود
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
