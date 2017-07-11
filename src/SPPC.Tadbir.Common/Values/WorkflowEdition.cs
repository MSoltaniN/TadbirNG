using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// متن های فارسی مربوط به نام ویرایش های مختلف گردش های کاری را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    public sealed class WorkflowEdition
    {
        private WorkflowEdition()
        {
        }

        /// <summary>
        /// عنوان ویرایش : ماشین حالت
        /// </summary>
        public const string StateMachine = "State Machine";

        /// <summary>
        /// عنوان ویرایش : فلوچارت
        /// </summary>
        public const string Flowchart = "Flowchart";

        /// <summary>
        /// عنوان ویرایش : تاخیر زمانی
        /// </summary>
        public const string Timeout = "Timeout";

        /// <summary>
        /// عنوان ویرایش : متوالی
        /// </summary>
        public const string Sequential = "Sequential";

        /// <summary>
        /// متن فارسی متناظر با عنوان ویرایش داده شده را بدست آورده و برمی گرداند
        /// </summary>
        /// <param name="value">یکی از عنوان های پیش فرض برای ویرایش گردش کار</param>
        /// <returns>متن فارسی ویرایش داده شده. اگر مقدار آرگومان یکی از عنوان های موجود برای ویرایش گردش کار
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

        private const string _StateMachine = "ماشین حالت";
        private const string _Flowchart = "فلوچارت";
        private const string _Timeout = "تاخیر زمانی";
        private const string _Sequential = "متوالی";

        private static IDictionary<string, string> _localValues = new Dictionary<string, string>
            {
                { StateMachine, _StateMachine },
                { Flowchart, _Flowchart },
                { Timeout, _Timeout },
                { Sequential, _Sequential }
            };
    }
}
