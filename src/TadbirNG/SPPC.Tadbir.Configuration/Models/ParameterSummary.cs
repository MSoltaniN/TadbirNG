using System;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// اطلاعات خلاصه یکی از پارامترهای مورد نیاز توابع محاسباتی را نگهداری می کند
    /// </summary>
    public class ParameterSummary
    {
        /// <summary>
        /// نام پارامتر
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// نوع داده ای پارامتر
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// مقدار جاری پارامتر
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// شکل نمایشی اطلاعات آبجکت را به صورت متنی برمی گرداند
        /// </summary>
        /// <returns>شکل نمایشی اطلاعات آبجکت</returns>
        public override string ToString()
        {
            return $"[{Name}] = {Value} ({Type})";
        }
    }
}
