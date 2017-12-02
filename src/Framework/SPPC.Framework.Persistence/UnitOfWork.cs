using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Provides operations required for supporting the Unit of Work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class using an Entity Framework Core
        /// <see cref="DbContext"/> instance
        /// </summary>
        /// <param name="dataContext">A <see cref="DbContext"/> instance used for implementing operations</param>
        public UnitOfWork(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Returns a concrete repository implementation for a specific entity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity whose repository is required</typeparam>
        /// <returns>Repository implementation for entity</returns>
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntity
        {
            var repository = new Repository<TEntity>(_dataContext);
            return repository;
        }

        /// <summary>
        /// Commits all repository operations that are not yet applied, using a database transaction
        /// </summary>
        public void Commit()
        {
            _dataContext.SaveChanges();
        }

        private DbContext _dataContext;
    }
}
