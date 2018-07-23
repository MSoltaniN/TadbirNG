﻿using System;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Persistence.Mapping;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Entity Framework (EF) data context class used for managing operations of a company database
    /// in Tadbir application
    /// </summary>
    public partial class TadbirContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TadbirContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options for this context</param>
        public TadbirContext(DbContextOptions<TadbirContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Performs entity mappings required for converting data between object and relational forms
        /// </summary>
        /// <param name="modelBuilder">Builder instance used for mapping definitions</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountMap.BuildMapping(modelBuilder.Entity<Account>());
            AccountDetailAccountMap.BuildMapping(modelBuilder.Entity<AccountDetailAccount>());
            AccountCostCenterMap.BuildMapping(modelBuilder.Entity<AccountCostCenter>());
            AccountProjectMap.BuildMapping(modelBuilder.Entity<AccountProject>());
            BranchMap.BuildMapping(modelBuilder.Entity<Branch>());
            CommandMap.BuildMapping(modelBuilder.Entity<Command>());
            CompanyMap.BuildMapping(modelBuilder.Entity<Company>());
            CostCenterMap.BuildMapping(modelBuilder.Entity<CostCenter>());
            CurrencyMap.BuildMapping(modelBuilder.Entity<Currency>());
            DetailAccountMap.BuildMapping(modelBuilder.Entity<DetailAccount>());
            DocumentMap.BuildMapping(modelBuilder.Entity<Document>());
            DocumentActionMap.BuildMapping(modelBuilder.Entity<DocumentAction>());
            DocumentStatusMap.BuildMapping(modelBuilder.Entity<DocumentStatus>());
            DocumentTypeMap.BuildMapping(modelBuilder.Entity<DocumentType>());
            EntityMap.BuildMapping(modelBuilder.Entity<Entity>());
            FiscalPeriodMap.BuildMapping(modelBuilder.Entity<FiscalPeriod>());
            PermissionMap.BuildMapping(modelBuilder.Entity<Permission>());
            PermissionGroupMap.BuildMapping(modelBuilder.Entity<PermissionGroup>());
            PersonMap.BuildMapping(modelBuilder.Entity<Person>());
            ProjectMap.BuildMapping(modelBuilder.Entity<Project>());
            PropertyMap.BuildMapping(modelBuilder.Entity<Property>());
            RoleMap.BuildMapping(modelBuilder.Entity<Role>());
            RoleBranchMap.BuildMapping(modelBuilder.Entity<RoleBranch>());
            RoleFiscalPeriodMap.BuildMapping(modelBuilder.Entity<RoleFiscalPeriod>());
            RolePermissionMap.BuildMapping(modelBuilder.Entity<RolePermission>());
            SettingMap.BuildMapping(modelBuilder.Entity<Setting>());
            VoucherMap.BuildMapping(modelBuilder.Entity<Voucher>());
            VoucherLineMap.BuildMapping(modelBuilder.Entity<VoucherLine>());
            UserMap.BuildMapping(modelBuilder.Entity<User>());
            UserRoleMap.BuildMapping(modelBuilder.Entity<UserRole>());
            UserSettingMap.BuildMapping(modelBuilder.Entity<UserSetting>());
            ViewRowPermissionMap.BuildMapping(modelBuilder.Entity<ViewRowPermission>());
            WorkItemMap.BuildMapping(modelBuilder.Entity<WorkItem>());
            WorkItemDocumentMap.BuildMapping(modelBuilder.Entity<WorkItemDocument>());
            WorkItemHistoryMap.BuildMapping(modelBuilder.Entity<WorkItemHistory>());
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
