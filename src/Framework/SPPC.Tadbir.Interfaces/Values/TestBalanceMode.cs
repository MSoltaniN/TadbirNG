using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// داده شمارشی برای انواع مختلف تراز آزمایشی
    /// </summary>
    public enum TestBalanceMode
    {
        /// <summary>
        /// تراز آزمایشی در سطح کل
        /// </summary>
        Ledger = 0,

        /// <summary>
        /// تراز آزمایشی در سطح معین
        /// </summary>
        Subsidiary = 1,

        /// <summary>
        /// تراز آزمایشی در سطح تفصیلی
        /// </summary>
        Detail = 2,

        /// <summary>
        /// تراز آزمایشی در سطحمعین های یک حساب  کل
        /// </summary>
        LedgerItems = 3,

        /// <summary>
        /// تراز آزمایشی در سطح تفصیلی های یک حساب معین
        /// </summary>
        SubsidiaryItems = 4,

        /// <summary>
        /// تراز آزمایشی برای یکی از سطوح تفصیلی شناور
        /// </summary>
        DetailAccountLevel = 5
    }
}
