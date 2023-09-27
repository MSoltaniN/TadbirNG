using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.ProductScope;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Persistence.Mapping;
using SPPC.Tadbir.Persistence.Mapping.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// Entity Framework (EF) data context class used for managing operations of a company database
    /// in Tadbir application
    /// </summary>
    public partial class TadbirContext : DbContext
    {
        /// <summary>
        /// Gets or sets the connection string currently set in this data context
        /// </summary>
        public string ConnectionString { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        /// <summary>
        /// Performs entity mappings required for converting data between object and relational forms
        /// </summary>
        /// <param name="modelBuilder">Builder instance used for mapping definitions</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountMap.BuildMapping(modelBuilder.Entity<Account>());
            AccountCollectionMap.BuildMapping(modelBuilder.Entity<AccountCollection>());
            AccountCollectionAccountMap.BuildMapping(modelBuilder.Entity<AccountCollectionAccount>());
            AccountCollectionCategoryMap.BuildMapping(modelBuilder.Entity<AccountCollectionCategory>());
            AccountCurrencyMap.BuildMapping(modelBuilder.Entity<AccountCurrency>());
            AccountGroupMap.BuildMapping(modelBuilder.Entity<AccountGroup>());
            AccountDetailAccountMap.BuildMapping(modelBuilder.Entity<AccountDetailAccount>());
            AccountCostCenterMap.BuildMapping(modelBuilder.Entity<AccountCostCenter>());
            AccountProjectMap.BuildMapping(modelBuilder.Entity<AccountProject>());
            AccountOwnerMap.BuildMapping(modelBuilder.Entity<AccountOwner>());
            AccountHolderMap.BuildMapping(modelBuilder.Entity<AccountHolder>());
            BranchMap.BuildMapping(modelBuilder.Entity<Branch>());
            CashRegisterMap.BuildMapping(modelBuilder.Entity<CashRegister>());
            CheckBookMap.BuildMapping(modelBuilder.Entity<CheckBook>());
            CheckBookPageMap.BuildMapping(modelBuilder.Entity<CheckBookPage>());
            CityMap.BuildMapping(modelBuilder.Entity<City>());
            CommandMap.BuildMapping(modelBuilder.Entity<Command>());
            CostCenterMap.BuildMapping(modelBuilder.Entity<CostCenter>());
            CurrencyMap.BuildMapping(modelBuilder.Entity<Currency>());
            CurrencyRateMap.BuildMapping(modelBuilder.Entity<CurrencyRate>());
            CustomerTaxInfoMap.BuildMapping(modelBuilder.Entity<CustomerTaxInfo>());
            CustomFormMap.BuildMapping(modelBuilder.Entity<CustomForm>());
            DashboardMap.BuildMapping(modelBuilder.Entity<Dashboard>());
            DashboardTabMap.BuildMapping(modelBuilder.Entity<DashboardTab>());
            DetailAccountMap.BuildMapping(modelBuilder.Entity<DetailAccount>());
            DocumentStatusMap.BuildMapping(modelBuilder.Entity<DocumentStatus>());
            DocumentTypeMap.BuildMapping(modelBuilder.Entity<DocumentType>());
            EntityTypeMap.BuildMapping(modelBuilder.Entity<EntityType>());
            FilterMap.BuildMapping(modelBuilder.Entity<Filter>());
            FiscalPeriodMap.BuildMapping(modelBuilder.Entity<FiscalPeriod>());
            FunctionParameterMap.BuildMapping(modelBuilder.Entity<FunctionParameter>());
            InactiveEntityMap.BuildMapping(modelBuilder.Entity<InactiveEntity>());
            LabelSettingMap.BuildMapping(modelBuilder.Entity<LabelSetting>());
            LogSettingMap.BuildMapping(modelBuilder.Entity<LogSetting>());
            OperationMap.BuildMapping(modelBuilder.Entity<Operation>());
            OperationLogMap.BuildMapping(modelBuilder.Entity<OperationLog>());
            OperationLogArchiveMap.BuildMapping(modelBuilder.Entity<OperationLogArchive>());
            OperationSourceMap.BuildMapping(modelBuilder.Entity<OperationSource>());
            OperationSourceListMap.BuildMapping(modelBuilder.Entity<OperationSourceList>());
            OperationSourceTypeMap.BuildMapping(modelBuilder.Entity<OperationSourceType>());
            PayReceiveMap.BuildMapping(modelBuilder.Entity<PayReceive>());
            PayReceiveAccountMap.BuildMapping(modelBuilder.Entity<PayReceiveAccount>());
            PayReceiveCashAccountMap.BuildMapping(modelBuilder.Entity<PayReceiveCashAccount>());
            PayReceiveVoucherLineMap.BuildMapping(modelBuilder.Entity<PayReceiveVoucherLine>());
            PermissionMap.BuildMapping(modelBuilder.Entity<Permission>());
            PermissionGroupMap.BuildMapping(modelBuilder.Entity<PermissionGroup>());
            PersonMap.BuildMapping(modelBuilder.Entity<Person>());
            ProjectMap.BuildMapping(modelBuilder.Entity<Project>());
            ProvinceMap.BuildMapping(modelBuilder.Entity<Province>());
            RoleMap.BuildMapping(modelBuilder.Entity<Role>());
            RoleBranchMap.BuildMapping(modelBuilder.Entity<RoleBranch>());
            RoleFiscalPeriodMap.BuildMapping(modelBuilder.Entity<RoleFiscalPeriod>());
            RolePermissionMap.BuildMapping(modelBuilder.Entity<RolePermission>());
            RoleWidgetMap.BuildMapping(modelBuilder.Entity<RoleWidget>());
            SettingMap.BuildMapping(modelBuilder.Entity<Setting>());
            SourceAppMap.BuildMapping(modelBuilder.Entity<SourceApp>());
            SubsystemMap.BuildMapping(modelBuilder.Entity<Subsystem>());
            TabWidgetMap.BuildMapping(modelBuilder.Entity<TabWidget>());
            TaxCurrencyMap.BuildMapping(modelBuilder.Entity<TaxCurrency>());
            UsedParameterMap.BuildMapping(modelBuilder.Entity<UsedParameter>());
            UserCashRegisterMap.BuildMapping(modelBuilder.Entity<UserCashRegister>());
            UserMap.BuildMapping(modelBuilder.Entity<User>());
            UserRoleMap.BuildMapping(modelBuilder.Entity<UserRole>());
            UserSettingMap.BuildMapping(modelBuilder.Entity<UserSetting>());
            UserValueCategoryMap.BuildMapping(modelBuilder.Entity<UserValueCategory>());
            UserValueMap.BuildMapping(modelBuilder.Entity<UserValue>());
            ViewRowPermissionMap.BuildMapping(modelBuilder.Entity<ViewRowPermission>());
            ViewSettingMap.BuildMapping(modelBuilder.Entity<ViewSetting>());
            VoucherMap.BuildMapping(modelBuilder.Entity<Voucher>());
            VoucherLineMap.BuildMapping(modelBuilder.Entity<VoucherLine>());
            VoucherOriginMap.BuildMapping(modelBuilder.Entity<VoucherOrigin>());
            WidgetAccountMap.BuildMapping(modelBuilder.Entity<WidgetAccount>());
            WidgetFunctionMap.BuildMapping(modelBuilder.Entity<WidgetFunction>());
            WidgetMap.BuildMapping(modelBuilder.Entity<Widget>());
            WidgetTypeMap.BuildMapping(modelBuilder.Entity<WidgetType>());
            BrandMap.BuildMapping(modelBuilder.Entity<Brand>());
            UnitMap.BuildMapping(modelBuilder.Entity<Unit>());
        }

        /// <summary>
        /// Configures this data context
        /// </summary>
        /// <param name="optionsBuilder">Builder used for configuring data context</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(600));
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
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
