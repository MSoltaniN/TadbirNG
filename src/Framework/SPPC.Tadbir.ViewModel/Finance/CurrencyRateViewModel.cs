using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CurrencyRateViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی ارز مرتبط با این نرخ
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این نرخ ارز در آن تعریف می شود
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه سازمانی که این نرخ ارز در آن تعریف می شود
        /// </summary>
        public string BranchName { get; set; }
    }
}
