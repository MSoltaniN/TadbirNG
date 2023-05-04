using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مقادیر کاربری را پیاده سازی می کند
    /// </summary>
    public class UserValueRepository : RepositoryBase, IUserValueRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public UserValueRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه دسته بندی های مقادیر کاربری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دسته بندی های مقادیر کاربری</returns>
        public async Task<IEnumerable<KeyValue>> GetCategoriesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<UserValueCategory>();
            return await repository
                .GetEntityQuery()
                .Select(cat => Mapper.Map<KeyValue>(cat))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مقادیر کاربری برای دسته بندی داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="categoryId">شناسه دیتابیسی یکی از دسته بندی های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مقادیر کاربری تعریف شده در دسته بندی</returns>
        public async Task<PagedList<UserValueViewModel>> GetUserValuesAsync(
            int categoryId, GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var repository = UnitOfWork.GetAsyncRepository<UserValue>();
            var values = await repository
                .GetEntityQuery()
                .Where(val => val.CategoryId == categoryId)
                .Select(val => Mapper.Map<UserValueViewModel>(val))
                .ToListAsync();
            return new PagedList<UserValueViewModel>(values, options);
        }

        /// <summary>
        /// به روش آسنکرون، مقدار کاربری داده شده را ایجاد می کند
        /// </summary>
        /// <param name="userValue">مقدار کاربری مورد نظر برای ایجاد</param>
        /// <returns>اطلاعات نمایشی مقدار کاربری ایجادشده</returns>
        public async Task<UserValueViewModel> SaveUserValueAsync(UserValueViewModel userValue)
        {
            Verify.ArgumentNotNull(userValue, nameof(userValue));
            var userValueView = userValue;
            if (userValue.Id == 0)
            {
                var repository = UnitOfWork.GetAsyncRepository<UserValue>();
                var newValue = Mapper.Map<UserValue>(userValue);
                repository.Insert(newValue);
                await UnitOfWork.CommitAsync();
                userValueView = Mapper.Map<UserValueViewModel>(newValue);
            }

            return userValueView;
        }

        /// <summary>
        /// به روش آسنکرون، مقدار کاربری مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="userValueId">شناسه عددی مقدار کاربری مورد نظر برای حذف</param>
        public async Task DeleteUserValueAsync(int userValueId)
        {
            var repository = UnitOfWork.GetAsyncRepository<UserValue>();
            var existing = await repository.GetByIDAsync(userValueId);
            if (existing != null)
            {
                repository.Delete(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مقادیر کاربری مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="userValueIds">مجموعه ای از شناسه های عددی مقادیر کاربری مورد نظر برای حذف</param>
        public async Task DeleteUserValuesAsync(IList<int> userValueIds)
        {
            Verify.ArgumentNotNull(userValueIds, nameof(userValueIds));
            var repository = UnitOfWork.GetAsyncRepository<UserValue>();
            var userValues = await repository.GetByCriteriaAsync(val => userValueIds.Contains(val.Id));
            foreach (var userValue in userValues)
            {
                repository.Delete(userValue);
            }

            await UnitOfWork.CommitAsync();
        }
    }
}
