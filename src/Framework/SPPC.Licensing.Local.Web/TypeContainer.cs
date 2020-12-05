using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Service;
using SPPC.Licensing.Local.Persistence;
using SPPC.Tadbir.Licensing;

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
            _services.AddTransient<IDigitalSigner, DigitalSigner>();
            _services.AddTransient<ICertificateManager, CertificateManager>();
        }

        private void AddUtilityTypes()
        {
            _services.AddTransient<IApiClient, ServiceClient>();
            _services.AddTransient<ILicenseUtility, LicenseUtility>();
        }

        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
    }
}
