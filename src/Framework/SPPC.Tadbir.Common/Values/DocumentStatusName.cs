using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی مربوط به وضعیت های عملیاتی رکوردهای عملیاتی برنامه را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    /// <remarks>
    /// وضعیت های عملیاتی درون دیتابیس به زبان انگلیسی نگهداری می شوند. پیش از نمایش این مقادیر در واسط کاربری
    /// برنامه، لازم است تابع استاتیک این کلاس برای بدست آوردن متن فارسی متناظر صدا زده شود. این کلاس یک نمونه از
    /// پیاده سازی های موقتی مشابه برای کار با داده های چندزبانه در برنامه به حساب می آید.
    /// </remarks>
    public sealed class DocumentStatusName
    {
        private DocumentStatusName()
        {
        }

        /// <summary>
        /// وضعیت عملیاتی ایجاد شده برای رکورد عملیاتی، که نشانه قرار نداشتن رکورد مربوطه
        /// در گردش کار تایید و تصویب است
        /// </summary>
        public const string Created = "Created";

        /// <summary>
        /// وضعیت عملیاتی تنظیم شده، که اولین وضعیت رکورد عملیاتی در گردش کار تایید و تصویب است
        /// </summary>
        public const string Prepared = "Prepared";

        /// <summary>
        /// وضعیت عملیاتی بررسی شده برای رکورد عملیاتی
        /// </summary>
        public const string Reviewed = "Reviewed";

        /// <summary>
        /// وضعیت عملیاتی تایید شده برای رکورد عملیاتی
        /// </summary>
        public const string Confirmed = "Confirmed";

        /// <summary>
        /// وضعیت عملیاتی تصویب شده برای رکورد عملیاتی، که وضعیت نهایی رکورد عملیاتی
        /// در گردش کار تایید و تصویب است
        /// </summary>
        public const string Approved = "Approved";

        /// <summary>
        /// متن فارسی متناظر با وضعیت عملیاتی داده شده را بدست آورده و برمی گرداند
        /// </summary>
        /// <param name="value">یکی از وضعیت های عملیاتی</param>
        /// <returns>متن فارسی وضعیت عملیاتی داده شده. اگر مقدار آرگومان یکی از وضعیت های عملیاتی تعریف شده
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

        private const string _Created = "ایجاد شده";
        private const string _Prepared = "تنظیم شده";
        private const string _Reviewed = "بررسی شده";
        private const string _Confirmed = "تایید شده";
        private const string _Approved = "تصویب شده";
        private static IDictionary<string, string> _localValues = new Dictionary<string, string>
            {
                { Created, _Created },
                { Prepared, _Prepared },
                { Reviewed, _Reviewed },
                { Confirmed, _Confirmed },
                { Approved, _Approved }
            };
    }
}
