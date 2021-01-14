using System;
using SPPC.Framework.Common;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides factory methods for creating ValidationRule objects.
    /// </summary>
    public class ValidationRuleFactory
    {
        /// <summary>
        /// Creates a default ValidationRule object for a built-in type specified by a BuiltinType enumeration value.
        /// </summary>
        /// <param name="type">A BuiltinType value to create a default ValidationRule for.</param>
        /// <returns>A default ValidationRule object whose members are populated for a built-in type.</returns>
        public static ValidationRule CreateDefault(BuiltinType type)
        {
            var rule = new ValidationRule() { Name = "Validation", Type = ValidationRuleType.Value, Minimum = "0" };
            switch (type)
            {
                case BuiltinType.String:
                    rule.Type = ValidationRuleType.Length;
                    rule.Maximum = "64";
                    break;
                case BuiltinType.Decimal:
                    rule.Maximum = Decimal.MaxValue.ToString();
                    break;
                case BuiltinType.Double:
                    rule.Maximum = Double.MaxValue.ToString();
                    break;
                case BuiltinType.Single:
                    rule.Maximum = Single.MaxValue.ToString();
                    break;
                case BuiltinType.DateTime:
                    rule.Minimum = Constants.MinDate;
                    rule.Maximum = DateTime.MaxValue.ToShortDateString();
                    rule.Format = Constants.DateFormat;
                    break;
                case BuiltinType.TimeSpan:
                    rule.Minimum = TimeSpan.MinValue.ToString();
                    rule.Maximum = TimeSpan.MaxValue.ToString();
                    break;
                case BuiltinType.Byte:
                    rule.Maximum = Byte.MaxValue.ToString();
                    break;
                case BuiltinType.Int16:
                    rule.Maximum = Int16.MaxValue.ToString();
                    break;
                case BuiltinType.Int32:
                    rule.Maximum = Int32.MaxValue.ToString();
                    break;
                case BuiltinType.Int64:
                    rule.Maximum = Int64.MaxValue.ToString();
                    break;
                case BuiltinType.SByte:
                    rule.Maximum = SByte.MaxValue.ToString();
                    break;
                case BuiltinType.UInt16:
                    rule.Maximum = UInt16.MaxValue.ToString();
                    break;
                case BuiltinType.UInt32:
                    rule.Maximum = UInt32.MaxValue.ToString();
                    break;
                case BuiltinType.UInt64:
                    rule.Maximum = UInt64.MaxValue.ToString();
                    break;
                case BuiltinType.Char:
                case BuiltinType.Boolean:
                case BuiltinType.Guid:
                    rule.Minimum = String.Empty;
                    break;
                default:
                    break;
            }

            return rule;
        }
    }
}
