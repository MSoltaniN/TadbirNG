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
        /// Gets or sets the first name and last name of the organization person related to this user.
        /// </summary>
        [Display(Name = FieldNames.FullNameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string PersonFullName { get; set; }
    }
}
