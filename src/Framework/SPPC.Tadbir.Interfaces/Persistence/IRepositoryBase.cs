using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Auth;

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
        /// امکان دسترسی به اطلاعات محیطی کاربر جاری برنامه را فراهم می کند
        /// </summary>
        UserContextViewModel UserContext { get; }

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
    }
}
