using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents the join table between roles and permissions.
    /// </summary>
    /// <remarks>
    /// This entity will likely be removed when Entity Framework Core adds support for many-to-many relationships
    /// in entity mappings. As of EF Core 2.0, this kind of support is missing, so a join table entity is required.
    /// </remarks>
    public class RolePermission : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public RolePermission()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the role
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the permission
        /// </summary>
        public virtual int PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the Role instance
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the Permission instance
        /// </summary>
        public virtual Permission Permission { get; set; }
    }
}
