using System;
using Microsoft.EntityFrameworkCore;
using SPPC.Licensing.Model;
using SPPC.Licensing.Persistence.Mapping;

namespace SPPC.Licensing.Persistence.Context
{
    /// <summary>
    /// Entity Framework (EF) data context class used for managing operations of the licensing database
    /// in online license server
    /// </summary>
    public partial class LicenseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options for this context</param>
        public LicenseContext(DbContextOptions<LicenseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Performs entity mappings required for converting data between object and relational forms
        /// </summary>
        /// <param name="modelBuilder">Builder instance used for mapping definitions</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CustomerMap.BuildMapping(modelBuilder.Entity<CustomerModel>());
            LicenseMap.BuildMapping(modelBuilder.Entity<LicenseModel>());
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
