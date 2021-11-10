using System;
using System.Collections.Generic;
using System.Linq;
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
        // GET: api/currbook/by-row/by-branch/{byBranch}
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookByRowAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.ByRows,
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
        // GET: api/currbook/voucher-sum/by-branch/{byBranch}
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookVoucherSumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.VoucherSum,
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
        // GET: api/currbook/daily-sum/by-branch/{byBranch}
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookDailySumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.DailySum,
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
        // GET: api/currbook/monthly-sum/by-branch/{byBranch}
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookMonthlySumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.MonthlySum,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="currFree"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="accountId"></param>
        /// <param name="faccountId"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/currbook/all-currencies/{currFree}
        [HttpGet]
        [Route(CurrencyBookApi.CurrencyBookAllCurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookAllCurrenciesAsync(
             bool currFree, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            var bookParams = new CurrencyBookParameters()
            {
                ByBranch = false,
                From = from,
                To = to,
                AccountId = accountId,
                DetailAccountId = faccountId,
                CostCenterId = ccenterId,
                ProjectId = projectId,
                CurrencyFree = currFree,
                GridOptions = GridOptions ?? new GridOptions()
            };

            var book = await _repository.GetCurrencyBookAllCurrenciesAsync(bookParams);
            SetItemCount(book.TotalCount);
            SortItems(book);
            return Json(book);
        }

        private static void SortItems(CurrencyBookViewModel book)
        {
            var currencyFreeItem = new CurrencyBookItemViewModel();
            var items = new List<CurrencyBookItemViewModel>();
            items.AddRange(book.Items.Where(item => item.CurrencyId != null));
            items = items.OrderBy(item => item.CurrencyName).ToList();
            currencyFreeItem = book.Items.FirstOrDefault(curr => curr.CurrencyId == null);
            if (currencyFreeItem != null)
            {
                items.Add(currencyFreeItem);
            }

            for (int i = 0; i < items.Count; i++)
            {
                items[i].RowNo = i + 1;
            }

            book.Items.Clear();
            book.Items.AddRange(items);
        }

        private async Task<IActionResult> CurrencyBookResultAsync(
            AccountBookMode bookMode, bool byBranch, DateTime from, DateTime to,
            int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            var bookParams = new CurrencyBookParameters()
            {
                Mode = bookMode,
                ByBranch = byBranch,
                From = from,
                To = to,
                AccountId = accountId,
                DetailAccountId = faccountId,
                CostCenterId = ccenterId,
                ProjectId = projectId,
                GridOptions = GridOptions ?? new GridOptions()
            };
            var book = byBranch
                ? await _repository.GetCurrencyBookByBranchAsync(bookParams)
                : await _repository.GetCurrencyBookAsync(bookParams);
            SetItemCount(book.TotalCount);
            SetRowNumbers(book.Items);
            return Json(book);
        }

        private readonly ICurrencyBookRepository _repository;
    }
}