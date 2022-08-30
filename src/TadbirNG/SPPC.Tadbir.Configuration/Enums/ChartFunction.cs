using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// توابع محاسباتی مورد استفاده در داشبورد را تعریف می کند
    /// </summary>
    public sealed class ChartFunction
    {
        private ChartFunction()
        {
        }

        /// <summary>
        /// تابع محاسباتی گردش بدهکار
        /// </summary>
        public const string DebitTurnover = "DebitTurnover";

        /// <summary>
        /// تابع محاسباتی گردش بستانکار
        /// </summary>
        public const string CreditTurnover = "CreditTurnover";

        /// <summary>
        /// تابع محاسباتی مانده
        /// </summary>
        public const string Balance = "Balance";
    }
}
