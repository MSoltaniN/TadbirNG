using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات تفصیلی های شناور را تعریف می کند.
    /// </summary>
    public interface IDetailAccountRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه تفصیلی های شناوری را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="activeState">وضعیت مورد نظر برای نمایش اطلاعات بر اساس وضعیت فعال و غیر فعال</param>
        /// <returns>مجموعه ای از تفصیلی های شناور تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<PagedList<DetailAccountViewModel>> GetDetailAccountsAsync(
            GridOptions gridOptions, ActiveState activeState = ActiveState.Active);

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه عددی یکی از تفصیلی های شناور موجود</param>
        /// <returns>تفصیلی شناور مشخص شده با شناسه عددی</returns>
        Task<DetailAccountViewModel> GetDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، برای تفصیلی شناور والد مشخص شده شناور زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی تفصیلی شناور والد - اگر مقدار نداشته باشد شناور جدید
        /// در سطح اول پیشنهاد می شود</param>
        /// <returns>مدل نمایشی تفصیلی شناور پیشنهادی</returns>
        Task<DetailAccountViewModel> GetNewChildDetailAccountAsync(int? parentId);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور در سطح اول را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه تفصیلی های شناور در سطح اول</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync();

        /// <summary>
        /// به روش آسنکرون، شناورهای زیرمجموعه را برای تفصیلی شناور مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی های شناور زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetDetailAccountChildrenAsync(int detailId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک تفصیلی شناور را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="detailAccount">تفصیلی شناور مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی تفصیلی شناور ایجاد یا اصلاح شده</returns>
        Task<DetailAccountViewModel> SaveDetailAccountAsync(DetailAccountViewModel detailAccount);

        /// <summary>
        /// به روش آسنکرون، تفصیلی شناور مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="faccountId">شناسه عددی تفصیلی شناور مورد نظر برای حذف</param>
        Task DeleteDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="detailIds">مجموعه ای از شناسه های عددی تفصیلی های مورد نظر برای حذف</param>
        Task DeleteDetailAccountsAsync(IList<int> detailIds);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد تفصیلی شناور مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="detailAccount">مدل نمایشی تفصیلی شناور مورد نظر</param>
        /// <returns>اگر کد تفصیلی شناور تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateFullCodeAsync(DetailAccountViewModel detailAccount);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که نام تفصیلی شناور مورد نظر بین تفصیلی های همسطح
        /// با والد یکسان تکراری است یا نه
        /// </summary>
        /// <param name="detailAccount">مدل نمایشی تفصیلی شناور مورد نظر</param>
        /// <returns>اگر نام تفصیلی شناور تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateNameAsync(DetailAccountViewModel detailAccount);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRelatedDetailAccountAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا تفصیلی شناور انتخاب شده دارای شناور زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="faccountId">شناسه یکتای یکی از شناور های موجود</param>
        /// <returns>در حالتی که تفصیلی شناور مشخص شده دارای شناور زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int faccountId);

        /// <summary>
        /// به روش آسنکرون، کد کامل تفصیلی شناور با شناسه داده شده را برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی یکی از تفصیلی های شناور موجود</param>
        /// <returns>اگر تفصیلی شناور با شناسه داده شده وجود نداشته باشد مقدار خالی
        /// و در غیر این صورت کد کامل را برمی گرداند</returns>
        Task<string> GetDetailAccountFullCodeAsync(int faccountId);
    }
}
