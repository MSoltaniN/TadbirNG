using System;
using Microsoft.EntityFrameworkCore;
using SPPC.CodeChallenge.Model.Core;
using SPPC.CodeChallenge.Model.Metadata;
using SPPC.CodeChallenge.Persistence.Mapping;

namespace SPPC.CodeChallenge.Persistence
{
    /// <summary>
    /// Entity Framework (EF) data context class used for managing operations of a database
    /// in Code Challenge application
    /// </summary>
    public partial class SchoolContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchoolContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options for this context</param>
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Performs entity mappings required for converting data between object and relational forms
        /// </summary>
        /// <param name="modelBuilder">Builder instance used for mapping definitions</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CityMap.BuildMapping(modelBuilder.Entity<City>());
            ProvinceMap.BuildMapping(modelBuilder.Entity<Province>());
            SchoolMap.BuildMapping(modelBuilder.Entity<School>());
        }

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);
            GC.SuppressFinalize(this);

            base.Dispose();
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
                _disposed = true;
            }
        }

        #endregion

        private bool _disposed = false;
    }
}
