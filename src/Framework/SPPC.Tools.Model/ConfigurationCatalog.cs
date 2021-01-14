using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata information for a collection of configuration items and properties related
    /// to a single product.
    /// </summary>
    public class ConfigurationCatalog
    {
        /// <summary>
        /// Initializes a new instance of ConfigurationCatalog class.
        /// </summary>
        public ConfigurationCatalog()
        {
            this.Sections = new List<ConfigurationItem>();
            this.Elements = new List<ConfigurationItem>();
            this.Collections = new List<ConfigurationCollection>();
        }

        /// <summary>
        /// Gets or sets the name of the provider company for the product configured by items in this catalog.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the name of the product configured by items in this catalog.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets a collection of all custom configuration sections in this catalog.
        /// </summary>
        public ICollection<ConfigurationItem> Sections { get; private set; }

        /// <summary>
        /// Gets a collection of all custom configuration elements in this catalog.
        /// </summary>
        public ICollection<ConfigurationItem> Elements { get; private set; }

        /// <summary>
        /// Gets a collection of all custom configuration element collections in this catalog.
        /// </summary>
        public ICollection<ConfigurationCollection> Collections { get; private set; }
    }
}
