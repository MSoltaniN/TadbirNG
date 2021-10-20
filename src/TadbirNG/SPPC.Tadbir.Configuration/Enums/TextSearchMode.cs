using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// حالات مختلف پشتیبانی از جستجوی متنی را تعریف می کند
    /// </summary>
    public sealed class TextSearchMode
    {
        private TextSearchMode()
        {
        }

        /// <summary>
        /// جستجوی متن در کل کاراکترهای فیلد
        /// </summary>
        public const string Contains = "Contains";

        /// <summary>
        /// جستجوی متن در ابتدای فیلد
        /// </summary>
        public const string StartsWith = "StartsWith";

        /// <summary>
        /// جستجوی متن در انتهای فیلد
        /// </summary>
        public const string EndsWith = "EndsWith";
    }
}
