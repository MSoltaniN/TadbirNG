﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند
    /// </summary>
    public interface ISecureRepository
    {
        /// <summary>
        /// محدودیت دسترسی به سطرهای اطلاعاتی را با توجه به تنظیمات موجود اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فیلتر روی سطرهای آن باید اعمال شود</typeparam>
        /// <param name="records">مجموعه سطرهای اطلاعاتی</param>
        /// <param name="roleId">شناسه دیتابیسی نقش امنیتی دارای محدودیت دسترسی</param>
        /// <returns>مجموعه سطرهای اطلاعاتی فیلتر شده</returns>
        Task<IQueryable<TEntity>> ApplyRowFilterAsync<TEntity>(IQueryable<TEntity> records, int roleId)
            where TEntity : class, IEntity;
    }
}
