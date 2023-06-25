using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///  عملیات مورد نیاز برای مدیریت اطلاعات دوره مالی را تعریف می کند.
    /// </summary>
    public interface IFiscalPeriodRepository : IRepositoryBase
    {
        /// <summary>
        /// به روش آسنکرون، کلیه دوره های مالی را که در شرکت جاری تعریف شده اند
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دوره های مالی تعریف شده در شرکت جاری</returns>
        Task<PagedList<FiscalPeriodViewModel>> GetFiscalPeriodsAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون،دوره مالی با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی یکی از دوره های مالی</param>
        /// <returns>دوره مالی مشخص شده با شناسه عددی</returns>
        Task<FiscalPeriodViewModel> GetFiscalPeriodAsync(int fperiodId);

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک دوره مالی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        Task<RelatedItemsViewModel> GetFiscalPeriodRolesAsync(int fpId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های دارای دسترسی به یک دوره مالی را ذخیره می کند
        /// </summary>
        /// <param name="periodRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        Task SaveFiscalPeriodRolesAsync(RelatedItemsViewModel periodRoles);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دوره مالی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="fiscalPeriod">دوره مالی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دوره مالی ایجاد یا اصلاح شده</returns>
        Task<FiscalPeriodViewModel> SaveFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod);

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی دوره مالی مورد نظر برای حذف</param>
        Task DeleteFiscalPeriodAsync(int fperiodId);

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه عددی را به همراه کلیه اطلاعات وابسته به آن حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه عددی دوره مالی مورد نظر برای حذف</param>
        Task DeleteFiscalPeriodWithDataAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، دوره های مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteFiscalPeriodsAsync(IEnumerable<int> items);

        /// <summary>
        /// مشخص میکند که آیا تاریخ شروع دوره مالی بعد از تاریخ پایان دوره مالی است؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>اگر تاریخ شروع دوره مالی بعد از تاریخ پایان دوره مالی باشد مقدار "درست"
        /// در غیر این صورت مقدار "نادرست" برمیگرداند</returns>
        bool IsStartDateAfterEndDate(FiscalPeriodViewModel fiscalPeriod);

        /// <summary>
        /// به روش آسنکرون، مشخص میکند که آیا این دوره مالی با سایر دوره های مالی شرکت مربوطه هم پوشانی دارد یا خیر؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی هم پوشان با مدل نمایشی مورد نظر وجود داشته باشد مقدار "درست" در غیر این صورت مقدار "نادرست" برمیگرداند</returns>
        Task<bool> IsOverlapFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod);

        /// <summary>
        /// مشخص می کند که آیا دوره مالی داده شده از نظر تاریخ شروع رو به جلو است یا نه؟
        /// </summary>
        /// <param name="fiscalPeriod">مدل نمایشی دوره مالی مورد نظر</param>
        /// <returns>در صورتی که تاریخ شروع دوره بعد از تاریخ پایان دوره قبل باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsProgressiveFiscalPeriodAsync(FiscalPeriodViewModel fiscalPeriod);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا دوره مالی مشخص شده قابل حذف است یا نه؟
        /// </summary>
        /// <param name="fiscalPeriodId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی مورد نظر در برنامه به طور مستقیم استفاده شده باشد
        /// مقدار "نادرست" و در غیر این صورت مقدار "درست" را برمی گرداند</returns>
        Task<bool> CanDeleteFiscalPeriodAsync(int fiscalPeriodId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا دوره مالی مشخص شده به همراه اطلاعاتش قابل حذف است یا نه؟
        /// </summary>
        /// <param name="fiscalPeriodId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی مورد نظر آخرین دوره مالی باشد مقدار بولی درست و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> CanDeleteFiscalPeriodWithDataAsync(int fiscalPeriodId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که دوره مالی با شناسه دیتابیسی داده شده
        /// سندی با وضعیت ثبت، ثبت قطعی، تأییدشده یا تصویب شده دارد یا نه
        /// </summary>
        /// <param name="fiscalPeriodId"></param>
        /// <returns></returns>
        Task<bool> HasCommittedVouchersAsync(int fiscalPeriodId);
    }
}
