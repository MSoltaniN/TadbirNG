using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Persistence.Mapping;
using SPPC.Tadbir.Persistence.Mapping.Reporting;
using SPPC.Tadbir.Persistence.Seeding;

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
            modelBuilder.ApplyConfiguration(new VersionConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyDbConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());

            modelBuilder.ApplyConfiguration(new SysSubSystemConfiguration());
            modelBuilder.ApplyConfiguration(new SysOperationSourceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionGroupConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new SysOperationSourceConfiguration());
            modelBuilder.ApplyConfiguration(new SysEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysOperationConfiguration());

            modelBuilder.ApplyConfiguration(new SysLogSettingConfiguration());

            modelBuilder.ApplyConfiguration(new LocaleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new ViewConfiguration());
            modelBuilder.ApplyConfiguration(new ColumnConfiguration());
            modelBuilder.ApplyConfiguration(new CommandConfiguration());
            modelBuilder.ApplyConfiguration(new ReportParameterConfiguration());

            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new LocalReportConfiguration());

            ColumnMap.BuildMapping(modelBuilder.Entity<Column>());
            CommandMap.BuildMapping(modelBuilder.Entity<Command>());
            CompanyDbMap.BuildMapping(modelBuilder.Entity<CompanyDb>());
            EntityTypeMap.BuildMapping(modelBuilder.Entity<EntityType>());
            LocaleMap.BuildMapping(modelBuilder.Entity<Locale>());
            LocalReportMap.BuildMapping(modelBuilder.Entity<LocalReport>());
            OperationMap.BuildMapping(modelBuilder.Entity<Operation>());
            OperationSourceMap.BuildMapping(modelBuilder.Entity<OperationSource>());
            OperationSourceTypeMap.BuildMapping(modelBuilder.Entity<OperationSourceType>());
            ParameterMap.BuildMapping(modelBuilder.Entity<Parameter>());
            PermissionMap.BuildMapping(modelBuilder.Entity<Permission>());
            PermissionGroupMap.BuildMapping(modelBuilder.Entity<PermissionGroup>());
            PersonMap.BuildMapping(modelBuilder.Entity<Person>());
            ReportMap.BuildMapping(modelBuilder.Entity<Report>());
            ReportViewMap.BuildMapping(modelBuilder.Entity<ReportView>());
            RoleMap.BuildMapping(modelBuilder.Entity<Role>());
            RoleCompanyMap.BuildMapping(modelBuilder.Entity<RoleCompany>());
            RolePermissionMap.BuildMapping(modelBuilder.Entity<RolePermission>());
            SessionMap.BuildMapping(modelBuilder.Entity<Session>());
            SettingMap.BuildMapping(modelBuilder.Entity<Setting>());
            ShortcutCommandMap.BuildMapping(modelBuilder.Entity<ShortcutCommand>());
            SubsystemMap.BuildMapping(modelBuilder.Entity<Subsystem>());
            SysLogSettingMap.BuildMapping(modelBuilder.Entity<SysLogSetting>());
            SysOperationLogMap.BuildMapping(modelBuilder.Entity<SysOperationLog>());
            SysOperationLogArchiveMap.BuildMapping(modelBuilder.Entity<SysOperationLogArchive>());
            SystemErrorMap.BuildMapping(modelBuilder.Entity<SystemError>());
            SystemIssueMap.BuildMapping(modelBuilder.Entity<SystemIssue>());
            UserMap.BuildMapping(modelBuilder.Entity<User>());
            UserRoleMap.BuildMapping(modelBuilder.Entity<UserRole>());
            SysUserSettingMap.BuildMapping(modelBuilder.Entity<UserSetting>());
            ValidRowPermissionMap.BuildMapping(modelBuilder.Entity<ValidRowPermission>());
            ViewMap.BuildMapping(modelBuilder.Entity<View>());
            ViewRowPermissionMap.BuildMapping(modelBuilder.Entity<ViewRowPermission>());

            AdjustDBColumnNames(modelBuilder);
        }

        /// <summary>
        /// Code to add a specific character to the column names of all properties
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AdjustDBColumnNames(ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                var properties = entityType.ClrType.GetProperties();

                foreach (var property in properties)
                {
                    if (property.Name.EndsWith("Id") && !property.Name.StartsWith("Id"))
                    {
                        var columnName = property.Name.Replace("Id", "ID");
                        modelBuilder.Entity(entityType.ClrType)
                            .Property(property.Name)
                            .HasColumnName(columnName);
                    }
                }
            }
        }

        /// <summary>
        /// Configures this data context
        /// </summary>
        /// <param name="optionsBuilder">Builder used for configuring data context</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(sqlServerOptions => sqlServerOptions.CommandTimeout(600));
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
            optionsBuilder.UseSqlServer(b => b.MigrationsAssembly("SPPC.Tadbir.Web.Api"));
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
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
