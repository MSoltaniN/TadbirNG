using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides localized text for user interface content related to transaction lines (in Persian).
    /// </summary>
    public sealed class TransactionLines
    {
        private TransactionLines()
        {
        }

        /// <summary>
        /// Localized text for the text indicating that no transaction lines (articles) could be found
        /// </summary>
        public const string NoRecords = "آرتیکلی یافت نشد";

        /// <summary>
        /// Localized text for the paging information in current transaction line (article) list page
        /// </summary>
        public const string CurrentPageInfo = "{0} آرتیکل، صفحه {1} از {2}";

        /// <summary>
        /// Localized text for the caption for creating a new transaction line (article)
        /// </summary>
        public const string CreateNew = "ایجاد آرتیکل جدید";

        /// <summary>
        /// Localized text for the caption for editing an existing transaction line (article)
        /// </summary>
        public const string EditExisting = "ویرایش آرتیکل";

        /// <summary>
        /// Localized text for the caption for transaction line (article) details
        /// </summary>
        public const string Details = "جزئیات آرتیکل";
    }
}
