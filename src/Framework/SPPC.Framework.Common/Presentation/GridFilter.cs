using System;
using System.Linq;

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
        /// نوع داده ای فیلد اطلاعاتی مورد استفاده در فیلتر به صورت متنی
        /// </summary>
        /// <remarks>
        /// برای این فیلد لازم است از نام کامل نوع داده ای در دات نت استفاده شود، به عنوان مثال مقادیر زیر مجاز هستند :
        /// System.String, System.Int32, System.DateTime, etc.
        /// </remarks>
        public string FieldTypeName { get; set; }

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
            string op = OperatorFromFieldType();
            string toString = !String.IsNullOrEmpty(Value)
                ? String.Format("{0}{1}", FieldName, String.Format(op, Value))
                : String.Format("{0} {1}", FieldName, Operator);
            return toString;
        }

        private string OperatorFromFieldType()
        {
            string op = null;
            var quotedTypes = new string[]
            {
                "System.String", "System.DateTime", "System.TimeSpan"
            };

            if (quotedTypes.Contains(FieldTypeName))
            {
                op = Operator.Replace("{0}", "\"{0}\"");
            }
            else if (FieldTypeName == "System.Date")
            {
                op = Operator.Replace("{0}", "\"{0}\"");
                op = String.Format(".Date{0}", op);
            }
            else if (FieldTypeName == "System.Date?")
            {
                string transformed = Operator.Replace("{0}", "\"{0}\"");
                transformed = String.Format(" != null && {0}{1}", FieldName, transformed);
                op = transformed;
            }
            else
            {
                op = Operator;
            }

            return op;
        }
    }
}
