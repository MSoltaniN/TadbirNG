using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata information for a collection of custom configuration elements in a .NET configuration file.
    /// </summary>
    public class ConfigurationCollection
    {
        /// <summary>
        /// Gets or sets the name of the configuration collection. When used in a configuration file, this will become
        /// the XML element name for custom configuration node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the custom configuration collection class.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the type name of configuration items in this collection.
        /// </summary>
        /// <remarks>The type name in this property does not need to be fully qualified, as it will be used
        /// for specifying a .NET property type in a generated class. The code generator is responsible for
        /// adding required using statement that qualifies this type. Assembly and namespace information
        /// required for proper type resolution must be provided at runtime, when generated class is used.
        /// </remarks>
        public string ItemType { get; set; }

        /// <summary>
        /// Gets or sets the metadata description that provides more information about collection's purpose and usage.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Provides a string representation for this configuration item collection.
        /// </summary>
        /// <returns>String representation for this configuration item collection</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
