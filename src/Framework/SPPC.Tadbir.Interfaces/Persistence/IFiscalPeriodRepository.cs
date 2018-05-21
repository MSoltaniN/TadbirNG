using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///  عملیات مورد نیاز برای مدیریت اطلاعات د.ره مالی را پیاده سازی می کند.
    /// </summary>
    public interface IFiscalPeriodRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه دوره های مالی را که در شرکت مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دوره های مالی تعریف شده در شرکت مشخص شده</returns>
        Task<IList<FiscalPeriodViewModel>> GetFiscalPeriodsAsync(int companyId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد دوره های مالی تعریف شده در شرکت مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد دوره های مالی تعریف شده در شرکت مشخص شده</returns>
        Task<int> GetCountAsync(int companyId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون،دوره مالی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی یکی از دوره های مالی</param>
        /// <returns>دوره مالی مشخص شده با شناسه عددی</returns>
        Task<FiscalPeriodViewModel> GetFiscalPeriodAsync(int fperiodId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای دوره مالی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای دوره مالی</returns>
        Task<EntityItemViewModel<FiscalPeriodViewModel>> GetFiscalPeriodMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دوره مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="fiscalPeriod">دوره مالی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دوره مالی ایجاد یا اصلاح شده</returns>
        Task<FiscalPeriodViewModel> SaveFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod);

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی دوره مالی مورد نظر برای حذف</param>
        Task DeleteFiscalPeriodAsync(int fperiodId);
    }
}
