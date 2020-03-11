using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class RolesController : ValidatingController<RoleFullViewModel>
    {
        public RolesController(IRoleRepository repository, IStringLocalizer<AppStrings> strings)
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
            SetRowNumbers(roles);
            Localize(roles);
            return Json(roles);
        }

        // GET: api/roles/new
        [Route(RoleApi.NewRoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Create)]
        public async Task<IActionResult> GetNewRoleAsync()
        {
            var newRole = await _repository.GetNewRoleAsync();
            LocalizeRole(newRole);
            return Json(newRole);
        }

        // GET: api/roles/{roleId:min(1)}
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleAsync(int roleId)
        {
            var role = await _repository.GetRoleAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            LocalizeRole(role);
            return JsonReadResult(role);
        }

        // GET: api/roles/{roleId:min(1)}/details
        [Route(RoleApi.RoleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleDetailsAsync(int roleId)
        {
            var role = await _repository.GetRoleDetailsAsync(roleId);
            LocalizeRoleDetails(role);
            return JsonReadResult(role);
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
                return BadRequest(String.Format(
                    _strings.Format(AppStrings.CannotDeleteAssignedRole), role.Name));
            }

            if (await _repository.IsRoleRelatedToBranchAsync(roleId))
            {
                return BadRequest(String.Format(
                    _strings[AppStrings.CannotDeleteRoleHavingRelation], role.Name, _strings[AppStrings.Branch]));
            }

            if (await _repository.IsRoleRelatedToFiscalPeriodAsync(roleId))
            {
                return BadRequest(String.Format(
                    _strings[AppStrings.CannotDeleteRoleHavingRelation], role.Name, _strings[AppStrings.FiscalPeriod]));
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
        public async Task<IActionResult> PutModifiedRoleBranchesAsync(
            int roleId, [FromBody] RelatedItemsViewModel roleBranches)
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
        public async Task<IActionResult> PutModifiedRoleUsersAsync(
            int roleId, [FromBody] RelatedItemsViewModel roleUsers)
        {
            var result = BasicValidationResult(roleUsers, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleUsersAsync(roleUsers);
            return Ok();
        }

        // GET: api/roles/{roleId:min(1)}/fperiods
        [Route(RoleApi.RoleFiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleFiscalPeriodsAsync(int roleId)
        {
            var fiscalPeriods = await _repository.GetRoleFiscalPeriodsAsync(roleId);
            return JsonReadResult(fiscalPeriods);
        }

        // PUT: api/roles/{roleId:min(1)}/fperiods
        [HttpPut]
        [Route(RoleApi.RoleFiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.AssignFiscalPeriods)]
        public async Task<IActionResult> PutModifiedRoleFiscalPeriodsAsync(
            int roleId, [FromBody] RelatedItemsViewModel roleFiscalPeriods)
        {
            var result = BasicValidationResult(roleFiscalPeriods, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleFiscalPeriodsAsync(roleFiscalPeriods);
            return Ok();
        }

        // GET: api/roles/{roleId:min(2)}/rowaccess
        [Route(RoleApi.RowAccessSettingsUrl)]
        [AuthorizeRequest(SecureEntity.RowAccess, (int)RowAccessPermissions.ViewRowAccess)]
        public async Task<IActionResult> GetRowAccessSettingsForRoleAsync(int roleId)
        {
            var settings = await _repository.GetRowAccessSettingsAsync(roleId);
            return Json(settings);
        }

        // PUT: api/roles/{roleId:min(2)}/rowaccess
        [HttpPut]
        [Route(RoleApi.RowAccessSettingsUrl)]
        [AuthorizeRequest(SecureEntity.RowAccess, (int)RowAccessPermissions.ManageRowAccess)]
        public async Task<IActionResult> PutModifiedRowAccessSettingsForRoleAsync(
            int roleId, [FromBody] RowPermissionsForRoleViewModel permissions)
        {
            var result = BasicValidationResult(permissions, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = PermissionValidationResult(permissions);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRowAccessSettingsAsync(permissions);
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

            if (role.Permissions.Any(perm => !_repository.IsPublicPermission(perm)))
            {
                return BadRequest(_strings.Format(AppStrings.InvalidPermissionsInRole));
            }

            return Ok();
        }

        private IActionResult PermissionValidationResult(RowPermissionsForRoleViewModel permissions)
        {
            var messages = new List<string>();
            foreach (var permission in permissions.RowPermissions)
            {
                messages.Add(ValidateRowPermission(permission));
            }

            if (messages.Any(msg => !String.IsNullOrEmpty(msg)))
            {
                return BadRequest(messages.Where(msg => !String.IsNullOrEmpty(msg)).ToArray());
            }

            return Ok();
        }

        private string ValidateRowPermission(ViewRowPermissionViewModel rowPermission)
        {
            if (rowPermission.Items.Count == 0
                && (rowPermission.AccessMode == RowAccessOptions.SpecificRecords
                    || rowPermission.AccessMode == RowAccessOptions.AllExceptSpecificRecords))
            {
                return _strings.Format(AppStrings.SpecificRecordsNotSelected, rowPermission.ViewName);
            }

            if (rowPermission.Value == 0.0F && rowPermission.Value2 == 0.0F
                && rowPermission.AccessMode == RowAccessOptions.MaxMoneyValue)
            {
                return _strings.Format(AppStrings.MaxMoneyValueNotSelected, rowPermission.ViewName);
            }

            if (rowPermission.Value == 0.0F
                && rowPermission.AccessMode == RowAccessOptions.MaxQuantityValue)
            {
                return _strings.Format(AppStrings.MaxQuantityValueNotSelected, rowPermission.ViewName);
            }

            return String.Empty;
        }

        private void Localize(IList<RoleViewModel> roles)
        {
            Array.ForEach(roles.ToArray(), role =>
            {
                role.Name = _strings[role.Name];
                role.Description = _strings[role.Description ?? String.Empty];
            });
        }

        private void LocalizeRole(RoleFullViewModel role)
        {
            role.Role.Name = _strings[role.Role.Name];
            role.Role.Description = _strings[role.Role.Description ?? String.Empty];
            Array.ForEach(role.Permissions.ToArray(), perm =>
            {
                perm.Name = GetLocalName(perm.Name);
                perm.GroupName = GetLocalName(perm.GroupName);
            });
        }

        private void LocalizeRoleDetails(RoleDetailsViewModel role)
        {
            role.Role.Name = _strings[role.Role.Name];
            role.Role.Description = _strings[role.Role.Description ?? String.Empty];
            Array.ForEach(role.Permissions.ToArray(), perm =>
            {
                perm.Name = GetLocalName(perm.Name);
                perm.GroupName = GetLocalName(perm.GroupName);
            });
        }

        private string GetLocalName(string nameKey)
        {
            string name = nameKey;
            var items = nameKey.Split(',');
            if (items.Length == 1)
            {
                name = _strings.Format(items[0]);
            }
            else
            {
                name = _strings.Format(items[0], items[1]);
            }

            return name;
        }

        private IRoleRepository _repository;
    }
}
