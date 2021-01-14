using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Represents a top-level container for all metadata information used by an application.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Initializes a new instance of Repository class.
        /// </summary>
        public Repository()
        {
            this.Entities = new List<Entity>();
        }

        /// <summary>
        /// Gets or sets the display name for this repository.
        /// </summary>
        [Description("Display name for this repository.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the product whose metadata is to be maintained in this repository.
        /// </summary>
        [Description("Name of the product whose metadata is to be maintained in this repository.")]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the name of the company who is the legal owner of this repository's product.
        /// </summary>
        [Description("Name of the company who is the legal owner of this repository's product.")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the database in which entities will be persisted.
        /// </summary>
        [Description("Name of the database in which entities will be persisted.")]
        public string Database { get; set; }

        /// <summary>
        /// Gets or sets the path to the folder where generated items will be stored.
        /// </summary>
        [Description("Path to the folder where generated items will be stored.")]
        public string GenerationOutputPath { get; set; }

        /// <summary>
        /// Gets a collection of all domain entities currently defined in this repository.
        /// </summary>
        [Description("Collection of all domain entities currently defined in this repository.")]
        public ICollection<Entity> Entities { get; private set; }

        /// <summary>
        /// Gets or sets a Storage object that defines persistent store for this repository.
        /// </summary>
        [Description("Storage object that defines persistent store for this repository.")]
        public Storage Store { get; set; }

        /// <summary>
        /// Provides a string representation for this repository.
        /// </summary>
        /// <returns>String representation of the object</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
