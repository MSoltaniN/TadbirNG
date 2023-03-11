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
        Task<CheckBookViewModel> GetCheckBookByNoAsync(string checkBookNo);

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
        /// به روش آسنکرون، مشخص می کند که آیا دسته چک دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <returns>در حالتی که دسته چک دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> HasChildrenAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا قسمت والد دسته چک وجود دارد یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که دسته چک وجود داشته باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> HasParentAsync(int checkBookId);

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
        Task<bool> HasConnectedToCheckAsync(int checkBookId);

    }
}
