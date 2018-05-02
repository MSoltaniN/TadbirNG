using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی مربوط به عنوان گردش های کاری را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    public sealed class WorkflowTitle
    {
        private WorkflowTitle()
        {
        }

        /// <summary>
        /// عنوان گردش کار : وضعیت سند
        /// </summary>
        public const string VoucherState = "Voucher State";

        /// <summary>
        /// متن فارسی متناظر با عنوان گردش کار داده شده را بدست آورده و برمی گرداند
        /// </summary>
        /// <param name="value">یکی از عنوان های پیش فرض برای گردش کار</param>
        /// <returns>متن فارسی عنوان گردش کار داده شده. اگر مقدار آرگومان یکی از عنوان های موجود برای گردش کار
        /// نباشد، رشته خالی بر می گرداند.</returns>
        public static string ToLocalValue(string value)
        {
            string local = String.Empty;
            if (_localValues.ContainsKey(value))
            {
                local = _localValues[value];
            }

            return local;
        }

        private const string _VoucherState = "وضعیت سند";
        private static IDictionary<string, string> _localValues = new Dictionary<string, string>
            {
                { VoucherState, _VoucherState }
            };
    }
}
