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
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Corporate;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با شعبه های سازمانی را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class BranchesController : ValidatingController<BranchViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات شعبه ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenService"></param>
        public BranchesController(
            IBranchRepository repository, IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام موجودیت شعبه
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Branch; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه شعبه های سازمانی قابل دسترسی توسط کاربر جاری را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده شعبه های سازمانی</returns>
        // GET: api/branches/company/{companyId:min(1)}
        [HttpGet]
        [Route(BranchApi.CompanyBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchesAsync()
        {
            var branches = await _repository.GetBranchesAsync(GridOptions);
            return JsonListResult(branches);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی شعبه مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر</param>
        /// <returns>اطلاعات نمایشی شعبه</returns>
        // GET: api/branches/{branchId:min(1)}
        [HttpGet]
        [Route(BranchApi.BranchUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchAsync(int branchId)
        {
            var branch = await _repository.GetBranchAsync(branchId);
            return JsonReadResult(branch);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شعبه های بالاترین سطح را برمی گرداند
        /// </summary>
        /// <returns>لیست اطلاعات شعبه های بالاترین سطح</returns>
        // GET: api/branches/root
        [HttpGet]
        [Route(BranchApi.RootBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetRootBranchesAsync()
        {
            var rootBranches = await _repository.GetBranchChildrenAsync(null);
            return Json(rootBranches);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه شعبه های زیرمجموعه شعبه داده شده را برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه والد</param>
        /// <returns>لیست اطلاعات شعبه های زیرمجموعه</returns>
        // GET: api/branches/{branchId:min(1)}/children
        [HttpGet]
        [Route(BranchApi.BranchChildrenUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.View)]
        public async Task<IActionResult> GetBranchChildrenAsync(int branchId)
        {
            var children = await _repository.GetBranchChildrenAsync(branchId);
            return JsonReadResult(children);
        }

        /// <summary>
        /// به روش آسنکرون، شعبه داده شده را ایجاد می کند
        /// </summary>
        /// <param name="branch">اطلاعات کامل شعبه جدید</param>
        /// <returns>اطلاعات شعبه بعد از ایجاد در دیتابیس</returns>
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
                return BadRequestResult(_strings.Format(AppStrings.RootBranchAlreadyDefined));
            }

            var outputItem = await _repository.SaveBranchAsync(branch);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، بودن شعبه در بالاترین سطح را بررسی می کند
        /// </summary>
        /// <param name="branch">اطلاعات اولیه شعبه جدید برای اعتبارسنجی</param>
        /// <returns>در صورت بودن شعبه در بالاترین سطح، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
        // POST: api/branches/root
        [HttpPost]
        [Route(BranchApi.RootBranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Create)]
        public async Task<IActionResult> PostNewBranchValidationAsync([FromBody] BranchViewModel branch)
        {
            if (!await _repository.IsValidBranchAsync(branch))
            {
                return BadRequestResult(_strings.Format(AppStrings.RootBranchAlreadyDefined));
            }

            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، شعبه مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر برای اصلاح</param>
        /// <param name="branch">اطلاعات اصلاح شده شعبه</param>
        /// <returns>اطلاعات شعبه بعد از اصلاح در دیتابیس</returns>
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

        /// <summary>
        /// به روش آسنکرون، شعبه مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/branches/{branchId:min(1)}
        [HttpDelete]
        [Route(BranchApi.BranchUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingBranchAsync(int branchId)
        {
            var result = await ValidateDeleteResultAsync(branchId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteBranchAsync(branchId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، شعبه مشخص شده با شناسه دیتابیسی را به همراه کلیه اطلاعات وابسته حذف می کند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/branches/{branchId:min(1)}/data
        [HttpDelete]
        [Route(BranchApi.BranchDataUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingBranchWithDataAsync(int branchId)
        {
            string result = await BasicValidateDeleteAsync(branchId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequestResult(result);
            }

            await _repository.DeleteBranchWithDataAsync(branchId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، شعبه های داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>سطرهای اطلاعاتی قابل حذف را از دیتابیس حذف می کند و موارد مشکل دار را به همراه
        /// کد وضعیتی 200 برمی گرداند</returns>
        // PUT: api/branches
        [HttpPut]
        [Route(BranchApi.BranchesUrl)]
        [AuthorizeRequest(SecureEntity.Branch, (int)BranchPermissions.Delete)]
        public async Task<IActionResult> PutExistingBranchesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteBranchesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه برای نقش های دارای دسترسی به شعبه داده شده را برمی گرداند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر</param>
        /// <returns>اطلاعات خلاصه برای نقش های دارای دسترسی به شعبه</returns>
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

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به شعبه داده شده را در دیتابیس اصلاح می کند
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه مورد نظر</param>
        /// <param name="branchRoles">اطلاعات جدید برای نقش های دارای دسترسی به شعبه</param>
        /// <returns>در صورت بروز خطا، کد وضعیت 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
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

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی شعبه مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رفرنس بدون مقدار در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            var branch = await _repository.GetBranchAsync(item);
            string error = await BasicValidateDeleteAsync(item);
            if (!String.IsNullOrEmpty(error))
            {
                return GetGroupActionResult(error, branch);
            }

            var canDelete = await _repository.CanDeleteBranchAsync(item);
            if (canDelete == false)
            {
                error = _strings.Format(AppStrings.CantDeleteItemWithData, EntityNameKey, branch.Name);
            }

            return GetGroupActionResult(error, branch);
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
                return _strings.Format(AppStrings.CantDeleteNonLeafBranch, branch.Name);
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