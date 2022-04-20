using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// عملگرهای مجاز را برای استفاده در عملیات فیلترکردن سطرها در نمای جدولی تعریف می کند
    /// </summary>
    public sealed class GridFilterOperator
    {
        private GridFilterOperator()
        {
        }

        /// <summary>
        /// عملگر خنثی که عملکرد آن عدم اعمال هر گونه فیلتر است
        /// </summary>
        public const string True = "1 = 1";

        /// <summary>
        /// فیلتر بر مبنای برابری مقدار یک فیلد با یک مقدار تعیین شده
        /// </summary>
        public const string IsEqualTo = " == {0}";

        /// <summary>
        /// فیلتر بر مبنای نابرابری مقدار یک فیلد با یک مقدار تعیین شده
        /// </summary>
        public const string IsNotEqualTo = " != {0}";

        /// <summary>
        /// فیلتر بر مبنای بزرگتر بودن مقدار یک فیلد از یک مقدار تعیین شده
        /// </summary>
        public const string IsGreaterThan = " > {0}";

        /// <summary>
        /// فیلتر بر مبنای بزرگتر یا مساوی بودن مقدار یک فیلد با یک مقدار تعیین شده
        /// </summary>
        public const string IsGreaterOrEqualTo = " >= {0}";

        /// <summary>
        /// فیلتر بر مبنای کوچکتر بودن مقدار یک فیلد از یک مقدار تعیین شده
        /// </summary>
        public const string IsLessThan = " < {0}";

        /// <summary>
        /// فیلتر بر مبنای کوچکتر یا مساوی بودن مقدار یک فیلد با یک مقدار تعیین شده
        /// </summary>
        public const string IsLessOrEqualTo = " <= {0}";

        /// <summary>
        /// فیلتر بر مبنای مقدار نداشتن یک فیلد
        /// </summary>
        public const string IsNull = " == null";

        /// <summary>
        /// فیلتر بر مبنای مقدار داشتن یک فیلد
        /// </summary>
        public const string IsNotNull = " != null";

        /// <summary>
        /// فیلتر بر مبنای شروع یک فیلد متنی با یک متن تعیین شده
        /// </summary>
        public const string StartsWith = ".StartsWith({0})";

        /// <summary>
        /// فیلتر بر مبنای پایان یک فیلد متنی با یک متن تعیین شده
        /// </summary>
        public const string EndsWith = ".EndsWith({0})";

        /// <summary>
        /// فیلتر بر مبنای وجود داشتن یک متن در یک فیلد متنی
        /// </summary>
        public const string Contains = ".Contains({0})";

        /// <summary>
        /// فیلتر بر مبنای وجود نداشتن یک متن در یک فیلد متنی
        /// </summary>
        public const string NotContains = ".IndexOf({0}) == -1";
    }
}
