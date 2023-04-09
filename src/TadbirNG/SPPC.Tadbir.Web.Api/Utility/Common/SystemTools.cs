using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// امکانات سیستمی پرکاربرد را به طور متمرکز در اختیار کلاس ها قرار می دهد
    /// </summary>
    public class SystemTools : ISystemTools
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="crypto">امکان رمزنگاری اطلاعات حساس برنامه را با روش های مختلف فراهم می کند</param>
        /// <param name="token">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        /// <param name="config">امکان دسترسی به پیکربندی سرویس وب را فراهم می کند</param>
        /// <param name="paths">امکان دسترسی به مسیر فایل ها کاربردی سرویس را فراهم می کند</param>
        /// <param name="strings">امکان کار با متن های چندزبانه برنامه را فراهم می کند</param>
        /// <param name="dbVersions">امکان خواندن آخرین نسخه را برای دیتابیس های برنامه فراهم می کند</param>
        /// <param name="filter">امکان فیلتر کردن منوهای برنامه را فراهم می کند</param>
        /// <param name="upgrade">امکان ارتقاء ساختار دیتابیس های برنامه را فراهم می کند</param>
        public SystemTools(
            ICryptoService crypto, ITokenManager token, IConfiguration config, IApiPathProvider paths,
            IStringLocalizer<AppStrings> strings, IDbVersionProvider dbVersions, ICommandFilter filter,
            IDbUpgrade upgrade)
        {
            Crypto = crypto;
            TokenManager = token;
            Configuration = config;
            PathProvider = paths;
            Strings = strings;
            DbVersions = dbVersions;
            CommandFilter = filter;
            DbUpgrade = upgrade;
        }

        /// <summary>
        /// امکان رمزنگاری اطلاعات حساس برنامه را با روش های مختلف فراهم می کند
        /// </summary>
        public ICryptoService Crypto { get; }

        /// <summary>
        /// امکان کار با توکن امنیتی برنامه را فراهم می کند
        /// </summary>
        public ITokenManager TokenManager { get; }

        /// <summary>
        /// امکان دسترسی به پیکربندی سرویس وب را فراهم می کند
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// امکان دسترسی به مسیر فایل ها کاربردی سرویس را فراهم می کند
        /// </summary>
        public IApiPathProvider PathProvider { get; }

        /// <summary>
        /// امکان کار با متن های چندزبانه برنامه را فراهم می کند
        /// </summary>
        public IStringLocalizer<AppStrings> Strings { get; }

        /// <summary>
        /// امکان خواندن آخرین نسخه را برای دیتابیس های برنامه فراهم می کند
        /// </summary>
        public IDbVersionProvider DbVersions { get; }

        /// <summary>
        /// امکان فیلتر کردن منوهای برنامه را فراهم می کند
        /// </summary>
        public ICommandFilter CommandFilter { get; }

        /// <summary>
        /// امکان ارتقاء ساختار دیتابیس های برنامه را فراهم می کند
        /// </summary>
        public IDbUpgrade DbUpgrade { get; }
    }
}
