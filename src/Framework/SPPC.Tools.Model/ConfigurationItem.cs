using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata information for a custom configuration item in a .NET configuration file.
    /// </summary>
    /// <remarks>This class may represent either a configuration section or a configuration element inside a section.
    /// When used as input to configuration Section class generator, it acts as configuration section metadata.
    /// When used as input to configuration Element class generator, it acts as configuration element metadata.
    /// </remarks>
    public class ConfigurationItem
    {
        /// <summary>
        /// Initializes a new instance of Configuration metadata class.
        /// </summary>
        public ConfigurationItem()
        {
            this.Properties = new List<ConfigurationProperty>();
        }

        /// <summary>
        /// Gets or sets the name of the configuration item. When used in a configuration file, this will become
        /// the XML element name for custom configuration item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the custom configuration item class.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets a collection of all properties currently defined for this configuration item.
        /// </summary>
        /// <remarks>When used in a configuration file, a built-in property type will become an XML attribute
        /// and a custom property type will become an XML node having the name of custom property type.
        /// In custom configuration class, each property will be mapped to a read/write .NET property.</remarks>
        public ICollection<ConfigurationProperty> Properties { get; private set; }

        /// <summary>
        /// Gets or sets the metadata description that provides more information about the item's purpose and usage.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Provides a string representation for this configuration item.
        /// </summary>
        /// <returns>String representation for this configuration item</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
