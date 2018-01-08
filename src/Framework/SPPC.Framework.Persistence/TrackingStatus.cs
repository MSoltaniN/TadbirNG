using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Provides tracking information that overrides default behavior of EF Core change tracker.
    /// </summary>
    public class TrackingStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingStatus"/> class.
        /// </summary>
        /// <param name="selectors">
        /// Collection of all navigation properties that must be operated on, along with the main entity.
        /// </param>
        public TrackingStatus(ICollection<Expression<Func<object, object>>> selectors)
        {
            Selectors = selectors;
        }

        /// <summary>
        /// Gets or sets the entity instance that is currently being operated on
        /// </summary>
        public object Entity { get; set; }

        /// <summary>
        /// Gets the collection of all navigation properties that must be operated on, along with the main entity.
        /// </summary>
        public ICollection<Expression<Func<object, object>>> Selectors { get; private set; }

        /// <summary>
        /// Gets or sets the EF state value that must be applied to the main entity and all cascading
        /// navigation properties, if any.
        /// </summary>
        public EntityState State { get; set; }
    }
}
