using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شعب را پیاده سازی میکند.
    /// </summary>
    public interface IBranchRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه شعب سازمانی را که در شرکت مشخص شده تعریف شده اند،
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شعب سازمانی تعریف شده در شرکت مشخص شده</returns>
        Task<IList<BranchViewModel>> GetBranchesAsync(int companyId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تعداد شعب سازمانی تعریف شده در شرکت مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="companyId"> شناسه عددی یکی از شرکت های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد شعب  سازمانی تعریف شده در شرکت مشخص شده</returns>
        Task<int> GetCountAsync(int companyId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون،شعبه سازمانی با شناسه عددی مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه عددی یکی از شعب سازمانی موجود</param>
        /// <returns>شعبه سازمانی مشخص شده با شناسه عددی</returns>
        Task<BranchViewModel> GetBranchAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات فراداده ای تعریف شده برای شعبه سازمانی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات فراداده ای تعریف شده برای شعبه سازمانی</returns>
        Task<EntityItemViewModel<BranchViewModel>> GetBranchMetadataAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شعبه سازمانی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="branch">شعبه سازمانی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شعبه سازمانی ایجاد یا اصلاح شده</returns>
        Task<BranchViewModel> SaveBranchAsync(BranchViewModel branch);

        /// <summary>
        /// به روش آسنکرون، شعبه سازمانی مشخص شده با شناسه عددی را از محل ذخیره حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه عددی شعبه سازمانی مورد نظر برای حذف</param>
        Task DeleteBranchAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شعبه سازمانی انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="branchId">شناسه یکتای یکی از شعب سازمانی موجود</param>
        /// <returns>در حالتی که شعبه سازمانی مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool?> HasChildrenAsync(int branchId);
    }
}
