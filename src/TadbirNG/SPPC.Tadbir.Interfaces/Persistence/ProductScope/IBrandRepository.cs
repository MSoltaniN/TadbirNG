using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.ProductScope;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت برندها را تعریف می کند
    /// </summary>
    public interface IBrandRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه برندها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از برندها تعریف شده</returns>
        Task<PagedList<BrandViewModel>> GetBrandsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، برند با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="brandId">شناسه عددی یکی از برندها موجود</param>
        /// <returns>برند مشخص شده با شناسه عددی</returns>
        Task<BrandViewModel> GetBrandAsync(int brandId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک برند را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="brand">برند مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی برند ایجاد یا اصلاح شده</returns>
        Task<BrandViewModel> SaveBrandAsync(BrandViewModel brand);

        /// <summary>
        /// به روش آسنکرون، برند مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="brandId">شناسه عددی برند مورد نظر برای حذف</param>
        Task DeleteBrandAsync(int brandId);

        /// <summary>
        /// به روش آسنکرون، برندها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="brandIds">مجموعه ای از شناسه های عددی برندها مورد نظر برای حذف</param>
        Task DeleteBrandsAsync(IList<int> brandIds);
    }
}
