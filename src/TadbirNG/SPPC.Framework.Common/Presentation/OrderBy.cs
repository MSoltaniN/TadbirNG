using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// مقادیر مجاز برای جهت مرتب سازی اطلاعات را تعریف می کند
    /// </summary>
    public sealed class OrderBy
    {
        private OrderBy()
        {
        }

        /// <summary>
        /// مرتب سازی اطلاعات در جهت صعودی (از مقادیر کوچک به بزرگ)
        /// </summary>
        public const string Ascending = "ASC";

        /// <summary>
        /// مرتب سازی اطلاعات در جهت نزولی (از مقادیر بزرگ به کوچک)
        /// </summary>
        public const string Descending = "DESC";
    }
}
