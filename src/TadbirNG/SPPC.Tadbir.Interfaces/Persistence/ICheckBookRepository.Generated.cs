using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دسته چک ها را تعریف می کند
    /// </summary>
    public interface ICheckBookRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه دسته چک ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دسته چک ها تعریف شده</returns>
        Task<PagedList<CheckBookViewModel>> GetCheckBooksAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، دسته چک با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkbookId">شناسه عددی یکی از دسته چک ها موجود</param>
        /// <returns>دسته چک مشخص شده با شناسه عددی</returns>
        Task<CheckBookViewModel> GetCheckBookAsync(int checkbookId);

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

        /// <summary>
        /// به روش آسنکرون، دسته چک ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="checkBookIds">مجموعه ای از شناسه های عددی دسته چک ها مورد نظر برای حذف</param>
        Task DeleteCheckBooksAsync(IList<int> checkBookIds);
    }
}
