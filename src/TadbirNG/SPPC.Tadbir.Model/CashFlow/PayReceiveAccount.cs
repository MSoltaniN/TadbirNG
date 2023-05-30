using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Model.CashFlow
{
    public partial class PayReceiveAccount
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
        /// شناسه دیتابیسی مولفه تفضیلی شناور در بردار حساب        
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
    }
}
