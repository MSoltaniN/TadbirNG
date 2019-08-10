using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
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
    public class TestBalanceController : ApiControllerBase
    {
        public TestBalanceController(ITestBalanceRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        #region Ledger Level reports

        // GET: api/testbal/ledger/2-col
        [Route(TestBalanceApi.TwoColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnLedgerBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.TwoColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/4-col
        [Route(TestBalanceApi.FourColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnLedgerBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.FourColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/6-col
        [Route(TestBalanceApi.SixColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnLedgerBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.SixColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/8-col
        [Route(TestBalanceApi.EightColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnLedgerBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.EightColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/10-col
        [Route(TestBalanceApi.TenColumnLedgerBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnLedgerBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Ledger, TestBalanceFormat.TenColumn, from, to, byBranch);
        }

        #endregion

        #region Subsidiary Level reports

        // GET: api/testbal/subsid/2-col
        [Route(TestBalanceApi.TwoColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.TwoColumn, from, to, byBranch);
        }

        // GET: api/testbal/subsid/4-col
        [Route(TestBalanceApi.FourColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.FourColumn, from, to, byBranch);
        }

        // GET: api/testbal/subsid/6-col
        [Route(TestBalanceApi.SixColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.SixColumn, from, to, byBranch);
        }

        // GET: api/testbal/subsid/8-col
        [Route(TestBalanceApi.EightColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.EightColumn, from, to, byBranch);
        }

        // GET: api/testbal/subsid/10-col
        [Route(TestBalanceApi.TenColumnSubsidiaryBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnSubsidiaryBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Subsidiary, TestBalanceFormat.TenColumn, from, to, byBranch);
        }

        #endregion

        #region Detail Level reports

        // GET: api/testbal/ledger/2-col
        [Route(TestBalanceApi.TwoColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnDetailBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.TwoColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/4-col
        [Route(TestBalanceApi.FourColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnDetailBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.FourColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/6-col
        [Route(TestBalanceApi.SixColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnDetailBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.SixColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/8-col
        [Route(TestBalanceApi.EightColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnDetailBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.EightColumn, from, to, byBranch);
        }

        // GET: api/testbal/ledger/10-col
        [Route(TestBalanceApi.TenColumnDetailBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnDetailBalanceAsync(string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Detail, TestBalanceFormat.TenColumn, from, to, byBranch);
        }

        #endregion

        #region Ledger Items Level reports

        // GET: api/testbal/ledger/{accountId:min(1)}/items/2-col
        [Route(TestBalanceApi.TwoColumnLedgerItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnLedgerItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.LedgerItems, TestBalanceFormat.TwoColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/ledger/{accountId:min(1)}/items/4-col
        [Route(TestBalanceApi.FourColumnLedgerItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnLedgerItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.LedgerItems, TestBalanceFormat.FourColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/ledger/{accountId:min(1)}/items/6-col
        [Route(TestBalanceApi.SixColumnLedgerItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnLedgerItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.LedgerItems, TestBalanceFormat.SixColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/ledger/{accountId:min(1)}/items/8-col
        [Route(TestBalanceApi.EightColumnLedgerItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnLedgerItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.LedgerItems, TestBalanceFormat.EightColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/ledger/{accountId:min(1)}/items/10-col
        [Route(TestBalanceApi.TenColumnLedgerItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnLedgerItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.LedgerItems, TestBalanceFormat.TenColumn,
                from, to, byBranch, accountId);
        }

        #endregion

        #region Subsidiary Level Items reports

        // GET: api/testbal/subsid/{accountId:min(1)}/items/2-col
        [Route(TestBalanceApi.TwoColumnSubsidiaryItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnSubsidiaryItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.SubsidiaryItems, TestBalanceFormat.TwoColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/subsid/{accountId:min(1)}/items/4-col
        [Route(TestBalanceApi.FourColumnSubsidiaryItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnSubsidiaryItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.SubsidiaryItems, TestBalanceFormat.FourColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/subsid/{accountId:min(1)}/items/6-col
        [Route(TestBalanceApi.SixColumnSubsidiaryItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnSubsidiaryItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.SubsidiaryItems, TestBalanceFormat.SixColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/subsid/{accountId:min(1)}/items/8-col
        [Route(TestBalanceApi.EightColumnSubsidiaryItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnSubsidiaryItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.SubsidiaryItems, TestBalanceFormat.EightColumn,
                from, to, byBranch, accountId);
        }

        // GET: api/testbal/subsid/{accountId:min(1)}/items/10-col
        [Route(TestBalanceApi.TenColumnSubsidiaryItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnSubsidiaryItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.SubsidiaryItems, TestBalanceFormat.TenColumn,
                from, to, byBranch, accountId);
        }

        #endregion

        #region DetailAccount Level reports

        // GET: api/testbal/detail/level/{levelId:min(1)}/2-col
        [Route(TestBalanceApi.TwoColumnDetailLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnDetailLevelBalanceAsync(
            int level, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.DetailAccountLevel, TestBalanceFormat.TwoColumn,
                from, to, byBranch, 0, level);
        }

        // GET: api/testbal/detail/level/{levelId:min(1)}/4-col
        [Route(TestBalanceApi.FourColumnDetailLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnDetailLevelBalanceAsync(
            int level, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.DetailAccountLevel, TestBalanceFormat.FourColumn,
                from, to, byBranch, 0, level);
        }

        // GET: api/testbal/detail/level/{levelId:min(1)}/6-col
        [Route(TestBalanceApi.SixColumnDetailLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnDetailLevelBalanceAsync(
            int level, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.DetailAccountLevel, TestBalanceFormat.SixColumn,
                from, to, byBranch, 0, level);
        }

        // GET: api/testbal/detail/level/{levelId:min(1)}/8-col
        [Route(TestBalanceApi.EightColumnDetailLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnDetailLevelBalanceAsync(
            int level, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.DetailAccountLevel, TestBalanceFormat.EightColumn,
                from, to, byBranch, 0, level);
        }

        // GET: api/testbal/detail/level/{levelId:min(1)}/10-col
        [Route(TestBalanceApi.TenColumnDetailLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnDetailLevelBalanceAsync(
            int level, string from, string to, bool? byBranch)
        {
            return await TestBalanceResultAsync(TestBalanceMode.DetailAccountLevel, TestBalanceFormat.TenColumn,
                from, to, byBranch, 0, level);
        }

        #endregion

        private static TestBalanceParameters GetParameters(
            string from, string to, TestBalanceFormat format, bool? byBranch)
        {
            var parameters = new TestBalanceParameters() { Format = format };
            if (DateTime.TryParse(from, out DateTime fromDate))
            {
                parameters.FromDate = fromDate;
            }

            if (DateTime.TryParse(to, out DateTime toDate))
            {
                parameters.ToDate = toDate;
            }

            if (Int32.TryParse(from, out int fromNo))
            {
                parameters.FromNo = fromNo;
            }

            if (Int32.TryParse(to, out int toNo))
            {
                parameters.FromNo = fromNo;
            }

            if (byBranch.HasValue)
            {
                parameters.IsByBranch = byBranch.Value;
            }

            return parameters;
        }

        private async Task<IActionResult> TestBalanceResultAsync(TestBalanceMode mode, TestBalanceFormat format,
            string from, string to, bool? byBranch, int itemId = 0, int level = 0)
        {
            var parameters = GetParameters(from, to, format, byBranch);
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
                case TestBalanceMode.LedgerItems:
                    balance = await _repository.GetLedgerItemsBalanceAsync(itemId, parameters);
                    break;
                case TestBalanceMode.SubsidiaryItems:
                    balance = await _repository.GetSubsidiaryItemsBalanceAsync(itemId, parameters);
                    break;
                case TestBalanceMode.DetailAccountLevel:
                    balance = await _repository.GetDetailAccountLevelBalanceAsync(level, parameters);
                    break;
            }

            return Json(balance);
        }

        private readonly ITestBalanceRepository _repository;
    }
}