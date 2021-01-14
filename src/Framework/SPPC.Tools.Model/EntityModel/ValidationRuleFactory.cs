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
                case BuiltinType.Text:
                    rule.Type = ValidationRuleType.Length;
                    rule.Maximum = "64";
                    break;
                case BuiltinType.DecimalNumber:
                    rule.Maximum = Decimal.MaxValue.ToString();
                    break;
                case BuiltinType.DoublePrecision:
                    rule.Maximum = Double.MaxValue.ToString();
                    break;
                case BuiltinType.SinglePrecision:
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
                case BuiltinType.TinyNumber:
                    rule.Maximum = Byte.MaxValue.ToString();
                    break;
                case BuiltinType.SmallNumber:
                    rule.Maximum = Int16.MaxValue.ToString();
                    break;
                case BuiltinType.Number:
                    rule.Maximum = Int32.MaxValue.ToString();
                    break;
                case BuiltinType.BigNumber:
                    rule.Maximum = Int64.MaxValue.ToString();
                    break;
                case BuiltinType.SignedTinyNumber:
                    rule.Maximum = SByte.MaxValue.ToString();
                    break;
                case BuiltinType.UnsignedSmallNumber:
                    rule.Maximum = UInt16.MaxValue.ToString();
                    break;
                case BuiltinType.UnsignedNumber:
                    rule.Maximum = UInt32.MaxValue.ToString();
                    break;
                case BuiltinType.UnsignedBigNumber:
                    rule.Maximum = UInt64.MaxValue.ToString();
                    break;
                case BuiltinType.Character:
                case BuiltinType.Boolean:
                case BuiltinType.SystemGuid:
                    rule.Minimum = String.Empty;
                    break;
                default:
                    break;
            }

            return rule;
        }
    }
}
