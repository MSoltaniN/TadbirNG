using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// اطلاعات یک عبارت فیلتر ساده یا مرکب مورد استفاده در فیلتر لیست های اطلاعاتی را نگهداری می کند
    /// </summary>
    public class FilterExpression
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FilterExpression()
        {
            Filter = new GridFilter();
            Operator = FilterExpressionOperator.None;
            Children = new List<FilterExpression>();
        }

        /// <summary>
        /// عبارت فیلتر اصلی روی یکی از فیلد های اطلاعاتی
        /// </summary>
        public GridFilter Filter { get; set; }

        /// <summary>
        /// عملگر مورد استفاده برای ترکیب این فیلتر با فیلترهای عبارت والد
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// عبارت فیلتر والد در ساختار درختی یک فیلتر ترکیبی پیچیده
        /// </summary>
        public FilterExpression Parent { get; set; }

        /// <summary>
        /// عبارات فیلتر اضافه که توسط عملگرهای منطقی "و" و "یا" قابل ترکیب با عبارت اصلی هستند
        /// </summary>
        public IList<FilterExpression> Children { get; private set; }

        /// <summary>
        /// یک رشته متنی شامل مقادیر این نمونه ساخته و برمی گرداند
        /// </summary>
        /// <returns>یک رشته متنی شامل مقادیر این نمونه</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Filter.Braces != null)
            {
                foreach (var brace in Filter.Braces)
                {
                    if (brace.Brace == "(")
                    {
                        builder.Append(brace.Brace.ToString());
                    }
                }

                builder = builder.Append(Filter.ToString());
            }
            else
            {
                builder = new StringBuilder(Filter.ToString());
            }

            foreach (var item in Children)
            {
                builder.Append(item.Operator);
                if (item.Filter.Braces != null)
                {
                    foreach (var brace in item.Filter.Braces)
                    {
                        if (brace.Brace == "(")
                        {
                            builder.Append(brace.Brace.ToString());
                        }
                    }
                }

                builder.Append(item.Filter.ToString());

                if (item.Children != null && item.Children.Count > 0)
                {
                    foreach (var childItem in item.Children)
                    {
                        builder.Append(childItem.Operator);
                        if (childItem.Filter.Braces != null)
                        {
                            foreach (var brace in childItem.Filter.Braces)
                            {
                                if (brace.Brace == "(")
                                {
                                    builder.Append(brace.Brace.ToString());
                                }
                            }
                        }

                        builder.Append(childItem.Filter.ToString());

                        if (childItem.Filter.Braces != null)
                        {
                            foreach (var brace in childItem.Filter.Braces)
                            {
                                if (brace.Brace == ")")
                                {
                                    builder.Append(brace.Brace.ToString());
                                }
                            }
                        }
                    }
                }

                if (item.Filter.Braces != null)
                {
                    foreach (var brace in item.Filter.Braces)
                    {
                        if (brace.Brace == ")")
                        {
                            builder.Append(brace.Brace.ToString());
                        }
                    }
                }
            }

            if (Children.Count > 0)
            {
                builder.Insert(0, '(');
                builder.Append(')');
            }

            if (Parent != null)
            {
                builder.Insert(0, Operator);
            }

            return builder.ToString();
        }
    }
}
