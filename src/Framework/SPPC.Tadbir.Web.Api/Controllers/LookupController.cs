using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public partial class LookupController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="config"></param>
        /// <param name="strings"></param>
        public LookupController(ILookupRepository repository,
            IConfigRepository config, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _configRepository = config;
            _strings = strings;
        }

        #region Finance Subsystem API

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/accounts
        [HttpGet]
        [Route(LookupApi.EnvironmentAccountsUrl)]
        public async Task<IActionResult> GetAccountsLookupAsync()
        {
            var accountLookup = await _repository.GetAccountsAsync(GridOptions);
            return Json(accountLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/faccounts
        [HttpGet]
        [Route(LookupApi.EnvironmentDetailAccountsUrl)]
        public async Task<IActionResult> GetDetailAccountsLookupAsync()
        {
            var lookup = await _repository.GetDetailAccountsAsync(GridOptions);
            return Json(lookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/costcenters
        [HttpGet]
        [Route(LookupApi.EnvironmentCostCentersUrl)]
        public async Task<IActionResult> GetCostCentersLookupAsync()
        {
            var lookup = await _repository.GetCostCentersAsync(GridOptions);
            return Json(lookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/projects
        [HttpGet]
        [Route(LookupApi.EnvironmentProjectsUrl)]
        public async Task<IActionResult> GetProjectsLookupAsync()
        {
            var lookup = await _repository.GetProjectsAsync(GridOptions);
            return Json(lookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/vouchers
        [HttpGet]
        [Route(LookupApi.EnvironmentVouchersUrl)]
        public async Task<IActionResult> GetVouchersLookupAsync()
        {
            var lang = Request.Headers["Accept-Language"].ToString();
            lang = lang ?? "fa";
            var items = await _repository.GetVouchersAsync(GridOptions);
            var lookup = items.ToList();
            foreach (var kv in lookup)
            {
                var valueItems = kv.Value.Split(',');
                var date = DateTime.Parse(valueItems[2]);
                var dateDisplay = lang.StartsWith("fa")
                    ? JalaliDateTime.FromDateTime(date).ToShortDateString()
                    : date.ToShortDateString();
                kv.Value = String.Format(_strings[valueItems[0]], valueItems[1], dateDisplay);
            }

            return Json(lookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/vouchers/lines
        [HttpGet]
        [Route(LookupApi.EnvironmentVoucherLinesUrl)]
        public async Task<IActionResult> GetVoucherLinesLookupAsync()
        {
            var items = await _repository.GetVoucherLinesAsync(GridOptions);
            var lookup = items.ToList();
            foreach (var kv in lookup)
            {
                var valueItems = kv.Value.Split('|');
                kv.Value = String.Format(_strings[valueItems[0]], valueItems[1], valueItems[2], valueItems[3]);
            }

            return Json(lookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/currencies
        [HttpGet]
        [Route(LookupApi.CurrenciesUrl)]
        public async Task<IActionResult> GetCurrenciesLookupAsync()
        {
            var currencyLookup = await _repository.GetCurrenciesAsync();
            var localized = Localize(currencyLookup.ToList(), true);
            return Json(localized);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="withRate"></param>
        /// <returns></returns>
        // GET: api/lookup/currencies/info[?withRate={true|false}]
        [HttpGet]
        [Route(LookupApi.CurrenciesInfoUrl)]
        public async Task<IActionResult> GetCurrenciesInfoLookupAsync(bool withRate = true)
        {
            var currencyLookup = await _repository.GetCurrenciesInfoAsync(withRate);
            var localized = Localize(currencyLookup.ToList(), true);
            return Json(localized);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/lookup/companies/user/{userId:min(1)}
        [HttpGet]
        [Route(LookupApi.UserAccessibleCompaniesUrl)]
        public async Task<IActionResult> GetUserAccessibleCompaniesAsync(int userId)
        {
            var accessibleCompanies = await _repository.GetUserAccessibleCompaniesAsync(userId);
            return Json(accessibleCompanies);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/lookup/fps/company/{companyId:min(1)}/user/{userId:min(1)}
        [HttpGet]
        [Route(LookupApi.UserAccessibleCompanyFiscalPeriodsUrl)]
        public async Task<IActionResult> GetFiscalPeriodsLookupAsync(int companyId, int userId)
        {
            var fiscalPeriodLookup = await _repository.GetUserAccessibleFiscalPeriodsAsync(companyId, userId);
            return Json(fiscalPeriodLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET: api/lookup/branches/company/{companyId:min(1)}/user/{userId:min(1)}
        [HttpGet]
        [Route(LookupApi.UserAccessibleCompanyBranchesUrl)]
        public async Task<IActionResult> GetBranchesLookupAsync(int companyId, int userId)
        {
            var branchLookup = await _repository.GetUserAccessibleBranchesAsync(companyId, userId);
            return Json(branchLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/accgroup/categories
        [HttpGet]
        [Route(LookupApi.AccountGroupCategoriesUrl)]
        public IActionResult GetAccountGroupCategoriesLookup()
        {
            var categoryLookup = _repository.GetAccountGroupCategories();
            var sortedLookup = Localize(categoryLookup, true);
            return Json(sortedLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/accgroups
        [HttpGet]
        [Route(LookupApi.AccountGroupsUrl)]
        public async Task<IActionResult> GetAccountGroupsLookupAsync()
        {
            var accGroupLookup = await _repository.GetAccountGroupsAsync();
            return Json(accGroupLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/types/voucher
        [HttpGet]
        [Route(LookupApi.VoucherSysTypesUrl)]
        public IActionResult GetVoucherTypeslookup()
        {
            var voucherTypes = _repository.GetVoucherTypes().ToList();
            var localized = Localize(voucherTypes);
            return Json(localized);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/types/voucher-line
        [HttpGet]
        [Route(LookupApi.VoucherLineTypesUrl)]
        public IActionResult GetVoucherLineTypesLookup()
        {
            var lineTypes = _repository.GetVoucherLineTypes().ToList();
            var localized = Localize(lineTypes);
            return Json(localized);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/accturnovermodes
        [HttpGet]
        [Route(LookupApi.AccountTurnoversUrl)]
        public IActionResult GetAccountTurnoverModesLookup()
        {
            var turnoverLookup = _repository.GetAccountTurnoverModes();
            var localizedTurnoversLookup = Localize(turnoverLookup);
            return Json(localizedTurnoversLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/inv-acc
        [HttpGet]
        [Route(LookupApi.InventoryAccountsUrl)]
        public async Task<IActionResult> GetInventoryAccountsAsync()
        {
            var inventoryAccounts = await _repository.GetInventoryAccountsAsync();
            return Json(inventoryAccounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/vouchers/references
        [HttpGet]
        [Route(LookupApi.VoucherReferencesUrl)]
        public async Task<IActionResult> GetVoucherReferencesAsync()
        {
            var references = await _repository.GetVoucherReferencesAsync();
            return Json(references);
        }

        #endregion

        #region Security Subsystem API

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/roles
        [HttpGet]
        [Route(LookupApi.RolesUrl)]
        public async Task<IActionResult> GetRolesLookupAsync()
        {
            var rolesLookup = await _repository.GetRolesAsync();
            var sortedLookup = Localize(rolesLookup, true);
            return Json(sortedLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/users
        [HttpGet]
        [Route(LookupApi.UsersUrl)]
        public async Task<IActionResult> GetUsersLookupAsync()
        {
            var lookup = await _repository.GetUsersAsync();
            Array.ForEach(lookup.ToArray(), kv => kv.Value = _strings[kv.Value]);
            var sortedLookup = lookup.OrderBy(item => item.Value).ToList();
            return Json(sortedLookup);
        }

        #endregion

        #region Metadata Subsystem API

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/views
        [HttpGet]
        [Route(LookupApi.EntityViewsUrl)]
        public async Task<IActionResult> GetEntityViewsLookupAsync()
        {
            var viewsLookup = await _repository.GetEntityViewsAsync();
            Array.ForEach(viewsLookup.ToArray(), kv => kv.Value = _strings[kv.Value]);
            viewsLookup = viewsLookup
                .OrderBy(kv => kv.Value)
                .ToList();
            return Json(viewsLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/views/base
        [HttpGet]
        [Route(LookupApi.BaseEntityViewsUrl)]
        public async Task<IActionResult> GetBaseViewsLookupAsync()
        {
            var baseLookup = await _repository.GetBaseEntityViewsAsync();
            Array.ForEach(baseLookup.ToArray(), lookup => lookup.Name = _strings[lookup.Name]);
            return Json(baseLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/lookup/views/{viewId:min(1)}
        [HttpGet]
        [Route(LookupApi.EntityViewUrl)]
        public async Task<IActionResult> GetViewLookupAsync(int viewId)
        {
            var baseLookup = await _repository.GetEntityViewAsync(viewId);
            Array.ForEach(baseLookup.ToArray(), lookup => lookup.Name = _strings[lookup.Name]);
            return Json(baseLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/views/tree
        [HttpGet]
        [Route(LookupApi.TreeViewsUrl)]
        public async Task<IActionResult> GetTreeViewsLookupAsync()
        {
            var treesLookup = await _repository.GetTreeViewsAsync();
            Array.ForEach(treesLookup.ToArray(), kv => kv.Value = _strings[kv.Value]);
            return Json(treesLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/lookup/accbook/views/{viewId:min(1)}/levels
        [HttpGet]
        [Route(LookupApi.AccountBookLevelsUrl)]
        public async Task<IActionResult> GetAccountBookLevelsAsync(int viewId)
        {
            var levels = await _repository.GetAccountBookLevelsAsync(viewId);
            foreach (var level in levels)
            {
                level.Title = LocalizeLevelTitle(level.Title, level.Level);
            }

            return Json(levels);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/entities
        [HttpGet]
        [Route(LookupApi.EntityTypesUrl)]
        public async Task<IActionResult> GetEntityTypesLookupAsync()
        {
            var lookup = await _repository.GetEntityTypesAsync();
            Array.ForEach(lookup.ToArray(), item => item.Name = _strings[item.Name]);
            var sortedLookup = lookup.OrderBy(item => item.Name).ToList();
            return Json(sortedLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/sys/entities
        [HttpGet]
        [Route(LookupApi.SystemEntityTypesUrl)]
        public async Task<IActionResult> GetSystemEntityTypesLookupAsync()
        {
            var lookup = await _repository.GetSystemEntityTypesAsync();
            Array.ForEach(lookup.ToArray(), item => item.Name = _strings[item.Name]);
            var sortedLookup = lookup.OrderBy(item => item.Name).ToList();
            return Json(sortedLookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/lookup/provinces
        [HttpGet]
        [Route(LookupApi.ProvincesUrl)]
        public async Task<IActionResult> GetProvincesAsync()
        {
            var lookup = await _repository.GetProvincesAsync();
            return Json(lookup);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="provinceCode"></param>
        /// <returns></returns>
        // GET: api/lookup/cities/{provinceCode}
        [HttpGet]
        [Route(LookupApi.CitiesUrl)]
        public async Task<IActionResult> GetCitiesAsync(string provinceCode)
        {
            var lookup = await _repository.GetCitiesAsync(provinceCode);
            return Json(lookup);
        }
        #endregion

        private IList<KeyValue> Localize(IList<KeyValue> keyValues, bool isNameSorted = false)
        {
            for (int i = 0; i < keyValues.Count; i++)
            {
                keyValues[i].Value = _strings[keyValues[i].Value];
            }

            return isNameSorted
                ? keyValues.OrderBy(kv => kv.Value).ToList()
                : keyValues;
        }

        private IList<CurrencyInfoViewModel> Localize(IList<CurrencyInfoViewModel> items, bool isNameSorted = false)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Name = _strings[items[i].Name];
            }

            return isNameSorted
                ? items.OrderBy(item => item.Name).ToList()
                : items;
        }

        private string LocalizeLevelTitle(string title, int level)
        {
            return title == "LevelX"
                ? _strings.Format(title, (level + 1).ToString())
                : _strings.Format(title);
        }

        private readonly ILookupRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}
