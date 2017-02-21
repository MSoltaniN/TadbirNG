using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides localized text for user interface content related to transactions (in Persian).
    /// </summary>
    public sealed class Transactions
    {
        private Transactions()
        {
        }

        /// <summary>
        /// Localized text for the title of transaction list page
        /// </summary>
        public const string IndexTitle = "فهرست اسناد";

        /// <summary>
        /// Localized text for the text indicating that no transaction could be found
        /// </summary>
        public const string NoRecords = "سندی یافت نشد";

        /// <summary>
        /// Localized text for the paging information in current transaction list page
        /// </summary>
        public const string CurrentPageInfo = "{0} سند، صفحه {1} از {2}";

        /// <summary>
        /// Localized text for the caption for creating a new transaction
        /// </summary>
        public const string CreateNew = "ایجاد سند جدید";
    }
}
