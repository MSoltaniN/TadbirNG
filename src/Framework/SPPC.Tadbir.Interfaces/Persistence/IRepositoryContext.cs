using System;
using System.Collections.Generic;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکانات مشترک مورد نیاز در کلاس های دیتابیسی را تعریف می کند
    /// </summary>
    public interface IRepositoryContext
    {
        /// <summary>
        /// امکان دسترسی به دیتابیس ها و انجام تراکنش های دیتابیسی را فراهم می کند
        /// </summary>
        IAppUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// امکان تبدیل کلاس های مختلف به یکدیگر را فراهم می کند
        /// </summary>
        IDomainMapper Mapper { get; }

        /// <summary>
        /// امکان دسترسی به اطلاعات محیطی کاربر جاری برنامه را فراهم می کند
        /// </summary>
        UserContextViewModel UserContext { get; }
    }
}
