using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Corporate;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides complete details for a role, including additional fields from related entities.
    /// </summary>
    public class RoleDetailsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDetailsViewModel"/> class.
        /// </summary>
        public RoleDetailsViewModel()
        {
            Role = new RoleViewModel();
            Permissions = new List<PermissionViewModel>();
            Users = new List<UserBriefViewModel>();
        }

        /// <summary>
        /// Gets or sets a <see cref="RoleViewModel"/> object containing the main role data.
        /// </summary>
        public RoleViewModel Role { get; set; }

        /// <summary>
        /// Gets the collection of permissions that are enabled for this role.
        /// </summary>
        public IList<PermissionViewModel> Permissions { get; private set; }

        /// <summary>
        /// Gets the collection of users that this role is assigned to them.
        /// </summary>
        public IList<UserBriefViewModel> Users { get; private set; }
    }
}
