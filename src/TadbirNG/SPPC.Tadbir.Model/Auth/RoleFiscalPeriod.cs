﻿using System;
using SPPC.Tadbir.Model.Finance;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// Represents the join table between roles and fiscal periods.
    /// </summary>
    /// <remarks>
    /// This entity will likely be removed when Entity Framework Core adds support for many-to-many relationships
    /// in entity mappings. As of EF Core 2.0, this kind of support is missing, so a join table entity is required.
    /// </remarks>
    public class RoleFiscalPeriod : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of this class
        /// </summary>
        public RoleFiscalPeriod()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the role
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the fiscal period
        /// </summary>
        public virtual int FiscalPeriodId { get; set; }

        /// <summary>
        /// Gets or sets the FiscalPeriod instance
        /// </summary>
        public virtual FiscalPeriod FiscalPeriod { get; set; }
    }
}
