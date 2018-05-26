﻿using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// اطلاعات ارتباط بین یک سرفصل حسابداری و یک پروژه را نگهداری می کند
    /// </summary>
    public class AccountProject : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AccountProject()
        {
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

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

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        private void InitReferences()
        {
            Account = new Account();
            Project = new Project();
        }
    }
}