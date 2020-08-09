using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Persistence.Repository;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Web.Api.Extensions;

namespace SPPC.Tadbir.Web.Api
{
    /// <summary>
    /// کلاس مرکزی برای مدیریت و پیکربندی سرویس ها و پیاده سازی مورد استفاده برای هر کدام
    /// </summary>
    public class TypeContainer
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="services">مجموعه سرویس های مورد استفاده در سرویس وب</param>
        /// <param name="configuration">تنظیمات زیرساختی سرویس وب</param>
        public TypeContainer(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        /// <summary>
        /// کلیه سرویس های داخلی مورد استفاده در سرویس وب را تنظیم و اضافه می کند
        /// </summary>
        public void AddServices()
        {
            AddContextTypes();
            AddPersistenceTypes();
            AddServiceTypes();
            AddUtilityTypes();
        }

        private void AddContextTypes()
        {
            _services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.AddDbContext<TadbirContext>();
            _services.AddDbContext<SystemContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("TadbirSysApi")));
            _services.AddScoped<SystemContext>();
            _services.AddScoped(provider =>
            {
                var crypto = new CryptoService();
                var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
                var securityContext = httpContext.Request.CurrentSecurityContext();
                string connectionString = securityContext?.User.Connection;
                var companyConnection = String.IsNullOrEmpty(connectionString)
                    ? connectionString
                    : crypto.Decrypt(connectionString);
                return new TadbirContext(companyConnection);
            });
            _services.AddTransient<IDbContextAccessor, DbContextAccessor>();
            _services.AddScoped(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
                return httpContext.Request.CurrentSecurityContext()
                    as ISecurityContext;
            });
            _services.AddTransient<IRepositoryContext, RepositoryContext>();
        }

        private void AddPersistenceTypes()
        {
            _services.AddTransient<ISqlConsole>(provider =>
            {
                return new SqlServerConsole()
                {
                    ConnectionString = _configuration.GetConnectionString("TadbirSysApi")
                };
            });

            _services.AddTransient<IAppUnitOfWork, AppUnitOfWork>();
            _services.AddTransient<IAccountRepository, AccountRepository>();
            _services.AddTransient<IDetailAccountRepository, DetailAccountRepository>();
            _services.AddTransient<ICostCenterRepository, CostCenterRepository>();
            _services.AddTransient<IProjectRepository, ProjectRepository>();
            _services.AddTransient<IAccountItemRepository, AccountItemRepository>();
            _services.AddTransient<ILookupRepository, LookupRepository>();
            _services.AddTransient<IUserRepository, UserRepository>();
            _services.AddTransient<IRoleRepository, RoleRepository>();
            _services.AddTransient<IVoucherRepository, VoucherRepository>();
            _services.AddTransient<IVoucherLineRepository, VoucherLineRepository>();
            _services.AddTransient<IFiscalPeriodRepository, FiscalPeriodRepository>();
            _services.AddTransient<IBranchRepository, BranchRepository>();
            _services.AddTransient<ICompanyRepository, CompanyRepository>();
            _services.AddTransient<IRelationRepository, RelationRepository>();
            _services.AddTransient<IMetadataRepository, MetadataRepository>();
            _services.AddTransient<IConfigRepository, ConfigRepository>();
            _services.AddTransient<IOperationLogRepository, OperationLogRepository>();
            _services.AddTransient<ISecureRepository, SecureRepository>();
            _services.AddTransient<IAccountSetRepository, AccountSetRepository>();
            _services.AddTransient<IDashboardRepository, DashboardRepository>();
            _services.AddTransient<IAccountGroupRepository, AccountGroupRepository>();
            _services.AddTransient<IReportRepository, ReportRepository>();
            _services.AddTransient<IReportSystemRepository, ReportSystemRepository>();
            _services.AddTransient<IAccountCollectionRepository, AccountCollectionRepository>();
            _services.AddTransient<IJournalRepository, JournalRepository>();
            _services.AddTransient<IAccountBookRepository, AccountBookRepository>();
            _services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            _services.AddTransient<ICurrencyRateRepository, CurrencyRateRepository>();
            _services.AddTransient<ITestBalanceRepository, TestBalanceRepository>();
            _services.AddTransient<IAccessRepository, AccessRepository>();
            _services.AddTransient<ICurrencyBookRepository, CurrencyBookRepository>();
            _services.AddTransient<ISystemRepository, SystemRepository>();
            _services.AddTransient<ISystemConfigRepository, SystemConfigRepository>();
            _services.AddTransient<ISystemIssueRepository, SystemIssueRepository>();
            _services.AddTransient<IFilterRepository, FilterRepository>();
            _services.AddTransient<IBalanceByAccountRepository, BalanceByAccountRepository>();
            _services.AddTransient<ILogConfigRepository, LogConfigRepository>();
            _services.AddTransient<ICustomerTaxInfoRepository, CustomerTaxInfoRepository>();
            _services.AddTransient<IAccountOwnerRepository, AccountOwnerRepository>();
            _services.AddTransient<IProfitLossRepository, ProfitLossRepository>();
        }

        private void AddServiceTypes()
        {
            _services.AddTransient<ICryptoService, CryptoService>();
            _services.AddTransient<ISecurityContextManager, ServiceContextManager>();
        }

        private void AddUtilityTypes()
        {
            _services.AddTransient<IDomainMapper, DomainMapper>();
            _services.AddTransient<IReportUtility, ReportUtility>();
            _services.AddTransient<IAccountCollectionUtility, AccountCollectionUtility>();
            _services.AddTransient<IAccountItemUtilityFactory, AccountItemUtilityFactory>();
            _services.AddTransient<ITestBalanceUtilityFactory, TestBalanceUtilityFactory>();
            _services.AddTransient<ITestBalanceHelper, TestBalanceHelper>();
            _services.AddTransient<ITextEncoder<SecurityContext>, Base64Encoder<SecurityContext>>();
        }

        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
    }
}
