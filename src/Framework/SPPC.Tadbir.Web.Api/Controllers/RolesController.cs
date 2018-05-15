using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class RolesController : ApiControllerBase<RoleFullViewModel>
    {
        public RolesController(ISecurityRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Role; }
        }

        // GET: api/roles
        [Route(RoleApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRolesAsync()
        {
            int itemCount = await _repository.GetRoleCountAsync(GridOptions);
            SetItemCount(itemCount);
            var roles = await _repository.GetRolesAsync(GridOptions);
            return Json(roles);
        }

        // GET: api/roles/new
        [Route(RoleApi.NewRoleUrl)]
        public async Task<IActionResult> GetNewRoleAsync()
        {
            var newRole = await _repository.GetNewRoleAsync();
            return Json(newRole);
        }

        // GET: api/roles/{roleId:min(1)}
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleAsync(int roleId)
        {
            var role = await _repository.GetRoleAsync(roleId);
            return JsonReadResult(role);
        }

        // GET: api/roles/{roleId:min(1)}/details
        [Route(RoleApi.RoleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleDetailsAsync(int roleId)
        {
            var role = await _repository.GetRoleDetailsAsync(roleId);
            return JsonReadResult(role);
        }

        // GET: api/roles/metadata
        [Route(RoleApi.RoleMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleMetadataAsync()
        {
            var metadata = await _repository.GetRoleMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/roles
        [HttpPost]
        [Route(RoleApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Create)]
        public async Task<IActionResult> PostNewRoleAsync([FromBody] RoleFullViewModel role)
        {
            var result = BasicValidationResult(role);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputRole = await _repository.SaveRoleAsync(role);
            return StatusCode(StatusCodes.Status201Created, outputRole);
        }

        // PUT: api/roles/{roleId:min(1)}
        [HttpPut]
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Edit)]
        public async Task<IActionResult> PutModifiedRoleAsync(int roleId, [FromBody] RoleFullViewModel role)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest(_strings.Format(AppStrings.AdminRoleIsReadonly));
            }

            var result = BasicValidationResult(role, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputRole = await _repository.SaveRoleAsync(role);
            return Ok(outputRole);
        }

        // DELETE: api/roles/{roleId:min(1)}
        [HttpDelete]
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingRoleAsync(int roleId)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequest(_strings.Format(AppStrings.AdminRoleIsReadonly));
            }

            var role = await _repository.GetRoleBriefAsync(roleId);
            if (role == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.Role));
            }

            if (await _repository.IsAssignedRoleAsync(roleId))
            {
                return BadRequest(String.Format(_strings.Format(AppStrings.CannotDeleteAssignedRole), role.Name));
            }

            await _repository.DeleteRoleAsync(roleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/roles/{roleId:min(1)}/branches
        [Route(RoleApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleBranchesAsync(int roleId)
        {
            var branches = await _repository.GetRoleBranchesAsync(roleId);
            return JsonReadResult(branches);
        }

        // PUT: api/roles/{roleId:min(1)}/branches
        [HttpPut]
        [Route(RoleApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignBranches)]
        public async Task<IActionResult> PutModifiedRoleBranchesAsync(int roleId, [FromBody] RoleBranchesViewModel roleBranches)
        {
            var result = BasicValidationResult(roleBranches, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleBranchesAsync(roleBranches);
            return Ok();
        }

        // GET: api/roles/{roleId:min(1)}/users
        [Route(RoleApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleUsersAsync(int roleId)
        {
            var users = await _repository.GetRoleUsersAsync(roleId);
            return JsonReadResult(users);
        }

        // PUT: api/roles/{roleId:min(1)}/users
        [HttpPut]
        [Route(RoleApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignUsers)]
        public async Task<IActionResult> PutModifiedRoleUsersAsync(int roleId, [FromBody] RoleUsersViewModel roleUsers)
        {
            var result = BasicValidationResult(roleUsers, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleUsersAsync(roleUsers);
            return Ok();
        }

        protected override IActionResult BasicValidationResult(RoleFullViewModel role, int roleId = 0)
        {
            if (role == null || role.Role == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Role));
            }

            if (roleId != role.Role.Id)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.Role));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        private IActionResult BasicValidationResult<TModel>(TModel model, int roleId)
        {
            if (model == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Role));
            }

            int id = (int)Reflector.GetProperty(model, "Id");
            if (roleId != id)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.Role));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        private ISecurityRepository _repository;
    }
}
