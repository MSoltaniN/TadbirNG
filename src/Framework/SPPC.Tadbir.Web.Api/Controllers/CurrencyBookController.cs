using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class CurrencyBookController : ApiControllerBase
    {
        public CurrencyBookController(ICurrencyBookRepository repository,
            IConfigRepository config, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            _configRepository = config;
        }

        // GET: api/currbook/by-row/by-branch/{byBranch}
        [Route(CurrencyBookApi.CurrencyBookByRowUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookByRowAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.ByRows,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        // GET: api/currbook/voucher-sum/by-branch/{byBranch}
        [Route(CurrencyBookApi.CurrencyBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookVoucherSumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.VoucherSum,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        // GET: api/currbook/daily-sum/by-branch/{byBranch}
        [Route(CurrencyBookApi.CurrencyBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookDailySumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.DailySum,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        // GET: api/currbook/monthly-sum/by-branch/{byBranch}
        [Route(CurrencyBookApi.CurrencyBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookMonthlySumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            return await CurrencyBookResultAsync(AccountBookMode.MonthlySum,
                byBranch, from, to, accountId, faccountId, ccenterId, projectId);
        }

        // GET: api/currbook/all-currencies/{currFree}
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
                FAccountId = faccountId,
                CCenterId = ccenterId,
                ProjectId = projectId,
                CurrFree = currFree,
                GridOptions = GridOptions ?? new GridOptions()
            };

            var book = await _repository.GetCurrencyBookAllCurrenciesAsync(bookParams);
            SetItemCount(book.TotalCount);
            Localize(book);
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
            Sanitize(ref from, ref to);
            var bookParams = new CurrencyBookParameters()
            {
                Mode = bookMode,
                ByBranch = byBranch,
                From = from,
                To = to,
                AccountId = accountId,
                FAccountId = faccountId,
                CCenterId = ccenterId,
                ProjectId = projectId,
                GridOptions = GridOptions ?? new GridOptions()
            };
            var book = byBranch
                ? await _repository.GetCurrencyBookByBranchAsync(bookParams)
                : await _repository.GetCurrencyBookAsync(bookParams);
            SetItemCount(book.TotalCount);
            Localize(book);
            return Json(book);
        }

        private void Sanitize(ref DateTime from, ref DateTime to)
        {
            if (from == null || to == null)
            {
                DateTime rangeFrom, rangeTo;
                _configRepository.GetCurrentFiscalDateRange(out rangeFrom, out rangeTo);
                from = (from != DateTime.MinValue) ? from : rangeFrom;
                to = (to != DateTime.MinValue) ? to : rangeTo;
            }
        }

        private void Localize(CurrencyBookViewModel book)
        {
            Array.ForEach(book.Items.ToArray(), item =>
            {
                item.Description = _strings[item.Description ?? String.Empty];
                item.CurrencyName = _strings[item.CurrencyName ?? String.Empty];
            });
        }

        private readonly ICurrencyBookRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}