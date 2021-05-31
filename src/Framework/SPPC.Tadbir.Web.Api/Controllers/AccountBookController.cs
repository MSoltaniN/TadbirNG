using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class AccountBookController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="config"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public AccountBookController(IAccountBookRepository repository, IConfigRepository config,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
            _configRepository = config;
        }

        #region Account Books

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/by-row
        [HttpGet]
        [Route(AccountBookApi.AccountBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookByRowAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.Account, accountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/voucher-sum
        [HttpGet]
        [Route(AccountBookApi.AccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookVoucherSumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.Account, accountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/daily-sum
        [HttpGet]
        [Route(AccountBookApi.AccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookDailySumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.Account, accountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/monthly-sum
        [HttpGet]
        [Route(AccountBookApi.AccountBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookMonthlySumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.Account, accountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/by-row/by-branch
        [HttpGet]
        [Route(AccountBookApi.AccountBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetAccountBookByRowByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.Account, accountId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/voucher-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.AccountBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetAccountBookVoucherSumByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.Account, accountId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/daily-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.AccountBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetAccountBookDailySumByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.Account, accountId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/account/{accountId:min(1)}/monthly-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.AccountBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetAccountBookMonthlySumByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.Account, accountId, from, to, true);
        }

        #endregion

        #region DetailAccount Books

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/by-row
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookByRowAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.DetailAccount, faccountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/voucher-sum
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookVoucherSumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.DetailAccount, faccountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/daily-sum
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookDailySumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.DetailAccount, faccountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/monthly-sum
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookMonthlySumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.DetailAccount, faccountId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/by-row/by-branch
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetDetailAccountBookByRowByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.DetailAccount, faccountId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/voucher-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetDetailAccountBookVoucherSumByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.DetailAccount, faccountId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/daily-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetDetailAccountBookDailySumByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.DetailAccount, faccountId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/faccount/{faccountId:min(1)}/monthly-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.DetailAccountBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetDetailAccountBookMonthlySumByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.DetailAccount, faccountId, from, to, true);
        }

        #endregion

        #region CostCenter Books

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/by-row
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookByRowAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.CostCenter, ccenterId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/voucher-sum
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookVoucherSumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.CostCenter, ccenterId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/daily-sum
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookDailySumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.CostCenter, ccenterId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/monthly-sum
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookMonthlySumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.CostCenter, ccenterId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/by-row/by-branch
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetCostCenterBookByRowByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.CostCenter, ccenterId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/voucher-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetCostCenterBookVoucherSumByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.CostCenter, ccenterId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/daily-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetCostCenterBookDailySumByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.CostCenter, ccenterId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/ccenter/{ccenterId:min(1)}/monthly-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.CostCenterBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetCostCenterBookMonthlySumByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.CostCenter, ccenterId, from, to, true);
        }

        #endregion

        #region Project Books

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/by-row
        [HttpGet]
        [Route(AccountBookApi.ProjectBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookByRowAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.Project, projectId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/voucher-sum
        [HttpGet]
        [Route(AccountBookApi.ProjectBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookVoucherSumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.Project, projectId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/daily-sum
        [HttpGet]
        [Route(AccountBookApi.ProjectBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookDailySumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.Project, projectId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/monthly-sum
        [HttpGet]
        [Route(AccountBookApi.ProjectBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookMonthlySumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.Project, projectId, from, to);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/by-row/by-branch
        [HttpGet]
        [Route(AccountBookApi.ProjectBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetProjectBookByRowByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewId.Project, projectId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/voucher-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.ProjectBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetProjectBookVoucherSumByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewId.Project, projectId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/daily-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.ProjectBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetProjectBookDailySumByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewId.Project, projectId, from, to, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/accbook/project/{projectId:min(1)}/monthly-sum/by-branch
        [HttpGet]
        [Route(AccountBookApi.ProjectBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)(AccountBookPermissions.View | AccountBookPermissions.ByBranch))]
        public async Task<IActionResult> GetProjectBookMonthlySumByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewId.Project, projectId, from, to, true);
        }

        #endregion

        #region Account Item Navigation

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        // GET: api/accbook/view/{viewId:min(1)}/item/{itemId:min(1)}/prev
        [HttpGet]
        [Route(AccountBookApi.PreviousEnvironmentItemUrl)]
        public async Task<IActionResult> GetPreviousEnvironmentItemAsync(int viewId, int itemId)
        {
            var previous = await _repository.GetPreviousAccountItemAsync(viewId, itemId);
            return JsonReadResult(previous);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        // GET: api/accbook/view/{viewId:min(1)}/item/{itemId:min(1)}/next
        [HttpGet]
        [Route(AccountBookApi.NextEnvironmentItemUrl)]
        public async Task<IActionResult> GetNextEnvironmentItemAsync(int viewId, int itemId)
        {
            var next = await _repository.GetNextAccountItemAsync(viewId, itemId);
            return JsonReadResult(next);
        }

        #endregion

        private AccountBookParameters GetParameters(
            AccountBookMode bookMode, int viewId, int accountId, DateTime? from, DateTime? to, bool byBranch)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            return new AccountBookParameters()
            {
                Mode = bookMode,
                FromDate = from.Value,
                ToDate = to.Value,
                ViewId = viewId,
                ItemId = accountId,
                IsByBranch = byBranch,
                GridOptions = gridOptions
            };
        }

        private async Task<IActionResult> AccountBookResultAsync(
            AccountBookMode bookMode, int viewId, int accountId, DateTime? from, DateTime? to,
            bool byBranch = false)
        {
            var parameters = GetParameters(bookMode, viewId, accountId, from, to, byBranch);
            var book = byBranch
                ? await _repository.GetAccountBookByBranchAsync(parameters)
                : await _repository.GetAccountBookAsync(parameters);
            SetItemCount(book.TotalCount);
            SetRowNumbers(book.Items);
            Localize(book);
            return Json(book);
        }

        private void Sanitize(ref DateTime? from, ref DateTime? to)
        {
            if (from == null || to == null)
            {
                DateTime rangeFrom, rangeTo;
                _configRepository.GetCurrentFiscalDateRange(out rangeFrom, out rangeTo);
                from = from ?? rangeFrom;
                to = to ?? rangeTo;
            }
        }

        private void Localize(AccountBookViewModel book)
        {
            Array.ForEach(book.Items.ToArray(), item => item.Description = _strings[item.Description ?? String.Empty]);
        }

        private delegate Task<AccountBookViewModel> AccountBookDelegate(int viewId, int itemId,
            DateTime from, DateTime to, GridOptions gridOptions);

        private readonly IAccountBookRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}