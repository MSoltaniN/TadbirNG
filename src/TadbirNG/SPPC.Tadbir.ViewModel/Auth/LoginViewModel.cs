using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Represents security credentials of an existing user.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the user name for an application user.
        /// </summary>
        [Display(Name = FieldNames.UserName)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for an application user.
        /// </summary>
        [Display(Name = FieldNames.Password)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Password { get; set; }
    }
}
