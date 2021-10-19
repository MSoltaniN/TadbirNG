using System;
using System.Threading.Tasks;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات کمکی برای ایجاد لاگ های عملیاتی موجودیت های سیستمی همزمان با عملیات ذخیره و بازیابی را پیاده سازی می کند
    /// </summary>
    /// <typeparam name="TEntity">نوع مدل اطلاعاتی موجودیت</typeparam>
    /// <typeparam name="TEntityView">نوع مدل نمایشی موجودیت</typeparam>
    public class SystemEntityLoggingRepository<TEntity, TEntityView>
        : EntityLoggingRepository<TEntity, TEntityView>
            where TEntity : class, IEntity
            where TEntityView : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="logRepository">امکان ایجاد لاگ های عملیاتی و سیستمی را در برنامه فراهم می کند</param>
        public SystemEntityLoggingRepository(IRepositoryContext context, IOperationLogRepository logRepository)
            : base(context, logRepository)
        {
        }

        /// <summary>
        /// رکورد لاگ عملیاتی را در جدول مرتبط ایجاد می کند.
        /// </summary>
        /// <remarks>توجه : هر گونه خطای زمان اجرا حین عملیات، نادیده گرفته می‌شود</remarks>
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
    }
}
