using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// اقدامات مجاز روی یک سند مالی ذخیره شده را تعریف می کند
    /// </summary>
    public static class VoucherAction
    {
        /// <summary>
        /// عمل ثبت سند
        /// </summary>
        public const string Check = "Check";

        /// <summary>
        /// عمل برگشت از ثبت سند
        /// </summary>
        public const string UndoCheck = "UndoCheck";

        /// <summary>
        /// عمل تایید سند
        /// </summary>
        public const string Confirm = "Confirm";

        /// <summary>
        /// عمل برگشت از تایید سند
        /// </summary>
        public const string UndoConfirm = "UndoConfirm";

        /// <summary>
        /// عمل تصویب سند
        /// </summary>
        public const string Approve = "Approve";

        /// <summary>
        /// عمل برگشت از تصویب سند
        /// </summary>
        public const string UndoApprove = "UndoApprove";

        /// <summary>
        /// عمل ثبت قطعی سند
        /// </summary>
        public const string Finalize = "Finalize";
    }
}
