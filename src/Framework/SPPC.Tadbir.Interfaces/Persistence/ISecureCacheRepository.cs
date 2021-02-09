using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Persistence
{
    public interface ISecureCacheRepository
    {
        IEnumerable<TModel> ApplyOperationBranchFilter<TModel>(IEnumerable<TModel> items)
            where TModel : class, IFiscalEntity;

        /// <summary>
        /// تنظیمات موجود برای فیلتر سطرهای اطلاعاتی را روی مجموعه ای از اطلاعات اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید فیلتر شود</typeparam>
        /// <param name="items">مجوعه سطرهای اطلاعاتی اولیه</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <returns>مجوعه سطرهای اطلاعاتی فیلتر شده</returns>
        IEnumerable<TEntity> ApplyRowFilter<TEntity>(IEnumerable<TEntity> items, int viewId)
            where TEntity : class, IEntity;
    }
}
