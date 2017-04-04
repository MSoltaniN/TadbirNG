using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// Provides concise permission information for operations available for managing an entity.
    /// </summary>
    [Serializable]
    public class PermissionBriefViewModel
    {
        /// <summary>
        /// Gets or sets the name of entity related to this permission.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets available permissions in the form of one or more bit flags.
        /// </summary>
        public int Flags { get; set; }
    }
}
