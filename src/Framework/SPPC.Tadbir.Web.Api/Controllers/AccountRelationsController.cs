using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountRelationsController : ApiControllerBase<AccountItemRelationsViewModel>
    {
        public AccountRelationsController(
            IRelationRepository repository, IConfigRepository configRepository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _configRepository = configRepository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.AccountRelations; }
        }

        // GET: api/relations/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        [Route(AccountRelationApi.FiscalPeriodBranchAccountsUrl)]
        public async Task<IActionResult> GetConnectableAccountsAsync(int fpId, int branchId)
        {
            var config = _configRepository.GetRelationsConfig();
            var accounts = await _repository.GetConnectableAccountsAsync(
                fpId, branchId, config.UseLeafAccounts, GridOptions);
            return Json(accounts);
        }

        // GET: api/relations/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        [Route(AccountRelationApi.FiscalPeriodBranchDetailAccountsUrl)]
        public async Task<IActionResult> GetConnectableDetailAccountsAsync(int fpId, int branchId)
        {
            var config = _configRepository.GetRelationsConfig();
            var detailAccounts = await _repository.GetConnectableDetailAccountsAsync(
                fpId, branchId, config.UseLeafDetails, GridOptions);
            return Json(detailAccounts);
        }

        // GET: api/relations/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        [Route(AccountRelationApi.FiscalPeriodBranchCostCentersUrl)]
        public async Task<IActionResult> GetConnectableCostCentersAsync(int fpId, int branchId)
        {
            var config = _configRepository.GetRelationsConfig();
            var costCenters = await _repository.GetConnectableCostCentersAsync(
                fpId, branchId, config.UseLeafCostCenters, GridOptions);
            return Json(costCenters);
        }

        // GET: api/relations/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        [Route(AccountRelationApi.FiscalPeriodBranchProjectsUrl)]
        public async Task<IActionResult> GetConnectableProjectsAsync(int fpId, int branchId)
        {
            var config = _configRepository.GetRelationsConfig();
            var projects = await _repository.GetConnectableProjectsAsync(
                fpId, branchId, config.UseLeafProjects, GridOptions);
            return Json(projects);
        }

        // GET: api/relations/account/{accountId:min(1)}/faccounts
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountDetailAccountsAsync(int accountId)
        {
            var detailAccounts = await _repository.GetAccountDetailAccountsAsync(accountId, GridOptions);
            return Json(detailAccounts);
        }

        // PUT: api/relations/account/{accountId:min(1)}/faccounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ManageRelationships)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> PutModifiedAccountDetailAccountsAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveAccountDetailAccountsAsync(relations);
            return Ok();
        }

        // GET: api/relations/account/{accountId:min(1)}/ccenters
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.CostCentersRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountCostCentersAsync(int accountId)
        {
            var costCenters = await _repository.GetAccountCostCentersAsync(accountId, GridOptions);
            return Json(costCenters);
        }

        // PUT: api/relations/account/{accountId:min(1)}/ccenters
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ManageRelationships)]
        [Route(AccountRelationApi.CostCentersRelatedToAccountUrl)]
        public async Task<IActionResult> PutModifiedAccountCostCentersAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveAccountCostCentersAsync(relations);
            return Ok();
        }

        // GET: api/relations/account/{accountId:min(1)}/projects
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.ProjectsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountProjectsAsync(int accountId)
        {
            var projects = await _repository.GetAccountProjectsAsync(accountId, GridOptions);
            return Json(projects);
        }

        // PUT: api/relations/account/{accountId:min(1)}/projects
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ManageRelationships)]
        [Route(AccountRelationApi.ProjectsRelatedToAccountUrl)]
        public async Task<IActionResult> PutModifiedAccountProjectsAsync(
            int accountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, accountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveAccountProjectsAsync(relations);
            return Ok();
        }

        // GET: api/relations/faccount/{faccountId:min(1)}/accounts
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToDetailAccountUrl)]
        public async Task<IActionResult> GetDetailAccountAccountsAsync(int faccountId)
        {
            var accounts = await _repository.GetDetailAccountAccountsAsync(faccountId, GridOptions);
            return Json(accounts);
        }

        // PUT: api/relations/faccount/{faccountId:min(1)}/accounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ManageRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToDetailAccountUrl)]
        public async Task<IActionResult> PutModifiedDetailAccountAccountsAsync(
            int faccountId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, faccountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveDetailAccountAccountsAsync(relations);
            return Ok();
        }

        // GET: api/relations/ccenter/{ccenterId:min(1)}/accounts
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToCostCenterUrl)]
        public async Task<IActionResult> GetCostCenterAccountsAsync(int ccenterId)
        {
            var accounts = await _repository.GetCostCenterAccountsAsync(ccenterId, GridOptions);
            return Json(accounts);
        }

        // PUT: api/relations/ccenter/{ccenterId:min(1)}/accounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ManageRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToCostCenterUrl)]
        public async Task<IActionResult> PutModifiedCostCenterAccountsAsync(
            int ccenterId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, ccenterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveCostCenterAccountsAsync(relations);
            return Ok();
        }

        // GET: api/relations/project/{projectId:min(1)}/accounts
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToProjectUrl)]
        public async Task<IActionResult> GetProjectAccountsAsync(int projectId)
        {
            var accounts = await _repository.GetProjectAccountsAsync(projectId, GridOptions);
            return Json(accounts);
        }

        // PUT: api/relations/project/{projectId:min(1)}/accounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ManageRelationships)]
        [Route(AccountRelationApi.AccountsRelatedToProjectUrl)]
        public async Task<IActionResult> PutModifiedProjectAccountsAsync(
            int projectId, [FromBody] AccountItemRelationsViewModel relations)
        {
            var result = BasicValidationResult(relations, projectId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveProjectAccountsAsync(relations);
            return Ok();
        }

        private IRelationRepository _repository;
        private IConfigRepository _configRepository;
    }
}