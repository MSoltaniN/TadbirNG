using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Licensing;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;

namespace SPPC.Licensing.Local.Web
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
            AddSecurityTypes();
            AddUtilityTypes();
        }

        private void AddSecurityTypes()
        {
            _services.AddTransient<ICryptoService, CryptoService>();
            _services.AddTransient<IEncodedSerializer, JsonSerializer>();
            _services.AddTransient<ICertificateManager, CertificateManager>();
            _services.AddTransient<ISecurityContext, SecurityContext>();
        }

        private void AddUtilityTypes()
        {
            _services.AddTransient<IApiClient>(provider =>
            {
                return new ServiceClient()
                {
                    ServiceRoot = _configuration["ServerRoot"]
                };
            });
            _services.AddTransient<ILicenseUtility, LicenseUtility>();
            _services.AddTransient<IDeviceIdProvider, NetDeviceIdProvider>();
            _services.AddTransient<ILicensePathProvider, LicenseResourcePaths>();
            _services.AddTransient<ISessionProvider, SessionProvider>();
            _services.AddTransient<ISessionRepository, SessionRepository>();

            _services.AddDbContext<TadbirContext>();
            _services.AddDbContext<SystemContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("TadbirSysApi")));
            _services.AddScoped<SystemContext>();

            _services.AddTransient<IAppUnitOfWork, AppUnitOfWork>();
            _services.AddTransient<IDomainMapper, DomainMapper>();
            _services.AddTransient<ISqlConsole, SqlServerConsole>();
            _services.AddTransient<IDbContextAccessor, DbContextAccessor>();
            _services.AddTransient<IRepositoryContext, RepositoryContext>();
        }

        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
    }
}
