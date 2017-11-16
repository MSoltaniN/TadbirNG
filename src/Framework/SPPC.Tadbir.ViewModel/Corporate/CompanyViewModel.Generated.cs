// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-15 2:58:00 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Corporate
{
    /// <summary>
    /// Represents a business unit that is registered as a legal entity
    /// </summary>
    public partial class CompanyViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyViewModel"/> class.
        /// </summary>
        public CompanyViewModel()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this company
        /// </summary>
        [Display(Name = FieldNames.NameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(128, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this company
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
