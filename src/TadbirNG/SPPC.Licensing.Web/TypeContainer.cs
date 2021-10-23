using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Persistence;
using SPPC.Framework.Service;
using SPPC.Licensing.Persistence;
using SPPC.Licensing.Persistence.Context;
using SPPC.Licensing.Service;

namespace SPPC.Licensing.Web
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
            AddPersistenceTypes();
            AddSecurityTypes();
            AddUtilityTypes();
        }

        private void AddPersistenceTypes()
        {
            _services.AddTransient<IUnitOfWork, UnitOfWork>();
            _services.AddDbContext<LicenseContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("LicenseDb")));
            _services.AddScoped<LicenseContext>();
            _services.AddTransient<DbContext, LicenseContext>();
            _services.AddTransient<ICustomerRepository, CustomerRepository>();
            _services.AddTransient<ILicenseRepository, LicenseRepository>();
        }

        private void AddSecurityTypes()
        {
            _services.AddTransient<ICryptoService, CryptoService>();
            _services.AddTransient<IEncodedSerializer, JsonSerializer>();
            _services.AddTransient<IDigitalSigner, DigitalSigner>();
            _services.AddTransient<ICertificateManager, CertificateManager>();
        }

        private void AddUtilityTypes()
        {
            _services.AddTransient<IApiClient, ServiceClient>();
            _services.AddTransient<ILicenseService, LicenseService>();
            _services.AddTransient<ILicenseManager, LicenseManager>();
        }

        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
    }
}
