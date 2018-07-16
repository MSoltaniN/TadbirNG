using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SPPC.Framework.Domain;
using SPPC.Framework.Presentation;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Provides operations required for reading and manipulating data in a database.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that can be handled</typeparam>
    public class Repository<TEntity> : IDisposable, IAsyncRepository<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> using an Entity Framework Core
        /// <see cref="DbContext"/> object.
        /// </summary>
        /// <param name="dataContext">A <see cref="DbContext"/> instance used for implementing operations</param>
        public Repository(DbContext dataContext)
        {
            _dataContext = dataContext;
            _dataSet = dataContext.Set<TEntity>();
        }

        #region Asynchronous Methods

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
        public async Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(null, relatedProperties);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves complete information for all existing entities in data store,
        /// including specified navigation properties, if any.
        /// </summary>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records (can be null)
        /// </param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Collection of all existing entities</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        public async Task<IList<TEntity>> GetAllAsync(
            GridOptions gridOptions, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(gridOptions, relatedProperties);
            return await query.ToListAsync();
        }

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
        public async Task<TEntity> GetByIDAsync(int id, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(id, relatedProperties);
            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a single entity instance with the specified unique identifier, including specified
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
        public async Task<TEntity> GetByIDWithTrackingAsync(
            int id, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet
                .AsQueryable()
                .Where(e => e.Id == id);
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            return await query.SingleOrDefaultAsync();
        }

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
        public async Task<IList<TEntity>> GetByCriteriaAsync(
            Expression<Func<TEntity, bool>> criteria,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(criteria, null, relatedProperties);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves complete information for a subset of existing entities, as defined by
        /// the specified criteria, including specified navigation properties, if any.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records (can be null)
        /// </param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns></returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        public async Task<IList<TEntity>> GetByCriteriaAsync(
            Expression<Func<TEntity, bool>> criteria,
            GridOptions gridOptions,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(criteria, gridOptions, relatedProperties);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves record count for a subset of existing entities, as defined by
        /// the specified criteria.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records (can be null)
        /// </param>
        /// <returns></returns>
        public async Task<int> GetCountByCriteriaAsync(Expression<Func<TEntity, bool>> criteria, GridOptions gridOptions)
        {
            var query = GetCountByCriteriaQuery(criteria, gridOptions);
            return await query.CountAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a single entity instance with the specified row identifier
        /// </summary>
        /// <param name="rowId">A <see cref="Guid"/> value that uniquely identifies a row of information in data store</param>
        /// <returns>Entity instance having the specified row identifier, if found; otherwise, returns null.</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        public async Task<TEntity> GetByRowIDAsync(Guid rowId)
        {
            var entity = await _dataSet
                .Where(item => item.RowGuid == rowId)
                .SingleOrDefaultAsync();
            return entity;
        }

        #endregion

        #region Synchronous Methods

        /// <summary>
        /// Returns a queryable object for entity that can be further manipulated to include related properties
        /// and perform other standard LINQ functions. This method is suitable for read-only operations, as it
        /// disables EF Core tracking mechanism.
        /// </summary>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records</param>
        /// <returns>Queryable object for entity</returns>
        public IQueryable<TEntity> GetEntityQuery(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var query = _dataSet.AsNoTracking();
            if (options.Filter != null)
            {
                query = query.Where(options.Filter.ToString());
            }

            if (options.SortColumns.Count > 0)
            {
                string ordering = String.Join(", ", options.SortColumns.Select(col => col.ToString()));
                query = query.OrderBy(ordering);
            }

            return query;
        }

        /// <summary>
        /// Returns a queryable object for entity that can be further manipulated to include related properties
        /// and perform other standard LINQ functions.
        /// </summary>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records</param>
        /// <returns>Queryable object for entity</returns>
        public IQueryable<TEntity> GetEntityWithTrackingQuery(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var query = _dataSet.AsQueryable();
            if (options.Filter != null)
            {
                query = query.Where(options.Filter.ToString());
            }

            if (options.SortColumns.Count > 0)
            {
                string ordering = String.Join(", ", options.SortColumns.Select(col => col.ToString()));
                query = query.OrderBy(ordering);
            }

            return query;
        }

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
        public IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(null, relatedProperties);
            return query.ToList();
        }

        /// <summary>
        /// Retrieves complete information for all existing entities in data store, including specified
        /// navigation properties, if any.
        /// </summary>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records (can be null)
        /// </param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Collection of all existing entities</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        public IList<TEntity> GetAll(
            GridOptions gridOptions, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(gridOptions, relatedProperties);
            return query.ToList();
        }

        /// <summary>
        /// Retrieves a single entity instance with the specified unique identifier, including specified
        /// navigation properties, if any.
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <param name="relatedProperties">Variable array of expressions the specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        public TEntity GetByID(int id, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(id, relatedProperties);
            return query.SingleOrDefault();
        }

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
        public TEntity GetByIDWithTracking(int id, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet
                .AsQueryable()
                .Where(e => e.Id == id);
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            return query.SingleOrDefault();
        }

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
        public IList<TEntity> GetByCriteria(
            Expression<Func<TEntity, bool>> criteria,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(criteria, null, relatedProperties);
            return query.ToList();
        }

        /// <summary>
        /// Retrieves complete information for a subset of existing entities, as defined by the specified criteria,
        /// including specified navigation properties, if any.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records (can be null)
        /// </param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns></returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        public IList<TEntity> GetByCriteria(
            Expression<Func<TEntity, bool>> criteria,
            GridOptions gridOptions,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityWithNavigationQuery(criteria, gridOptions, relatedProperties);
            return query.ToList();
        }

        /// <summary>
        /// Retrieves record count for a subset of existing entities, as defined by the specified criteria.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <param name="gridOptions">Options used for filtering, sorting and paging retrieved records (can be null)
        /// </param>
        /// <returns>Record count for filtered items</returns>
        public int GetCountByCriteria(Expression<Func<TEntity, bool>> criteria, GridOptions gridOptions)
        {
            var query = GetCountByCriteriaQuery(criteria, gridOptions);
            return query.Count();
        }

        /// <summary>
        /// Retrieves a single entity instance with the specified row identifier
        /// </summary>
        /// <param name="rowId">A <see cref="Guid"/> value that uniquely identifies a row of information in data store</param>
        /// <returns>Entity instance having the specified row identifier, if found; otherwise, returns null.</returns>
        public TEntity GetByRowID(Guid rowId)
        {
            var entity = _dataSet
                .Where(item => item.RowGuid == rowId)
                .SingleOrDefault();
            return entity;
        }

        /// <summary>
        /// Inserts a single entity instance into the data store
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <param name="cascadeProperties">
        /// Collection of all navigation properties that must be saved along with the main entity.
        /// When not used, only the main entity will be inserted.
        /// </param>
        public void Insert(TEntity entity, params Expression<Func<TEntity, object>>[] cascadeProperties)
        {
            _trackingStatus = new TrackingStatus<TEntity>(cascadeProperties)
            {
                Entity = entity,
                State = EntityState.Added
            };
            _dataContext.ChangeTracker.TrackGraph(entity, SetTrackingStatus);
            _dataSet.Add(entity);
        }

        /// <summary>
        /// Updates an existing entity instance in the data store
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="cascadeProperties">
        /// Collection of all navigation properties that must be saved along with the main entity.
        /// When not used, only the main entity will be updated.
        /// </param>
        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] cascadeProperties)
        {
            _trackingStatus = new TrackingStatus<TEntity>(cascadeProperties)
            {
                Entity = entity,
                State = EntityState.Modified
            };
            _dataContext.ChangeTracker.TrackGraph(entity, SetTrackingStatus);
            _dataSet.Update(entity);
        }

        /// <summary>
        /// Updates an existing entity instance in the data store, using EF auto-tracking mechanism
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public void UpdateWithTracking(TEntity entity)
        {
            _dataContext.Attach(entity);
            _dataSet.Update(entity);
        }

        /// <summary>
        /// Deletes an existing entity instance from the data store
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(TEntity entity)
        {
            _dataSet.Remove(entity);
        }

        #endregion

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Supports correct implementation of the Disposable pattern for this class.
        /// </summary>
        /// <param name="disposing">Indicates if this instance is currently being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dataContext.Dispose();
                _disposed = true;
            }
        }

        #endregion

        private static IQueryable<TEntity> GetEntityWithNavigationQuery(
            IQueryable<TEntity> query,
            GridOptions gridOptions,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            var resultQuery = (gridOptions != null)
                ? query
                    .Skip((gridOptions.Paging.PageIndex - 1) * gridOptions.Paging.PageSize)
                    .Take(gridOptions.Paging.PageSize)
                : query;
            return resultQuery;
        }

        private IQueryable<TEntity> GetEntityWithNavigationQuery(GridOptions gridOptions,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityQuery(gridOptions);
            return GetEntityWithNavigationQuery(query, gridOptions, relatedProperties);
        }

        private IQueryable<TEntity> GetEntityWithNavigationQuery(
            int id, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityQuery()
                .Where(e => e.Id == id);
            return GetEntityWithNavigationQuery(query, null, relatedProperties);
        }

        private IQueryable<TEntity> GetEntityWithNavigationQuery(
            Expression<Func<TEntity, bool>> criteria,
            GridOptions gridOptions,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = GetEntityQuery(gridOptions)
                .Where(criteria);
            return GetEntityWithNavigationQuery(query, gridOptions, relatedProperties);
        }

        private void SetTrackingStatus(EntityEntryGraphNode entity)
        {
            bool mustSave = Object.ReferenceEquals(entity.Entry.Entity, _trackingStatus.Entity);
            foreach (var selector in _trackingStatus.CascadeProperties)
            {
                var cascadedProperty = selector.Compile().Invoke(_trackingStatus.Entity);
                var asEnumerable = cascadedProperty as IEnumerable;
                if (asEnumerable != null)
                {
                    foreach (var item in asEnumerable)
                    {
                        mustSave = mustSave || Object.ReferenceEquals(entity.Entry.Entity, item);
                    }
                }
                else
                {
                    mustSave = mustSave || Object.ReferenceEquals(entity.Entry.Entity, cascadedProperty);
                }
            }

            entity.Entry.State = mustSave
                ? _trackingStatus.State
                : EntityState.Detached;
        }

        private IQueryable<TEntity> GetCountByCriteriaQuery(
            Expression<Func<TEntity, bool>> criteria, GridOptions gridOptions)
        {
            var options = gridOptions ?? new GridOptions();
            var currentCriteria = criteria ?? (entity => true);
            var query = _dataSet
                .AsNoTracking()
                .Where(currentCriteria);
            if (options.Filter != null)
            {
                query = query.Where(options.Filter.ToString());
            }

            return query;
        }

        private DbContext _dataContext;
        private bool _disposed = false;
        private DbSet<TEntity> _dataSet;
        private TrackingStatus<TEntity> _trackingStatus;
    }
}
