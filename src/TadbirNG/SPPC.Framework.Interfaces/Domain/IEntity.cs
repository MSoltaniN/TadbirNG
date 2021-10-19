using System;
using System.Collections.Generic;

namespace SPPC.Framework.Domain
{
    /// <summary>
    /// Defines members required for identifying and tracking a business entity that is persisted to a database.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for a business entity, suitable for being mapped to an auto-generated
        /// integer field.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for a business entity, suitable for being mapped to an auto-generated
        /// globally unique identifier (GUID) field.
        /// </summary>
        Guid RowGuid { get; set; }

        /// <summary>
        /// Gets or sets the last modification date for a business entity.
        /// </summary>
        DateTime ModifiedDate { get; set; }
    }
}
