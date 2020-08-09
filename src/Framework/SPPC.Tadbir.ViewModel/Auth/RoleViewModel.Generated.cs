// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-03-08 5:59:53 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Represents a logical authority used in a role-based security subsystem.
    /// </summary>
    public partial class RoleViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleViewModel"/> class.
        /// </summary>
        public RoleViewModel()
        {
            this.Name = String.Empty;
            this.Description = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this application role
        /// </summary>
        [Display(Name = FieldNames.NameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the detail information related to this role
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }
    }
}
