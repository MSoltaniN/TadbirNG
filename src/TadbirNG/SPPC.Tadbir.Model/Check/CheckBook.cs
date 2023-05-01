﻿using System;

namespace SPPC.Tadbir.Model.Check
{
    public partial class CheckBook
    {
        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور از بردار حساب مورد استفاده در این دسته چک 
        /// </summary>
        public virtual int? DetailAccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه از بردار حساب مورد استفاده در این دسته چک
        /// </summary>
        public virtual int? ProjectId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر ایجادکننده این دسته چک
        /// </summary>
        public virtual int CreatedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربری که آخرین تغییرات را روی دسته چک داده است
        /// </summary>
        public virtual int ModifiedById { get; set; }
    }
}
