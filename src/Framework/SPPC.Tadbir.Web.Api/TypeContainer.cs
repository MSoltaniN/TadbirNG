using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPPC.Framework.Mapper;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.Persistence;
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
            _services.AddTransient<SystemContext>();
            _services.AddTransient(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
                var securityContext = httpContext.Request.CurrentSecurityContext();         // TODO: Set connection string in Company selection form
                string connectionString = securityContext?.User.Connection ?? _configuration.GetConnectionString("TadbirApi");
                return new TadbirContext(connectionString);
            });
            _services.AddTransient<IDbContextAccessor, DbContextAccessor>();
        }

        private void AddPersistenceTypes()
        {
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
        }

        private void AddServiceTypes()
        {
            _services.AddTransient<ICryptoService, CryptoService>();
            _services.AddTransient<ISecurityContextManager, ServiceContextManager>();
        }

        private void AddUtilityTypes()
        {
            _services.AddTransient<IDomainMapper, DomainMapper>();
            _services.AddTransient<ITextEncoder<SecurityContext>, Base64Encoder<SecurityContext>>();
        }

        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
    }
}
