using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountRelationsController : Controller
    {
        public AccountRelationsController(IRelationRepository repository)
        {
            _repository = repository;
        }

        // GET: api/relations/account/{accountId:min(1)}/faccounts
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        [Route(AccountRelationApi.DetailAccountsRelatedToAccountUrl)]
        public async Task<IActionResult> GetAccountDetailAccountsAsync(int accountId)
        {
            var detailAccounts = await _repository.GetRelatedDetailAccountsAsync(accountId);
            return Json(detailAccounts);
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