using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات دیتابیسی پایه ای را برای لایه دیتابیسی تعریف می کند
    /// </summary>
    public interface IRepositoryBase
    {
        /// <summary>
        /// رشته اتصال مرتبط با شرکت جاری
        /// </summary>
        string CompanyConnection { get; set; }

        /// <summary>
        /// به روش آسنکرون، شرکت جاری در برنامه را به شرکت مشخص شده تغییر می دهد
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی شرکت مورد نظر</param>
        Task SetCurrentCompanyAsync(int companyId);
    }
}
