using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;
using SPPC.Framework.Persistence;
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
        public RepositoryContext(IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecurityContext security,
            ISqlConsole dbConsole)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            UserContext = security?.User;
            DbConsole = dbConsole;
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
    }
}
