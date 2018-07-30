using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
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

        // GET: api/lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsLookupAsync(int fpId, int branchId)
        {
            var accountLookup = await _repository.GetAccountsAsync(fpId, branchId, GridOptions);
            return Json(accountLookup);
        }

        // GET: api/lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountsLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetDetailAccountsAsync(fpId, branchId, GridOptions);
            return Json(lookup);
        }

        // GET: api/lookup/costcenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCentersLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetCostCentersAsync(fpId, branchId, GridOptions);
            return Json(lookup);
        }

        // GET: api/lookup/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectsLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetProjectsAsync(fpId, branchId, GridOptions);
            return Json(lookup);
        }

        // GET: api/lookup/vouchers/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetVouchersLookupAsync(int fpId, int branchId)
        {
            var lang = Request.Headers["Accept-Language"].ToString();
            lang = lang ?? "fa";
            var items = await _repository.GetVouchersAsync(fpId, branchId, GridOptions);
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

        // GET: api/lookup/vouchers/lines/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchVoucherLinesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetVoucherLinesLookupAsync(int fpId, int branchId)
        {
            var items = await _repository.GetVoucherLinesAsync(fpId, branchId, GridOptions);
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
        public async Task<IActionResult> GetFiscalPeriodsLookupAsync(int companyId, int userId)
        {
            var fiscalPeriodLookup = await _repository.GetUserAccessibleFiscalPeriodsAsync(companyId, userId);
            return Json(fiscalPeriodLookup);
        }

        // GET: api/lookup/branches/company/{companyId:min(1)}/user/{userId:min(1)}
        [Route(LookupApi.UserAccessibleCompanyBranchesUrl)]
        public async Task<IActionResult> GetBranchesLookupAsync(int companyId, int userId)
        {
            var branchLookup = await _repository.GetUserAccessibleBranchesAsync(companyId, userId);
            return Json(branchLookup);
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

        #endregion

        private readonly ILookupRepository _repository;
    }
}
