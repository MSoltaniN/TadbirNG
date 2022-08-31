using System;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class ChartAccount
    {
        /// <summary>
        /// شناسه دیتابیسی نموداری که این بردار حساب برای آن انتخاب شده است
        /// </summary>
        public virtual int ChartId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری در این بردار حساب
        /// </summary>
        public virtual int? AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور در این بردار حساب
        /// </summary>
        public virtual int? DetailAccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه در این بردار حساب
        /// </summary>
        public virtual int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه در این بردار حساب
        /// </summary>
        public virtual int? ProjectId { get; set; }
    }
}
