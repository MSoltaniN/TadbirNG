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

        /// <summary>
        /// به روش آسنکرون، رشته اتصال شرکت را ایجاد میکند
        /// </summary>
        /// <param name="companyId">شناسه یکتای شرکت</param>
        /// <returns>رشته اتصال</returns>
        Task<string> BuildConnectionStringAsync(int companyId);

        /// <summary>
        /// شعبه های زیرمجموعه شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه والد مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های زیرمجموعه</returns>
        IEnumerable<int> GetChildTree(int branchId);

        /// <summary>
        /// شعبه های والد شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه زیرمجموعه مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های والد</returns>
        IEnumerable<int> GetParentTree(int branchId);

        /// <summary>
        /// به روش آسنکرون، شعبه های زیرمجموعه شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه والد مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های زیرمجموعه</returns>
        Task<IEnumerable<int>> GetChildTreeAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، شعبه های والد شعبه داده شده را به صورت مجموعه ای از
        /// شناسه های دیتابیسی خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه زیرمجموعه مورد نظر</param>
        /// <returns>مجموعه شناسه های دیتابیسی شعبه های والد</returns>
        Task<IEnumerable<int>> GetParentTreeAsync(int branchId);
    }
}
