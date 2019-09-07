using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Currency
    {
        /// <summary>
        /// مجموعه نرخ های تعریف شده برای ارز
        /// </summary>
        public IList<CurrencyRate> Rates { get; }

        /// <summary>
        /// مجموعه ای از سرفصل های مختلف تعریف شده برای ارز در شعب مختلف
        /// </summary>
        public IList<AccountCurrency> AccountCurrencies { get; set; }
    }
}
