using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides summary information about an application user.
    /// </summary>
    public class UserBriefViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the login name of this user
        /// </summary>
        [Display(Name = FieldNames.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the organization person related to this user.
        /// </summary>
        [Display(Name = FieldNames.FirstNameField)]
        public string PersonFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the organization person related to this user.
        /// </summary>
        [Display(Name = FieldNames.LastNameField)]
        public string PersonLastName { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates if this user is enabled inside the application's security system
        /// </summary>
        [Display(Name = FieldNames.StatusField)]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates if this user has a specific role.
        /// </summary>
        public bool HasRole { get; set; }
    }
}
