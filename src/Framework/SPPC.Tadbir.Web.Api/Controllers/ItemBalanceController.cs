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
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class ItemBalanceController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public ItemBalanceController(ITestBalanceRepository repository,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/lookup/types
        [HttpGet]
        [Route(ItemBalanceApi.ItemBalanceTypeLookupUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTestBalanceTypesLookupAsync(int viewId)
        {
            var lookup = await _repository.GetBalanceTypesLookupAsync(viewId);
            Localize(lookup);
            return Json(lookup);
        }

        #region Specific Level reports

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/2-col
        [HttpGet]
        [Route(ItemBalanceApi.TwoColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.TwoColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/4-col
        [HttpGet]
        [Route(ItemBalanceApi.FourColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.FourColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/6-col
        [HttpGet]
        [Route(ItemBalanceApi.SixColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.SixColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/8-col
        [HttpGet]
        [Route(ItemBalanceApi.EightColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.EightColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/10-col
        [HttpGet]
        [Route(ItemBalanceApi.TenColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.TenColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        #endregion

        #region Child Items Level reports

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/2-col
        [HttpGet]
        [Route(ItemBalanceApi.TwoColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.TwoColumn,
                from, to, byBranch, options, itemId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/4-col
        [HttpGet]
        [Route(ItemBalanceApi.FourColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.FourColumn,
                from, to, byBranch, options, itemId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/6-col
        [HttpGet]
        [Route(ItemBalanceApi.SixColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.SixColumn,
                from, to, byBranch, options, itemId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/8-col
        [HttpGet]
        [Route(ItemBalanceApi.EightColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.EightColumn,
                from, to, byBranch, options, itemId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="itemId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="byBranch"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/10-col
        [HttpGet]
        [Route(ItemBalanceApi.TenColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTenColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.TenColumn,
                from, to, byBranch, options, itemId);
        }

        #endregion

        private async Task<IActionResult> ItemBalanceResultAsync(
            int viewId, TestBalanceMode mode, TestBalanceFormat format,
            string from, string to, bool? byBranch, int? options,
            int itemId = 0, int level = 0)
        {
            if (format == TestBalanceFormat.TenColumn)
            {
                return NotFound();      // 10-column balance modes are temporarily disabled (until further notice)
            }

            var gridOptions = GridOptions ?? new GridOptions();
            var parameters = _repository.BuildParameters(
                viewId, from, to, mode, format, gridOptions, byBranch, options);
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
                info.Name = _strings.Format(info.Name, info.Level.ToString());
            }

            foreach (var info in lookup.Where(item => item.IsDetail))
            {
                info.Name = _strings.Format(info.Name, (info.Level - 1).ToString());
                info.Name = _strings.Format(AppStrings.ChildrenOfLevel, info.Name);
            }
        }

        private readonly ITestBalanceRepository _repository;
    }
}