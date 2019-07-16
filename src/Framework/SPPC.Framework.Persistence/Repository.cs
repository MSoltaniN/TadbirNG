using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;

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
            return await GetEntityQuery(relatedProperties)
                .ToListAsync();
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
            return await GetEntityQuery(relatedProperties)
                .Where(e => e.Id == id)
                .SingleOrDefaultAsync();
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
            return await GetEntityWithTrackingQuery(relatedProperties)
                .Where(e => e.Id == id)
                .SingleOrDefaultAsync();
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
            return await GetEntityQuery(relatedProperties)
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves complete information for a subset of existing entities, as defined by
        /// the specified criteria.
        /// </summary>
        /// <param name="queryable">A queryable to use as the main source for output records</param>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns>Filtered list of entities</returns>
        public async Task<IList<TEntity>> GetByCriteriaAsync(
            IQueryable<TEntity> queryable,
            Expression<Func<TEntity, bool>> criteria)
        {
            Verify.ArgumentNotNull(queryable, "queryable");
            return await queryable
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves complete information for the first of existing entities, as defined by
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
        public async Task<TEntity> GetFirstByCriteriaAsync(
            Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            return await GetEntityQuery(relatedProperties)
                .Where(criteria)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Asynchronously retrieves complete information for a single entity, as defined by
        /// the specified criteria, including specified navigation properties, if any.
        /// This method will throw exception when more than one entity meets given criteria.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns></returns>
        /// <remarks>
        /// Use this method when you need to retrieve the entity's navigation properties in a single level
        /// (i.e. no navigation properties inside the main entity's navigation properties are required)
        /// </remarks>
        public async Task<TEntity> GetSingleByCriteriaAsync(
            Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            return await GetEntityQuery(relatedProperties)
                .Where(criteria)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Asynchronously retrieves record count for a subset of existing entities, as defined by
        /// the specified criteria.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns></returns>
        public async Task<int> GetCountByCriteriaAsync(Expression<Func<TEntity, bool>> criteria)
        {
            var query = GetCountByCriteriaQuery(criteria);
            return await query.CountAsync();
        }

        /// <summary>
        /// Asynchronously retrieves record count for a subset of existing entities, as defined by
        /// any configured row access filters and the specified criteria.
        /// </summary>
        /// <param name="queryable">Entity collection to apply other criteria to</param>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns></returns>
        public async Task<int> GetCountByCriteriaAsync(
            IQueryable<TEntity> queryable,
            Expression<Func<TEntity, bool>> criteria)
        {
            Verify.ArgumentNotNull(queryable, "queryable");
            return await queryable
                .Where(criteria)
                .CountAsync();
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

        /// <summary>
        /// Asynchronously, loads a navigation property into a tracked entity instance
        /// </summary>
        /// <typeparam name="TReference">Type of navigation property</typeparam>
        /// <param name="entity">Entity instance whose property must be loaded</param>
        /// <param name="reference">Lambda expression that specifies the navigation property</param>
        public async Task LoadReferenceAsync<TReference>(
            TEntity entity, Expression<Func<TEntity, TReference>> reference)
            where TReference : class
        {
            await _dataContext.Entry(entity)
                .Reference(reference)
                .LoadAsync();
        }

        /// <summary>
        /// Asynchronously, loads a navigation collection into a tracked entity instance
        /// </summary>
        /// <typeparam name="TProperty">Type of items in navigation collection</typeparam>
        /// <param name="entity">Entity instance whose collection must be loaded</param>
        /// <param name="collection">Lambda expression that specifies the navigation collection</param>
        public async Task LoadCollectionAsync<TProperty>(
            TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collection)
            where TProperty : class
        {
            await _dataContext.Entry(entity)
                .Collection(collection)
                .LoadAsync();
        }

        #endregion

        #region Synchronous Methods

        /// <summary>
        /// Returns a queryable object for entity that can be further manipulated to include related properties
        /// and perform other standard LINQ functions. This method is suitable for read-only operations, as it
        /// disables EF Core tracking mechanism.
        /// </summary>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Queryable object for entity</returns>
        public IQueryable<TEntity> GetEntityQuery(
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet.AsNoTracking();
            Array.ForEach(relatedProperties, prop => query = query.Include(prop));
            return query;
        }

        /// <summary>
        /// Returns a queryable object for entity that can be further manipulated to include related properties
        /// and perform other standard LINQ functions. This method is suitable for read/write operations, as it
        /// enables default EF Core tracking mechanism.
        /// </summary>
        /// <param name="relatedProperties">Variable array of expressions that specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Queryable object for entity</returns>
        public IQueryable<TEntity> GetEntityWithTrackingQuery(
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet.AsQueryable();
            Array.ForEach(relatedProperties, prop => query = query.Include(prop));
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
            return GetEntityQuery(relatedProperties)
                .ToList();
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
            return GetEntityQuery(relatedProperties)
                .Where(e => e.Id == id)
                .SingleOrDefault();
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
            return GetEntityWithTrackingQuery(relatedProperties)
                .Where(e => e.Id == id)
                .SingleOrDefault();
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
            return GetEntityQuery(relatedProperties)
                .Where(criteria)
                .ToList();
        }

        /// <summary>
        /// Retrieves record count for a subset of existing entities, as defined by the specified criteria.
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns>Record count for filtered items</returns>
        public int GetCountByCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            var query = GetCountByCriteriaQuery(criteria);
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
        /// Loads a navigation property into a tracked entity instance
        /// </summary>
        /// <typeparam name="TReference">Type of navigation property</typeparam>
        /// <param name="entity">Entity instance whose property must be loaded</param>
        /// <param name="reference">Lambda expression that specifies the navigation property</param>
        public void LoadReference<TReference>(TEntity entity, Expression<Func<TEntity, TReference>> reference)
            where TReference : class
        {
            _dataContext.Entry(entity)
                .Reference(reference)
                .Load();
        }

        /// <summary>
        /// Loads a navigation collection into a tracked entity instance
        /// </summary>
        /// <typeparam name="TProperty">Type of items in navigation collection</typeparam>
        /// <param name="entity">Entity instance whose collection must be loaded</param>
        /// <param name="collection">Lambda expression that specifies the navigation collection</param>
        public void LoadCollection<TProperty>(
            TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collection)
            where TProperty : class
        {
            _dataContext.Entry(entity)
                .Collection(collection)
                .Load();
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

        /// <summary>
        /// Directly executes a SQL command on the database represented by current DbContext
        /// </summary>
        /// <param name="command">SQL command to execute (syntax is checked by database provider)</param>
        public void ExecuteCommand(string command)
        {
            _dataContext.Database.ExecuteSqlCommand(command);
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

        private IQueryable<TEntity> GetCountByCriteriaQuery(Expression<Func<TEntity, bool>> criteria)
        {
            return GetEntityQuery()
                .Where(criteria ?? (entity => true));
        }

        private DbContext _dataContext;
        private bool _disposed = false;
        private DbSet<TEntity> _dataSet;
        private TrackingStatus<TEntity> _trackingStatus;
    }
}
