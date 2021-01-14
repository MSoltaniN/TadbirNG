using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides a generic container of metadata information for an object.
    /// </summary>
    public class ObjectMetadata
    {
        /// <summary>
        /// Gets or sets the name of the object described by this metadata instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the CLR type name of items in a collection object described by this metadata instance.
        /// If the object being described does not have a collection type, this member should be null.
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// Provides a string representation for this object.
        /// </summary>
        /// <returns>String representation of this object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
