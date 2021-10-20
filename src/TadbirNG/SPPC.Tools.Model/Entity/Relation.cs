using System;
using System.ComponentModel;
using SPPC.Framework.Common;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata for an association between two data objects.
    /// </summary>
    public class Relation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Relation"/> class.
        /// </summary>
        public Relation()
        {
            IsRequired = true;
        }

        /// <summary>
        /// Gets or sets the design-time name for this entity.
        /// </summary>
        [Description("Design-time name for this entity")]
        public string Name { get { return GetDefaultName(); } }

        /// <summary>
        /// Gets or sets the multiplicity (or cardinality) of this association.
        /// </summary>
        [Description("Multiplicity (or cardinality) of this association")]
        public RelationMultiplicity Multiplicity { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity member used for representing this association. If not specified,
        /// entity type name will be used.
        /// </summary>
        [Description("Name of the entity member used for representing this association")]
        public string EndpointName { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity that represents this association.
        /// </summary>
        [Description("Name of the entity that represents this association")]
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if the owner entity for this relation has a key column to referenced entity.
        /// </summary>
        [Description("Value that indicates if the owner entity for this relation has a key column to referenced entity")]
        public bool HasKey { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if related entity is required to always have a value
        /// </summary>
        [Description("Value that indicates if related entity is required to always have a value")]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the name of the join table in a many to many relationship. If current multiplicity is
        /// anything other than ManyToMany, this member will be ignored.
        /// </summary>
        [Description("Name of the join table in a many to many relationship")]
        public string JoinTable { get; set; }

        /// <summary>
        /// Gets or sets a short description of purpose and usage of the relation in the application.
        /// </summary>
        /// <remarks>If this property has value, the value will be used as XML documentation for generated POCO class.
        /// </remarks>
        [Description("Short description of purpose and usage of the entity in the application")]
        public string Description { get; set; }

        private static string GetPluralName(string name)
        {
            Verify.ArgumentNotNullOrEmptyString(name, "name");
            char lastChar = name[name.Length - 1];
            string plural;
            switch (lastChar)
            {
                case 'h':
                case 's':
                case 'x':
                case 'z':
                    plural = String.Format("{0}es", name);
                    break;
                case 'y':
                    plural = String.Format("{0}ies", name.Substring(0, name.Length - 1));
                    break;
                default:
                    plural = String.Format("{0}s", name);
                    break;
            }

            return plural;
        }

        private string GetDefaultName()
        {
            string name = EndpointName;
            if (Multiplicity == RelationMultiplicity.OneToMany || Multiplicity == RelationMultiplicity.ManyToMany)
            {
                name = GetPluralName(name);
            }

            return name;
        }
    }
}
