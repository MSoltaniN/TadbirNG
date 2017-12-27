using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Framework.Domain;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Defines asynchronous operations required for reading and manipulating data in a permanent data store.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that can be handled</typeparam>
    public interface IAsyncRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Asynchronously retrieves complete information for all existing entities in data store
        /// </summary>
        /// <returns>Collection of all existing entities</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        Task<IList<TEntity>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves complete information for all existing entities in data store,
        /// including specified navigation properties, if any.
        /// </summary>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Collection of all existing entities</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Asynchronously retrieves a single entity instance with the specified unique identifier
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        Task<TEntity> GetByIDAsync(int id);

        /// <summary>
        /// Asynchronously retrieves a single entity instance with the specified unique identifier,
        /// including specified navigation properties, if any.
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        Task<TEntity> GetByIDAsync(int id, params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Asynchronously retrieves complete information for a subset of existing entities,
        /// as defined by the specified criteria
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns>Filtered collection of existing entities</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        Task<IList<TEntity>> GetByCriteriaAsync(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Asynchronously retrieves complete information for a subset of existing entities, as defined by
        /// the specified criteria, including specified navigation properties, if any.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns></returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        Task<IList<TEntity>> GetByCriteriaAsync(
            Expression<Func<TEntity, bool>> criteria,
            params Expression<Func<TEntity, object>>[] relatedProperties);

        /// <summary>
        /// Asynchronously retrieves a single entity instance with the specified row identifier
        /// </summary>
        /// <param name="rowId">A <see cref="Guid"/> value that uniquely identifies a row of information in data store</param>
        /// <returns>Entity instance having the specified row identifier, if found; otherwise, returns null.</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        Task<TEntity> GetByRowIDAsync(Guid rowId);
    }
}
