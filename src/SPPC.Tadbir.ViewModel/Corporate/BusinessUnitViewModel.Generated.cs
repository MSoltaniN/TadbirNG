// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:33 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Corporate
{
    public partial class BusinessUnitViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessUnitViewModel"/> class.
        /// </summary>
        public BusinessUnitViewModel()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Name { get; set; }
        [MaxLength(256, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Description { get; set; }
    }
}
