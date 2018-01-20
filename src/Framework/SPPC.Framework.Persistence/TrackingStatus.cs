using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Provides tracking information that overrides default behavior of EF Core change tracker.
    /// </summary>
    public class TrackingStatus<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingStatus&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="cascadeProperties">
        /// Collection of all navigation properties that must be operated on, along with the main entity.
        /// </param>
        public TrackingStatus(ICollection<Expression<Func<TEntity, object>>> cascadeProperties)
        {
            CascadeProperties = cascadeProperties ?? new List<Expression<Func<TEntity, object>>>();
        }

        /// <summary>
        /// Gets or sets the entity instance that is currently being operated on
        /// </summary>
        public TEntity Entity { get; set; }

        /// <summary>
        /// Gets the collection of all navigation properties that must be operated on, along with the main entity.
        /// </summary>
        public ICollection<Expression<Func<TEntity, object>>> CascadeProperties { get; private set; }

        /// <summary>
        /// Gets or sets the EF state value that must be applied to the main entity and all cascading
        /// navigation properties, if any.
        /// </summary>
        public EntityState State { get; set; }
    }
}
