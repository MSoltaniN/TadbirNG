using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکان اتصال به دیتابیس های قابل دستیابی در برنامه تدبیر را فراهم می کند
    /// </summary>
    public class DbContextAccessor : IDbContextAccessor
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="companyContext">دیتابیس شرکت جاری در برنامه</param>
        /// <param name="systemContext">دیتابیس سیستمی برنامه</param>
        public DbContextAccessor(TadbirContext companyContext, SystemContext systemContext)
        {
            CompanyContext = companyContext;
            SystemContext = systemContext;
        }

        /// <summary>
        /// دیتابیس شرکت جاری در برنامه
        /// </summary>
        public TadbirContext CompanyContext { get; }

        /// <summary>
        /// دیتابیس سیستمی برنامه
        /// </summary>
        public SystemContext SystemContext { get; }
    }
}
