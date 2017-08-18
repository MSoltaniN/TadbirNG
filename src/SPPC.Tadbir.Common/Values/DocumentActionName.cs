using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی مربوط به نوع اقدامات ممکن روی رکوردهای عملیاتی برنامه را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    /// <remarks>
    /// نوع اقدامات ممکن درون دیتابیس به زبان انگلیسی نگهداری می شوند. پیش از نمایش این مقادیر در واسط کاربری
    /// برنامه، لازم است تابع استاتیک این کلاس برای بدست آوردن متن فارسی متناظر صدا زده شود. این کلاس یک نمونه از
    /// پیاده سازی های موقتی مشابه برای کار با داده های چندزبانه در برنامه به حساب می آید.
    /// </remarks>
    public sealed class DocumentActionName
    {
        private DocumentActionName()
        {
        }

        /// <summary>
        /// اقدام تنظیم روی یک موجودیت عملیاتی
        /// </summary>
        public const string Prepare = "Prepare";

        /// <summary>
        /// اقدام بررسی روی یک موجودیت عملیاتی
        /// </summary>
        public const string Review = "Review";

        /// <summary>
        /// اقدام رد بررسی روی یک موجودیت عملیاتی
        /// </summary>
        public const string Reject = "Reject";

        /// <summary>
        /// اقدام تایید روی یک موجودیت عملیاتی
        /// </summary>
        public const string Confirm = "Confirm";

        /// <summary>
        /// اقدام تصویب روی یک موجودیت عملیاتی
        /// </summary>
        public const string Approve = "Approve";

        /// <summary>
        /// متن فارسی متناظر با اقدام داده شده را بدست آورده و برمی گرداند
        /// </summary>
        /// <param name="value">یکی از اقدامات</param>
        /// <returns>متن فارسی اقدام داده شده. اگر مقدار آرگومان یکی از اقدامات تعریف شده
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

        private const string _Prepare = "تنظیم";
        private const string _Review = "بررسی";
        private const string _Reject = "رد بررسی";
        private const string _Confirm = "تایید";
        private const string _Approve = "تصویب";
        private static IDictionary<string, string> _localValues = new Dictionary<string, string>
            {
                { Prepare, _Prepare },
                { Review, _Review },
                { Reject, _Reject },
                { Confirm, _Confirm },
                { Approve, _Approve }
            };
    }
}
