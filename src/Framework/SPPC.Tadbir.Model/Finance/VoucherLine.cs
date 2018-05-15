using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class VoucherLine
    {
        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        public virtual int? DetailId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        public virtual int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        public virtual int? ProjectId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پول یا ارز مورد استفاده برای مبلغ بدهکار یا بستانکار این آرتیکل
        /// </summary>
        public virtual int CurrencyId { get; set; }
    }
}
