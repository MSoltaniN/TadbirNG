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
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های حسابداری در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafAccountsAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafDetailAccountsAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafCostCentersAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه ها در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafProjectsAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه سرفصل های حسابداری در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootAccountsAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه تفصیلی های شناور در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه مراکز هزینه در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootCostCentersAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);

        /// <summary>
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="userContext">اطلاعات دسترسی کاربر به منابع محدود شده مانند نقش ها، دوره های مالی و شعبه ها</param>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه پروژه ها در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootProjectsAsync(
            UserContextViewModel userContext, int fpId, int branchId, GridOptions gridOptions = null);
    }
}
