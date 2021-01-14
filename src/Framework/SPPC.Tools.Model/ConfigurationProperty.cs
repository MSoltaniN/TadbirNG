using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata information for a property in a custom configuration.
    /// </summary>
    public class ConfigurationProperty
    {
        /// <summary>
        /// Initializes a new instance of ConfigurationProperty class.
        /// </summary>
        public ConfigurationProperty()
        {
            this.DefaultValue = String.Empty;
        }

        /// <summary>
        /// Gets or sets the display name of this property. This is the name for XML attribute that provides storage
        /// for this property in an XML configuration file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of this property. This is the name for generated ConfigurationElement class property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the fully qualified name of the .NET type for this property.
        /// </summary>
        /// <remarks>This property must have a built-in type.</remarks>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if this property is required or optional.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the default value for this property.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the metadata description that provides more information about the property's purpose and usage.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Provides a string representation for this configuration property.
        /// </summary>
        /// <returns>String representation for this configuration property</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
