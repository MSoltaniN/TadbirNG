using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی مربوط به انواع اطلاعات عملیاتی را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    /// <remarks>
    /// نوع مستند عملیاتی درون دیتابیس به زبان انگلیسی نگهداری می شوند. پیش از نمایش این مقادیر در واسط کاربری
    /// برنامه، لازم است تابع استاتیک این کلاس برای بدست آوردن متن فارسی متناظر صدا زده شود. این کلاس یک نمونه از
    /// پیاده سازی های موقتی مشابه برای کار با داده های چندزبانه در برنامه به حساب می آید.
    /// </remarks>
    public sealed class DocumentTypeName
    {
        private DocumentTypeName()
        {
        }

        /// <summary>
        /// مستند عملیاتی سند مالی
        /// </summary>
        public const string Transaction = "Transaction";

        /// <summary>
        /// متن فارسی متناظر با نوع مستند داده شده را بدست آورده و برمی گرداند
        /// </summary>
        /// <param name="value">یکی از انواع مستند های عملیاتی</param>
        /// <returns>متن فارسی نوع مستند عملیاتی داده شده. اگر مقدار آرگومان یکی از انواع مستند های عملیاتی
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

        private const string _Transaction = "سند مالی";
        private static IDictionary<string, string> _localValues = new Dictionary<string, string>
            {
                { Transaction, _Transaction }
            };
    }
}
