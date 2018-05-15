using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public partial class LookupController : Controller
    {
        public LookupController(ILookupRepository repository)
        {
            _repository = repository;
        }

        #region Finance Subsystem API

        // GET: api/lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchAccountsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountsLookupAsync(int fpId, int branchId)
        {
            var accountLookup = await _repository.GetAccountsAsync(fpId, branchId);
            return Json(accountLookup);
        }

        // GET: api/lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountsLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetDetailAccountsAsync(fpId, branchId);
            return Json(lookup);
        }

        // GET: api/lookup/costcenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCentersLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetCostCentersAsync(fpId, branchId);
            return Json(lookup);
        }

        // GET: api/lookup/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(LookupApi.FiscalPeriodBranchProjectsUrl)]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        public async Task<IActionResult> GetProjectsLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetProjectsAsync(fpId, branchId);
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

        private ILookupRepository _repository;
    }
}
