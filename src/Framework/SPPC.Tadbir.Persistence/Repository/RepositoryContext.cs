using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکانات مشترک مورد نیاز در کلاس های دیتابیسی را پیاده سازی می کند
    /// </summary>
    public class RepositoryContext : IRepositoryContext
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">امکان دسترسی به دیتابیس ها و انجام تراکنش های دیتابیسی را فراهم می کند</param>
        /// <param name="mapper">امکان تبدیل کلاس های مختلف به یکدیگر را فراهم می کند</param>
        /// <param name="security">امکان دسترسی به اطلاعات محیطی کاربر جاری برنامه را فراهم می کند</param>
        /// <param name="dbConsole">امکان اجرای مستقیم دستورات دیتابیسی را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را به زبان جاری برنامه فراهم می کند</param>
        /// <param name="configuration"></param>
        public RepositoryContext(IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecurityContext security,
            ISqlConsole dbConsole, IStringLocalizer<AppStrings> strings, IConfiguration configuration)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            UserContext = security?.User;
            DbConsole = dbConsole;
            SystemConnection = configuration.GetConnectionString("TadbirSysApi");
            _strings = strings;
        }

        /// <summary>
        /// امکان دسترسی به دیتابیس ها و انجام تراکنش های دیتابیسی را فراهم می کند
        /// </summary>
        public IAppUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// امکان تبدیل کلاس های مختلف به یکدیگر را فراهم می کند
        /// </summary>
        public IDomainMapper Mapper { get; }

        /// <summary>
        /// امکان اجرای مستقیم دستورات دیتابیسی را فراهم می کند
        /// </summary>
        public ISqlConsole DbConsole { get; }

        /// <summary>
        /// امکان دسترسی به اطلاعات محیطی کاربر جاری برنامه را فراهم می کند
        /// </summary>
        public UserContextViewModel UserContext { get; }

        /// <summary>
        /// رشته اتصال به دیتابیس سیستمی که از پیکربندی برنامه وب خوانده می شود
        /// </summary>
        public string SystemConnection { get; }

        /// <summary>
        /// یک رشته متنی شامل ترکیب دلخواهی از متن و کلید متنی چندزبانه را به زبان جاری برنامه ترجمه می کند
        /// </summary>
        /// <param name="resourceKey">رشته متنی مورد نظر برای ترجمه</param>
        /// <returns>ترجمه رشته داده شده به زبان جاری برنامه</returns>
        public string Localize(string resourceKey)
        {
            if (String.IsNullOrEmpty(resourceKey))
            {
                return resourceKey;
            }

            var parts = resourceKey.Split(' ');
            var localizedParts = parts.Select(part => _strings[part]);
            return String.Join(" ", localizedParts);
        }

        private readonly IStringLocalizer<AppStrings> _strings;
    }
}
