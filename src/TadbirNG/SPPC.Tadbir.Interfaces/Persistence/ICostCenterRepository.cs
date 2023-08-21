using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مراکز هزینه را تعریف می کند.
    /// </summary>
    public interface ICostCenterRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه ای را که در دوره مالی و شعبه جاری تعریف شده اند،
        /// از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="activeState">وضعیت مورد نظر برای نمایش اطلاعات بر اساس وضعیت فعال و غیر فعال</param>
        /// <returns>مجموعه ای از مراکز هزینه تعریف شده در دوره مالی و شعبه جاری</returns>
        Task<PagedList<CostCenterViewModel>> GetCostCentersAsync(
            GridOptions gridOptions, ActiveState activeState = ActiveState.Active);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه در سطح اول را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدل نمایشی خلاصه مراکز هزینه در سطح اول</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootCostCentersAsync();

        /// <summary>
        /// به روش آسنکرون،مرکز هزینه با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی یکی از مراکز هزینه موجود</param>
        /// <returns>مرکز هزینه مشخص شده با شناسه عددی</returns>
        Task<CostCenterViewModel> GetCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، برای مرکز هزینه والد مشخص شده مرکز زیرمجموعه جدیدی پیشنهاد داده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی مرکز هزینه والد - اگر مقدار نداشته باشد مرکز جدید
        /// در سطح اول پیشنهاد می شود</param>
        /// <returns>مدل نمایشی مرکز هزینه پیشنهادی</returns>
        Task<CostCenterViewModel> GetNewChildCostCenterAsync(int? parentId);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه زیرمجموعه را برای مرکز هزینه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مراکز هزینه زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetCostCenterChildrenAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک مرکز هزینه را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="costCenter">تفصیلی شناور مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی مرکز هزینه ایجاد یا اصلاح شده</returns>
        Task<CostCenterViewModel> SaveCostCenterAsync(CostCenterViewModel costCenter);

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی مرکز هزینه مورد نظر برای حذف</param>
        Task DeleteCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، مراکز مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="centerIds">مجموعه ای از شناسه های عددی مراکز مورد نظر برای حذف</param>
        Task DeleteCostCentersAsync(IList<int> centerIds);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد مرکز هزینه مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="costCenter">مدل نمایشی مرکز هزینه مورد نظر</param>
        /// <returns>اگر کد مرکز هزینه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateFullCodeAsync(CostCenterViewModel costCenter);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که نام مرکز هزینه مورد نظر بین مراکز همسطح با والد یکسان تکراری است یا نه
        /// </summary>
        /// <param name="costCenter">مدل نمایشی مرکز هزینه مورد نظر</param>
        /// <returns>اگر نام مرکز هزینه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateNameAsync(CostCenterViewModel costCenter);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده توسط ارتباطات موجود برای بردار حساب
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مرکز هزینه های موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRelatedCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، کد کامل مرکز هزینه با شناسه داده شده را برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه دیتابیسی یکی از مراکز هزینه موجود</param>
        /// <returns>اگر مرکز هزینه با شناسه داده شده وجود نداشته باشد مقدار خالی
        /// و در غیر این صورت کد کامل را برمی گرداند</returns>
        Task<string> GetCostCenterFullCodeAsync(int costCenterId);
    }
}
