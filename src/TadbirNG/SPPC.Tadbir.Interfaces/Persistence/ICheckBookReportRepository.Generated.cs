using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دفتر دسته های چک را تعریف می کند
    /// </summary>
    public interface ICheckBookReportRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر دسته چک را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دسته های چک تعریف شده</returns>
        Task<PagedList<CheckBookReportViewModel>> GetCheckBooksReportAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، وضعیت بایگانی دسته چک های مشخص شده با شناسه عددی را تغییر می دهد
        /// </summary>
        /// <param name="checkBookIds">مجموعه ای از شناسه های عددی دسته چک های مورد نظر 
        /// برای تغییر وضعیت بایگانی</param>
        /// <param name="isArchived">مقدار مورد نظر برای تغییر وضعیت بایگانی دسته چک ها</param>
        Task UpdateArchiveStatusAsync(IList<int> checkBookIds, bool isArchived);

        /// <summary>
        /// به روش آسنکرون، دسته چک با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شناسه عددی</returns>
        Task<CheckBookViewModel> GetCheckBookAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، وجود برگه سفید در دسته چک را بررسی می کند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی یکی از دسته چک های موجود</param>
        /// <returns>در صورت وجود برگه سفید در دسته چک مقدار درست و
        /// در غیر اینصورت مقدار نادرست</returns>
        Task<bool> HasCheckBookBlankPageAsync(int checkBookId);
    }
}
