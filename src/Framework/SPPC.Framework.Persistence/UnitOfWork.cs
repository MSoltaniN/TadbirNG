using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Domain;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Provides operations required for supporting the Unit of Work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
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
        /// Returns a concrete asynchronous repository implementation for a specific entity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity whose repository is required</typeparam>
        /// <returns>Asynchronous repository implementation for entity</returns>
        public IAsyncRepository<TEntity> GetAsyncRepository<TEntity>()
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

        /// <summary>
        /// Asynchronously commits all repository operations that are not yet applied, using a database transaction
        /// </summary>
        public async Task CommitAsync()
        {
            await _dataContext.SaveChangesAsync();
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

        private DbContext _dataContext;
        private bool _disposed = false;
    }
}
