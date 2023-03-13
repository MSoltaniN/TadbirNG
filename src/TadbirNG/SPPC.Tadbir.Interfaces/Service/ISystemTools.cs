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
    /// <remarks>
    /// این اینترفیس به منظور کاهش تعداد وابستگی ها در پیاده سازی کلاس ها و اینترفیس های دیگر طراحی شده است
    /// </remarks>
    public interface ISystemTools
    {
        /// <summary>
        /// امکان رمزنگاری اطلاعات حساس برنامه را با روش های مختلف فراهم می کند
        /// </summary>
        ICryptoService Crypto { get; }

        /// <summary>
        /// امکان کار با توکن امنیتی برنامه را فراهم می کند
        /// </summary>
        ITokenManager TokenManager { get; }

        /// <summary>
        /// امکان دسترسی به پیکربندی سرویس وب را فراهم می کند
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// امکان دسترسی به مسیر فایل ها کاربردی سرویس را فراهم می کند
        /// </summary>
        IApiPathProvider PathProvider { get; }

        /// <summary>
        /// امکان کار با متن های چندزبانه برنامه را فراهم می کند
        /// </summary>
        IStringLocalizer<AppStrings> Strings { get; }

        /// <summary>
        /// امکان فیلتر کردن منوهای برنامه را فراهم می کند
        /// </summary>
        ICommandFilter CommandFilter { get; }

        /// <summary>
        /// امکان ارتقاء ساختار دیتابیس های برنامه را فراهم می کند
        /// </summary>
        IDbUpgrade DbUpgrade { get; }
    }
}
