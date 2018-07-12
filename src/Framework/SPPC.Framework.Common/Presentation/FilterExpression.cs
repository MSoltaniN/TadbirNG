using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Framework.Presentation
{
    public class FilterExpression
    {
        public FilterExpression()
        {
            Filter = new GridFilter();
            Operator = FilterExpressionOperator.None;
            Children = new List<FilterExpression>();
        }

        public GridFilter Filter { get; set; }

        public string Operator { get; set; }

        public FilterExpression Parent { get; set; }

        public IList<FilterExpression> Children { get; private set; }

        public override string ToString()
        {
            var builder = new StringBuilder(Filter.ToString());
            foreach (var item in Children)
            {
                builder.Append(item.Operator);
                builder.Append(item.Filter.ToString());
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
