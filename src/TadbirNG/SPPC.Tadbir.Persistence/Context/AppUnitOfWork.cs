﻿using System;
using System.Collections.Generic;
using SPPC.Framework.Persistence;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای کار با دیتابیس های مختلف مورد استفاده برنامه را پیاده سازی می کند
    /// </summary>
    public class AppUnitOfWork : UnitOfWork, IAppUnitOfWork
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="contextAccessor"></param>
        public AppUnitOfWork(IDbContextAccessor contextAccessor)
            : base(contextAccessor.CompanyContext)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// رشته اتصال مرتبط با شرکت جاری را برمی گرداند
        /// </summary>
        public string CompanyConnection
        {
            get { return _contextAccessor.CompanyContext.ConnectionString; }
        }

        /// <summary>
        /// دیتابیس جاری را مطابق با رشته اتصال دیتابیسی داده شده تنظیم می کند
        /// </summary>
        /// <param name="connection">رشته اتصال دیتابیسی مورد نظر</param>
        public void SwitchCompany(string connection)
        {
            _contextAccessor.SwitchCompanyContext(connection);
        }

        /// <summary>
        /// واحد کاری را برای ارتباط با دیتابیس شرکت جاری تنظیم می کند
        /// </summary>
        public void UseCompanyContext()
        {
            SwitchContext(_contextAccessor.CompanyContext);
        }

        /// <summary>
        /// واحد کاری را برای ارتباط با دیتابیس سیستمی برنامه تنظیم می کند
        /// </summary>
        public void UseSystemContext()
        {
            SwitchContext(_contextAccessor.SystemContext);
        }

        private readonly IDbContextAccessor _contextAccessor;
    }
}
