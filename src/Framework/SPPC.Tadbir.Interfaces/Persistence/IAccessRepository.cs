using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات از بانک اطلاعاتی اکسس را تعریف می کند
    /// </summary>
    public interface IAccessRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات جدول مشخص شده را از بانک اطلاعاتی اکسس در مسیر داده شده خوانده و برمی کرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نظر برای دریافت اطلاعات جدول اکسس</typeparam>
        /// <param name="mdbPath">مسیر فایل بانک اطلاعاتی اکسس</param>
        /// <param name="tableName">نام جدولی که اطلاعات آن باید خوانده شود</param>
        /// <returns>اطلاعات جدول مورد نظر به شکل مدل نمایشی داده شده</returns>
        Task<IList<TModel>> GetAllAsync<TModel>(string mdbPath, string tableName);
    }
}
