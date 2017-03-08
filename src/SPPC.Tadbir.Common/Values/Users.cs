using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides localized text for user interface content related to application users (in Persian).
    /// </summary>
    public sealed class Users
    {
        private Users()
        {
        }

        /// <summary>
        /// Localized text for the title of user list page
        /// </summary>
        public const string IndexTitle = "فهرست کاربران";

        /// <summary>
        /// Localized text for the text indicating that no user could be found
        /// </summary>
        public const string NoRecords = "کاربری یافت نشد";

        /// <summary>
        /// Localized text for the paging information in current user list page
        /// </summary>
        public const string CurrentPageInfo = "{0} کاربر، صفحه {1} از {2}";

        /// <summary>
        /// Localized text for the caption for creating a new user
        /// </summary>
        public const string CreateNew = "ایجاد کاربر جدید";

        /// <summary>
        /// Localized text for the caption for editing an existing user
        /// </summary>
        public const string EditExisting = "ویرایش کاربر";

        /// <summary>
        /// Localized text for the caption for user details
        /// </summary>
        public const string Details = "جزئیات حساب";

        /// <summary>
        /// Localized text for the caption for Enabled
        /// </summary>
        public const string Enabled = "فعال";

        /// <summary>
        /// Localized text for the caption for Disabled
        /// </summary>
        public const string Disabled = "غیرفعال";

        /// <summary>
        /// Special-purpose name of the system administrator user
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// Dummy text to display in password boxes
        /// </summary>
        public const string DummyPassword = "************";
    }
}
