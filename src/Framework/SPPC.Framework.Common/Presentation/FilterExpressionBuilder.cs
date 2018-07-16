using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// امکان ساخت یک عبارت فیلتر ساده یا ترکیبی را فراهم می کند
    /// </summary>
    public class FilterExpressionBuilder
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FilterExpressionBuilder()
        {
        }

        /// <summary>
        /// عبارت فیلتر ساده یا ترکیبی ساخته شده توسط این نمونه
        /// </summary>
        public FilterExpression Expression { get; private set; }

        /// <summary>
        /// متد موزد استفاده برای شروع ساخت عبارت فیلتر
        /// </summary>
        /// <param name="filter">عبارت شرطی مورد استفاده</param>
        /// <returns>
        /// نمونه فعلی که با استفاده از آن می توان متدهای بعدی را به صورت زنجیره ای فراخوانی کرد
        /// </returns>
        public FilterExpressionBuilder New(GridFilter filter)
        {
            Expression = new FilterExpression()
            {
                Filter = filter
            };

            return this;
        }

        /// <summary>
        /// امکان ترکیب عبارت فیلتر موجود با یک عبارت شرطی جدید را به کمک عملگر منطقی "و" فراهم می کند
        /// </summary>
        /// <param name="filter">عبارت شرطی جدید</param>
        /// <returns>
        /// نمونه فعلی که با استفاده از آن می توان متدهای بعدی را به صورت زنجیره ای فراخوانی کرد
        /// </returns>
        public FilterExpressionBuilder And(GridFilter filter)
        {
            Expression.Children.Add(new FilterExpression()
            {
                Filter = filter,
                Operator = FilterExpressionOperator.And
            });

            return this;
        }

        /// <summary>
        /// امکان ترکیب عبارت فیلتر موجود با تعداد متغیری از عبارات شرطی جدید را به کمک عملگر منطقی "و" فراهم می کند
        /// </summary>
        /// <param name="filters">تعداد متغیری از عبارات شرطی جدید</param>
        /// <returns>
        /// نمونه فعلی که با استفاده از آن می توان متدهای بعدی را به صورت زنجیره ای فراخوانی کرد
        /// </returns>
        public FilterExpressionBuilder And(params GridFilter[] filters)
        {
            Array.ForEach(filters, filter => And(filter));
            return this;
        }

        /// <summary>
        /// امکان ترکیب عبارت فیلتر موجود با یک عبارت شرطی جدید را به کمک عملگر منطقی "یا" فراهم می کند
        /// </summary>
        /// <param name="filter">عبارت شرطی جدید</param>
        /// <returns>
        /// نمونه فعلی که با استفاده از آن می توان متدهای بعدی را به صورت زنجیره ای فراخوانی کرد
        /// </returns>
        public FilterExpressionBuilder Or(GridFilter filter)
        {
            Expression.Children.Add(new FilterExpression()
            {
                Filter = filter,
                Operator = FilterExpressionOperator.Or
            });

            return this;
        }

        /// <summary>
        /// امکان ترکیب عبارت فیلتر موجود با تعداد متغیری از عبارات شرطی جدید را به کمک عملگر منطقی "یا" فراهم می کند
        /// </summary>
        /// <param name="filters">تعداد متغیری از عبارات شرطی جدید</param>
        /// <returns>
        /// نمونه فعلی که با استفاده از آن می توان متدهای بعدی را به صورت زنجیره ای فراخوانی کرد
        /// </returns>
        public FilterExpressionBuilder Or(params GridFilter[] filters)
        {
            Array.ForEach(filters, filter => Or(filter));
            return this;
        }

        /// <summary>
        /// عبارت فیلتر ساخته شده را برمی گرداند
        /// </summary>
        /// <returns>عبارت فیلتر ساخته شده</returns>
        public FilterExpression Build()
        {
            return Expression;
        }
    }
}
