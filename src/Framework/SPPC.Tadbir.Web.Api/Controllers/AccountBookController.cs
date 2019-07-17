using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountBookController : ApiControllerBase
    {
        public AccountBookController(IAccountBookRepository repository,
            IConfigRepository config, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            _configRepository = config;
        }

        #region Account Books

        // GET: api/accbook/account/{accountId:min(1)}/by-row
        [Route(AccountBookApi.AccountBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookByRowAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.Account, accountId, from, to);
        }

        // GET: api/accbook/account/{accountId:min(1)}/voucher-sum
        [Route(AccountBookApi.AccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookVoucherSumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.Account, accountId, from, to);
        }

        // GET: api/accbook/account/{accountId:min(1)}/daily-sum
        [Route(AccountBookApi.AccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookDailySumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.Account, accountId, from, to);
        }

        // GET: api/accbook/account/{accountId:min(1)}/monthly-sum
        [Route(AccountBookApi.AccountBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookMonthlySumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.Account, accountId, from, to);
        }

        // GET: api/accbook/account/{accountId:min(1)}/by-row/by-branch
        [Route(AccountBookApi.AccountBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookByRowByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.Account, accountId, from, to, true);
        }

        // GET: api/accbook/account/{accountId:min(1)}/voucher-sum/by-branch
        [Route(AccountBookApi.AccountBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookVoucherSumByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.Account, accountId, from, to, true);
        }

        // GET: api/accbook/account/{accountId:min(1)}/daily-sum/by-branch
        [Route(AccountBookApi.AccountBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookDailySumByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.Account, accountId, from, to, true);
        }

        // GET: api/accbook/account/{accountId:min(1)}/monthly-sum/by-branch
        [Route(AccountBookApi.AccountBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookMonthlySumByBranchAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.Account, accountId, from, to, true);
        }

        #endregion

        #region DetailAccount Books

        // GET: api/accbook/faccount/{faccountId:min(1)}/by-row
        [Route(AccountBookApi.DetailAccountBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookByRowAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.DetailAccount, faccountId, from, to);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/voucher-sum
        [Route(AccountBookApi.DetailAccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookVoucherSumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.DetailAccount, faccountId, from, to);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/daily-sum
        [Route(AccountBookApi.DetailAccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookDailySumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.DetailAccount, faccountId, from, to);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/monthly-sum
        [Route(AccountBookApi.DetailAccountBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookMonthlySumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.DetailAccount, faccountId, from, to);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/by-row/by-branch
        [Route(AccountBookApi.DetailAccountBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookByRowByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.DetailAccount, faccountId, from, to, true);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/voucher-sum/by-branch
        [Route(AccountBookApi.DetailAccountBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookVoucherSumByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.DetailAccount, faccountId, from, to, true);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/daily-sum/by-branch
        [Route(AccountBookApi.DetailAccountBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookDailySumByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.DetailAccount, faccountId, from, to, true);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/monthly-sum/by-branch
        [Route(AccountBookApi.DetailAccountBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookMonthlySumByBranchAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.DetailAccount, faccountId, from, to, true);
        }

        #endregion

        #region CostCenter Books

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/by-row
        [Route(AccountBookApi.CostCenterBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookByRowAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.CostCenter, ccenterId, from, to);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/voucher-sum
        [Route(AccountBookApi.CostCenterBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookVoucherSumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.CostCenter, ccenterId, from, to);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/daily-sum
        [Route(AccountBookApi.CostCenterBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookDailySumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.CostCenter, ccenterId, from, to);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/monthly-sum
        [Route(AccountBookApi.CostCenterBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookMonthlySumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.CostCenter, ccenterId, from, to);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/by-row/by-branch
        [Route(AccountBookApi.CostCenterBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookByRowByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.CostCenter, ccenterId, from, to, true);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/voucher-sum/by-branch
        [Route(AccountBookApi.CostCenterBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookVoucherSumByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.CostCenter, ccenterId, from, to, true);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/daily-sum/by-branch
        [Route(AccountBookApi.CostCenterBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookDailySumByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.CostCenter, ccenterId, from, to, true);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/monthly-sum/by-branch
        [Route(AccountBookApi.CostCenterBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookMonthlySumByBranchAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.CostCenter, ccenterId, from, to, true);
        }

        #endregion

        #region Project Books

        // GET: api/accbook/project/{projectId:min(1)}/by-row
        [Route(AccountBookApi.ProjectBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookByRowAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.Project, projectId, from, to);
        }

        // GET: api/accbook/project/{projectId:min(1)}/voucher-sum
        [Route(AccountBookApi.ProjectBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookVoucherSumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.Project, projectId, from, to);
        }

        // GET: api/accbook/project/{projectId:min(1)}/daily-sum
        [Route(AccountBookApi.ProjectBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookDailySumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.Project, projectId, from, to);
        }

        // GET: api/accbook/project/{projectId:min(1)}/monthly-sum
        [Route(AccountBookApi.ProjectBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookMonthlySumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.Project, projectId, from, to);
        }

        // GET: api/accbook/project/{projectId:min(1)}/by-row/by-branch
        [Route(AccountBookApi.ProjectBookByRowByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookByRowByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.ByRows, ViewName.Project, projectId, from, to, true);
        }

        // GET: api/accbook/project/{projectId:min(1)}/voucher-sum/by-branch
        [Route(AccountBookApi.ProjectBookVoucherSumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookVoucherSumByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.VoucherSum, ViewName.Project, projectId, from, to, true);
        }

        // GET: api/accbook/project/{projectId:min(1)}/daily-sum/by-branch
        [Route(AccountBookApi.ProjectBookDailySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookDailySumByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.DailySum, ViewName.Project, projectId, from, to, true);
        }

        // GET: api/accbook/project/{projectId:min(1)}/monthly-sum/by-branch
        [Route(AccountBookApi.ProjectBookMonthlySumByBranchUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookMonthlySumByBranchAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            return await AccountBookResultAsync(AccountBookMode.MonthlySum, ViewName.Project, projectId, from, to, true);
        }

        #endregion

        #region Account Item Navigation

        // GET: api/accbook/view/{viewId:min(1)}/item/{itemId:min(1)}/prev
        [Route(AccountBookApi.PreviousEnvironmentItemUrl)]
        public async Task<IActionResult> GetPreviousEnvironmentItemAsync(int viewId, int itemId)
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var previous = await _repository.GetPreviousAccountItemAsync(viewId, itemId);
            return JsonReadResult(previous);
        }

        // GET: api/accbook/view/{viewId:min(1)}/item/{itemId:min(1)}/next
        [Route(AccountBookApi.NextEnvironmentItemUrl)]
        public async Task<IActionResult> GetNextEnvironmentItemAsync(int viewId, int itemId)
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var next = await _repository.GetNextAccountItemAsync(viewId, itemId);
            return JsonReadResult(next);
        }

        #endregion

        private async Task<IActionResult> AccountBookResultAsync(
            AccountBookMode bookMode, int viewId, int accountId, DateTime? from, DateTime? to,
            bool byBranch = false)
        {
            var accountBook = GetAccountBookDelegate(bookMode, byBranch);
            var gridOptions = GridOptions ?? new GridOptions();
            Sanitize(ref from, ref to);
            _repository.SetCurrentContext(SecurityContext.User);
            var book = await accountBook(viewId, accountId, from.Value, to.Value, gridOptions);
            SetItemCount(book.TotalCount);
            Localize(book);
            return Json(book);
        }

        private AccountBookDelegate GetAccountBookDelegate(AccountBookMode bookMode, bool byBranch = false)
        {
            var bookDelegate = default(AccountBookDelegate);
            switch (bookMode)
            {
                case AccountBookMode.ByRows:
                    bookDelegate = byBranch
                        ? _repository.GetAccountBookByRowByBranchAsync
                        : (AccountBookDelegate)_repository.GetAccountBookByRowAsync;
                    break;
                case AccountBookMode.VoucherSum:
                    bookDelegate = byBranch
                        ? _repository.GetAccountBookVoucherSumByBranchAsync
                        : (AccountBookDelegate)_repository.GetAccountBookVoucherSumAsync;
                    break;
                case AccountBookMode.DailySum:
                    bookDelegate = byBranch
                        ? _repository.GetAccountBookDailySumByBranchAsync
                        : (AccountBookDelegate)_repository.GetAccountBookDailySumAsync;
                    break;
                case AccountBookMode.MonthlySum:
                    bookDelegate = byBranch
                        ? _repository.GetAccountBookMonthlySumByBranchAsync
                        : (AccountBookDelegate)_repository.GetAccountBookMonthlySumAsync;
                    break;
                default:
                    break;
            }

            return bookDelegate;
        }

        private void Sanitize(ref DateTime? from, ref DateTime? to)
        {
            if (from == null || to == null)
            {
                DateTime rangeFrom, rangeTo;
                _configRepository.SetCurrentContext(SecurityContext.User);
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