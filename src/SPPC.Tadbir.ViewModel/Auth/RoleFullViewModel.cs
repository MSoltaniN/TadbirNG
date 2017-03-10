using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides complete details for a security, including additional fields from related entities.
    /// </summary>
    public class RoleFullViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleFullViewModel"/> class.
        /// </summary>
        public RoleFullViewModel()
        {
            Role = new RoleViewModel();
            Permissions = new List<PermissionViewModel>();
        }

        /// <summary>
        /// Gets or sets a <see cref="RoleViewModel"/> object containing the main role data.
        /// </summary>
        public RoleViewModel Role { get; set; }

        /// <summary>
        /// Gets or sets the collection of permissions that are enabled for this role.
        /// </summary>
        public IList<PermissionViewModel> Permissions { get; private set; }
    }
}
