using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class AccountRelationsController : ValidatingController<AccountItemRelationsViewModel>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="configRepository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public AccountRelationsController(
            IRelationRepository repository, IConfigRepository configRepository,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            Verify.ArgumentNotNull(configRepository, "configRepository");
            _repository = repository;
            _config = configRepository.GetConfigByTypeAsync<RelationsConfig>().Result;
        }

        /// <summary>
        ///
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.AccountRelations; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/accounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        [Route(AccountRelationApi.EnvironmentAccountsUrl)]
        public async Task<IActionResult> GetEnvironmentConnectableAccountsAsync()
        {
            var accounts = await _repository.GetConnectableAccountsAsync(GridOptions);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/faccounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        [Route(AccountRelationApi.EnvironmentDetailAccountsUrl)]
        public async Task<IActionResult> GetEnvironmentConnectableDetailAccountsAsync()
        {
            var detailAccounts = await _repository.GetConnectableDetailAccountsAsync(_config.UseLeafDetails, GridOptions);
            return Json(detailAccounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/ccenters
        [HttpGet]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        [Route(AccountRelationApi.EnvironmentCostCentersUrl)]
        public async Task<IActionResult> GetEnvironmentConnectableCostCentersAsync()
        {
            var costCenters = await _repository.GetConnectableCostCentersAsync(_config.UseLeafCostCenters, GridOptions);
            return Json(costCenters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/projects
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        [Route(AccountRelationApi.EnvironmentProjectsUrl)]
        public async Task<IActionResult> GetEnvironmentConnectableProjectsAsync()
        {
            var projects = await _repository.GetConnectableProjectsAsync(_config.UseLeafProjects, GridOptions);
            return Json(projects);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/accounts/lookup
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        [Route(AccountRelationApi.EnvironmentAccountsLookupUrl)]
        public async Task<IActionResult> GetUsableAccountsLookupAsync()
        {
            var accounts = await _repository.GetUsableAccountsLookupAsync(GridOptions);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/faccounts/lookup
        [HttpGet]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        [Route(AccountRelationApi.EnvironmentDetailAccountsLookupUrl)]
        public async Task<IActionResult> GetUsableDetailAccountsLookupAsync()
        {
            var detailAccounts = await _repository.GetUsableDetailAccountsLookupAsync(GridOptions);
            return Json(detailAccounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/ccenters/lookup
        [HttpGet]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        [Route(AccountRelationApi.EnvironmentCostCentersLookupUrl)]
        public async Task<IActionResult> GetUsableCostCentersLookupAsync()
        {
            var costCenters = await _repository.GetUsableCostCentersLookupAsync(GridOptions);
            return Json(costCenters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/relations/projects/lookup
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        [Route(AccountRelationApi.EnvironmentProjectsLookupUrl)]
        public async Task<IActionResult> GetUsableProjectsLookupAsync()
        {
            var projects = await _repository.GetUsableProjectsLookupAsync(GridOptions);
            return Json(projects);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/account/{accountId:min(1)}/faccounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountDetailAccountsAsync(int accountId)
        {
            var detailAccounts = await _repository.GetAccountDetailAccountsAsync(accountId, GridOptions, false);
            return Json(detailAccounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/account/{accountId:min(1)}/faccounts/usable
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.UsableDetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> GetUsableAccountDetailAccountsAsync(int accountId)
        {
            var detailAccounts = await _repository.GetAccountDetailAccountsAsync(accountId, GridOptions);
            return Json(detailAccounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // POST: api/relations/account/{accountId:min(1)}/faccounts
        [HttpPost]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> PostNewAccountDetailAccountsAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.AddAccountDetailAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // PUT: api/relations/account/{accountId:min(1)}/faccounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> PutModifiedAccountDetailAccountsAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.RemoveAccountDetailAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/account/{accountId:min(1)}/ccenters
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.CostCentersRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountCostCentersAsync(int accountId)
        {
            var costCenters = await _repository.GetAccountCostCentersAsync(accountId, GridOptions, false);
            return Json(costCenters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/account/{accountId:min(1)}/ccenters/usable
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.UsableCostCentersRelatedToAccountUrl)]
        public async Task<IActionResult> GetUsableAccountCostCentersAsync(int accountId)
        {
            var costCenters = await _repository.GetAccountCostCentersAsync(accountId, GridOptions);
            return Json(costCenters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // POST: api/relations/account/{accountId:min(1)}/ccenters
        [HttpPost]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.CostCentersRelatedToAccountUrl)]
        public async Task<IActionResult> PostNewAccountCostCentersAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.AddAccountCostCentersAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // PUT: api/relations/account/{accountId:min(1)}/ccenters
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.CostCentersRelatedToAccountUrl)]
        public async Task<IActionResult> PutModifiedAccountCostCentersAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.RemoveAccountCostCentersAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/account/{accountId:min(1)}/projects
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.ProjectsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountProjectsAsync(int accountId)
        {
            var projects = await _repository.GetAccountProjectsAsync(accountId, GridOptions, false);
            return Json(projects);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/account/{accountId:min(1)}/projects/usable
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.UsableProjectsRelatedToAccountUrl)]
        public async Task<IActionResult> GetUsableAccountProjectsAsync(int accountId)
        {
            var projects = await _repository.GetAccountProjectsAsync(accountId, GridOptions);
            return Json(projects);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // POST: api/relations/account/{accountId:min(1)}/projects
        [HttpPost]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.ProjectsRelatedToAccountUrl)]
        public async Task<IActionResult> PostNewAccountProjectsAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.AddAccountProjectsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // PUT: api/relations/account/{accountId:min(1)}/projects
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.ProjectsRelatedToAccountUrl)]
        public async Task<IActionResult> PutModifiedAccountProjectsAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.RemoveAccountProjectsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <returns></returns>
        // GET: api/relations/faccount/{faccountId:min(1)}/accounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToDetailAccountUrl)]
        public async Task<IActionResult> GetDetailAccountAccountsAsync(int faccountId)
        {
            var accounts = await _repository.GetDetailAccountAccountsAsync(faccountId, GridOptions);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // POST: api/relations/faccount/{faccountId:min(1)}/accounts
        [HttpPost]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToDetailAccountUrl)]
        public async Task<IActionResult> PostNewDetailAccountAccountsAsync(
            int faccountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, faccountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.AddDetailAccountAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // PUT: api/relations/faccount/{faccountId:min(1)}/accounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToDetailAccountUrl)]
        public async Task<IActionResult> PutModifiedDetailAccountAccountsAsync(
            int faccountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, faccountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.RemoveDetailAccountAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <returns></returns>
        // GET: api/relations/ccenter/{ccenterId:min(1)}/accounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToCostCenterUrl)]
        public async Task<IActionResult> GetCostCenterAccountsAsync(int ccenterId)
        {
            var accounts = await _repository.GetCostCenterAccountsAsync(ccenterId, GridOptions);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // POST: api/relations/ccenter/{ccenterId:min(1)}/accounts
        [HttpPost]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToCostCenterUrl)]
        public async Task<IActionResult> PostNewCostCenterAccountsAsync(
            int ccenterId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, ccenterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.AddCostCenterAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // PUT: api/relations/ccenter/{ccenterId:min(1)}/accounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToCostCenterUrl)]
        public async Task<IActionResult> PutModifiedCostCenterAccountsAsync(
            int ccenterId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, ccenterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.RemoveCostCenterAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/relations/project/{projectId:min(1)}/accounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToProjectUrl)]
        public async Task<IActionResult> GetProjectAccountsAsync(int projectId)
        {
            var accounts = await _repository.GetProjectAccountsAsync(projectId, GridOptions);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // POST: api/relations/project/{projectId:min(1)}/accounts
        [HttpPost]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToProjectUrl)]
        public async Task<IActionResult> PostNewProjectAccountsAsync(
            int projectId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, projectId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.AddProjectAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        // PUT: api/relations/project/{projectId:min(1)}/accounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.SaveRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToProjectUrl)]
        public async Task<IActionResult> PutModifiedProjectAccountsAsync(
            int projectId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, projectId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.RemoveProjectAccountsAsync(relations);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/free/accounts/{accountId:min(1)}/faccounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        [Route(AccountRelationApi.DetailAccountsNotRelatedToAccountUrl)]
        public async Task<IActionResult> GetConnectableDetailAccountsForAccountAsync(int accountId)
        {
            var detailAccounts = await _repository.GetConnectableDetailAccountsForAccountAsync(
                accountId, _config.UseLeafDetails, GridOptions);
            return Json(detailAccounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/free/accounts/{accountId:min(1)}/ccenters
        [HttpGet]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        [Route(AccountRelationApi.CostCentersNotRelatedToAccountUrl)]
        public async Task<IActionResult> GetConnectableCostCentersForAccountAsync(int accountId)
        {
            var costCenters = await _repository.GetConnectableCostCentersForAccountAsync(
                accountId, _config.UseLeafCostCenters, GridOptions);
            return Json(costCenters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // GET: api/relations/free/accounts/{accountId:min(1)}/projects
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        [Route(AccountRelationApi.ProjectsNotRelatedToAccountUrl)]
        public async Task<IActionResult> GetConnectableProjectsForAccountAsync(int accountId)
        {
            var projects = await _repository.GetConnectableProjectsForAccountAsync(
                accountId, _config.UseLeafProjects, GridOptions);
            return Json(projects);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="faccountId"></param>
        /// <returns></returns>
        // GET: api/relations/free/faccounts/{faccountId:min(1)}/accounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        [Route(AccountRelationApi.AccountsNotRelatedToDetailAccountUrl)]
        public async Task<IActionResult> GetConnectableAccountsForDetailAccountAsync(int faccountId)
        {
            var accounts = await _repository.GetConnectableAccountsForDetailAccountAsync(
                faccountId, GridOptions);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ccenterId"></param>
        /// <returns></returns>
        // GET: api/relations/free/ccenters/{ccenterId:min(1)}/accounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        [Route(AccountRelationApi.AccountsNotRelatedToCostCenterUrl)]
        public async Task<IActionResult> GetConnectableAccountsForCostCenterAsync(int ccenterId)
        {
            var accounts = await _repository.GetConnectableAccountsForCostCenterAsync(
                ccenterId, GridOptions);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/relations/free/projects/{projectId:min(1)}/accounts
        [HttpGet]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        [Route(AccountRelationApi.AccountsNotRelatedToProjectUrl)]
        public async Task<IActionResult> GetConnectableAccountsForProjectAsync(int projectId)
        {
            var accounts = await _repository.GetConnectableAccountsForProjectAsync(
                projectId, GridOptions);
            return Json(accounts);
        }

        private IRelationRepository _repository;
        private RelationsConfig _config;
    }
}