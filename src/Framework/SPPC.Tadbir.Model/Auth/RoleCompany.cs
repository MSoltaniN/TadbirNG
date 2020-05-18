using System;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents the join table between roles and companies.
    /// </summary>
    /// <remarks>
    /// This entity will likely be removed when Entity Framework Core adds support for many-to-many relationships
    /// in entity mappings. As of EF Core 2.0, this kind of support is missing, so a join table entity is required.
    /// </remarks>
    public class RoleCompany : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public RoleCompany()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the role
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the company
        /// </summary>
        public virtual int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the Role instance
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the CompanyDb instance
        /// </summary>
        public virtual CompanyDb Company { get; set; }
    }
}
