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
        /// Initializes a new instance of the <see cref="PermissionBriefViewModel"/> class.
        /// </summary>
        public PermissionBriefViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionBriefViewModel"/> class
        /// using specified property values.
        /// </summary>
        /// <param name="entity">Name of application domain entity that is controlled by this permission.
        /// To enable easy refactoring, you should use values from SPPC.Tadbir.Security.SecureEntity class.</param>
        /// <param name="flags">Bit flag that specifies one or more permissions valid for specified entity.
        /// Permissions applied to each secure entity is defined in a separate enumeration type, defined in
        /// SPPC.Tadbir.Security namespace (SPPC.Tadbir.Interfaces assembly)</param>
        public PermissionBriefViewModel(string entity, int flags)
        {
            EntityName = entity;
            Flags = flags;
        }

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
