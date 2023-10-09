using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.ProductScope;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ویژگی ها را تعریف می کند
    /// </summary>
    public interface IPropertyRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه ویژگی ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از ویژگی ها تعریف شده</returns>
        Task<PagedList<PropertyViewModel>> GetPropertiesAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، ویژگی با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="propertyId">شناسه عددی یکی از ویژگی ها موجود</param>
        /// <returns>ویژگی مشخص شده با شناسه عددی</returns>
        Task<PropertyViewModel> GetPropertyAsync(int propertyId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک ویژگی را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="property">ویژگی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی ویژگی ایجاد یا اصلاح شده</returns>
        Task<PropertyViewModel> SavePropertyAsync(PropertyViewModel property);

        /// <summary>
        /// به روش آسنکرون، ویژگی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="propertyId">شناسه عددی ویژگی مورد نظر برای حذف</param>
        Task DeletePropertyAsync(int propertyId);

        /// <summary>
        /// به روش آسنکرون، ویژگی ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="propertyIds">مجموعه ای از شناسه های عددی ویژگی ها مورد نظر برای حذف</param>
        Task DeletePropertiesAsync(IList<int> propertyIds);
    }
}
