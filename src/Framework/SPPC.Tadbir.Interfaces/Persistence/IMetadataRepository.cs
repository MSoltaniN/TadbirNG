using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Domain;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات فراداده ای از محل ذخیره را تعریف می کند
    /// </summary>
    public interface IMetadataRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای نوع موجودیت مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که فراداده آن مورد نیاز است</typeparam>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        Task<ViewViewModel> GetViewMetadataAsync<TEntity>()
            where TEntity : IEntity;

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewName">نام (شناسه متنی) موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        Task<ViewViewModel> GetViewMetadataAsync(string viewName);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای موجودیت با نام مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه عددی موجودیت مورد نظر</param>
        /// <returns>اطلاعات فراداده ای تعریف شده برای موجودیت</returns>
        Task<ViewViewModel> GetViewMetadataByIdAsync(int viewId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی تمام دستوراتی که در بالاترین سطح ساختار درختی قرار دارند را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از دستورات در بالاترین سطح</returns>
        Task<IList<CommandViewModel>> GetTopLevelCommandsAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی دستورات پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دستورات در منوی پیش فرض کاربران</returns>
        Task<IList<CommandViewModel>> GetDefaultCommandsAsync();
    }
}
