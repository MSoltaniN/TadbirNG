using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides information about all users that a security role is assigned to them.
    /// </summary>
    public class RoleUsersViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleUsersViewModel"/> class
        /// </summary>
        public RoleUsersViewModel()
        {
            Users = new List<UserBriefViewModel>();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this role.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets a collection of all users that this role is assigned to them.
        /// </summary>
        public IList<UserBriefViewModel> Users { get; private set; }
    }
}
