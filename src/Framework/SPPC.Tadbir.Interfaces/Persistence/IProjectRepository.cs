using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات پروژه ها را تعریف می کند.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه پروژه هایی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<IList<ProjectViewModel>> GetProjectsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، کلیه پروژه هایی را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// به صورت مجموعه ای از کد و نام خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<IList<KeyValue>> GetProjectsLookupAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد پروژه های تعریف شده در دوره مالی و شعبه جاری را
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TViewModel">نوع مدل نمایشی که برای نمایش اطلاعات از آن استفاده می شود</typeparam>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد پروژه های تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<int> GetCountAsync<TViewModel>(GridOptions gridOptions = null)
            where TViewModel : class, new();

        /// <summary>
        /// به روش آسنکرون، پروژه با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه عددی یکی از پروژه های موجود</param>
        /// <returns>پروژه مشخص شده با شناسه عددی</returns>
        Task<ProjectViewModel> GetProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، برای پروژه والد مشخص شده پروژه زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی پروژه والد - اگر مقدار نداشته باشد پروژه جدید
        /// در سطح کل پیشنهاد می شود</param>
        /// <returns>مدل نمایشی پروژه پیشنهادی</returns>
        Task<ProjectViewModel> GetNewChildProjectAsync(int? parentId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های سطح اول را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه پروژه های سطح اول</returns>
        Task<IList<AccountItemBriefViewModel>> GetProjectsLedgerAsync();

        /// <summary>
        /// به روش آسنکرون، پروژه های زیرمجموعه را برای پروژه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکی از پروژه های موجود</param>
        /// <returns>مدل نمایشی پروژه های زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetProjectChildrenAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک پروژه را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="project">پروژه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی پروژه ایجاد یا اصلاح شده</returns>
        Task<ProjectViewModel> SaveProjectAsync(ProjectViewModel project);

        /// <summary>
        /// به روش آسنکرون، پروژه مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="projectId">شناسه عددی پروژه مورد نظر برای حذف</param>
        Task DeleteProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، پروژه های مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="projectIds">مجموعه ای از شناسه های عددی پروژه های مورد نظر برای حذف</param>
        Task DeleteProjectsAsync(IList<int> projectIds);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد پروژه مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="project">مدل نمایشی پروژه مورد نظر</param>
        /// <returns>اگر کد پروژه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateProjectAsync(ProjectViewModel project);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا پروژه انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <returns>در حالتی که پروژه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا پروژه انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <returns>در حالتی که پروژه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRelatedProjectAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا پروژه انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <returns>در حالتی که پروژه مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int projectId);

        /// <summary>
        /// به روش آسنکرون، مقدار فیلد FullCode والد هر پروژه را برمیگرداند
        /// </summary>
        /// <param name="parentId">شناسه والد هر پروژه</param>
        /// <returns>اگر پروژه والد نداشته باشد مقدار خالی و اگر والد داشته باشد مقدار FullCode والد را برمیگرداند</returns>
        Task<string> GetProjectFullCodeAsync(int parentId);
    }
}
