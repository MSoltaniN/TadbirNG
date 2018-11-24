using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///  عملیات مورد نیاز برای مدیریت اطلاعات دوره مالی را تعریف می کند.
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
        /// به روش آسنکرون، تعداد اسناد مالی ایجاد شده در دوره مالی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>تعداد اسناد مالی ایجاد شده در دوره مالی</returns>
        Task<int> GetVoucherCountAsync(int fpId);

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
        Task<ViewViewModel> GetFiscalPeriodMetadataAsync();

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
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="fperiodId">شناسه عددی دوره مالی مورد نظر برای حذف</param>
        Task DeleteFiscalPeriodAsync(int fperiodId);

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
        /// به روش آسنکرون، مشخص می کند که آیا دوره مالی مشخص شده قابل حذف است یا نه؟
        /// </summary>
        /// <param name="fperiodId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اگر دوره مالی مورد نظر در برنامه به طور مستقیم استفاده شده باشد
        /// مقدار "نادرست" و در غیر این صورت مقدار "درست" را برمی گرداند</returns>
        Task<bool> CanDeleteFiscalPeriodAsync(int fperiodId);

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای ایجاد لاگ های عملیاتی تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}
