using System;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Persistence.Mapping;
using SPPC.Tadbir.Persistence.Mapping.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Entity Framework (EF) data context class used for managing operations of the system database
    /// in Tadbir application
    /// </summary>
    public partial class SystemContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options for this context</param>
        public SystemContext(DbContextOptions<SystemContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Performs entity mappings required for converting data between object and relational forms
        /// </summary>
        /// <param name="modelBuilder">Builder instance used for mapping definitions</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ColumnMap.BuildMapping(modelBuilder.Entity<Column>());
            CommandMap.BuildMapping(modelBuilder.Entity<Command>());
            CompanyDbMap.BuildMapping(modelBuilder.Entity<CompanyDb>());
            EntityTypeMap.BuildMapping(modelBuilder.Entity<EntityType>());
            LocaleMap.BuildMapping(modelBuilder.Entity<Locale>());
            LocalReportMap.BuildMapping(modelBuilder.Entity<LocalReport>());
            OperationMap.BuildMapping(modelBuilder.Entity<Operation>());
            OperationSourceMap.BuildMapping(modelBuilder.Entity<OperationSource>());
            ParameterMap.BuildMapping(modelBuilder.Entity<Parameter>());
            PermissionMap.BuildMapping(modelBuilder.Entity<Permission>());
            PermissionGroupMap.BuildMapping(modelBuilder.Entity<PermissionGroup>());
            PersonMap.BuildMapping(modelBuilder.Entity<Person>());
            ReportMap.BuildMapping(modelBuilder.Entity<Report>());
            ReportViewMap.BuildMapping(modelBuilder.Entity<ReportView>());
            RoleMap.BuildMapping(modelBuilder.Entity<Role>());
            RoleCompanyMap.BuildMapping(modelBuilder.Entity<RoleCompany>());
            RolePermissionMap.BuildMapping(modelBuilder.Entity<RolePermission>());
            SettingMap.BuildMapping(modelBuilder.Entity<Setting>());
            ShortcutCommandMap.BuildMapping(modelBuilder.Entity<ShortcutCommand>());
            SysLogSettingMap.BuildMapping(modelBuilder.Entity<SysLogSetting>());
            SysOperationLogMap.BuildMapping(modelBuilder.Entity<SysOperationLog>());
            SysOperationLogArchiveMap.BuildMapping(modelBuilder.Entity<SysOperationLogArchive>());
            SystemErrorMap.BuildMapping(modelBuilder.Entity<SystemError>());
            SystemIssueMap.BuildMapping(modelBuilder.Entity<SystemIssue>());
            UserMap.BuildMapping(modelBuilder.Entity<User>());
            UserRoleMap.BuildMapping(modelBuilder.Entity<UserRole>());
            UserSettingMap.BuildMapping(modelBuilder.Entity<UserSetting>());
            ViewMap.BuildMapping(modelBuilder.Entity<View>());
            ViewRowPermissionMap.BuildMapping(modelBuilder.Entity<ViewRowPermission>());
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
