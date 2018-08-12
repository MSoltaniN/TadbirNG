using System;
using Microsoft.Extensions.DependencyInjection;
using SPPC.Tadbir.Persistence;

namespace SPPC.Tadbir.Web.Api.Context
{
    /// <summary>
    /// امکان اتصال به دیتابیس های قابل دستیابی در برنامه تدبیر را فراهم می کند
    /// </summary>
    public class DbContextAccessor : IDbContextAccessor
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="userContext">دیتابیس شرکت جاری در برنامه</param>
        /// <param name="serviceProvider">آبجکت مورد نیاز برای دسترسی به سرویس های موجود در برنامه</param>
        public DbContextAccessor(TadbirContext userContext, IServiceProvider serviceProvider)
        {
            UserContext = userContext;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// دیتابیس شرکت جاری در برنامه
        /// </summary>
        public TadbirContext UserContext { get; }

        /// <summary>
        /// دیتابیس سیستمی برنامه
        /// </summary>
        public SystemContext SystemContext
        {
            get { return _serviceProvider.GetService<SystemContext>(); }
        }

        private readonly IServiceProvider _serviceProvider;
    }
}
