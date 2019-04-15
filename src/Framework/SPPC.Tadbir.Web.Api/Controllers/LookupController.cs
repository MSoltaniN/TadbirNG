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
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public partial class LookupController : ApiControllerBase
    {
        public LookupController(ILookupRepository repository, IStringLocalizer<AppStrings> strings)
        {
            _repository = repository;
            _strings = strings;
        }

        #region Finance Subsystem API

        // GET: api/lookup/accounts
        [Route(LookupApi.EnvironmentAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsLookupAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var accountLookup = await _repository.GetAccountsAsync(GridOptions);
            return Json(accountLookup);
        }

        // GET: api/lookup/faccounts
        [Route(LookupApi.EnvironmentDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountsLookupAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var lookup = await _repository.GetDetailAccountsAsync(GridOptions);
            return Json(lookup);
        }

        // GET: api/lookup/costcenters
        [Route(LookupApi.EnvironmentCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCentersLookupAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var lookup = await _repository.GetCostCentersAsync(GridOptions);
            return Json(lookup);
        }

        // GET: api/lookup/projects
        [Route(LookupApi.EnvironmentProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectsLookupAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var lookup = await _repository.GetProjectsAsync(GridOptions);
            return Json(lookup);
        }

        // GET: api/lookup/vouchers
        [Route(LookupApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetVouchersLookupAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
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

        // GET: api/lookup/vouchers/lines
        [Route(LookupApi.EnvironmentVoucherLinesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetVoucherLinesLookupAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var items = await _repository.GetVoucherLinesAsync(GridOptions);
            var lookup = items.ToList();
            foreach (var kv in lookup)
            {
                var valueItems = kv.Value.Split('|');
                kv.Value = String.Format(_strings[valueItems[0]], valueItems[1], valueItems[2], valueItems[3]);
            }

            return Json(lookup);
        }

        // GET: api/lookup/currencies
        [Route(LookupApi.CurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetCurrenciesLookupAsync()
        {
            var currencyLookup = await _repository.GetCurrenciesAsync();
            return Json(currencyLookup);
        }

        // GET: api/lookup/companies/user/{userId:min(1)}
        [Route(LookupApi.UserAccessibleCompaniesUrl)]
        public async Task<IActionResult> GetUserAccessibleCompaniesAsync(int userId)
        {
            var accessibleCompanies = await _repository.GetUserAccessibleCompaniesAsync(userId);
            return Json(accessibleCompanies);
        }

        // GET: api/lookup/fps/company/{companyId:min(1)}/user/{userId:min(1)}
        [Route(LookupApi.UserAccessibleCompanyFiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodsLookupAsync(int companyId, int userId)
        {
            var fiscalPeriodLookup = await _repository.GetUserAccessibleFiscalPeriodsAsync(companyId, userId);
            return Json(fiscalPeriodLookup);
        }

        // GET: api/lookup/branches/company/{companyId:min(1)}/user/{userId:min(1)}
        [Route(LookupApi.UserAccessibleCompanyBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchesLookupAsync(int companyId, int userId)
        {
            var branchLookup = await _repository.GetUserAccessibleBranchesAsync(companyId, userId);
            return Json(branchLookup);
        }

        // GET: api/lookup/accgroup/categories
        [Route(LookupApi.AccountGroupCategoriesUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public IActionResult GetAccountGroupCategoriesLookup()
        {
            var categoryLookup = _repository.GetAccountGroupCategories();
            var sortedLookup = Localize(categoryLookup);
            return Json(sortedLookup);
        }

        // GET: api/lookup/accgroups
        [Route(LookupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.AccountGroup, (int)AccountGroupPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsLookupAsync()
        {
            var accGroupLookup = await _repository.GetAccountGroupsAsync();
            return Json(accGroupLookup);
        }

        // GET: api/lookup/accturnovermodes
        [Route(LookupApi.AccountTurnoversUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public IActionResult GetAccountTurnoverModeslookup()
        {
            var turnoverLookup = _repository.GetAccountTurnoverModes();
            var localizedTurnoversLookup = Localize(turnoverLookup);
            return Json(localizedTurnoversLookup);
        }
        #endregion

        #region Security Subsystem API

        // GET: api/lookup/roles
        [Route(LookupApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRolesLookupAsync()
        {
            var rolesLookup = await _repository.GetRolesAsync();
            return Json(rolesLookup);
        }

        #endregion

        #region Metadata Subsystem API

        // GET: api/lookup/views
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

        // GET: api/lookup/views/tree
        [Route(LookupApi.TreeViewsUrl)]
        public async Task<IActionResult> GetTreeViewsLookupAsync()
        {
            var treesLookup = await _repository.GetTreeViewsAsync();
            Array.ForEach(treesLookup.ToArray(), kv => kv.Value = _strings[kv.Value]);
            return Json(treesLookup);
        }

        #endregion

        private IList<KeyValue> Localize(IList<KeyValue> keyValues)
        {
            for (int i = 0; i < keyValues.Count; i++)
            {
                keyValues[i].Value = _strings[keyValues[i].Value];
            }

            return keyValues
                .OrderBy(kv => kv.Value)
                .ToList();
        }

        private readonly ILookupRepository _repository;
    }
}
