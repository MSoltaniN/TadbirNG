using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت منابع و مصارف را تعریف می کند
    /// </summary>
    public interface ISourceAppRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه منابع و مصارف را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="activeState">وضعیت مورد نظر برای نمایش اطلاعات بر اساس وضعیت فعال و غیر فعال</param>
        /// <returns>مجموعه ای از منابع و مصارف تعریف شده</returns>
        Task<PagedList<SourceAppViewModel>> GetSourceAppsAsync(
            GridOptions gridOptions, ActiveState activeState = ActiveState.Active);

        /// <summary>
        /// به روش آسنکرون، منبع و مصرف با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="sourceAppId">شناسه عددی یکی از منابع و مصارف موجود</param>
        /// <returns>منبع و مصرف مشخص شده با شناسه عددی</returns>
        Task<SourceAppViewModel> GetSourceAppAsync(int sourceAppId);

        /// <summary>
        /// به روش آسنکرون، منبع یا مصرف با مقادیر پیشنهادی را برمی گرداند
        /// </summary>
        /// <returns>منبع یا مصرف با مقادیر پیشنهادی</returns>
        Task<SourceAppViewModel> GetNewSourceAppAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک منبع و مصرف را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="sourceApp">منبع و مصرف مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی منبع و مصرف ایجاد یا اصلاح شده</returns>
        Task<SourceAppViewModel> SaveSourceAppAsync(SourceAppViewModel sourceApp);

        /// <summary>
        /// به روش آسنکرون، منبع و مصرف مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="sourceAppId">شناسه عددی منبع و مصرف مورد نظر برای حذف</param>
        Task DeleteSourceAppAsync(int sourceAppId);

        /// <summary>
        /// به روش آسنکرون، منابع و مصارف مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="sourceAppIds">مجموعه ای از شناسه های عددی منابع و مصارف مورد نظر برای حذف</param>
        Task DeleteSourceAppsAsync(IList<int> sourceAppIds);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نام منبع یا مصرف مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="sourceApp">مشخصات منبع یا مصرف که تکراری بودن آنها باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن نام در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsDuplicateNameAsync(SourceAppViewModel sourceApp);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد منبع یا مصرف مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="sourceApp"> مشخصات منبع یا مصرف که تکراری بودن آنها باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن کد در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsDuplicateCodeAsync(SourceAppViewModel sourceApp);
    }
}
