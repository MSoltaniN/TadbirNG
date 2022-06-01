using System;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// این نوع داده شمارشی مقادیر سیستمی را برای مأخذ یک سند مالی تعریف می کند
    /// </summary>
    public enum VoucherOriginId
    {
        /// <summary>
        /// وضعیت نامشخص برای مأخذ سند
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// سند عادی
        /// </summary>
        NormalVoucher = 1,

        /// <summary>
        /// سند افتتاحیه
        /// </summary>
        OpeningVoucher = 2,

        /// <summary>
        /// سند بستن حساب های موقت
        /// </summary>
        ClosingTempAccounts = 3,

        /// <summary>
        /// سند اختتامیه
        /// </summary>
        ClosingVoucher = 4
    }
}
