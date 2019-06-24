using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
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
            var book = await GetAccountBookByRowAsync(ViewName.Account, accountId, from, to);
            return Json(book);
        }

        // GET: api/accbook/account/{accountId:min(1)}/voucher-sum
        [Route(AccountBookApi.AccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookVoucherSumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookVoucherSumAsync(ViewName.Account, accountId, from, to);
            return Json(book);
        }

        // GET: api/accbook/account/{accountId:min(1)}/daily-sum
        [Route(AccountBookApi.AccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookDailySumAsync(
            int accountId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookDailySumAsync(ViewName.Account, accountId, from, to);
            return Json(book);
        }

        // GET: api/accbook/account/{accountId:min(1)}/monthly-sum
        [Route(AccountBookApi.AccountBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookMonthlySumAsync(int accountId)
        {
            return Ok();
        }

        #endregion

        #region DetailAccount Books

        // GET: api/accbook/faccount/{faccountId:min(1)}/by-row
        [Route(AccountBookApi.DetailAccountBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookByRowAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookByRowAsync(ViewName.DetailAccount, faccountId, from, to);
            return Json(book);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/voucher-sum
        [Route(AccountBookApi.DetailAccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookVoucherSumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookVoucherSumAsync(ViewName.DetailAccount, faccountId, from, to);
            return Json(book);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/daily-sum
        [Route(AccountBookApi.DetailAccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookDailySumAsync(
            int faccountId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookDailySumAsync(ViewName.DetailAccount, faccountId, from, to);
            return Json(book);
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/monthly-sum
        [Route(AccountBookApi.DetailAccountBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookMonthlySumAsync(int faccountId)
        {
            return Ok();
        }

        #endregion

        #region CostCenter Books

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/by-row
        [Route(AccountBookApi.CostCenterBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookByRowAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookByRowAsync(ViewName.CostCenter, ccenterId, from, to);
            return Json(book);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/voucher-sum
        [Route(AccountBookApi.CostCenterBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookVoucherSumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookVoucherSumAsync(ViewName.CostCenter, ccenterId, from, to);
            return Json(book);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/daily-sum
        [Route(AccountBookApi.CostCenterBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookDailySumAsync(
            int ccenterId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookDailySumAsync(ViewName.DetailAccount, ccenterId, from, to);
            return Json(book);
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/monthly-sum
        [Route(AccountBookApi.CostCenterBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookMonthlySumAsync(int ccenterId)
        {
            return Ok();
        }

        #endregion

        #region Project Books

        // GET: api/accbook/project/{projectId:min(1)}/by-row
        [Route(AccountBookApi.ProjectBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookByRowAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookByRowAsync(ViewName.Project, projectId, from, to);
            return Json(book);
        }

        // GET: api/accbook/project/{projectId:min(1)}/voucher-sum
        [Route(AccountBookApi.ProjectBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookVoucherSumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookVoucherSumAsync(ViewName.Project, projectId, from, to);
            return Json(book);
        }

        // GET: api/accbook/project/{projectId:min(1)}/daily-sum
        [Route(AccountBookApi.ProjectBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookDailySumAsync(
            int projectId, DateTime? from, DateTime? to)
        {
            var book = await GetAccountBookDailySumAsync(ViewName.DetailAccount, projectId, from, to);
            return Json(book);
        }

        // GET: api/accbook/project/{projectId:min(1)}/monthly-sum
        [Route(AccountBookApi.ProjectBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookMonthlySumAsync(int projectId)
        {
            return Ok();
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

        private async Task<AccountBookViewModel> GetAccountBookByRowAsync(
            int viewId, int accountId, DateTime? from, DateTime? to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            Sanitize(ref from, ref to);
            _repository.SetCurrentContext(SecurityContext.User);
            var book = await _repository.GetAccountBookByRowAsync(
                viewId, accountId, from.Value, to.Value, gridOptions);
            SetItemCount(book.Items.Count);
            Localize(book);
            return book;
        }

        private async Task<AccountBookViewModel> GetAccountBookVoucherSumAsync(
            int viewId, int accountId, DateTime? from, DateTime? to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            Sanitize(ref from, ref to);
            _repository.SetCurrentContext(SecurityContext.User);
            var book = await _repository.GetAccountBookVoucherSumAsync(
                viewId, accountId, from.Value, to.Value, gridOptions);
            SetItemCount(book.Items.Count);
            Localize(book);
            return book;
        }

        private async Task<AccountBookViewModel> GetAccountBookDailySumAsync(
            int viewId, int accountId, DateTime? from, DateTime? to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            Sanitize(ref from, ref to);
            _repository.SetCurrentContext(SecurityContext.User);
            var book = await _repository.GetAccountBookDailySumAsync(
                viewId, accountId, from.Value, to.Value, gridOptions);
            SetItemCount(book.Items.Count);
            Localize(book);
            return book;
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
            Array.ForEach(book.Items.ToArray(), item => item.Description = _strings[item.Description]);
        }

        private readonly IAccountBookRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}