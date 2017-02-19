using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides localized text for user interface content related to accounts (in Persian).
    /// </summary>
    public sealed class Accounts
    {
        private Accounts()
        {
        }

        /// <summary>
        /// Localized text for the title of account list page
        /// </summary>
        public const string IndexTitle = "فهرست حساب ها";

        /// <summary>
        /// Localized text for the text indicating that no account could be found
        /// </summary>
        public const string NoRecords = "حسابی یافت نشد";

        /// <summary>
        /// Localized text for the paging information in current account list page
        /// </summary>
        public const string CurrentPageInfo = "{0} حساب، صفحه {1} از {2}";

        /// <summary>
        /// Localized text for the caption for creating a new account
        /// </summary>
        public const string CreateNew = "ایجاد حساب جدید";
    }
}
