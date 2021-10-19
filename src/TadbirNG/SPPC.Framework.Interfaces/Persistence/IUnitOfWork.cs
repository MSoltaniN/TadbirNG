using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Domain;

namespace SPPC.Framework.Persistence
{
    /// <summary>
    /// Defines operations required for supporting the Unit of Work pattern
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Returns a concrete repository implementation for a specific entity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity whose repository is required</typeparam>
        /// <returns>Repository implementation for entity</returns>
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntity;

        /// <summary>
        /// Returns a concrete asynchronous repository implementation for a specific entity
        /// </summary>
        /// <typeparam name="TEntity">Type of entity whose repository is required</typeparam>
        /// <returns>Asynchronous repository implementation for entity</returns>
        IAsyncRepository<TEntity> GetAsyncRepository<TEntity>()
            where TEntity : class, IEntity;

        /// <summary>
        /// Commits all repository operations that are not yet applied, using a database transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Asynchronously commits all repository operations that are not yet applied, using a database transaction
        /// </summary>
        Task CommitAsync();
    }
}
