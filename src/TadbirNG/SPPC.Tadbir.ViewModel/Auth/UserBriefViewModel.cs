using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Domain;

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
        /// Gets or sets the first name and last name of the organization person related to this user.
        /// </summary>
        [Display(Name = FieldNames.FullNameField)]
        public string PersonFullName { get; set; }

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
