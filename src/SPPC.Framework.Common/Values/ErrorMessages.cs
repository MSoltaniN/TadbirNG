using System;
using System.Collections.Generic;

namespace SPPC.Framework.Values
{
    /// <summary>
    /// Provides localized text for error messages (in Persian).
    /// </summary>
    public sealed class ErrorMessages
    {
        private ErrorMessages()
        {
        }

        /// <summary>
        /// Localized text for Not Found title
        /// </summary>
        public const string PageNotFoundTitle = "صفحه مورد نظر یافت نشد";

        /// <summary>
        /// Localized text for user hint applicable to a Not Found (404) error
        /// </summary>
        public const string PageNotFoundHint = "لطفا در مورد درستی آدرس وب مورد نظرتان اطمینان حاصل نموده و دوباره تلاش کنید.";

        /// <summary>
        /// Localized text for a short message displayed in case of a Not Found (404) error
        /// </summary>
        public const string PageNotFound = "صفحه مورد نظر شما در این برنامه وجود ندارد.";

        /// <summary>
        /// Localized text for Access Denied title
        /// </summary>
        public const string AccessDeniedTitle = "دسترسی کافی نیست";

        /// <summary>
        /// Localized text for user hint applicable to an Access Denied (401) error
        /// </summary>
        public const string AccessDeniedHint = "در صورتی که فکر می کنید اشتباهی صورت گرفته است، لطفا با راهبر سیستم تماس بگیرید.";

        /// <summary>
        /// Localized text for a short message displayed in case of an Access Denied (401) error
        /// </summary>
        public const string AccessDenied = "شما مجوز امنیتی کافی برای دسترسی به این صفحه ندارید.";

        /// <summary>
        /// Localized text for Error Occured title
        /// </summary>
        public const string ErrorOccurredTitle = "بروز خطا";

        /// <summary>
        /// Localized text for common courtesy message displayed in case of a server error
        /// (i.e. We appologize for the inconvenience)
        /// </summary>
        public const string ErrorOccurredHint = "پوزش ما را بابت وقفه احتمالی در کار خود بپذیرید.";

        /// <summary>
        /// Localized text for a short message displayed in case of a server error
        /// </summary>
        public const string ErrorOccurred = "متاسفانه بدلیل بروز خطای سمت سرور، درخواست شما در این زمان قابل اجرا نیست.";

        /// <summary>
        /// Localized text for the message indicating an error in deleting an entity record
        /// </summary>
        public const string DeleteError = "بروز خطا در حذف {0}";
    }
}
