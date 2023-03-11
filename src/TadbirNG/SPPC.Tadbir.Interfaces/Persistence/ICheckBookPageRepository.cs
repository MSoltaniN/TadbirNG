using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت برگه های چک را تعریف می کند
    /// </summary>
    public interface ICheckBookPageRepository
    {
        /// <summary>
        /// به روش آسنکرون، وضعیت یک برگه چک را اصلاح می کند
        /// </summary>
        /// <param name="checkBookPageId">شناسه عددی یکی از برگه های چک موجود</param>
        /// <param name="status">وضعیت برگه چک</param>
        /// <returns>اطلاعات نمایشی برگه چک ایجاد یا اصلاح شده</returns>
        Task<CheckBookPageViewModel> ChangeStateCheckAsync(int checkBookPageId, CheckBookPageState status);

        /// <summary>
        /// به روش آسنکرون، برگه های چک مشخص شده با شناسه دسته چک را حذف می کند
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک جهت حذف برگه های چک</param>
        Task DeleteCheckBookPagesAsync(int checkBookId);

        /// <summary>
        ///  به روش آسنکرون، اطلاعات نمایشی برگه های یک دسته چک جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <returns>برگه های چک مشخص شده با شناسه عددی</returns>
        Task<PagedList<CheckBookPageViewModel>> CreatePagesAsync(int checkBookId);

        /// <summary>
        /// به روش آسنکرون، برگه های های یک دسته چک مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>برگه های چک مشخص شده با شناسه عددی</returns>
        Task<PagedList<CheckBookPageViewModel>> GetPagesAsync(int checkBookId, GridOptions gridOptions = null);
    }
}
