﻿using System;
using System.Threading.Tasks;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی  سیستمی همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی که عملیات روی آن انجام می شود</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی که برای اصلاح اطلاعات استفاده می شود</typeparam>
    public abstract class SystemLoggingRepository<TEntity, TEntityView> : LoggingRepositoryBase<TEntity, TEntityView>
        where TEntity : class, IEntity
        where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public SystemLoggingRepository(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context)
        {
            _logRepository = logRepository;
        }

        protected override async Task TrySaveLogAsync()
        {
            try
            {
                await _logRepository.SaveSystemLogAsync(Log);
            }
            catch (Exception ex)
            {
                ReportLoggingError(ex);

                // Ignored (logging should not throw exception)
            }
        }

        private readonly IOperationLogRepository _logRepository;
    }
}
