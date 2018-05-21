using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    public partial class RoleViewModel
    {
        /// <summary>
        /// Gets or sets the collection of permission names that are enabled for this role.
        /// </summary>
        public IList<string> Permissions { get; set; }
    }
}
