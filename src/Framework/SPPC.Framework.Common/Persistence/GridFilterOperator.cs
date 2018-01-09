using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// عملگرهای مجاز را برای استفاده در عملیات فیلترکردن اطلاعات در جداول اطلاعاتی تعریف می کند
    /// </summary>
    public sealed class GridFilterOperator
    {
        private GridFilterOperator()
        {
        }

        /// <summary>
        /// فیلتر اطلاعات بر مبنای برابری با یک مقدار
        /// </summary>
        public const string IsEqualTo = "=";

        /// <summary>
        /// فیلتر اطلاعات متنی بر مبنای شروع مقدار متنی با یک متن تعیین شده
        /// </summary>
        public const string StartsWith = "LIKE '{0}%'";

        /// <summary>
        /// فیلتر اطلاعات متنی بر مبنای پایان مقدار متنی با یک متن تعیین شده
        /// </summary>
        public const string EndsWith = "LIKE '%{0}'";

        /// <summary>
        /// فیلتر اطلاعات متنی بر مبنای وجود داشتن یک مقدار تعیین شده
        /// </summary>
        public const string Contains = "LIKE '%{0}%'";

        /// <summary>
        /// فیلتر اطلاعات متنی بر مبنای وجود نداشتن یک مقدار تعیین شده
        /// </summary>
        public const string NotContains = "NOT LIKE '%{0}%'";
    }
}
