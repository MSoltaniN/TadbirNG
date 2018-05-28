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
            var accounts = await _repository.GetConnectableAccountsAsync(fpId, branchId, config.UseLeafAccounts);
            return Json(accounts);
        }

        // GET: api/relations/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        [Route(AccountRelationApi.FiscalPeriodBranchDetailAccountsUrl)]
        public async Task<IActionResult> GetConnectableDetailAccountsAsync(int fpId, int branchId)
        {
            var config = _configRepository.GetRelationsConfig();
            var detailAccounts = await _repository.GetConnectableDetailAccountsAsync(
                fpId, branchId, config.UseLeafDetails);
            return Json(detailAccounts);
        }

        // GET: api/relations/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        [Route(AccountRelationApi.FiscalPeriodBranchCostCentersUrl)]
        public async Task<IActionResult> GetConnectableCostCentersAsync(int fpId, int branchId)
        {
            var config = _configRepository.GetRelationsConfig();
            var costCenters = await _repository.GetConnectableCostCentersAsync(
                fpId, branchId, config.UseLeafCostCenters);
            return Json(costCenters);
        }

        // GET: api/relations/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        [Route(AccountRelationApi.FiscalPeriodBranchProjectsUrl)]
        public async Task<IActionResult> GetConnectableProjectsAsync(int fpId, int branchId)
        {
            var config = _configRepository.GetRelationsConfig();
            var projects = await _repository.GetConnectableProjectsAsync(fpId, branchId, config.UseLeafProjects);
            return Json(projects);
        }

        // GET: api/relations/account/{accountId:min(1)}/faccounts
        [AuthorizeRequest(SecureEntity.AccountRelations, (int)AccountRelationPermissions.ViewRelationships)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountDetailAccountsAsync(int accountId)
        {
            var config = _configRepository.GetRelationsConfig();
            var detailAccounts = await _repository.GetAccountDetailAccountsAsync(accountId, config.UseLeafDetails);
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
            var config = _configRepository.GetRelationsConfig();
            var costCenters = await _repository.GetAccountCostCentersAsync(accountId, config.UseLeafCostCenters);
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
            var config = _configRepository.GetRelationsConfig();
            var projects = await _repository.GetAccountProjectsAsync(accountId, config.UseLeafProjects);
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

        private IRelationRepository _repository;
        private IConfigRepository _configRepository;
    }
}