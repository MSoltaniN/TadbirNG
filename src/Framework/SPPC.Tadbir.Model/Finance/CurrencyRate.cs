using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class CurrencyRate
    {
        /// <summary>
        /// شناسه دیتابیسی ارز مرتبط با این نرخ
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// شناسه یکتای شعبه سازمانی که این نرخ ارز در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }
    }
}
