using System;
using System.ComponentModel;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata that helps validate the value of a single property in an editor.
    /// </summary>
    public class ValidationRule
    {
        /// <summary>
        /// Initializes a new instance of ValidationRule class.
        /// </summary>
        public ValidationRule()
        {
            this.Minimum = String.Empty;
            this.Maximum = String.Empty;
            this.Format = String.Empty;
        }

        /// <summary>
        /// Gets or sets the design-time name for this validation rule.
        /// </summary>
        /// <remarks>This member currently serves no special purpose and should be automatically generated
        /// by a metadata generator class.
        /// </remarks>
        [Description("Design-time name for this validation rule.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if a value is required for this property.
        /// </summary>
        [Description("True if a value is required for container property, otherwise false.")]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the minimum value or length for container property.
        /// </summary>
        [Description("Minimum value or length for container property.")]
        public string Minimum { get; set; }

        /// <summary>
        /// Gets or sets the maximum value or length for container property.
        /// </summary>
        [Description("Maximum value or length for container property.")]
        public string Maximum { get; set; }

        /// <summary>
        /// Gets or sets the required format for the value of containing property.
        /// </summary>
        /// <remarks>This member is reserved for property validation using regular expressions.</remarks>
        [Description("Required format for the value of container property.")]
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the type of validation required for this validation rule.
        /// </summary>
        [Description("Type of validation required for this validation rule.")]
        public ValidationRuleType Type { get; set; }

        /// <summary>
        /// Provides a string representation for this validation rule.
        /// </summary>
        /// <returns>String representation of this validation rule.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
