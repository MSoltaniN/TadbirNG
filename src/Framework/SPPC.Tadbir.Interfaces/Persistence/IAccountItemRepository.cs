using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات متداول برای کار با مولفه های بردار حساب با ساختار درختی را تعریف می کند
    /// </summary>
    public interface IAccountItemRepository
    {
        /// <summary>
        /// حساب های زیرمجموعه را برای حساب مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکی از حساب های موجود</param>
        /// <returns>مدل نمایشی حساب های زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildAccounts(int accountId);

        /// <summary>
        /// شناورهای زیرمجموعه را برای تفصیلی شناور مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکی از تفصیلی های شناور موجود</param>
        /// <returns>مدل نمایشی تفصیلی های شناور زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildDetailAccounts(int detailId);

        /// <summary>
        /// مراکز هزینه زیرمجموعه را برای مرکز هزینه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکی از مراکز هزینه موجود</param>
        /// <returns>مدل نمایشی مراکز هزینه زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildCostCenters(int costCenterId);

        /// <summary>
        /// پروژه های زیرمجموعه را برای پروژه مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکی از پروژه های موجود</param>
        /// <returns>مدل نمایشی پروژه های زیرمجموعه</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildProjects(int projectId);

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه سرفصل های حسابداری در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafAccountsAsync(int fpId, int branchId);

        /// <summary>
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه تفصیلی های شناور در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafDetailAccountsAsync(int fpId, int branchId);

        /// <summary>
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه مراکز هزینه در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafCostCentersAsync(int fpId, int branchId);

        /// <summary>
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در آخرین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه پروژه ها در آخرین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetLeafProjectsAsync(int fpId, int branchId);

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه سرفصل های حسابداری در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootAccountsAsync(int fpId, int branchId);

        /// <summary>
        /// مجموعه ای از تفصیلی های شناور در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه تفصیلی های شناور در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootDetailAccountsAsync(int fpId, int branchId);

        /// <summary>
        /// مجموعه ای از مراکز هزینه در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه مراکز هزینه در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootCostCentersAsync(int fpId, int branchId);

        /// <summary>
        /// مجموعه ای از پروژه ها در دوره مالی و شعبه مشخص شده که در بالاترین سطح ساختار درختی قرار دارند
        /// </summary>
        /// <param name="fpId">کد یکتای یکی از دوره های مالی موجود</param>
        /// <param name="branchId">کد یکتای یکی از شعبه های موجود</param>
        /// <returns>مجموعه پروژه ها در بالاترین سطح</returns>
        Task<IList<AccountItemBriefViewModel>> GetRootProjectsAsync(int fpId, int branchId);
    }
}
