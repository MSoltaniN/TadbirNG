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

        /// <summary>
        /// Localized text for the caption for editing an existing account
        /// </summary>
        public const string EditExisting = "ویرایش حساب";

        /// <summary>
        /// Localized text for the caption for account details
        /// </summary>
        public const string Details = "جزئیات حساب";

        /// <summary>
        /// Localized text for the caption for deleting an existing account
        /// </summary>
        public const string Delete = "حذف حساب";

        /// <summary>
        /// Localized text for the text indicating an error in deleting an account
        /// </summary>
        public const string DeleteError = "بروز خطا در حذف حساب";

        /// <summary>
        /// Localized text for the text providing a hint about how to delete an account
        /// </summary>
        public const string HowToDeleteHint = "برای حذف این حساب ابتدا لازم است سندهای مربوطه را حذف یا اصلاح کنید.";

        /// <summary>
        /// Localized text for the caption for returning to account list page
        /// </summary>
        public const string ReturnToIndex = "بازگشت به فهرست حساب ها";

        /// <summary>
        /// Localized text for the text indicating that an account is used in financial transactions and cannot be deleted.
        /// </summary>
        public static readonly string CannotDeleteUsedAccount = "حساب {0} در یک یا چند سند مالی استفاده شده و قابل حذف نیست.";
    }
}
