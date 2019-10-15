using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// کلیدهای متنی چندزبانه را برای مقادیر محدودیت ثبت در سرفصل های حسابداری تعریف می کند
    /// </summary>
    public static class TurnoverModeTitle
    {
        /// <summary>
        /// نداشتن محدودیت برای ثبت مالی
        /// </summary>
        public const string Unlimited = "Unlimited";

        /// <summary>
        /// محدودیت بدهکار در طول دوره
        /// </summary>
        public const string DebtorDuringPeriod = "DebtorDuringPeriod";

        /// <summary>
        /// محدودیت بستانکار در طول دوره
        /// </summary>
        public const string CreditorDuringPeriod = "CreditorDuringPeriod";

        /// <summary>
        /// محدودیت بدهکار در انتهای دوره
        /// </summary>
        public const string DebtorEndPeriod = "DebtorEndPeriod";

        /// <summary>
        /// محدودیت بستانکار در انتهای دوره
        /// </summary>
        public const string CreditorEndPeriod = "CreditorEndPeriod";
    }
}
