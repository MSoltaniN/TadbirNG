// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:32 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Inventory
{
    /// <summary>
    /// TODO: Add description...
    /// </summary>
    public partial class UnitOfMeasurementViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfMeasurementViewModel"/> class.
        /// </summary>
        public UnitOfMeasurementViewModel()
        {
            this.Name = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Name { get; set; }
    }
}
