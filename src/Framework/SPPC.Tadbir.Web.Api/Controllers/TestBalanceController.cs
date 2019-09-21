using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class TestBalanceController : ApiControllerBase
    {
        public TestBalanceController(ITestBalanceRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/testbal/lookup/types
        [Route(TestBalanceApi.TestBalanceTypeLookupUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTestBalanceTypesLookupAsync()
        {
            var lookup = await _repository.GetBalanceTypesLookupAsync();
            Localize(lookup);
            return Json(lookup);
        }

        #region Ledger Level reports

        // GET: api/testbal/ledger/2-col
        [Route(TestBalanceApi.TwoColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnLedgerBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.TwoColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/ledger/4-col
        [Route(TestBalanceApi.FourColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnLedgerBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.FourColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/ledger/6-col
        [Route(TestBalanceApi.SixColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnLedgerBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.SixColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/ledger/8-col
        [Route(TestBalanceApi.EightColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnLedgerBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.EightColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/ledger/10-col
        [Route(TestBalanceApi.TenColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnLedgerBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.TenColumn, from, to, byBranch, options);
        }

        #endregion

        #region Subsidiary Level reports

        // GET: api/testbal/subsid/2-col
        [Route(TestBalanceApi.TwoColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.TwoColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/subsid/4-col
        [Route(TestBalanceApi.FourColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.FourColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/subsid/6-col
        [Route(TestBalanceApi.SixColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.SixColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/subsid/8-col
        [Route(TestBalanceApi.EightColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.EightColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/subsid/10-col
        [Route(TestBalanceApi.TenColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.TenColumn, from, to, byBranch, options);
        }

        #endregion

        #region Detail Level reports

        // GET: api/testbal/detail/2-col
        [Route(TestBalanceApi.TwoColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnDetailBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.TwoColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/detail/4-col
        [Route(TestBalanceApi.FourColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnDetailBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.FourColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/detail/6-col
        [Route(TestBalanceApi.SixColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnDetailBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.SixColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/detail/8-col
        [Route(TestBalanceApi.EightColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnDetailBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.EightColumn, from, to, byBranch, options);
        }

        // GET: api/testbal/detail/10-col
        [Route(TestBalanceApi.TenColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnDetailBalanceAsync(string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.TenColumn, from, to, byBranch, options);
        }

        #endregion

        #region Child Items Level reports

        // GET: api/testbal/{accountId:min(1)}/items/2-col
        [Route(TestBalanceApi.TwoColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.TwoColumn,
                from, to, byBranch, options, accountId);
        }

        // GET: api/testbal/{accountId:min(1)}/items/4-col
        [Route(TestBalanceApi.FourColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.FourColumn,
                from, to, byBranch, options, accountId);
        }

        // GET: api/testbal/{accountId:min(1)}/items/6-col
        [Route(TestBalanceApi.SixColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.SixColumn,
                from, to, byBranch, options, accountId);
        }

        // GET: api/testbal/{accountId:min(1)}/items/8-col
        [Route(TestBalanceApi.EightColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.EightColumn,
                from, to, byBranch, options, accountId);
        }

        // GET: api/testbal/{accountId:min(1)}/items/10-col
        [Route(TestBalanceApi.TenColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.TenColumn,
                from, to, byBranch, options, accountId);
        }

        #endregion

        private static TestBalanceParameters GetParameters(
            string from, string to, TestBalanceMode mode, TestBalanceFormat format,
            GridOptions gridOptions, bool? byBranch, int? options)
        {
            var parameters = new TestBalanceParameters()
            {
                Mode = mode,
                Format = format,
                GridOptions = gridOptions
            };
            var culture = new CultureInfo("en");
            if (DateTime.TryParse(from, culture, DateTimeStyles.None, out DateTime fromDate))
            {
                parameters.FromDate = fromDate;
            }

            if (DateTime.TryParse(to, culture, DateTimeStyles.None, out DateTime toDate))
            {
                parameters.ToDate = toDate;
            }

            if (Int32.TryParse(from, out int fromNo))
            {
                parameters.FromNo = fromNo;
            }

            if (Int32.TryParse(to, out int toNo))
            {
                parameters.ToNo = toNo;
            }

            if (byBranch.HasValue)
            {
                parameters.IsByBranch = byBranch.Value;
            }

            if (options.HasValue)
            {
                parameters.Options = (TestBalanceOptions)options.Value;
            }

            return parameters;
        }

        private async Task<IActionResult> TestBalanceResultAsync(TestBalanceMode mode, TestBalanceFormat format,
            string from, string to, bool? byBranch, int? options, int itemId = 0)
        {
            if (format == TestBalanceFormat.TenColumn)
            {
                return NotFound();      // 10-column balance modes are temporarily disabled (until further notice)
            }

            var gridOptions = GridOptions ?? new GridOptions();
            var parameters = GetParameters(from, to, mode, format, gridOptions, byBranch, options);
            var balance = default(TestBalanceViewModel);
            switch (mode)
            {
                case TestBalanceMode.Ledger:
                    balance = await _repository.GetLedgerBalanceAsync(parameters);
                    break;
                case TestBalanceMode.Subsidiary:
                    balance = await _repository.GetSubsidiaryBalanceAsync(parameters);
                    break;
                case TestBalanceMode.Detail:
                    balance = await _repository.GetDetailBalanceAsync(parameters);
                    break;
                case TestBalanceMode.AccountItems:
                    balance = await _repository.GetChildrenBalanceAsync(itemId, parameters);
                    break;
            }

            SetItemCount(balance.Items.Count);
            balance.SetBalanceItems(balance.Items.ApplyPaging(gridOptions).ToList());
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var balanceItem in balance.Items)
            {
                balanceItem.RowNo = rowNo++;
            }

            return Json(balance);
        }

        private void Localize(IEnumerable<TestBalanceModeInfo> lookup)
        {
            foreach (var info in lookup.Where(item => !item.IsDetail))
            {
                info.Name = _strings[info.Name];
            }

            foreach (var info in lookup.Where(item => item.IsDetail))
            {
                info.Name = info.Level <= 3
                    ? _strings.Format(info.Name)
                    : _strings.Format(AppStrings.ChildrenOfLevel, info.Name);
            }
        }

        private readonly ITestBalanceRepository _repository;
    }
}