using System.ComponentModel;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Provides metadata information required for displaying a property inside an editor.
    /// </summary>
    public class PropertyView
    {
        /// <summary>
        /// Gets or sets the design-time name of the visualizing control for containing property.
        /// </summary>
        [Description("Design-time name of the visualizing control for containing property.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the format string to use for formatting the value of containing property
        /// in the visualizing control.
        /// </summary>
        [Description("Format string to use for formatting the value of containing property in the visualizing control.")]
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the ViewType value that specifies framework-agnostic type of the visualizing control.
        /// </summary>
        [Description("ViewType value that specifies framework-agnostic type of the visualizing control.")]
        public ViewType Type { get; set; }

        /// <summary>
        /// Gets or sets the name of a property in visualizing control whose value must be bound to the value
        /// of containing property.
        /// </summary>
        [Description("Name of a property in visualizing control whose value must be bound to the value of containing property.")]
        public string BindingMember { get; set; }

        /// <summary>
        /// Provides a string representation for this property view.
        /// </summary>
        /// <returns>String representation of this property view.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
