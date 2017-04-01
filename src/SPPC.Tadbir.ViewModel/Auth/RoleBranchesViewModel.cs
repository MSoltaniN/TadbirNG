using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Corporate;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides information about all branches that a security role can access.
    /// </summary>
    public class RoleBranchesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleBranchesViewModel"/> class
        /// </summary>
        public RoleBranchesViewModel()
        {
            Branches = new List<BranchViewModel>();
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
        /// Gets a collection of all branches that are accessible by this role.
        /// </summary>
        public IList<BranchViewModel> Branches { get; private set; }
    }
}
