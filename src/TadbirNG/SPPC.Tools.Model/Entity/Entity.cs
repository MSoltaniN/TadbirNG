using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata for a generic data object.
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Initializes a new instance of Entity class.
        /// </summary>
        public Entity()
        {
            this.Properties = new List<Property>();
            this.Relations = new List<Relation>();
        }

        /// <summary>
        /// Gets or sets the design-time name for this entity.
        /// </summary>
        [Description("Design-time name for this entity")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the application area to which this entity belongs.
        /// </summary>
        /// <remarks>When used in a database scripting scenario, this property can be used as a qualifying namespace
        /// (e.g. SQL Server Schema) where corresponding table can be added to.</remarks>
        [Description("Application area to which this entity belongs")]
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the name of an existing property that uniquely identifies the entity.
        /// </summary>
        [Description("Name of an existing property that uniquely identifies the entity")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the type of entity which can be the base class in a framework class hierarchy
        /// </summary>
        [Description("Type of entity which can be the base class in a framework class hierarchy")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a short description of purpose and usage of the entity in the application.
        /// </summary>
        /// <remarks>If this property has value, the value will be used as XML documentation for generated POCO class.
        /// </remarks>
        [Description("Short description of purpose and usage of the entity in the application.")]
        public string Description { get; set; }

        /// <summary>
        /// Gets a collection of Property items in this entity.
        /// </summary>
        [Description("A collection of Property items in this entity")]
        public ICollection<Property> Properties { get; private set; }

        /// <summary>
        /// Gets a collection of Relation items in this entity.
        /// </summary>
        [Description("A collection of Relation items in this entity")]
        public ICollection<Relation> Relations { get; private set; }

        /// <summary>
        /// Provides a string representation for this entity.
        /// </summary>
        /// <returns>String representation of this entity.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
