using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    public partial class PermissionGroupViewModel
    {
        /// <summary>
        /// Gets the collection of all permissions defined in this group
        /// </summary>
        public List<PermissionViewModel> Permissions { get; }

        /// <summary>
        /// Gets or sets the unique identifier of the subsystem that defines the general scope
        /// for this permission group
        /// </summary>
        public int SubsystemId { get; set; }

        /// <summary>
        /// Gets or sets the name of the subsystem that defines the general scope
        /// for this permission group
        /// </summary>
        public string SubsystemName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the general category for the entity or form
        /// that this permission group protects
        /// </summary>
        public int SourceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the general category for the entity or form
        /// that this permission group protects
        /// </summary>
        public string SourceTypeName { get; set; }
    }
}
