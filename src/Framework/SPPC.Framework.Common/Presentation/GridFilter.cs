using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
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
        /// مقدار مورد استفاده برای فیلتر که باید برای فیلد اطلاعاتی انتخاب شده یک مقدار مجاز باشد
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// نمایش متنی برای این نمونه را برمی گرداند
        /// </summary>
        /// <returns>نمایش متنی برای این نمونه</returns>
        public override string ToString()
        {
            string toString = Operator.Contains("{0}")
                ? String.Format("{0}{1}", FieldName, String.Format(Operator, Value))
                : String.Format("{0} {1} '{2}'", FieldName, Operator, Value);
            return toString;
        }
    }
}
