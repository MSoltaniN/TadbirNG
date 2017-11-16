using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides complete contextual information about a user and his/her security permissions.
    /// </summary>
    [Serializable]
    public class UserContextViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContextViewModel"/> class.
        /// </summary>
        public UserContextViewModel()
        {
            Branches = new List<int>();
            Roles = new List<int>();
            Permissions = new List<PermissionBriefViewModel>();
        }

        /// <summary>
        /// Gets or sets the unique identifier of this user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the organization person related to this user.
        /// </summary>
        public string PersonFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the organization person related to this user.
        /// </summary>
        public string PersonLastName { get; set; }

        /// <summary>
        /// Gets a collection of identifiers for all branches accessible to this user.
        /// </summary>
        public IList<int> Branches { get; private set; }

        /// <summary>
        /// Gets a collection of identifiers for all roles assigned to this user.
        /// </summary>
        public IList<int> Roles { get; private set; }

        /// <summary>
        /// Gets a collection of all security permissions granted to this user.
        /// </summary>
        public IList<PermissionBriefViewModel> Permissions { get; private set; }
    }
}
