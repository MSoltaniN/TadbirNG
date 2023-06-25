using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Currency
    {
        /// <summary>
        /// مجموعه نرخ های تعریف شده برای ارز
        /// </summary>
        public IList<CurrencyRate> Rates { get; }
    }
}
