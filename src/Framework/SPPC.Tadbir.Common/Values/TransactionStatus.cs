using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی مربوط به وضعیت های ثبتی سند مالی را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    /// <remarks>
    /// وضعیت های ثبتی درون دیتابیس به زبان انگلیسی نگهداری می شوند. پیش از نمایش این مقادیر در واسط کاربری
    /// برنامه، لازم است تابع استاتیک این کلاس برای بدست آوردن متن فارسی متناظر صدا زده شود. این کلاس یک نمونه از
    /// پیاده سازی های موقتی مشابه برای کار با داده های چندزبانه در برنامه به حساب می آید.
    /// </remarks>
    public sealed class TransactionStatus
    {
        private TransactionStatus()
        {
        }

        /// <summary>
        /// وضعیت پیش نویس برای سند مالی. سند مالی تا پیش از ورود به گردش کار تایید و تصویب،
        /// در این وضعیت می ماند.
        /// </summary>
        public const string Draft = "Draft";

        /// <summary>
        /// وضعیت ثبت نشده برای سند مالی
        /// </summary>
        public const string Unchecked = "Unchecked";

        /// <summary>
        /// وضعیت ثبت عادی برای سند مالی
        /// </summary>
        public const string NormalCheck = "NormalCheck";

        /// <summary>
        /// وضعیت ثبت قطعی برای سند مالی
        /// </summary>
        public const string FinalCheck = "FinalCheck";

        /// <summary>
        /// متن فارسی متناظر با وضعیت ثبتی داده شده را بدست آورده و برمی گرداند
        /// </summary>
        /// <param name="value">یکی از وضعیت های ثبتی سند مالی</param>
        /// <returns>متن فارسی وضعیت ثبتی داده شده. اگر مقدار آرگومان یکی از وضعیت های ثبتی تعریف شده
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

        private const string _Draft = "پیش نویس";
        private const string _Unchecked = "ثبت نشده";
        private const string _NormalCheck = "ثبت عادی";
        private const string _FinalCheck = "ثبت قطعی";
        private static IDictionary<string, string> _localValues = new Dictionary<string, string>
            {
                { Draft, _Draft },
                { Unchecked, _Unchecked },
                { NormalCheck, _NormalCheck },
                { FinalCheck, _FinalCheck }
            };
    }
}
