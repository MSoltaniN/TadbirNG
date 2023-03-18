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
            var rule = new ValidationRule() { Name = "Validation", Type = ValidationRuleType.Value };
            switch (type)
            {
                case BuiltinType.String:
                    rule.Type = ValidationRuleType.Length;
                    rule.Maximum = "64";
                    break;
                default:
                    break;
            }

            return rule;
        }
    }
}
