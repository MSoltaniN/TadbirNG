using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Auth
{
    public partial class UserViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the first name of the organization person related to this user.
        /// </summary>
        [Display(Name = FieldNames.FirstNameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string PersonFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the organization person related to this user.
        /// </summary>
        [Display(Name = FieldNames.LastNameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string PersonLastName { get; set; }
    }
}
