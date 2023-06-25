using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مقادیر کاربری را تعریف می کند
    /// </summary>
    public interface IUserValueRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه دسته بندی های مقادیر کاربری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دسته بندی های مقادیر کاربری</returns>
        Task<IEnumerable<KeyValue>> GetCategoriesAsync();

        /// <summary>
        /// به روش آسنکرون، کلیه مقادیر کاربری برای دسته بندی داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="categoryId">شناسه دیتابیسی یکی از دسته بندی های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مقادیر کاربری تعریف شده در دسته بندی</returns>
        Task<PagedList<UserValueViewModel>> GetUserValuesAsync(int categoryId, GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، مقدار کاربری داده شده را ایجاد می کند
        /// </summary>
        /// <param name="userValue">مقدار کاربری مورد نظر برای ایجاد</param>
        /// <returns>اطلاعات نمایشی مقدار کاربری ایجادشده</returns>
        Task<UserValueViewModel> SaveUserValueAsync(UserValueViewModel userValue);

        /// <summary>
        /// به روش آسنکرون، مقدار کاربری مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="userValueId">شناسه عددی مقدار کاربری مورد نظر برای حذف</param>
        Task DeleteUserValueAsync(int userValueId);

        /// <summary>
        /// به روش آسنکرون، مقادیر کاربری مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="userValueIds">مجموعه ای از شناسه های عددی مقادیر کاربری مورد نظر برای حذف</param>
        Task DeleteUserValuesAsync(IList<int> userValueIds);
    }
}
