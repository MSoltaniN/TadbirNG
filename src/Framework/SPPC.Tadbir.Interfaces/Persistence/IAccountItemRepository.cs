using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات متداول برای کار با مولفه های بردار حساب با ساختار درختی را تعریف می کند
    /// </summary>
    public interface IAccountItemRepository
    {
        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های حسابداری در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafDetailAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafCostCentersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه ها در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafProjectsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های حسابداری در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootCostCentersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه ها در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootProjectsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مانده حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی حساب مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetAccountBalanceAsync(int accountId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده تفصیلی شناور مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetDetailAccountBalanceAsync(int faccountId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده مرکز هزینه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetCostCenterBalanceAsync(int ccenterId, DateTime date);

        /// <summary>
        /// به روش آسنکرون، مانده پروژه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        Task<decimal> GetProjectBalanceAsync(int projectId, DateTime date);

        /// <summary>
        /// اطلاعات محیطی کاربر جاری برنامه را برای برای خواندن اطلاعات وابسته به شعبه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        void SetCurrentContext(UserContextViewModel userContext);
    }
}
