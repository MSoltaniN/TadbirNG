using System;
using System.Collections.Generic;
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
            Operator = GridFilterOperator.True;
        }

        /// <summary>
        /// شناسه فیلتر
        /// </summary>
        public string Id { get; set; }

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
        /// پرانتز مورد استفاده برای ترکیب این فیلتر با فیلترهای عبارت والد
        /// </summary>
        public IList<Braces> Braces { get; set; }

        /// <summary>
        /// نمایش متنی برای این نمونه را برمی گرداند
        /// </summary>
        /// <returns>نمایش متنی برای این نمونه</returns>
        public override string ToString()
        {
            string fieldName = String.Format("{0}{1}", Char.ToUpper(FieldName[0]), FieldName.Substring(1));
            string op = OperatorFromFieldType();
            string toString = !String.IsNullOrEmpty(Value)
                ? String.Format("{0}{1}", fieldName, String.Format(op, Value.Replace(@"\", @"\\")))
                : String.Format("{0}{1}", fieldName, Operator);
            return toString;
        }

        private string OperatorFromFieldType()
        {
            string op = null;
            var quotedTypes = new string[]
            {
                "System.String", "System.Date", "System.DateTime", "System.TimeSpan"
            };

            if (quotedTypes.Contains(FieldTypeName))
            {
                op = Operator.Replace("{0}", "\"{0}\"");
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
