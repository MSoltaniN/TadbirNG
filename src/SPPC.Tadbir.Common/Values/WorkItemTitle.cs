using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی مربوط به عنوان یا موضوع کار را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    /// <remarks>
    /// عنوان کار درون دیتابیس به زبان انگلیسی نگهداری می شوند. پیش از نمایش این مقادیر در واسط کاربری
    /// برنامه، لازم است تابع استاتیک این کلاس برای بدست آوردن متن فارسی متناظر صدا زده شود. این کلاس یک نمونه از
    /// پیاده سازی های موقتی مشابه برای کار با داده های چندزبانه در برنامه به حساب می آید.
    /// </remarks>
    public sealed class WorkItemTitle
    {
        private WorkItemTitle()
        {
        }

        /// <summary>
        /// عنوان کار : لطفا روی مستند ضمیمه اقدام فرمایید
        /// </summary>
        public const string ActOnDocument = "Please act on attached document";

        /// <summary>
        /// عنوان کار : لطفا مستند ضمیمه را بررسی فرمایید
        /// </summary>
        public const string ReviewDocument = "Please review attached document";

        /// <summary>
        /// عنوان کار : لطفا مستند ضمیمه را تایید فرمایید
        /// </summary>
        public const string ConfirmDocument = "Please confirm attached document";

        /// <summary>
        /// عنوان کار : لطفا مستند ضمیمه را تصویب فرمایید
        /// </summary>
        public const string ApproveDocument = "Please approve attached document";

        /// <summary>
        /// عنوان کار : مستند ضمیمه تصویب شد
        /// </summary>
        public const string DocumentApproved = "Attached document was approved";

        /// <summary>
        /// متن فارسی متناظر با عنوان کار داده شده را بدست آورده و برمی گرداند
        /// </summary>
        /// <param name="value">یکی از عنوان های پیش فرض برای کار</param>
        /// <returns>متن فارسی عنوان کار داده شده. اگر مقدار آرگومان یکی از عنوان های پیش فرض برای کار
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

        private const string _ActOnDocument = "لطفا روی مستند ضمیمه اقدام فرمایید";
        private const string _ReviewDocument = "لطفا مستند ضمیمه را بررسی فرمایید";
        private const string _ConfirmDocument = "لطفا مستند ضمیمه را تایید فرمایید";
        private const string _ApproveDocument = "لطفا مستند ضمیمه را تصویب فرمایید";
        private const string _DocumentApproved = "مستند ضمیمه تصویب شد";
        private static IDictionary<string, string> _localValues = new Dictionary<string, string>
            {
                { ActOnDocument, _ActOnDocument },
                { ReviewDocument, _ReviewDocument },
                { ConfirmDocument, _ConfirmDocument },
                { ApproveDocument, _ApproveDocument },
                { DocumentApproved, _DocumentApproved }
            };
    }
}
