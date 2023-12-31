﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SPPC.Framework.Domain;
using SPPC.Framework.Presentation;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Defines operations required for reading and manipulating data in a permanent data store.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that can be handled</typeparam>
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Returns a queryable object for entity that can be further manipulated to include related properties
        /// and perform other standard LINQ functions. This method is suitable for read-only operations, as it
        /// disables EF Core tracking mechanism.
        /// </summary>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Queryable object for entity</returns>
        IQueryable<TEntity> GetEntityQuery(
            params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Returns a queryable object for entity that can be further manipulated to include related properties
        /// and perform other standard LINQ functions. This method is suitable for read/write operations, as it
        /// enables default EF Core tracking mechanism.
        /// </summary>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Queryable object for entity</returns>
        IQueryable<TEntity> GetEntityWithTrackingQuery(
            params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Retrieves complete information for all existing entities in data store, including specified
        /// navigation properties, if any.
        /// </summary>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Collection of all existing entities</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Retrieves a single entity instance with the specified unique identifier, including specified
        /// navigation properties, if any.
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        TEntity GetByID(int id, params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Retrieves a single entity instance with the specified unique identifier, including specified
        /// navigation properties, if any. This overload is suitable for scenarios when you want to change
        /// retrieved entity.
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        TEntity GetByIDWithTracking(int id, params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Retrieves complete information for a subset of existing entities, as defined by the specified criteria,
        /// including specified navigation properties, if any.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns></returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        IList<TEntity> GetByCriteria(
            Expression<Func<TEntity, bool>> criteria,
            params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Retrieves record count for a subset of existing entities, as defined by the specified criteria.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns>Record count for filtered items</returns>
        int GetCountByCriteria(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Retrieves a single entity instance with the specified row identifier
        /// </summary>
        /// <param name="rowId">A <see cref="Guid"/> value that uniquely identifies a row of information in data store</param>
        /// <returns>Entity instance having the specified row identifier, if found; otherwise, returns null.</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        TEntity GetByRowID(Guid rowId);

        /// <summary>
        /// Loads a navigation property into a tracked entity instance (Needs tracking)
        /// </summary>
        /// <typeparam name="TReference">Type of navigation property</typeparam>
        /// <param name="entity">Entity instance whose property must be loaded</param>
        /// <param name="reference">Lambda expression that specifies the navigation property</param>
        void LoadReference<TReference>(
            TEntity entity, Expression<Func<TEntity, TReference>> reference)
            where TReference : class;

        /// <summary>
        /// Loads a navigation collection into a tracked entity instance (Needs tracking)
        /// </summary>
        /// <typeparam name="TProperty">Type of items in navigation collection</typeparam>
        /// <param name="entity">Entity instance whose collection must be loaded</param>
        /// <param name="collection">Lambda expression that specifies the navigation collection</param>
        void LoadCollection<TProperty>(
            TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collection)
            where TProperty : class;

        /// <summary>
        /// Inserts a single entity instance into the data store
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <param name="cascadeProperties">
        /// Collection of all navigation properties that must be saved along with the main entity.
        /// When not used, only the main entity will be inserted.
        /// </param>
        void Insert(TEntity entity, params Expression<Func<TEntity, object>>[] cascadeProperties);

        /// <summary>
        /// Updates an existing entity instance in the data store
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="cascadeProperties">
        /// Collection of all navigation properties that must be saved along with the main entity.
        /// When not used, only the main entity will be updated.
        /// </param>
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] cascadeProperties);

        /// <summary>
        /// Updates an existing entity instance in the data store, using default ORM tracking mechanism
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void UpdateWithTracking(TEntity entity);

        /// <summary>
        /// Deletes an existing entity instance from the data store
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Directly executes a SQL command on the database represented by current DbContext
        /// </summary>
        /// <param name="command">SQL command to execute (syntax is checked by database provider)</param>
        void ExecuteCommand(string command);
    }
}
