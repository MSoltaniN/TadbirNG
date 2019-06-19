using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
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
        public async Task<IActionResult> GetAccountBookByRowAsync(int accountId, DateTime? from, DateTime? to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            Sanitize(ref from, ref to);
            _repository.SetCurrentContext(SecurityContext.User);
            var book = await _repository.GetAccountBookByRowAsync(
                ViewName.Account, accountId, from.Value, to.Value, gridOptions);
            return Json(book);
        }

        // GET: api/accbook/account/{accountId:min(1)}/voucher-sum
        [Route(AccountBookApi.AccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookVoucherSumAsync(int accountId)
        {
            return Ok();
        }

        // GET: api/accbook/account/{accountId:min(1)}/daily-sum
        [Route(AccountBookApi.AccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetAccountBookDailySumAsync(int accountId)
        {
            return Ok();
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
        public async Task<IActionResult> GetDetailAccountBookByRowAsync(int faccountId)
        {
            return Ok();
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/voucher-sum
        [Route(AccountBookApi.DetailAccountBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookVoucherSumAsync(int faccountId)
        {
            return Ok();
        }

        // GET: api/accbook/faccount/{faccountId:min(1)}/daily-sum
        [Route(AccountBookApi.DetailAccountBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetDetailAccountBookDailySumAsync(int faccountId)
        {
            return Ok();
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
        public async Task<IActionResult> GetCostCenterBookByRowAsync(int ccenterId)
        {
            return Ok();
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/voucher-sum
        [Route(AccountBookApi.CostCenterBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookVoucherSumAsync(int ccenterId)
        {
            return Ok();
        }

        // GET: api/accbook/ccenter/{ccenterId:min(1)}/daily-sum
        [Route(AccountBookApi.CostCenterBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetCostCenterBookDailySumAsync(int ccenterId)
        {
            return Ok();
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
        public async Task<IActionResult> GetProjectBookByRowAsync(int projectId)
        {
            return Ok();
        }

        // GET: api/accbook/project/{projectId:min(1)}/voucher-sum
        [Route(AccountBookApi.ProjectBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookVoucherSumAsync(int projectId)
        {
            return Ok();
        }

        // GET: api/accbook/project/{projectId:min(1)}/daily-sum
        [Route(AccountBookApi.ProjectBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookDailySumAsync(int projectId)
        {
            return Ok();
        }

        // GET: api/accbook/project/{projectId:min(1)}/monthly-sum
        [Route(AccountBookApi.ProjectBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.AccountBook, (int)AccountBookPermissions.View)]
        public async Task<IActionResult> GetProjectBookMonthlySumAsync(int projectId)
        {
            return Ok();
        }

        #endregion

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

        private readonly IAccountBookRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}