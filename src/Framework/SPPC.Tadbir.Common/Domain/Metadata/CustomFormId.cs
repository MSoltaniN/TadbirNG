using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// شناسه های دیتابیسی فرم های گزارشی قابل سفارشی سازی را نگهداری می کند
    /// </summary>
    public sealed class CustomFormId
    {
        private CustomFormId()
        {
        }

        /// <summary>
        /// گزارش سود و زیان
        /// </summary>
        public const int ProfitLoss = 1;
    }
}
