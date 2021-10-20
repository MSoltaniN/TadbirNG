using System;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents the join table between roles and branches.
    /// </summary>
    /// <remarks>
    /// This entity will likely be removed when Entity Framework Core adds support for many-to-many relationships
    /// in entity mappings. As of EF Core 2.0, this kind of support is missing, so a join table entity is required.
    /// </remarks>
    public class RoleBranch : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public RoleBranch()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the role
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the branch
        /// </summary>
        public virtual int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the Branch instance
        /// </summary>
        public virtual Branch Branch { get; set; }
    }
}
