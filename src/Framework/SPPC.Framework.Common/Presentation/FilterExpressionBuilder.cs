using System;
using System.Collections.Generic;

namespace SPPC.Framework.Presentation
{
    public class FilterExpressionBuilder
    {
        public FilterExpressionBuilder()
        {
        }

        public FilterExpression Expression { get; private set; }

        public FilterExpressionBuilder New(GridFilter filter)
        {
            Expression = new FilterExpression()
            {
                Filter = filter
            };

            return this;
        }

        public FilterExpressionBuilder And(GridFilter filter)
        {
            Expression.Children.Add(new FilterExpression()
            {
                Filter = filter,
                Operator = FilterExpressionOperator.And,
                Parent = Expression
            });

            return this;
        }

        public FilterExpressionBuilder And(params GridFilter[] filters)
        {
            Array.ForEach(filters, filter => And(filter));
            return this;
        }

        public FilterExpressionBuilder Or(GridFilter filter)
        {
            Expression.Children.Add(new FilterExpression()
            {
                Filter = filter,
                Operator = FilterExpressionOperator.Or,
                Parent = Expression
            });

            return this;
        }

        public FilterExpressionBuilder Or(params GridFilter[] filters)
        {
            Array.ForEach(filters, filter => Or(filter));
            return this;
        }

        public FilterExpression Build()
        {
            return Expression;
        }
    }
}
