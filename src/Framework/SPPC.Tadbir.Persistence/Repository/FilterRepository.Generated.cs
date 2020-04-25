using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت فیلترها را پیاده سازی میکند
    /// </summary>
    public class FilterRepository : LoggingRepository<Filter, FilterViewModel>, IFilterRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public FilterRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه فیلترهای فرم مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی (فرم) مرتبط با فیلتر</param>
        /// <returns>مجموعه ای از فیلترها تعریف شده برای فرم مورد نظر</returns>
        public async Task<IList<FilterViewModel>> GetFiltersAsync(int viewId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Filter>();
            var filters = await repository
                .GetEntityQuery()
                .Where(filter => filter.ViewId == viewId &&
                    (filter.UserId == UserContext.Id || filter.IsPublic))
                .Select(item => Mapper.Map<FilterViewModel>(item))
                .ToListAsync();
            return filters;
        }

        /// <summary>
        /// به روش آسنکرون، فیلتر با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="filterId">شناسه عددی یکی از فیلترها موجود</param>
        /// <returns>فیلتر مشخص شده با شناسه عددی</returns>
        public async Task<FilterViewModel> GetFilterAsync(int filterId)
        {
            FilterViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Filter>();
            var filter = await repository.GetByIDAsync(filterId);
            if (filter != null)
            {
                item = Mapper.Map<FilterViewModel>(filter);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک فیلتر را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="filter">فیلتر مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی فیلتر ایجاد یا اصلاح شده</returns>
        public async Task<FilterViewModel> SaveFilterAsync(FilterViewModel filter)
        {
            Verify.ArgumentNotNull(filter, nameof(filter));
            Filter filterModel = default(Filter);
            var repository = UnitOfWork.GetAsyncRepository<Filter>();
            if (filter.Id == 0)
            {
                filterModel = Mapper.Map<Filter>(filter);
                await InsertAsync(repository, filterModel);
            }
            else
            {
                filterModel = await repository.GetByIDAsync(filter.Id);
                if (filterModel != null)
                {
                    await UpdateAsync(repository, filterModel, filter);
                }
            }

            return Mapper.Map<FilterViewModel>(filterModel);
        }

        /// <summary>
        /// به روش آسنکرون، فیلتر مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="filterId">شناسه عددی فیلتر مورد نظر برای حذف</param>
        public async Task DeleteFilterAsync(int filterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Filter>();
            var filter = await repository.GetByIDAsync(filterId);
            if (filter != null)
            {
                await DeleteAsync(repository, filter);
            }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="filterViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="filter">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(FilterViewModel filterViewModel, Filter filter)
        {
            filter.Name = filterViewModel.Name;
            filter.IsPublic = filterViewModel.IsPublic;
            filter.Values = filterViewModel.Values;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(Filter entity)
        {
            return (entity != null)
                ? String.Format("{0} : {1}", AppStrings.Name, entity.Name)
                : null;
        }
    }
}
