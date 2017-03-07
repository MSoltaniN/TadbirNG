// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-03-07 12:45:24 PM
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
    /// Represents an application user recognized by the security subsystem
    /// </summary>
    public partial class UserViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        public UserViewModel()
        {
            this.UserName = String.Empty;
            this.Password = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the login name of this user
        /// </summary>
        [Display(Name = FieldNames.UserName)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the calculated hash value of this user's password
        /// </summary>
        [Display(Name = FieldNames.Password)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this user last logged into the application
        /// </summary>
        [Display(Name = FieldNames.LastLoginDate)]
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates if this user is enabled inside the application's security system
        /// </summary>
        [Display(Name = FieldNames.StatusField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public bool IsEnabled { get; set; }
    }
}
