﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت شعب را تعریف میکند.
    /// </summary>
    public interface IBranchRepository
    {
        /// <summary>
        /// به روش آسنکرون، کلیه شعب سازمانی را که در شرکت جاری تعریف شده اند
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از شعب سازمانی تعریف شده در شرکت جاری</returns>
        Task<PagedList<BranchViewModel>> GetBranchesAsync(GridOptions gridOptions);

        /// <summary>
        /// به روش آسنکرون، شعبه سازمانی با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه عددی یکی از شعب سازمانی موجود</param>
        /// <returns>شعبه سازمانی مشخص شده با شناسه عددی</returns>
        Task<BranchViewModel> GetBranchAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، کلیه شعب سازمانی زیرمجموعه یک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parentId">شناسه دیتابیسی شعبه والد مورد نظر</param>
        /// <returns>مجموعه ای از شعب سازمانی زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetBranchChildrenAsync(int? parentId);

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به یک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <returns>اطلاعات نمایشی نقش های دارای دسترسی</returns>
        Task<RelatedItemsViewModel> GetBranchRolesAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های دارای دسترسی به یک شعبه را ذخیره می کند
        /// </summary>
        /// <param name="branchRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        Task SaveBranchRolesAsync(RelatedItemsViewModel branchRoles);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک شعبه سازمانی را در محل ذخیره ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="branch">شعبه سازمانی مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی شعبه سازمانی ایجاد یا اصلاح شده</returns>
        Task<BranchViewModel> SaveBranchAsync(BranchViewModel branch);

        /// <summary>
        /// به روش آسنکرون، شعبه سازمانی مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه عددی شعبه سازمانی مورد نظر برای حذف</param>
        Task DeleteBranchAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، شعبه سازمانی مشخص شده با شناسه عددی را به همراه کلیه اطلاعات وابسته به آن حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه عددی شعبه سازمانی مورد نظر برای حذف</param>
        Task DeleteBranchWithDataAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، شعبه های مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="items">مجموعه شناسه های دیتابیسی سطرهای مورد نظر برای حذف</param>
        Task DeleteBranchesAsync(IEnumerable<int> items);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شعبه سازمانی انتخاب شده دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="branchId">شناسه یکتای یکی از شعب سازمانی موجود</param>
        /// <returns>در حالتی که شعبه سازمانی مشخص شده دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> HasChildrenAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا شعبه مشخص شده قابل حذف است یا نه؟
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر</param>
        /// <returns>اگر شعبه مورد نظر در برنامه به طور مستقیم استفاده شده باشد
        /// مقدار "نادرست" و در غیر این صورت مقدار "درست" را برمی گرداند</returns>
        Task<bool> CanDeleteBranchAsync(int branchId);

        /// <summary>
        /// به روش آسنکرون، قواعد کاری تعریف شده را برای شعبه داده شده بررسی می کند
        /// </summary>
        /// <param name="branch">مدل نمایشی شعبه مورد بررسی</param>
        /// <returns>در صورت نبود اشکال، مقدار بولی "درست" و در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> IsValidBranchAsync(BranchViewModel branch);
    }
}
