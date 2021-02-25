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
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class TestBalanceController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        public TestBalanceController(ITestBalanceRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/testbal/lookup/types
        [HttpGet]
        [Route(TestBalanceApi.TestBalanceTypeLookupUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTestBalanceTypesLookupAsync()
        {
            var lookup = await _repository.GetBalanceTypesLookupAsync(ViewId.Account);
            Localize(lookup);
            return Json(lookup);
        }

        #region Specific Level reports

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/levels/{level:min(1)}/2-col
        [HttpGet]
        [Route(TestBalanceApi.TwoColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnLevelBalanceAsync(
            int level, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Level, TestBalanceFormat.TwoColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/levels/{level:min(1)}/4-col
        [HttpGet]
        [Route(TestBalanceApi.FourColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnLevelBalanceAsync(
            int level, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Level, TestBalanceFormat.FourColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/levels/{level:min(1)}/6-col
        [HttpGet]
        [Route(TestBalanceApi.SixColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnLevelBalanceAsync(
            int level, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(
                TestBalanceMode.Level, TestBalanceFormat.SixColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/levels/{level:min(1)}/8-col
        [HttpGet]
        [Route(TestBalanceApi.EightColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnLevelBalanceAsync(
            int level, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Level, TestBalanceFormat.EightColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/levels/{level:min(1)}/10-col
        [HttpGet]
        [Route(TestBalanceApi.TenColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnLevelBalanceAsync(
            int level, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.Level, TestBalanceFormat.TenColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        #endregion

        #region Child Items Level reports

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/{accountId:min(1)}/items/2-col
        [HttpGet]
        [Route(TestBalanceApi.TwoColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.TwoColumn,
                from, to, byBranch, options, accountId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/{accountId:min(1)}/items/4-col
        [HttpGet]
        [Route(TestBalanceApi.FourColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.FourColumn,
                from, to, byBranch, options, accountId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/{accountId:min(1)}/items/6-col
        [HttpGet]
        [Route(TestBalanceApi.SixColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.SixColumn,
                from, to, byBranch, options, accountId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/{accountId:min(1)}/items/8-col
        [HttpGet]
        [Route(TestBalanceApi.EightColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.EightColumn,
                from, to, byBranch, options, accountId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/testbal/{accountId:min(1)}/items/10-col
        [HttpGet]
        [Route(TestBalanceApi.TenColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.TestBalance, (int)TestBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnChildItemsBalanceAsync(
            int accountId, string from, string to, bool? byBranch, int? options)
        {
            return await TestBalanceResultAsync(TestBalanceMode.AccountItems, TestBalanceFormat.TenColumn,
                from, to, byBranch, options, accountId);
        }

        #endregion

        private async Task<IActionResult> TestBalanceResultAsync(
            TestBalanceMode mode, TestBalanceFormat format,
            string from, string to, bool? byBranch, int? options,
            int itemId = 0, int level = 0)
        {
            if (format == TestBalanceFormat.TenColumn)
            {
                return NotFound();      // 10-column balance modes are temporarily disabled (until further notice)
            }

            var gridOptions = GridOptions ?? new GridOptions();
            var parameters = _repository.BuildParameters(
                ViewId.Account, from, to, mode, format, gridOptions, byBranch, options);
            var balance = default(TestBalanceViewModel);
            switch (mode)
            {
                case TestBalanceMode.Level:
                    balance = await _repository.GetLevelBalanceAsync(level, parameters);
                    break;
                case TestBalanceMode.AccountItems:
                    balance = await _repository.GetChildrenBalanceAsync(itemId, parameters);
                    break;
            }

            SetItemCount(balance.TotalCount);
            SetRowNumbers(balance.Items);
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

            int id = 0;
            foreach (var info in lookup)
            {
                info.Id = id++;
            }
        }

        private readonly ITestBalanceRepository _repository;
    }
}