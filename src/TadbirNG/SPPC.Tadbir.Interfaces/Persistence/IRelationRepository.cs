using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارتباطات بین مولفه های مختلف بردار حساب را تعریف می کند
    /// </summary>
    public interface IRelationRepository
    {
        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سرفصل های حسابداری قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تفصیلی های شناور قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsAsync(
            bool useLeafItems = true, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مراکز هزینه قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersAsync(
            bool useLeafItems = true, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل ارتباط در یک دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>پروژه های قابل ارتباط در یک دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsAsync(
            bool useLeafItems = true, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، سرفصل های حسابداری قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>سرفصل های حسابداری قابل استفاده در دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetUsableAccountsLookupAsync(
            GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تفصیلی های شناور قابل استفاده در دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetUsableDetailAccountsLookupAsync(
            GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مراکز هزینه قابل استفاده در دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetUsableCostCentersLookupAsync(
            GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، پروژه های قابل استفاده در دوره مالی و شعبه جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>پروژه های قابل استفاده در دوره مالی و شعبه جاری</returns>
        Task<IList<AccountItemBriefViewModel>> GetUsableProjectsLookupAsync(
            GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مدل نمایشی بردار حساب داده شده را در ارتباطات موجود جستجو کرده
        /// و در صورت پیدا نکردن بردار حساب، وضعیت اشکال موجود را به صورت شناسه متن چندزبانه خطا برمی گرداند
        /// </summary>
        /// <param name="fullAccount">مدل نمایشی بردار حساب مورد جستجو</param>
        /// <returns>در صورت پیدا نکردن بردار حساب، شناسه متن چندزبانه خطا و در صورت پیدا کردن
        /// رشته خالی را برمی گرداند</returns>
        Task<string> LookupFullAccountAsync(FullAccountViewModel fullAccount);

        /// <summary>
        /// به روش سنکرون، معتبر بودن بردار حساب را با توجه به سرفصل های حسابداری داده شده بررسی میکند
        /// </summary>
        /// <param name="account">اطلاعات نمایشی خلاصه حساب</param>
        /// <param name="detailAccount">اطلاعات نمایشی خلاصه تفصیلی شناور</param>
        /// <param name="costCenter">اطلاعات نمایشی خلاصه مرکز هزینه</param>
        /// <param name="project">اطلاعات نمایشی خلاصه پروژه</param>
        /// <returns>در صورت معتبر بودن بردارحساب مقدار درست در غیر اینصورت مقدار نادرست برمیگرداند</returns>
        bool LookupFullAccount(
            AccountItemBriefViewModel account,
            AccountItemBriefViewModel detailAccount,
            AccountItemBriefViewModel costCenter,
            AccountItemBriefViewModel project);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از تفصیلی های شناور مرتبط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountDetailAccountsAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از مراکز هزینه مرتبط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountCostCentersAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های مرتبط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <param name="leafOnly">مشخص می کند که آیا ارتباطات موجود فقط در آخرین سطح مورد نظر است یا نه</param>
        /// <returns>مجموعه ای از پروژه های مرتبط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetAccountProjectsAsync(
            int accountId, GridOptions gridOptions = null, bool leafOnly = true);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با تفصیلی شناور مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetDetailAccountAccountsAsync(
            int detailId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با مرکز هزینه مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetCostCenterAccountsAsync(
            int costCenterId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های مرتبط با پروژه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های مرتبط با پروژه مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetProjectAccountsAsync(
            int projectId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور مرتبط با یک حساب را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات تفصیلی های شناور مرتبط با یک حساب</param>
        Task AddAccountDetailAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، تفصیلی های شناور مرتبط با یک حساب را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات تفصیلی های شناور مرتبط با یک حساب</param>
        Task RemoveAccountDetailAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه مرتبط با یک حساب را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات مراکز هزینه مرتبط با یک حساب</param>
        Task AddAccountCostCentersAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه مرتبط با یک حساب را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات مراکز هزینه مرتبط با یک حساب</param>
        Task RemoveAccountCostCentersAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، پروژه های مرتبط با یک حساب را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات پروژه های مرتبط با یک حساب</param>
        Task AddAccountProjectsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، پروژه های مرتبط با یک حساب را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات پروژه های مرتبط با یک حساب</param>
        Task RemoveAccountProjectsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک تفصیلی شناور را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک تفصیلی شناور</param>
        Task AddDetailAccountAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک تفصیلی شناور را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک تفصیلی شناور</param>
        Task RemoveDetailAccountAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک مرکز هزینه را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک مرکز هزینه</param>
        Task AddCostCenterAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک مرکز هزینه را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک مرکز هزینه</param>
        Task RemoveCostCenterAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک پروژه را اضافه می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک پروژه</param>
        Task AddProjectAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، حساب های مرتبط با یک پروژه را حذف می کند
        /// </summary>
        /// <param name="relations">اطلاعات حساب های مرتبط با یک پروژه</param>
        Task RemoveProjectAccountsAsync(AccountItemRelationsViewModel relations);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از تفصیلی های شناور قابل ارتباط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از تفصیلی های شناور قابل ارتباط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableDetailAccountsForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از مراکز هزینه قابل ارتباط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از مراکز هزینه قابل ارتباط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableCostCentersForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از پروژه های قابل ارتباط با حساب مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه یکتای یکی از حساب های موجود</param>
        /// <param name="useLeafItems">مشخص می کند که آیا ارتباطات فقط در آخرین سطح برقرار می شوند یا نه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از پروژه های قابل ارتباط با حساب مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableProjectsForAccountAsync(
            int accountId, bool useLeafItems = true, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با تفصیلی شناور مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="detailId">شناسه یکتای یکی از تفصیلی های شناور موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با تفصیلی شناور مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForDetailAccountAsync(
            int detailId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با مرکز هزینه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="costCenterId">شناسه یکتای یکی از مراکز هزینه موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با مرکز هزینه مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForCostCenterAsync(
            int costCenterId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، مجموعه ای از حساب های قابل ارتباط با پروژه مشخص شده را
        /// از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه یکتای یکی از پروژه های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از حساب های قابل ارتباط با پروژه مشخص شده</returns>
        Task<IList<AccountItemBriefViewModel>> GetConnectableAccountsForProjectAsync(
            int projectId, GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک تفصیلی شناور جدید انجام می دهد
        /// </summary>
        /// <param name="insertedDetailId">شناسه دیتابیسی تفصیلی شناور ایجاد شده</param>
        Task OnDetailAccountInsertedAsync(int insertedDetailId);

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک مرکز هزینه جدید انجام می دهد
        /// </summary>
        /// <param name="insertedCenterId">شناسه دیتابیسی مرکز هزینه ایجاد شده</param>
        Task OnCostCenterInsertedAsync(int insertedCenterId);

        /// <summary>
        /// به روش آسنکرون، هماهنگ سازی ارتباطات موجود را بعد از ایجاد یک پروژه جدید انجام می دهد
        /// </summary>
        /// <param name="insertedProjectId">شناسه دیتابیسی پروژه ایجاد شده</param>
        Task OnProjectInsertedAsync(int insertedProjectId);
    }
}
