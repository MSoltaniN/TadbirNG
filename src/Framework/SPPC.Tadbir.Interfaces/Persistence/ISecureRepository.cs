using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SPPC.Framework.Domain;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند
    /// </summary>
    public interface ISecureRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت پایه را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>لیست فیلتر شده از سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        Task<IList<TEntity>> GetAllAsync<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IBaseEntity;

        /// <summary>
        /// کوئری فیلترشده مورد نیاز برای خواندن اطلاعات دوره مالی و شعبه جاری برنامه را
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>کوئری فیلترشده خواندن اطلاعات دوره مالی و شعبه جاری برنامه</returns>
        IQueryable<TEntity> GetAllQuery<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IBaseEntity;

        /// <summary>
        /// کوئری فیلترشده مورد نیاز برای خواندن اطلاعات عملیاتی دوره مالی و شعبه جاری برنامه را
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت عملیاتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت عملیاتی</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>کوئری فیلترشده خواندن اطلاعات عملیاتی دوره مالی و شعبه جاری برنامه</returns>
        IQueryable<TEntity> GetAllOperationQuery<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IFiscalEntity;

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت عملیاتی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="relatedProperties">اطلاعات مرتبط مورد نیاز در موجودیت</param>
        /// <returns>لیست فیلتر شده از سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        Task<IList<TEntity>> GetAllOperationAsync<TEntity>(int viewId,
            params Expression<Func<TEntity, object>>[] relatedProperties)
            where TEntity : class, IFiscalEntity;

        /// <summary>
        /// به روش آسنکرون، کلیه سطرهای یک موجودیت را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها به صورت کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید خوانده شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns></returns>
        Task<IList<KeyValue>> GetAllLookupAsync<TEntity>(int viewId, GridOptions gridOptions = null)
            where TEntity : class, IBaseEntity;

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای یک موجودیت پایه را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که تعداد سطرهای آن باید خوانده شود</typeparam>
        /// <typeparam name="TEntityView">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        Task<int> GetCountAsync<TEntity, TEntityView>(int viewId, GridOptions gridOptions = null)
            where TEntity : class, IBaseEntity
            where TEntityView : class, new();

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای یک موجودیت عملیاتی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// پس از اعمال محدودیت های تعریف شده برای شعب و دسترسی به رکوردها از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که تعداد سطرهای آن باید خوانده شود</typeparam>
        /// <typeparam name="TEntityView">نوع مدل نمایشی که برای نمایش اطلاعات موجودیت استفاده می شود</typeparam>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای اطلاعاتی موجودیت مورد نظر</returns>
        Task<int> GetOperationCountAsync<TEntity, TEntityView>(int viewId, GridOptions gridOptions = null)
            where TEntity : class, IFiscalEntity
            where TEntityView : class, new();

        /// <summary>
        /// تنظیمات موجود برای فیلتر سطرهای اطلاعاتی را روی مجموعه ای از اطلاعات اعمال می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که سطرهای آن باید فیلتر شود</typeparam>
        /// <param name="records">مجوعه سطرهای اطلاعاتی اولیه</param>
        /// <param name="viewId">شناسه نمای اطلاعاتی اصلی موجودیت پایه</param>
        /// <returns>مجوعه سطرهای اطلاعاتی فیلتر شده</returns>
        IQueryable<TEntity> ApplyRowFilter<TEntity>(ref IQueryable<TEntity> records, int viewId)
            where TEntity : class, IEntity;
    }
}
