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
    }
}
