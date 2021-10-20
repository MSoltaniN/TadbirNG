using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents the join table between users and roles.
    /// </summary>
    /// <remarks>
    /// This entity will likely be removed when Entity Framework Core adds support for many-to-many relationships
    /// in entity mappings. As of EF Core 2.0, this kind of support is missing, so a join table entity is required.
    /// </remarks>
    public class UserRole : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        public UserRole()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the user
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the role
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the User instance
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the Role instance
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
