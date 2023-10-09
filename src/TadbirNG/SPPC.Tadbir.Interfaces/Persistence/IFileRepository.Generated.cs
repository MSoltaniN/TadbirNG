using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.ProductScope;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت فایل ها را تعریف می کند
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه فایل ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از فایل ها تعریف شده</returns>
        Task<PagedList<FileViewModel>> GetFilesAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، فایل با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fileId">شناسه عددی یکی از فایل ها موجود</param>
        /// <returns>فایل مشخص شده با شناسه عددی</returns>
        Task<FileViewModel> GetFileAsync(int fileId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک فایل را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="file">فایل مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی فایل ایجاد یا اصلاح شده</returns>
        Task<FileViewModel> SaveFileAsync(FileViewModel file);

        /// <summary>
        /// به روش آسنکرون، فایل مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="fileId">شناسه عددی فایل مورد نظر برای حذف</param>
        Task DeleteFileAsync(int fileId);

        /// <summary>
        /// به روش آسنکرون، فایل ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="fileIds">مجموعه ای از شناسه های عددی فایل ها مورد نظر برای حذف</param>
        Task DeleteFilesAsync(IList<int> fileIds);
    }
}
