using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class BranchesController : ValidatingController<BranchViewModel>
    {
        public BranchesController(
            IBranchRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Branch; }
        }

        // GET: api/branches/company/{companyId:min(1)}
        [HttpGet]
        [Route(BranchApi.CompanyBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchesAsync()
        {
            var branches = await _repository.GetBranchesAsync(GridOptions);
            return JsonListResult(branches);
        }

        // GET: api/branches/{branchId:min(1)}
        [HttpGet]
        [Route(BranchApi.BranchUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchAsync(int branchId)
        {
            var branch = await _repository.GetBranchAsync(branchId);
            return JsonReadResult(branch);
        }

        // GET: api/branches/root
        [HttpGet]
        [Route(BranchApi.RootBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetRootBranchesAsync()
        {
            var rootBranches = await _repository.GetRootBranchesAsync();
            return Json(rootBranches);
        }

        // GET: api/branches/{branchId:min(1)}/children
        [HttpGet]
        [Route(BranchApi.BranchChildrenUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchChildrenAsync(int branchId)
        {
            var children = await _repository.GetBranchChildrenAsync(branchId);
            return JsonReadResult(children);
        }

        // POST: api/branches
        [HttpPost]
        [Route(BranchApi.BranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Create)]
        public async Task<IActionResult> PostNewBranchAsync([FromBody] BranchViewModel branch)
        {
            var result = BasicValidationResult(branch);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!await _repository.IsValidBranchAsync(branch))
            {
                return BadRequest(_strings.Format(AppStrings.RootBranchAlreadyDefined));
            }

            var outputItem = await _repository.SaveBranchAsync(branch);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // POST: api/branches/root
        [HttpPost]
        [Route(BranchApi.RootBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Create)]
        public async Task<IActionResult> PostRootBranchValidationAsync([FromBody] BranchViewModel branch)
        {
            if (!await _repository.IsValidBranchAsync(branch))
            {
                return BadRequest(_strings.Format(AppStrings.RootBranchAlreadyDefined));
            }

            return NoContent();
        }

        // POST: api/branches/init
        [HttpPost]
        [Route(BranchApi.BrancheInitialUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Create)]
        public async Task<IActionResult> PostInitialBranchAsync([FromBody]BranchViewModel branch)
        {
            var result = BasicValidationResult(branch);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveInitialBranchAsync(branch);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/branches/{branchId:min(1)}
        [HttpPut]
        [Route(BranchApi.BranchUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Edit)]
        public async Task<IActionResult> PutModifiedBranchAsync(
            int branchId, [FromBody] BranchViewModel branch)
        {
            var result = BasicValidationResult(branch, branchId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveBranchAsync(branch);
            return OkReadResult(outputItem);
        }

        // DELETE: api/branches/{branchId:min(1)}
        [HttpDelete]
        [Route(BranchApi.BranchUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingBranchAsync(int branchId)
        {
            string result = await ValidateDeleteAsync(branchId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteBranchAsync(branchId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // DELETE: api/branches/{branchId:min(1)}/data
        [HttpDelete]
        [Route(BranchApi.BranchDataUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingBranchWithDataAsync(int branchId)
        {
            string result = await BasicValidateDeleteAsync(branchId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteBranchWithDataAsync(branchId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/branches
        [HttpPut]
        [Route(BranchApi.BranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Delete)]
        public async Task<IActionResult> PutExistingBranchesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await ValidateGroupDeleteAsync(actionDetail.Items);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            await _repository.DeleteBranchesAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/branches/{branchId:min(1)}/roles
        [HttpGet]
        [Route(BranchApi.BranchRolesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchRolesAsync(int branchId)
        {
            var roles = await _repository.GetBranchRolesAsync(branchId);
            Localize(roles);
            return JsonReadResult(roles);
        }

        // PUT: api/branches/{branchId:min(1)}/roles
        [HttpPut]
        [Route(BranchApi.BranchRolesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.AssignRoles)]
        public async Task<IActionResult> PutModifiedBranchRolesAsync(
            int branchId, [FromBody] RelatedItemsViewModel branchRoles)
        {
            var result = BasicValidationResult(branchRoles, branchId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveBranchRolesAsync(branchRoles);
            return Ok();
        }

        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string error = await BasicValidateDeleteAsync(item);
            if (!String.IsNullOrEmpty(error))
            {
                return error;
            }

            var canDelete = await _repository.CanDeleteBranchAsync(item);
            if (canDelete == false)
            {
                var branch = await _repository.GetBranchAsync(item);
                return _strings.Format(AppStrings.CantDeleteItemWithData, EntityNameKey, branch.Name);
            }

            return String.Empty;
        }

        private async Task<string> BasicValidateDeleteAsync(int item)
        {
            if (item == SecurityContext.User.BranchId)
            {
                return _strings.Format(AppStrings.CantDeleteCurrentItem, EntityNameKey);
            }

            var branch = await _repository.GetBranchAsync(item);
            if (branch == null)
            {
                return _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Branch, item.ToString());
            }

            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                return _strings.Format(AppStrings.CantDeleteNonLeafItem, AppStrings.Branch, branch.Name);
            }

            return String.Empty;
        }

        private void Localize(RelatedItemsViewModel roles)
        {
            Array.ForEach(roles.RelatedItems.ToArray(), item => item.Name = _strings[item.Name]);
        }

        private IBranchRepository _repository;
    }
}