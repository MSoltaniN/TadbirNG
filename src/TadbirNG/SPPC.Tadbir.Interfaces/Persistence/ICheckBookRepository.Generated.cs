using System;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Check;

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
        /// <param name="checkBookId">شناسه عددی یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شناسه عددی</returns>
        Task<CheckBookViewModel> GetCheckBookAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، دسته چک جدید با مقادیر پیشنهادی را برمی گرداند
        /// </summary>
        /// <returns>دسته چک جدید با مقادیر پیشنهادی</returns>
        Task<CheckBookViewModel> GetNewCheckBookAsync();

        /// <summary>
        /// به روش آسنکرون، دسته چک با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookNo">شماره یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شماره</returns>
        Task<CheckBookViewModel> GetCheckBookByNoAsync(int checkBookNo);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دسته چک را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="checkBook">دسته چک مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دسته چک ایجاد یا اصلاح شده</returns>
        Task<CheckBookViewModel> SaveCheckBookAsync(CheckBookViewModel checkBook);

        /// <summary>
        /// به روش آسنکرون، دسته چک مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی دسته چک مورد نظر برای حذف</param>
        Task DeleteCheckBookAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، اولین دسته چک را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>اولین دسته چک</returns>
        Task<CheckBookViewModel> GetFirstCheckBookAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دسته چک قبلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issueDate">تاریخ صدور دسته چک در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>دسته چک قبلی</returns>
        Task<CheckBookViewModel> GetPreviousCheckBookAsync(DateTime issueDate, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، اطلاعات دسته چک بعدی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issueDate">تاریخ صدور دسته چک در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>دسته چک بعدی</returns>
        Task<CheckBookViewModel> GetNextCheckBookAsync(DateTime issueDate, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، آخرین دسته چک را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آخرین دسته چک</returns>
        Task<CheckBookViewModel> GetLastCheckBookAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا دسته چک دارای برگه هست یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <returns>در حالتی که دسته چک دارای برگه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> HasPagesAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا دسته چک وجود دارد یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که دسته چک وجود داشته باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> ExistsCheckBookAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نام دسته چک مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="checkBook">دسته چکی که تکراری بودن نام آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن نام، در غیر این صورت مقدار بولی نادرست</returns>
        Task<bool> IsDuplicateCheckBookNameAsync(CheckBookViewModel checkBook);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حداقل یک برگ از دسته چک با چک ارتباط دارد یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که حداقل یک برگ از دسته چک ارتباط داشته باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsConnectedToCheckAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حداقل یک برگ از دسته چک ابطال شده است یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که حداقل یک برگ از دسته چک ابطال شده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> ExistsCancelledPage(int checkBookId);
    }
}
