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
        /// تراز آزمایشی زیرمجموعه های یک حساب
        /// </summary>
        AccountItems = 3,
    }
}
