﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Inventory;
using SPPC.Tadbir.Model.Procurement;
using SPPC.Tadbir.Model.Sales;
using SPPC.Tadbir.Model.Warehousing;
using SPPC.Tadbir.Model.Workflow;
using SPPC.Tadbir.Persistence.Mapping;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Entity Framework (EF) data context class used for managing database operations of Tadbir application
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
            BranchMap.BuildMapping(modelBuilder.Entity<Branch>());
            BusinessPartnerMap.BuildMapping(modelBuilder.Entity<BusinessPartner>());
            BusinessUnitMap.BuildMapping(modelBuilder.Entity<BusinessUnit>());
            CompanyMap.BuildMapping(modelBuilder.Entity<Company>());
            CostCenterMap.BuildMapping(modelBuilder.Entity<CostCenter>());
            CurrencyMap.BuildMapping(modelBuilder.Entity<Currency>());
            CustomerMap.BuildMapping(modelBuilder.Entity<Customer>());
            DetailAccountMap.BuildMapping(modelBuilder.Entity<DetailAccount>());
            DocumentMap.BuildMapping(modelBuilder.Entity<Document>());
            DocumentActionMap.BuildMapping(modelBuilder.Entity<DocumentAction>());
            DocumentStatusMap.BuildMapping(modelBuilder.Entity<DocumentStatus>());
            DocumentTypeMap.BuildMapping(modelBuilder.Entity<DocumentType>());
            FiscalPeriodMap.BuildMapping(modelBuilder.Entity<FiscalPeriod>());
            FullAccountMap.BuildMapping(modelBuilder.Entity<FullAccount>());
            FullDetailMap.BuildMapping(modelBuilder.Entity<FullDetail>());
            FullDetailTypeMap.BuildMapping(modelBuilder.Entity<FullDetailType>());
            InvoiceMap.BuildMapping(modelBuilder.Entity<Invoice>());
            InvoiceLineMap.BuildMapping(modelBuilder.Entity<InvoiceLine>());
            IssueReceiptVoucherMap.BuildMapping(modelBuilder.Entity<IssueReceiptVoucher>());
            IssueReceiptVoucherLineMap.BuildMapping(modelBuilder.Entity<IssueReceiptVoucherLine>());
            IssueReceiptVoucherTypeMap.BuildMapping(modelBuilder.Entity<IssueReceiptVoucherType>());
            PermissionMap.BuildMapping(modelBuilder.Entity<Permission>());
            PermissionGroupMap.BuildMapping(modelBuilder.Entity<PermissionGroup>());
            PersonMap.BuildMapping(modelBuilder.Entity<Person>());
            ProductMap.BuildMapping(modelBuilder.Entity<Product>());
            ProductCategoryMap.BuildMapping(modelBuilder.Entity<ProductCategory>());
            ProductInventoryMap.BuildMapping(modelBuilder.Entity<ProductInventory>());
            ProjectMap.BuildMapping(modelBuilder.Entity<Project>());
            RequisitionVoucherMap.BuildMapping(modelBuilder.Entity<RequisitionVoucher>());
            RequisitionVoucherLineMap.BuildMapping(modelBuilder.Entity<RequisitionVoucherLine>());
            RequisitionVoucherTypeMap.BuildMapping(modelBuilder.Entity<RequisitionVoucherType>());
            RoleMap.BuildMapping(modelBuilder.Entity<Role>());
            RoleBranchMap.BuildMapping(modelBuilder.Entity<RoleBranch>());
            RoleFiscalPeriodMap.BuildMapping(modelBuilder.Entity<RoleFiscalPeriod>());
            RolePermissionMap.BuildMapping(modelBuilder.Entity<RolePermission>());
            ServiceJobMap.BuildMapping(modelBuilder.Entity<ServiceJob>());
            TransactionMap.BuildMapping(modelBuilder.Entity<Transaction>());
            TransactionLineMap.BuildMapping(modelBuilder.Entity<TransactionLine>());
            UnitOfMeasurementMap.BuildMapping(modelBuilder.Entity<UnitOfMeasurement>());
            UserMap.BuildMapping(modelBuilder.Entity<User>());
            UserRoleMap.BuildMapping(modelBuilder.Entity<UserRole>());
            WarehouseMap.BuildMapping(modelBuilder.Entity<Warehouse>());
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
                _loggerFactory.Dispose();
                _disposed = true;
            }
        }

        #endregion

        private readonly LoggerFactory _loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        private bool _disposed = false;
    }
}
