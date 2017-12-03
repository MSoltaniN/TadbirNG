using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Provides operations required for reading and manipulating data in a database.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity that can be handled</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
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
        /// Retrieves complete information for all existing entities in data store
        /// </summary>
        /// <returns>Collection of all existing entities</returns>
        public IList<TEntity> GetAll()
        {
            return _dataSet.ToList();
        }

        /// <summary>
        /// Retrieves a single entity instance with the specified unique identifier
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        public TEntity GetByID(int id)
        {
            return _dataSet.Find(id);
        }

        /// <summary>
        /// Retrieves a single entity instance with the specified unique identifier, including specified
        /// navigation properties, if any.
        /// </summary>
        /// <param name="id">Identifier of an existing entity</param>
        /// <param name="relatedProperties">Variable array of expressions the specify navigation
        /// properties that must be loaded in the main entity</param>
        /// <returns>Entity instance having the specified identifier</returns>
        public TEntity GetByID(int id, params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet.Where(e => e.Id == id);
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            return query.SingleOrDefault();
        }

        /// <summary>
        /// Retrieves complete information for a subset of existing entities, as defined by the specified criteria
        /// </summary>
        /// <param name="criteria">Expression that defines criteria for filtering existing instances</param>
        /// <returns>Filtered collection of existing entities</returns>
        public IList<TEntity> GetByCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            var list = _dataSet.Where(criteria)
                .ToList();
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
        public IList<TEntity> GetByCriteria(Expression<Func<TEntity, bool>> criteria,
            params Expression<Func<TEntity, object>>[] relatedProperties)
        {
            var query = _dataSet.Where(criteria);
            foreach (var property in relatedProperties)
            {
                query = query.Include(property);
            }

            return query.ToList();
        }

        /// <summary>
        /// Retrieves a single entity instance with the specified row identifier
        /// </summary>
        /// <param name="rowId">A <see cref="Guid"/> value that uniquely identifies a row of information in data store</param>
        /// <returns></returns>
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

        private DbContext _dataContext;
        private DbSet<TEntity> _dataSet;
    }
}
