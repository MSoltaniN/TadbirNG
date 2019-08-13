using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// داده شمارشی برای انواع آرتیکل های سند مالی
    /// </summary>
    public enum VoucherLineType
    {
        /// <summary>
        /// آرتیکل عادی
        /// </summary>
        NormalLine = 0,

        /// <summary>
        /// آرتیکل مالیات و عوارض
        /// </summary>
        TaxAndToll = 1,

        /// <summary>
        /// آرتیکل اصلاحی
        /// </summary>
        Revised = 2
    }
}
