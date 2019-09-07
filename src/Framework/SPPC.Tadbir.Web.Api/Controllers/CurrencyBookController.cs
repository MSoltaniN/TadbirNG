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
            var bookParams = new CurrencyBookParamViewModel()
            {
                ByBranch = byBranch,
                From = from,
                To = to,
                AccountId = accountId,
                FAccountId = faccountId,
                CCenterId = ccenterId,
                ProjectId = projectId
            };

            return await CurrencyBookResultAsync(AccountBookMode.ByRows, bookParams);
        }

        // GET: api/currbook/voucher-sum/by-branch/{byBranch}
        [Route(CurrencyBookApi.CurrencyBookVoucherSumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookVoucherSumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            var bookParams = new CurrencyBookParamViewModel()
            {
                ByBranch = byBranch,
                From = from,
                To = to,
                AccountId = accountId,
                FAccountId = faccountId,
                CCenterId = ccenterId,
                ProjectId = projectId
            };

            return await CurrencyBookResultAsync(AccountBookMode.VoucherSum, bookParams);
        }

        // GET: api/currbook/daily-sum/by-branch/{byBranch}
        [Route(CurrencyBookApi.CurrencyBookDailySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookDailySumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            var bookParams = new CurrencyBookParamViewModel()
            {
                ByBranch = byBranch,
                From = from,
                To = to,
                AccountId = accountId,
                FAccountId = faccountId,
                CCenterId = ccenterId,
                ProjectId = projectId
            };

            return await CurrencyBookResultAsync(AccountBookMode.DailySum, bookParams);
        }

        // GET: api/currbook/monthly-sum/by-branch/{byBranch}
        [Route(CurrencyBookApi.CurrencyBookMonthlySumUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookMonthlySumAsync(
            bool byBranch, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            var bookParams = new CurrencyBookParamViewModel()
            {
                ByBranch = byBranch,
                From = from,
                To = to,
                AccountId = accountId,
                FAccountId = faccountId,
                CCenterId = ccenterId,
                ProjectId = projectId
            };

            return await CurrencyBookResultAsync(AccountBookMode.MonthlySum, bookParams);
        }

        // GET: api/currbook/all-currencies/{currFree}
        [Route(CurrencyBookApi.CurrencyBookAllCurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyBook, (int)CurrencyBookPermissions.View)]
        public async Task<IActionResult> GetCurrencyBookAllCurrenciesAsync(
             bool currFree, DateTime from, DateTime to, int? accountId, int? faccountId, int? ccenterId, int? projectId)
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var bookParams = new CurrencyBookParamViewModel()
            {
                ByBranch = false,
                From = from,
                To = to,
                AccountId = accountId,
                FAccountId = faccountId,
                CCenterId = ccenterId,
                ProjectId = projectId,
                CurrFree = currFree
            };
            var gridOptions = GridOptions ?? new GridOptions();

            var book = await _repository.GetCurrencyBookAllCurrenciesAsync(bookParams, gridOptions);
            SetItemCount(book.TotalCount);
            Localize(book);
            SortItems(book);
            return Json(book);
        }

        private async Task<IActionResult> CurrencyBookResultAsync(AccountBookMode bookMode, CurrencyBookParamViewModel bookParam)
        {
            var currencyBook = GetCurrencyBookDelegate(bookMode, bookParam.ByBranch);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var book = await currencyBook(bookParam, gridOptions);
            SetItemCount(book.TotalCount);
            Localize(book);
            return Json(book);
        }

        private CurrencyBookDelegate GetCurrencyBookDelegate(AccountBookMode bookMode, bool byBranch = false)
        {
            var bookDelegate = default(CurrencyBookDelegate);
            switch (bookMode)
            {
                case AccountBookMode.ByRows:
                    bookDelegate = byBranch
                        ? _repository.GetCurrencyBookByRowByBranchAsync
                        : (CurrencyBookDelegate)_repository.GetCurrencyBookByRowAsync;
                    break;
                case AccountBookMode.VoucherSum:
                    bookDelegate = byBranch
                        ? _repository.GetCurrencyBookVoucherSumByBranchAsync
                        : (CurrencyBookDelegate)_repository.GetCurrencyBookVoucherSumAsync;
                    break;
                case AccountBookMode.DailySum:
                    bookDelegate = byBranch
                        ? _repository.GetCurrencyBookDailySumByBranchAsync
                        : (CurrencyBookDelegate)_repository.GetCurrencyBookDailySumAsync;
                    break;
                case AccountBookMode.MonthlySum:
                    bookDelegate = byBranch
                        ? _repository.GetCurrencyBookMonthlySumByBranchAsync
                        : (CurrencyBookDelegate)_repository.GetCurrencyBookMonthlySumAsync;
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

        private void Localize(CurrencyBookViewModel book)
        {
            Array.ForEach(book.Items.ToArray(), item =>
            {
                item.Description = _strings[item.Description ?? String.Empty];
                item.CurrencyName = _strings[item.CurrencyName ?? String.Empty];
            });
        }

        private void SortItems(CurrencyBookViewModel book)
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

        private delegate Task<CurrencyBookViewModel> CurrencyBookDelegate(
            CurrencyBookParamViewModel bookParam, GridOptions gridOptions);

        private readonly ICurrencyBookRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}