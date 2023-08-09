using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مرتبط با فعال و غیرفعال کردن یک موجودیت پایه را تعریف می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع کلاس مدل اطلاعاتی برای موجودیت پایه</typeparam>
    public interface IActiveStateRepository<TEntity>
        where TEntity : IFiscalEntity
    {
        /// <summary>
        /// به روش آسنکرون، یکی از سطرهای اطلاعاتی موجودیت را به حالت فعال یا غیرفعال می برد
        /// </summary>
        /// <param name="entity">سطر اطلاعاتی مورد نظر برای فعال یا غیرفعال کردن</param>
        /// <param name="isActive">مقدار درست برای فعال کردن و مقدار بولی نادرست برای غیرفعال کردن</param>
        Task SetActiveStatusAsync(TEntity entity, bool isActive);

        /// <summary>
        /// به روش آسنکرون مشخص می کند که سطر داده شده غیرفعال شده یا نه
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی سطر مورد نظر برای بررسی</param>
        /// <returns>در صورتی که سطر مورد نظر غیرفعال شده باشد مقدار بولی درست و
        /// در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsDeactivatedAsync(int itemId);
    }
}
