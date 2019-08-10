using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// داده شمارشی برای قالب های مختلف تراز آزمایشی : دو ستونی، چهار ستونی و غیره
    /// </summary>
    public enum TestBalanceFormat
    {
        /// <summary>
        /// تراز آزمایشی دو ستونی
        /// </summary>
        TwoColumn = 0,

        /// <summary>
        /// تراز آزمایشی چهار ستونی
        /// </summary>
        FourColumn = 1,

        /// <summary>
        /// تراز آزمایشی شش ستونی
        /// </summary>
        SixColumn = 2,

        /// <summary>
        /// تراز آزمایشی هشت ستونی
        /// </summary>
        EightColumn = 3,

        /// <summary>
        /// تراز آزمایشی ده ستونی
        /// </summary>
        TenColumn = 4
    }
}
