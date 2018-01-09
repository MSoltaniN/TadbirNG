using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// مقادیر مجاز را برای استفاده در جهت مرتب سازی اطلاعات تعریف می کند
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
