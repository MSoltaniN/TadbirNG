﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides details for a security role, including enabled permissions.
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
        /// Gets or sets the unique identifier for this role
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a <see cref="RoleViewModel"/> object containing the main role data.
        /// </summary>
        public RoleViewModel Role { get; set; }

        /// <summary>
        /// Gets the collection of permissions that are enabled for this role.
        /// </summary>
        public IList<PermissionViewModel> Permissions { get; private set; }
    }
}
