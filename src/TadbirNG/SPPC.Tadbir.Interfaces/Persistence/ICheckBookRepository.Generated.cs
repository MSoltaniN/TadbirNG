using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دسته چک ها را تعریف می کند
    /// </summary>
    public interface ICheckBookRepository
    {
        /// <summary>
        /// به روش آسنکرون، دسته چک با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkbookId">شناسه عددی یکی از دسته چک ها موجود</param>
        /// <returns>دسته چک مشخص شده با شناسه عددی</returns>
        Task<CheckBookViewModel> GetCheckBookAsync(int checkbookId);

        /// <summary>
        /// به روش آسنکرون، دسته چک با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookNo">شماره یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شماره</returns>
        Task<CheckBookViewModel> GetCheckBookByNoAsync(int checkBookNo);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دسته چک را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="checkbook">دسته چک مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دسته چک ایجاد یا اصلاح شده</returns>
        Task<CheckBookViewModel> SaveCheckBookAsync(CheckBookViewModel checkbook);

        /// <summary>
        /// به روش آسنکرون، دسته چک مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="checkbookId">شناسه عددی دسته چک مورد نظر برای حذف</param>
        Task DeleteCheckBookAsync(int checkbookId);

        #region CheckBookPage
        /// <summary>
        /// به روش آسنکرون، برگه های های یک دسته چک مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>برگه های چک مشخص شده با شناسه عددی</returns>
        Task<PagedList<CheckBookPageViewModel>> GetPagesAsync(int checkBookId, GridOptions gridOptions = null);

        #endregion
    }
}
