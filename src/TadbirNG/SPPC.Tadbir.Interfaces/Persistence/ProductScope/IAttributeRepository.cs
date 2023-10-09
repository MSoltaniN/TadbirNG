using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.ProductScope;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت خصوصیت ها را تعریف می کند
    /// </summary>
    public interface IAttributeRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه خصوصیت ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از خصوصیت ها تعریف شده</returns>
        Task<PagedList<AttributeViewModel>> GetAttributesAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، خصوصیت با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="attributeId">شناسه عددی یکی از خصوصیت ها موجود</param>
        /// <returns>خصوصیت مشخص شده با شناسه عددی</returns>
        Task<AttributeViewModel> GetAttributeAsync(int attributeId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک خصوصیت را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="attribute">خصوصیت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی خصوصیت ایجاد یا اصلاح شده</returns>
        Task<AttributeViewModel> SaveAttributeAsync(AttributeViewModel attribute);

        /// <summary>
        /// به روش آسنکرون، خصوصیت مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="attributeId">شناسه عددی خصوصیت مورد نظر برای حذف</param>
        Task DeleteAttributeAsync(int attributeId);

        /// <summary>
        /// به روش آسنکرون، خصوصیت ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="attributeIds">مجموعه ای از شناسه های عددی خصوصیت ها مورد نظر برای حذف</param>
        Task DeleteAttributesAsync(IList<int> attributeIds);
    }
}
