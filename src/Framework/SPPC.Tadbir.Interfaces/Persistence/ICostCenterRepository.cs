using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات مراکز هزینه را تعریف می کند.
    /// </summary>
    public interface ICostCenterRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه ای را که در دوره مالی و شعبه مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<IList<CostCenterViewModel>> GetCostCentersAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه عددی یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه عددی یکی از شعب موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد مراکز هزینه تعریف شده در دوره مالی و شعبه مشخص شده</returns>
        Task<int> GetCountAsync(int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون،مرکز هزینه با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی یکی از مراکز هزینه موجود</param>
        /// <returns>مرکز هزینه مشخص شده با شناسه عددی</returns>
        Task<CostCenterViewModel> GetCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه زیرمجموعه را برای مرکز هزینه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مراکز هزینه زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetCostCenterChildrenAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای مرکز هزینه را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای مرکز هزینه</returns>
        Task<EntityViewModel> GetCostCenterMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک مرکز هزینه را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="costCenter">تفصیلی شناور مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی مرکز هزینه ایجاد یا اصلاح شده</returns>
        Task<CostCenterViewModel> SaveCostCenterAsync(CostCenterViewModel costCenter);

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="costCenterId">شناسه عددی مرکز هزینه مورد نظر برای حذف</param>
        Task DeleteCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا کد مرکز هزینه مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="costCenter">مدل نمایشی مرکز هزینه مورد نظر</param>
        /// <returns>اگر کد مرکز هزینه تکراری باشد مقدار "درست" و در غیر این صورت مقدار "نادرست" برمی گرداند</returns>
        Task<bool> IsDuplicateCostCenterAsync(CostCenterViewModel costCenter);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده توسط رکوردهای اطلاعاتی دیگر
        /// در حال استفاده است یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده در حال استفاده باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsUsedCostCenterAsync(int costCenterId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا مرکز هزینه انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <returns>در حالتی که مرکز هزینه مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int costCenterId);
    }
}
