using System;

namespace SPPC.Tadbir.Model.CashFlow
{
    public partial class PayReceiveCashAccount
    {
        /// <summary>
        /// شناسه دیتابیسی فرم دریافت/پرداخت
        /// </summary>
        public virtual int PayReceiveId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری در بردار حساب        
        /// </summary>
        public virtual int? AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور در بردار حساب        
        /// </summary>
        public virtual int? DetailAccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه در بردار حساب        
        /// </summary>
        public virtual int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه در بردار حساب        
        /// </summary>
        public virtual int? ProjectId { get; set; }

        /// <summary>
        /// شناسه منبع یا مصرف مورد استفاده        
        /// </summary>
        public virtual int? SourceAppId { get; set; }
    }
}
