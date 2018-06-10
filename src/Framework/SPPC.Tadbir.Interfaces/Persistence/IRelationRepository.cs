﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارتباطات بین مولفه های مختلف بردار حساب را تعریف می کند
    /// </summary>
    public interface IRelationRepository
    {
        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsAsync(
            int fpId, int branchId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsAsync(
            int fpId, int branchId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersAsync(
            int fpId, int branchId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل ارتباط در یک دوره مالی و شعبه مشخص را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه یکی از دوره های مالی موجود</param>
        /// <param name="branchId">شناسه یکی از شعبه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>پروژه های قابل ارتباط در یک دوره مالی و شعبه مشخص</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsAsync(
            int fpId, int branchId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountDetailAccountsAsync(int accountId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountCostCentersAsync(int accountId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از پروژه های مرتبط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountProjectsAsync(int accountId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetDetailAccountAccountsAsync(int detailId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetCostCenterAccountsAsync(int costCenterId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با پروژه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <returns>مجموعه ای از حساب های مرتبط با پروژه مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetProjectAccountsAsync(int projectId, bool useLeafItems = true);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت تفصیلی های شناور مرتبط با یک حساب را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات تفصیلی های شناور مرتبط با یک حساب</param>
        Task SaveAccountDetailAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت مراکز هزینه مرتبط با یک حساب را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات مراکز هزینه مرتبط با یک حساب</param>
        Task SaveAccountCostCentersAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت پروژه های مرتبط با یک حساب را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات پروژه های مرتبط با یک حساب</param>
        Task SaveAccountProjectsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت حساب های مرتبط با یک تفصیلی شناور را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک تفصیلی شناور</param>
        Task SaveDetailAccountAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت حساب های مرتبط با یک مرکز هزینه را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک مرکز هزینه</param>
        Task SaveCostCenterAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت حساب های مرتبط با یک پروژه را ذخیره می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک پروژه</param>
        Task SaveProjectAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک حساب با یک تفصیلی شناور را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه حساب پدر</param>
        /// <param name="faccountId">شناسه تفصیلی شناور مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های حساب پدر با تفصیلی شناور</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildAccountsRelatedToDetailAccount(int accountId, int faccountId);

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک حساب با یک مرکز هزینه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه حساب پدر</param>
        /// <param name="costCenterId">شناسه مرکز هزینه مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های حساب پدر با مرکز هزینه</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildAccountsRelatedToCostCenter(int accountId, int costCenterId);

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک حساب با یک پروژه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه حساب پدر</param>
        /// <param name="projectId">شناسه پروژه مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های حساب پدر با پروژه</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildAccountsRelatedToProject(int accountId, int projectId);

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک تفصیلی شناور با یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه تفصیلی شناور پدر</param>
        /// <param name="accountId">شناسه حساب مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های تفصیلی شناور پدر با حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildDetailAccountsRelatedToAccount(int faccountId, int accountId);

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک مرکز هزینه با یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه مرکز هزینه پدر</param>
        /// <param name="accountId">شناسه حساب مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های مرکز هزینه پدر با حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildCostCentersRelatedToAccount(int costCenterId, int accountId);

        /// <summary>
        /// به روش آسنکرون، ارتباطات زیرشاخه های یک پروژه با یک حساب را خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه پروژه پدر</param>
        /// <param name="accountId">شناسه حساب مرتبط</param>
        /// <returns>ارتباطات زیرشاخه های پروژه پدر با حساب</returns>
        Task<IList<AccountItemBriefViewModel>> GetChildProjectsRelatedToAccount(int projectId, int accountId);
    }
}
