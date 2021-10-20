using System.ComponentModel;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata for grid display attributes of a property.
    /// </summary>
    public class ColumnView
    {
        /// <summary>
        /// Gets or sets the grid column title for containing property.
        /// </summary>
        [Description("Grid column title for containing property.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates if containing property must be visible in grid display.
        /// </summary>
        [Description("True if containing property must be visible in grid display; otherwise false.")]
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the column width for grid display of containing property.
        /// </summary>
        [Description("Column width for grid display of containing property.")]
        public int Width { get; set; }

        /// <summary>
        /// Provides a string representation for this column view.
        /// </summary>
        /// <returns>String representation of this column view.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
