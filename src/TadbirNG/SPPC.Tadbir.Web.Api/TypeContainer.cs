using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service;
using SPPC.Tadbir.CrossCutting;
using SPPC.Tadbir.CrossCutting.Redis;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Persistence.Repository;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Web.Api.Filters;

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

        private static TadbirContext GetTadbirContext(IServiceProvider provider)
        {
            string companyConnection = null;
            var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
            string token = httpContext.Request.Headers[AppConstants.ContextHeaderName];
            if (!String.IsNullOrEmpty(token))
            {
                var crypto = provider.GetService<ICryptoService>();
                var tokenService = provider.GetService<ITokenService>();
                var securityContext = tokenService.GetSecurityContext(token);
                string connectionString = securityContext?.User.Connection;
                companyConnection = String.IsNullOrEmpty(connectionString)
                    ? connectionString
                    : crypto.Decrypt(connectionString);
            }

            return new TadbirContext() { ConnectionString = companyConnection };
        }

        private static ISecurityContext GetSecurityContext(IServiceProvider provider)
        {
            ISecurityContext securityContext = null;
            var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
            string token = httpContext.Request.Headers[AppConstants.ContextHeaderName];
            if (!String.IsNullOrEmpty(token))
            {
                var tokenService = provider.GetService<ITokenService>();
                securityContext = tokenService.GetSecurityContext(token);
            }

            return securityContext;
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
                return GetTadbirContext(provider);
            });
            _services.AddTransient<IDbContextAccessor, DbContextAccessor>();
            _services.AddScoped(provider =>
            {
                return GetSecurityContext(provider);
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
            _services.AddTransient<IDraftVoucherRepository, DraftVoucherRepository>();
            _services.AddTransient<IDraftVoucherLineRepository, DraftVoucherLineRepository>();
            _services.AddTransient<IFiscalPeriodRepository, FiscalPeriodRepository>();
            _services.AddTransient<IBranchRepository, BranchRepository>();
            _services.AddTransient<ICompanyRepository, CompanyRepository>();
            _services.AddTransient<IRelationRepository, RelationRepository>();
            _services.AddTransient<IMetadataRepository, MetadataRepository>();
            _services.AddTransient<IConfigRepository, ConfigRepository>();
            _services.AddTransient<IOperationLogRepository, OperationLogRepository>();
            _services.AddTransient<ISecureRepository, SecureRepository>();
            _services.AddTransient<ISecureCacheRepository, SecureCacheRepository>();
            _services.AddTransient<IDashboardRepository, DashboardRepository>();
            _services.AddTransient<IAccountGroupRepository, AccountGroupRepository>();
            _services.AddTransient<IFinanceReportRepository, FinanceReportRepository>();
            _services.AddTransient<IReportSystemRepository, ReportSystemRepository>();
            _services.AddTransient<IAccountCollectionRepository, AccountCollectionRepository>();
            _services.AddTransient<IJournalRepository, JournalRepositoryDirect>();
            _services.AddTransient<IAccountBookRepository, AccountBookRepositoryDirect>();
            _services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            _services.AddTransient<ICurrencyRateRepository, CurrencyRateRepository>();
            _services.AddTransient<ITestBalanceRepository, TestBalanceRepositoryDirect>();
            _services.AddTransient<IAccessRepository, AccessRepository>();
            _services.AddTransient<ICurrencyBookRepository, CurrencyBookRepository>();
            _services.AddTransient<ISystemRepository, SystemRepository>();
            _services.AddTransient<ISystemConfigRepository, SystemConfigRepository>();
            _services.AddTransient<ISystemIssueRepository, SystemIssueRepository>();
            _services.AddTransient<IFilterRepository, FilterRepository>();
            _services.AddTransient<IBalanceByAccountRepository, BalanceByAccountRepositoryDirect>();
            _services.AddTransient<ILogConfigRepository, LogConfigRepository>();
            _services.AddTransient<ICustomerTaxInfoRepository, CustomerTaxInfoRepository>();
            _services.AddTransient<IAccountOwnerRepository, AccountOwnerRepository>();
            _services.AddTransient<IProfitLossRepository, ProfitLossRepositoryDirect>();
            _services.AddTransient<IBalanceSheetRepository, BalanceSheetRepositoryDirect>();
            _services.AddTransient<ISystemErrorRepository, SystemErrorRepository>();
        }

        private void AddServiceTypes()
        {
            _services.AddTransient<ICryptoService, CryptoService>();
            _services.AddTransient<ITokenService, JwtTokenService>();
            _services.AddTransient<ISecurityContextManager, ServiceContextManager>();
            _services.AddTransient<IAuthorizeRequest, AuthorizeRequest>();
        }

        private void AddUtilityTypes()
        {
            _services.AddTransient<IDomainMapper, DomainMapper>();
            _services.AddTransient<IApiClient>(provider =>
            {
                return new ServiceClient()
                {
                    ServiceRoot = _configuration["ServerRoot"]
                };
            });
            _services.AddTransient<IDigitalSigner, DigitalSigner>();
            _services.AddTransient<ICertificateManager, CertificateManager>();
            _services.AddTransient<IEncodedSerializer, JsonSerializer>();
            _services.AddTransient<IReportUtility, ReportUtility>();
            _services.AddTransient<IReportDirectUtility, ReportDirectUtility>();
            _services.AddTransient<IAccountCollectionUtility, AccountCollectionUtility>();
            _services.AddTransient<IAccountItemUtilityFactory, AccountItemUtilityFactory>();
            _services.AddTransient<ICacheManager, RedisCacheManager>();
        }

        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
    }
}
