using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Domain;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ضمیمه کردن متادیتا به اقلام اطلاعاتی را تعریف می کند
    /// </summary>
    public interface IMetadataDecorator
    {
        /// <summary>
        /// آبجکت مورد استفاده برای خواندن اطلاعات متادیتا
        /// </summary>
        IMetadataRepository Repository { get; }

        /// <summary>
        /// به روش آسنکرون، اطلاعات متادیتا را به یک رکورد اطلاعاتی ضمیمه می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که متادیتای آن باید ضمیمه رکورد شود</typeparam>
        /// <typeparam name="TModel">نوع مدل نمایشی رکورد اطلاعاتی</typeparam>
        /// <param name="item">مدل نمایشی برای یک رکورد اطلاعاتی</param>
        /// <returns>رکورد اطلاعاتی با متادیتای ضمیمه شده</returns>
        Task<EntityItemViewModel<TModel>> GetDecoratedItemAsync<TEntity, TModel>(TModel item)
            where TEntity : IEntity
            where TModel : class, new();

        /// <summary>
        /// به روش آسنکرون، اطلاعات متادیتا را به یک لیست اطلاعاتی ضمیمه می کند
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیتی که متادیتای آن باید ضمیمه لیست شود</typeparam>
        /// <typeparam name="TModel">نوع مدل نمایشی موجود در لیست اطلاعاتی</typeparam>
        /// <param name="list">مجموعه ای از مدل های نمایشی</param>
        /// <returns>لیست اطلاعاتی با متادیتای ضمیمه شده</returns>
        Task<EntityListViewModel<TModel>> GetDecoratedListAsync<TEntity, TModel>(IList<TModel> list)
            where TEntity : IEntity
            where TModel : class, new();
    }
}
