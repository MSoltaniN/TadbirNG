using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Defines a leaf node in a tree hierarchy that does not have any child items.
    /// </summary>
    public class Leaf
    {
        /// <summary>
        /// Gets or sets the display name of this leaf node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the containing Tree object.
        /// </summary>
        public Tree Parent { get; internal set; }

        /// <summary>
        /// Gets or sets the data object associated with this leaf node.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Provides a string representation for this leaf node.
        /// </summary>
        /// <returns>String representation of this leaf node.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
