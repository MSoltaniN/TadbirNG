using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// اطلاعات مورد نیاز برای اعمال فیلتر روی یک فیلد اطلاعاتی را نگهداری می کند
    /// </summary>
    public class GridFilter
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public GridFilter()
        {
            Operator = GridFilterOperator.IsEqualTo;
        }

        /// <summary>
        /// نام فیلد اطلاعاتی مورد استفاده در فیلتر
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// عملگر مورد استفاده در فیلتر
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// مقدار مورد استفاده برای فیلتر کردن مقادیر فیلد اطلاعاتی
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// مقادیر این نمونه را به صورت متنی بر می گرداند
        /// </summary>
        /// <returns>مقادیر این نمونه به صورت متنی</returns>
        public override string ToString()
        {
            string toString = Operator.Contains("{0}")
                ? String.Format("[{0}] {1}", FieldName, String.Format(Operator, Value))
                : String.Format("[{0}] {1} '{2}'", FieldName, Operator, Value);
            return toString;
        }
    }
}
