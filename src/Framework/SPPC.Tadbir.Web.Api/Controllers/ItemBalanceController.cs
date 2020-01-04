﻿using System;
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
    public class ItemBalanceController : ApiControllerBase
    {
        public ItemBalanceController(ITestBalanceRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/itembal/views/{viewId:min(2)}/lookup/types
        [Route(ItemBalanceApi.ItemBalanceTypeLookupUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTestBalanceTypesLookupAsync(int viewId)
        {
            var lookup = await _repository.GetBalanceTypesLookupAsync(viewId);
            Localize(lookup);
            return Json(lookup);
        }

        #region Specific Level reports

        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/2-col
        [Route(ItemBalanceApi.TwoColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.TwoColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/4-col
        [Route(ItemBalanceApi.FourColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.FourColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/6-col
        [Route(ItemBalanceApi.SixColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.SixColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/8-col
        [Route(ItemBalanceApi.EightColumnLevelBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnLevelBalanceAsync(
            int viewId, int level, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.Level, TestBalanceFormat.EightColumn,
                from, to, byBranch, options, 0, level - 1);
        }

        // GET: api/itembal/views/{viewId:min(2)}/levels/{level:min(1)}/10-col
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

        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/2-col
        [Route(ItemBalanceApi.TwoColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetTwoColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.TwoColumn,
                from, to, byBranch, options, itemId);
        }

        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/4-col
        [Route(ItemBalanceApi.FourColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetFourColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.FourColumn,
                from, to, byBranch, options, itemId);
        }

        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/6-col
        [Route(ItemBalanceApi.SixColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetSixColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.SixColumn,
                from, to, byBranch, options, itemId);
        }

        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/8-col
        [Route(ItemBalanceApi.EightColumnChildItemsBalanceUrl)]
        [AuthorizeRequest(SecureEntity.ItemBalance, (int)ItemBalancePermissions.View)]
        public async Task<IActionResult> GetEightColumnChildItemsBalanceAsync(
            int viewId, int itemId, string from, string to, bool? byBranch, int? options)
        {
            return await ItemBalanceResultAsync(
                viewId, TestBalanceMode.AccountItems, TestBalanceFormat.EightColumn,
                from, to, byBranch, options, itemId);
        }

        // GET: api/itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/10-col
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

        private static TestBalanceParameters GetParameters(
            int viewId, string from, string to, TestBalanceMode mode, TestBalanceFormat format,
            GridOptions gridOptions, bool? byBranch, int? options)
        {
            var parameters = new TestBalanceParameters()
            {
                ViewId = viewId,
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
            var parameters = GetParameters(viewId, from, to, mode, format, gridOptions, byBranch, options);
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