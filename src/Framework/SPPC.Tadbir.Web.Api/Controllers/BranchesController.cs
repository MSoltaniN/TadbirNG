using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class BranchesController : ApiControllerBase<BranchViewModel>
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
        [Route(BranchApi.CompanyBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchesAsync(int companyId)
        {
            int itemCount = await _repository.GetCountAsync(companyId, GridOptions);
            SetItemCount(itemCount);
            var branches = await _repository.GetBranchesAsync(companyId, GridOptions);
            return Json(branches);
        }

        // GET: api/branches/{branchId:min(1)}
        [Route(BranchApi.BranchUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchAsync(int branchId)
        {
            var branch = await _repository.GetBranchAsync(branchId);
            return JsonReadResult(branch);
        }

        // GET: api/branches/metadata
        [Route(BranchApi.BranchMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchMetadataAsync()
        {
            var metadata = await _repository.GetBranchMetadataAsync();
            return JsonReadResult(metadata);
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

            var outputItem = await _repository.SaveBranchAsync(branch);
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
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
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

        // GET: api/branches/{branchId:min(1)}/roles
        [Route(BranchApi.BranchRolesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchRolesAsync(int branchId)
        {
            var roles = await _repository.GetBranchRolesAsync(branchId);
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

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var branch = await _repository.GetBranchAsync(item);
            if (branch == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Branch), item);
            }

            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                   _strings[AppStrings.CannotDeleteNonLeafItem], _strings[AppStrings.Branch], String.Format("'{0}'", branch.Name));
            }

            return message;
        }

        private IBranchRepository _repository;
    }
}