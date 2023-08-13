using System;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class VoucherLine
    {
        /// <summary>
        /// شناسه دیتابیسی سند مالی مربوط به این آرتیکل
        /// </summary>
        public virtual int VoucherId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        public virtual int? DetailAccountId { get; set; }

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
        public virtual int? CurrencyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر ایجادکننده این آرتیکل
        /// </summary>
        public virtual int CreatedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی منبع یا مصرف مرتبط با این آرتیکل
        /// </summary>
        public virtual int? SourceAppId { get; set; }
    }
}
