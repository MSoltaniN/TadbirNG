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
        public AccountRelationsController(IRelationRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return "Relationships"; }     // Temporarily hard-coded
        }

        // GET: api/relations/account/{accountId:min(1)}/faccounts
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountDetailAccountsAsync(int accountId)
        {
            var detailAccounts = await _repository.GetRelatedDetailAccountsAsync(accountId);
            return Json(detailAccounts);
        }

        // PUT: api/relations/account/{accountId:min(1)}/faccounts
        [HttpPut]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
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
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        [Route(AccountRelationApi.CostCentersRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountCostCentersAsync(int accountId)
        {
            var costCenters = await _repository.GetRelatedCostCentersAsync(accountId);
            return Json(costCenters);
        }

        // GET: api/relations/account/{accountId:min(1)}/projects
        [AuthorizeRequest(SecureEntity.Project, (int)ProjectPermissions.View)]
        [Route(AccountRelationApi.ProjectsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountProjectsAsync(int accountId)
        {
            var projects = await _repository.GetRelatedProjectsAsync(accountId);
            return Json(projects);
        }

        private IRelationRepository _repository;
    }
}