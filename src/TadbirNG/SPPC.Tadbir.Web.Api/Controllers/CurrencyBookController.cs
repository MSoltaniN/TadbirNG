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
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class CurrencyBookController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public CurrencyBookController(ICurrencyBookRepository repository,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="byBranch"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="accountId"></param>
        /// <param name="faccountId"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/currbook/by-row
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookByRowAsync(
            DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId,
            bool byBranch)
        {
            return await CurrencyBookResultAsync(CurrencyBookMode.ByRows,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="byBranch"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="accountId"></param>
        /// <param name="faccountId"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/currbook/voucher-sum
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookVoucherSumAsync(
            DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId,
            bool byBranch)
        {
            return await CurrencyBookResultAsync(CurrencyBookMode.VoucherSum,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="byBranch"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="accountId"></param>
        /// <param name="faccountId"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/currbook/daily-sum
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookDailySumAsync(
            DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId,
            bool byBranch)
        {
            return await CurrencyBookResultAsync(CurrencyBookMode.DailySum,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="byBranch"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="accountId"></param>
        /// <param name="faccountId"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/currbook/monthly-sum
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookMonthlySumAsync(
            DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId,
            bool byBranch)
        {
            return await CurrencyBookResultAsync(CurrencyBookMode.MonthlySum,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="noCurrency"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="accountId"></param>
        /// <param name="faccountId"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <param name="byBranch"></param>
        /// <returns></returns>
        // GET: api/currbook/all-currencies/{currFree}
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookAllCurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookAllCurrenciesAsync(
            DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId,
            bool byBranch, bool noCurrency)
        {
            var parameters = GetParameters(
                CurrencyBookMode.AllCurrencies, byBranch, from, to,
                accountId, faccountId, ccenterId, projectId, noCurrency);
            var book = await _repository.GetCurrencyBookAllCurrenciesAsync(parameters);
            SetItemCount(book.TotalCount);
            SetRowNumbers(book.Items);
            return Json(book);
        }

        private CurrencyBookParameters GetParameters(
            CurrencyBookMode mode, bool byBranch, DateTime from, DateTime to,
            int? accountId, int? faccountId, int? ccenterId, int? projectId, bool noCurrency = false)
        {
            return new CurrencyBookParameters()
            {
                Mode = mode,
                ByBranch = byBranch,
                FromDate = from,
                ToDate = to,
                AccountId = accountId,
                DetailAccountId = faccountId,
                CostCenterId = ccenterId,
                ProjectId = projectId,
                NoCurrency = noCurrency,
                GridOptions = GridOptions ?? new GridOptions()
            };
        }

        private async Task<IActionResult> CurrencyBookResultAsync(
            CurrencyBookMode mode, bool byBranch, DateTime from, DateTime to,
            int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            var parameters = GetParameters(
                mode, byBranch, from, to, accountId, faccountId, ccenterId, projectId);
            var book = await _repository.GetCurrencyBookAsync(parameters);
            SetItemCount(book.TotalCount);
            SetRowNumbers(book.Items);
            return Json(book);
        }

        private readonly ICurrencyBookRepository _repository;
    }
}