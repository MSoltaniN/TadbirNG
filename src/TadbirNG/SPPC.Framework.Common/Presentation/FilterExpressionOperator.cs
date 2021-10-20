using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// عملگرهای منطقی مورد استفاده در ساخت یک عبارت فیلتر ترکیبی را تعریف می کند
    /// </summary>
    public sealed class FilterExpressionOperator
    {
        private FilterExpressionOperator()
        {
        }

        /// <summary>
        /// علامت عدم استفاده از یک عملگر منطقی در عبارت فیلتر
        /// </summary>
        public const string None = "";

        /// <summary>
        /// عملگر منطقی "و" برای ترکیب دو گزاره شرطی
        /// </summary>
        public const string And = " && ";

        /// <summary>
        /// عملگر منطقی "یا" برای ترکیب دو گزاره شرطی
        /// </summary>
        public const string Or = " || ";
    }
}
