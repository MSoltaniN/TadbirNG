using System;
using System.Collections.Generic;

namespace SPPC.Framework.Values
{
    /// <summary>
    /// Provides localized text for common text content (in Persian).
    /// </summary>
    public sealed class LocalStrings
    {
        private LocalStrings()
        {
        }

        /// <summary>
        /// Localized text for home page title
        /// </summary>
        public const string HomePage = "صفحه اصلی";

        /// <summary>
        /// Localized text for the title of entity list page
        /// </summary>
        public const string IndexTitle = "فهرست {0}";

        /// <summary>
        /// Localized text for the message indicating that a required item could not be found
        /// </summary>
        public const string ItemNotFound = "{0} مورد نظر یافت نشد";

        /// <summary>
        /// Localized text for the text indicating that no record could be found
        /// </summary>
        public const string NoRecords = "{0} یافت نشد";

        /// <summary>
        /// Localized text for the paging information in current list page
        /// </summary>
        public const string CurrentPageInfo = "{0} {1}، صفحه {2} از {3}";

        /// <summary>
        /// Localized text for the caption for creating a new entity
        /// </summary>
        public const string CreateNewEntity = "ایجاد {0} جدید";

        /// <summary>
        /// Localized text for the caption for editing an existing entity
        /// </summary>
        public const string EditExistingEntity = "ویرایش {0}";

        /// <summary>
        /// Localized text for the caption for entity details
        /// </summary>
        public const string EntityDetails = "جزئیات {0}";

        /// <summary>
        /// Localized text for the caption for deleting an existing entity
        /// </summary>
        public const string DeleteEntity = "حذف {0}";

        /// <summary>
        /// Localized text for the caption for returning to a specific page
        /// </summary>
        public const string ReturnToPage = "بازگشت به {0}";

        /// <summary>
        /// Localized text for View caption
        /// </summary>
        public const string View = "مشاهده";

        /// <summary>
        /// Localized text for Edit caption
        /// </summary>
        public const string Edit = "ویرایش";

        /// <summary>
        /// Localized text for Details caption
        /// </summary>
        public const string Details = "جزئیات";

        /// <summary>
        /// Localized text for Delete caption
        /// </summary>
        public const string Delete = "حذف";

        /// <summary>
        /// Localized text for Save caption
        /// </summary>
        public const string Save = "ذخیره";

        /// <summary>
        /// Localized text for Cancel caption
        /// </summary>
        public const string Cancel = "انصراف";
    }
}
