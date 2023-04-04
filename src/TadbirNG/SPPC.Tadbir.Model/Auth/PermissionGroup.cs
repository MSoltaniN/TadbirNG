using System;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class PermissionGroup
    {
        /// <summary>
        /// Gets or sets the unique identifier of the subsystem that defines the general scope
        /// for this permission group
        /// </summary>
        public virtual int SubsystemId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the general category for the entity or form
        /// that this permission group protects
        /// </summary>
        public virtual int SourceTypeId { get; set; }
    }
}
