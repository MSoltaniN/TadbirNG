using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Provides operations required for reading and manipulating data in a database.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that can be handled</typeparam>
    public class Repository<TEntity> : IDisposable, IRepository<TEntity>, IAsyncRepository<TEntity>
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

        /// <summary>
        /// Returns a queryable object that is initially set to return all data
        /// </summary>
        /// <returns>Queryable object for all data</returns>
        public IQueryable<TEntity> GetAllAsQuery()
        {
            return _dataSet
                .AsNoTracking()
                .AsQueryable();
        }

        /// <summary>
        /// Retrieves complete information for all existing entities in data store
        /// </summary>
        /// <returns>Collection of all existing entities</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        public IList<TEntity> GetAll()
        {
            return _dataSet
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Asynchronously retrieves complete information for all existing entities in data store
        /// </summary>
        /// <returns>Collection of all existing entities</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dataSet.ToListAsync();
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
            var query = GetEntityQuery(relatedProperties);
            return query.ToList();
        }

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
            var query = GetEntityQuery(relatedProperties);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves a single entity instance with the specified unique identifier
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        public TEntity GetByID(int id)
        {
            return _dataSet.Find(id);
        }

        /// <summary>
        /// Asynchronously retrieves a single entity instance with the specified unique identifier
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        public async Task<TEntity> GetByIDAsync(int id)
        {
            return await _dataSet.FindAsync(id);
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
            var query = GetEntityQuery(id, relatedProperties);
            return query.SingleOrDefault();
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
            var query = GetEntityQuery(id, relatedProperties);
            return await query.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves complete information for a subset of existing entities, as defined by the specified criteria
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns>Filtered collection of existing entities</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        public IList<TEntity> GetByCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            var list = _dataSet.Where(criteria)
                .ToList();
            return list;
        }

        /// <summary>
        /// Asynchronously retrieves complete information for a subset of existing entities,
        /// as defined by the specified criteria
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns>Filtered collection of existing entities</returns>
        /// <remarks>Use this method when the entity does not have any navigation properties, or you don't need
        /// to retrieve them via additional JOIN statements.</remarks>
        public async Task<IList<TEntity>> GetByCriteriaAsync(Expression<Func<TEntity, bool>> criteria)
        {
            var list = await _dataSet.Where(criteria)
                .ToListAsync();
            return list;
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
            var query = GetEntityQuery(criteria, relatedProperties);
            return query.ToList();
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
            var query = GetEntityQuery(criteria, relatedProperties);
            return await query.ToListAsync();
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
        /// Inserts a single entity instance into the data store
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        public void Insert(TEntity entity)
        {
            _dataContext.Attach(entity);
            _dataSet.Add(entity);
        }

        /// <summary>
        /// Updates an existing entity instance in the data store
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public void Update(TEntity entity)
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

        private IQueryable<TEntity> GetEntityQuery(params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet.AsQueryable();
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            return query;
        }

        private IQueryable<TEntity> GetEntityQuery(int id, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet.Where(e => e.Id == id);
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            return query;
        }

        private IQueryable<TEntity> GetEntityQuery(
            Expression<Func<TEntity, bool>> criteria,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet.Where(criteria);
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            return query;
        }

        private DbContext _dataContext;
        private bool _disposed = false;
        private DbSet<TEntity> _dataSet;
    }
}
