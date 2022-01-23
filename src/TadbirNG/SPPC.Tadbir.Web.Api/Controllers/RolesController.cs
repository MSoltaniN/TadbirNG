using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با نقش های سازمانی را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class RolesController : ValidatingController<RoleFullViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات نقش های سازمانی در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        /// <param name="checkEdition"></param>
        public RolesController(IRoleRepository repository, ICheckEdition checkEdition,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _checkEdition = checkEdition;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام موجودیت نقش
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Role; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه نقش های تعریف شده را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده نقش ها</returns>
        // GET: api/roles
        [HttpGet]
        [Route(RoleApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRolesAsync()
        {
            var roles = await _repository.GetRolesAsync(GridOptions);
            return JsonListResult(roles);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی پیش فرض برای یک نقش سازمانی جدید را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نقش جدید به همراه کلیه دسترسی های امنیتی تعریف شده</returns>
        // GET: api/roles/new
        [HttpGet]
        [Route(RoleApi.NewRoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Create)]
        public async Task<IActionResult> GetNewRoleAsync()
        {
            var newRole = await _repository.GetNewRoleAsync();
            Localize(newRole);
            return Json(newRole);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی نقش مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اطلاعات نمایشی نقش به همراه دسترسی های امنیتی تنظیم شده برای نقش</returns>
        // GET: api/roles/{roleId:min(1)}
        [HttpGet]
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleAsync(int roleId)
        {
            var role = await _repository.GetRoleAsync(roleId);
            Localize(role);
            return JsonReadResult(role);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل نقش مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اطلاعات کامل نقش به همراه دسترسی های امنیتی تنظیم شده برای نقش</returns>
        // GET: api/roles/{roleId:min(1)}/details
        [HttpGet]
        [Route(RoleApi.RoleDetailsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleDetailsAsync(int roleId)
        {
            var role = await _repository.GetRoleDetailsAsync(roleId);
            Localize(role);
            return JsonReadResult(role);
        }

        /// <summary>
        /// به روش آسنکرون، نقش سازمانی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="role">اطلاعات کامل نقش سازمانی جدید</param>
        /// <returns>اطلاعات نقش سازمانی بعد از ایجاد در دیتابیس</returns>
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

        /// <summary>
        /// به روش آسنکرون، نقش سازمانی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش سازمانی مورد نظر برای اصلاح</param>
        /// <param name="role">اطلاعات اصلاح شده نقش سازمانی</param>
        /// <returns>اطلاعات نقش سازمانی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/roles/{roleId:min(1)}
        [HttpPut]
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Edit)]
        public async Task<IActionResult> PutModifiedRoleAsync(int roleId, [FromBody] RoleFullViewModel role)
        {
            if (roleId == AppConstants.AdminRoleId)
            {
                return BadRequestResult(_strings.Format(AppStrings.AdminRoleIsReadonly));
            }

            var result = BasicValidationResult(role, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputRole = await _repository.SaveRoleAsync(role);
            return Ok(outputRole);
        }

        /// <summary>
        /// به روش آسنکرون، نقش مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/roles/{roleId:min(1)}
        [HttpDelete]
        [Route(RoleApi.RoleUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingRoleAsync(int roleId)
        {
            var result = await ValidateDeleteResultAsync(roleId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteRoleAsync(roleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، نقش های سازمانی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/roles
        [HttpPut]
        [Route(RoleApi.RolesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.Delete)]
        public async Task<IActionResult> PutExistingRolesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteRolesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه برای شرکت های قابل دسترسی توسط نقش داده شده را برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اطلاعات خلاصه برای شرکت های قابل دسترسی توسط نقش</returns>
        // GET: api/roles/{roleId:min(1)}/companies
        [HttpGet]
        [Route(RoleApi.RoleCompaniesUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> GetRoleCompaniesAsync(int roleId)
        {
            var companies = await _repository.GetRoleCompaniesAsync(roleId);
            return JsonReadResult(companies);
        }

        /// <summary>
        /// به روش آسنکرون، شرکت های قابل دسترسی توسط نقش داده شده را در دیتابیس اصلاح می کند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <param name="roleCompanies">اطلاعات جدید برای شرکت های قابل دسترسی توسط نقش</param>
        /// <returns>در صورت بروز خطا، کد وضعیت 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
        // PUT: api/roles/{roleId:min(1)}/companies
        [HttpPut]
        [Route(RoleApi.RoleCompaniesUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutModifiedRoleCompaniesAsync(
            int roleId, [FromBody] RelatedItemsViewModel roleCompanies)
        {
            var result = BasicValidationResult(roleCompanies, roleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveRoleCompaniesAsync(roleCompanies);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه برای شعبه های قابل دسترسی توسط نقش داده شده را برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اطلاعات خلاصه برای شعبه های قابل دسترسی توسط نقش</returns>
        // GET: api/roles/{roleId:min(1)}/branches
        [HttpGet]
        [Route(RoleApi.RoleBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleBranchesAsync(int roleId)
        {
            var branches = await _repository.GetRoleBranchesAsync(roleId);
            return JsonReadResult(branches);
        }

        /// <summary>
        /// به روش آسنکرون، شعبه های قابل دسترسی توسط نقش داده شده را در دیتابیس اصلاح می کند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <param name="roleBranches">اطلاعات جدید برای شعبه های قابل دسترسی توسط نقش</param>
        /// <returns>در صورت بروز خطا، کد وضعیت 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
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

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه برای کاربران تخصیص داده شده به نقش مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اطلاعات خلاصه برای کاربران تخصیص داده شده به نقش</returns>
        // GET: api/roles/{roleId:min(1)}/users
        [HttpGet]
        [Route(RoleApi.RoleUsersUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleUsersAsync(int roleId)
        {
            var users = await _repository.GetRoleUsersAsync(roleId);
            return JsonReadResult(users);
        }

        /// <summary>
        /// به روش آسنکرون، کاربران تخصیص داده شده به نقش مشخص شده را در دیتابیس اصلاح می کند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <param name="roleUsers">اطلاعات جدید برای کاربران تخصیص داده شده به نقش</param>
        /// <returns>در صورت بروز خطا، کد وضعیت 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
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

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه برای دوره های مالی قابل دسترسی توسط نقش داده شده را برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اطلاعات خلاصه برای دوره های مالی قابل دسترسی توسط نقش</returns>
        // GET: api/roles/{roleId:min(1)}/fperiods
        [HttpGet]
        [Route(RoleApi.RoleFiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.Role, (int)RolePermissions.View)]
        public async Task<IActionResult> GetRoleFiscalPeriodsAsync(int roleId)
        {
            var fiscalPeriods = await _repository.GetRoleFiscalPeriodsAsync(roleId);
            return JsonReadResult(fiscalPeriods);
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی قابل دسترسی توسط نقش داده شده را در دیتابیس اصلاح می کند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <param name="roleFiscalPeriods">اطلاعات جدید برای دوره های مالی قابل دسترسی توسط نقش</param>
        /// <returns>در صورت بروز خطا، کد وضعیت 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
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

        /// <summary>
        /// به روش آسنکرون، اطلاعات تنظیمات دسترسی سطری تعریف شده برای نقش داده شده را برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>تنظیمات دسترسی سطری تعریف شده برای نقش</returns>
        // GET: api/roles/{roleId:min(2)}/rowaccess
        [HttpGet]
        [Route(RoleApi.RowAccessSettingsUrl)]
        [AuthorizeRequest(SecureEntity.RowAccess, (int)RowAccessPermissions.ViewRowAccess)]
        public async Task<IActionResult> GetRowAccessSettingsForRoleAsync(int roleId)
        {
            var settings = await _repository.GetRowAccessSettingsAsync(roleId);
            return Json(settings);
        }

        /// <summary>
        /// آخرین وضعیت تنظیمات دسترسی سطری را برای نقش داده شده در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <param name="permissions">اطلاعات جدید تنظیمات دسترسی سطری برای نقش</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 (به معنی نبود خطا) را برمی گرداند</returns>
        // PUT: api/roles/{roleId:min(2)}/rowaccess
        [HttpPut]
        [Route(RoleApi.RowAccessSettingsUrl)]
        [AuthorizeRequest(SecureEntity.RowAccess, (int)RowAccessPermissions.SaveRowAccess)]
        public async Task<IActionResult> PutModifiedRowAccessSettingsForRoleAsync(
            int roleId, [FromBody] RowPermissionsForRoleViewModel permissions)
        {
            var message = _checkEdition.ValidateNewModel(permissions, EditionLimit.RowPermissionAccess);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

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

        /// <summary>
        /// قواعد اولیه اعتبارسنجی را برای نقش داده شده بررسی می کند و نتیجه اعتبارسنجی را برمی گرداند
        /// </summary>
        /// <param name="role">اطلاعات نقش مورد نظر برای بررسی</param>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر برای بررسی</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیتی 400 را به همراه پیغام خطا
        /// و در غیر این صورت کد وضعیتی 200 را برمی گرداند</returns>
        protected override IActionResult BasicValidationResult(RoleFullViewModel role, int roleId = 0)
        {
            if (role == null || role.Role == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Role));
            }

            if (roleId != role.Role.Id)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.Role));
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            if (role.Permissions.Any(perm => !_repository.IsPublicPermission(perm)))
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidPermissionsInRole));
            }

            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی نقش مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رفرنس بدون مقدار در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            if (item == AppConstants.AdminRoleId)
            {
                message = _strings.Format(AppStrings.AdminRoleIsReadonly);
                return GetGroupActionResult<RoleViewModel>(message, null);
            }

            var role = await _repository.GetRoleBriefAsync(item);
            if (role == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Role, item.ToString());
            }
            else if (await _repository.IsAssignedRoleAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteAssignedRole, role.Name);
            }
            else if (await _repository.IsRoleRelatedToCompanyAsync(item))
            {
                message = _strings.Format(
                    AppStrings.CantDeleteRoleHavingRelation, role.Name, AppStrings.Company);
            }
            else if (await _repository.IsRoleRelatedToBranchAsync(item))
            {
                message = _strings.Format(
                    AppStrings.CantDeleteRoleHavingRelation, role.Name, AppStrings.Branch);
            }
            else if (await _repository.IsRoleRelatedToFiscalPeriodAsync(item))
            {
                message = _strings.Format(
                    AppStrings.CantDeleteRoleHavingRelation, role.Name, AppStrings.FiscalPeriod);
            }
            else if (await _repository.HasRowPermissions(item))
            {
                message = _strings.Format(AppStrings.CantDeleteRoleHavingPermissions, role.Name);
            }

            return GetGroupActionResult(message, role);
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
                var error = new ErrorViewModel() { Type = ErrorType.ValidationError };
                error.Messages.AddRange(messages.Where(msg => !String.IsNullOrEmpty(msg)));
                return BadRequest(error);
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

        private void Localize(RoleFullViewModel role)
        {
            if (role == null)
            {
                return;
            }

            role.Role.Name = _strings[role.Role.Name];
            role.Role.Description = _strings[role.Role.Description ?? String.Empty];
            Array.ForEach(role.Permissions.ToArray(), perm =>
            {
                perm.Name = GetLocalName(perm.Name);
                perm.GroupName = GetLocalName(perm.GroupName);
            });
        }

        private void Localize(RoleDetailsViewModel role)
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
            string name;
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

        private readonly IRoleRepository _repository;
        private readonly ICheckEdition _checkEdition;
    }
}
