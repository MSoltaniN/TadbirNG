using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// امکان اتصال به دیتابیس های قابل دستیابی در برنامه تدبیر را فراهم می کند
    /// </summary>
    public interface IDbContextAccessor
    {
        /// <summary>
        /// دیتابیس شرکت جاری در برنامه
        /// </summary>
        TadbirContext CompanyContext { get; }

        /// <summary>
        /// دیتابیس جاری را مطابق با رشته اتصال دیتابیسی داده شده تنظیم می کند
        /// </summary>
        /// <param name="connection">رشته اتصال دیتابیسی مورد نظر</param>
        void SwitchCompanyContext(string connection);

        /// <summary>
        /// دیتابیس سیستمی برنامه
        /// </summary>
        SystemContext SystemContext { get; }
    }
}
