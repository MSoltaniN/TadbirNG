using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// وضعیت های مجاز برای سند مالی را تعریف می کند
    /// </summary>
    public static class VoucherStatus
    {
        /// <summary>
        /// وضعیت ناتراز : جمع بدهکار و جمع بستانکار نابرابر
        /// </summary>
        public const string NotBalanced = "NotBalanced";

        /// <summary>
        /// وضعیت تراز : جمع بدهکار و جمع بستانکار برابر
        /// </summary>
        public const string Balanced = "Balanced";

        /// <summary>
        /// وضعیت ثبت شده
        /// </summary>
        public const string Checked = "Checked";

        /// <summary>
        /// وضعیت تاییدشده
        /// </summary>
        public const string Confirmed = "Confirmed";

        /// <summary>
        /// وضعیت تصویب شده
        /// </summary>
        public const string Approved = "Approved";

        /// <summary>
        /// وضعیت ثبت قطعی
        /// </summary>
        public const string Finalized = "Finalized";
    }
}
