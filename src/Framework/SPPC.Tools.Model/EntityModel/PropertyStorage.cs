using System;
using System.ComponentModel;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata information required for persistent storage of a property.
    /// </summary>
    public class PropertyStorage
    {
        /// <summary>
        /// Gets or sets the name of this property in a persistent storage media.
        /// </summary>
        [Description("Name of this property in a persistent storage media")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the storage-specific data type name for this property.
        /// </summary>
        [Description("Storage-specific data type name for this property")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if a property accepts null value in persistent storage.
        /// </summary>
        /// <remarks>This property may be redundant, since ValidationRule member of containing Property object already
        /// has a Required member with a similar purpose.</remarks>
        [Description("True if a property accepts null value in persistent storage, otherwise false")]
        public bool Nullable { get; set; }

        /// <summary>
        /// Provides a string representation for this property storage.
        /// </summary>
        /// <returns>String representation of this property storage.</returns>
        /// <remarks>Current implementation provides a string representation that corresponds to database storage
        /// for a property. Because of generic nature of metadata, this special representation is not generic and
        /// needs to be changed later.</remarks>
        public override string ToString()
        {
            return String.Format("{0} ({1} {2})", Name, Type.ToUpper(), Nullable ? "NULL" : "NOT NULL");
        }
    }
}
