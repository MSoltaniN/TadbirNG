using System;
using System.ComponentModel;
using SPPC.Framework.Common;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata for a single attribute in a data object.
    /// </summary>
    /// <remarks>Current design is geared towards facilitating different mappings required for data handling,
    /// such as model-to-view and model-to-storage mappings. To that end, a single storage and view is directly
    /// associated with a property. This limitation can be later removed by providing extension points that enable
    /// multiple simultaneous views and storages for a property. Current design is considered sufficient for use
    /// by a mini-framework that provides minimal RAD tools and features.</remarks>
    public class Property
    {
        /// <summary>
        /// Initializes a new instance of Property class.
        /// </summary>
        public Property()
        {
            Column = new ColumnView();
            Storage = new PropertyStorage();
            View = new PropertyView();
            ValidationRule = new ValidationRule();
            IsValidated = true;
        }

        /// <summary>
        /// Gets or sets the design-time name for this property.
        /// </summary>
        [Description("Design-time name for this property")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the generic data type for this property.
        /// </summary>
        [Description("Generic data type for this property")]
        public BuiltinType Type { get; set; }

        /// <summary>
        /// Gets or sets the metadata object containing display attributes for this property when managed inside an editor.
        /// </summary>
        [Description("Metadata object containing display attributes for this property when managed inside an editor")]
        public PropertyView View { get; set; }

        /// <summary>
        /// Gets or sets the metadata object containing persistent storage attributes for this property.
        /// </summary>
        [Description("Metadata object containing persistent storage attributes for this property")]
        public PropertyStorage Storage { get; set; }

        /// <summary>
        /// Gets or sets the metadata object containing grid display attributes for this property.
        /// </summary>
        [Description("Metadata object containing grid display attributes for this property")]
        public ColumnView Column { get; set; }

        /// <summary>
        /// Gets or sets the collective set of standard validation rules for this property.
        /// </summary>
        [Description("Collective set of standard validation rules for this property")]
        public ValidationRule ValidationRule { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if this property needs validation.
        /// </summary>
        [Description("Indicates if this property needs to be validated in user interface views.")]
        public bool IsValidated { get; set; }

        /// <summary>
        /// Gets or sets a short description of purpose and usage of the property.
        /// </summary>
        /// <remarks>If this property has value, the value will be used as XML documentation for generated property
        /// in containing POCO class.
        /// </remarks>
        [Description("Short description of purpose and usage of the property.")]
        public string Description { get; set; }

        /// <summary>
        /// Provides a string representation for this property.
        /// </summary>
        /// <returns>String representation of this property.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
