using System;
using System.Collections.Generic;
using SPPC.Framework.Persistence;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای کار با دیتابیس های مختلف مورد استفاده برنامه را تعریف می کند
    /// </summary>
    public interface IAppUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// واحد کاری را برای ارتباط با دیتابیس شرکت جاری تنظیم می کند
        /// </summary>
        void UseCompanyContext();

        /// <summary>
        /// دیتابیس جاری را مطابق با رشته اتصال دیتابیسی داده شده تنظیم می کند
        /// </summary>
        /// <param name="connection">رشته اتصال دیتابیسی مورد نظر</param>
        void SwitchCompany(string connection);

        /// <summary>
        /// واحد کاری را برای ارتباط با دیتابیس سیستمی برنامه تنظیم می کند
        /// </summary>
        void UseSystemContext();
    }
}
