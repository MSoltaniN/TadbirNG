using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// داده شمارشی برای انواع مختلف تراز آزمایشی
    /// </summary>
    public enum TestBalanceMode
    {
        /// <summary>
        /// تراز آزمایشی در یکی از سطوح حساب
        /// </summary>
        Level = 0,

        /// <summary>
        /// تراز آزمایشی زیرمجموعه های یک حساب
        /// </summary>
        AccountItems = 1,
    }
}
