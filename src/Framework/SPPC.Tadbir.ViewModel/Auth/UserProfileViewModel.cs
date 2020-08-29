using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides information required for managing an authenticated user's account information.
    /// </summary>
    public class UserProfileViewModel
    {
        /// <summary>
        /// Gets or sets user name of the authenticated user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the old password of the user.
        /// </summary>
        [Display(Name = FieldNames.OldPassword)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password of the user.
        /// </summary>
        [Display(Name = FieldNames.NewPassword)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = ValidationMessages.TextFieldHasLengthRange)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the repeated occurrence of the user's new password.
        /// </summary>
        [Display(Name = FieldNames.RepeatPassword)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [Compare("NewPassword", ErrorMessage = ValidationMessages.FieldsDoNotMatch)]
        public string RepeatPassword { get; set; }
    }
}
