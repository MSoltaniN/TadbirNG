using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Enums;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Licensing;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با مراکز هزینه در برنامه را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class CostCentersController : ValidatingController<CostCenterViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات مراکز هزینه در دیتابیس را فراهم می کند</param>
        /// <param name="config">امکان خواندن اطلاعات پیکربندی برنامه را فراهم می کند</param>
        /// <param name="checkEdition"></param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        public CostCentersController(
            ICostCenterRepository repository, IConfigRepository config, ICheckEdition checkEdition,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewId.CostCenter).Result;
            _checkEdition = checkEdition;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام مرکز هزینه
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.CostCenter; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده کلیه مراکز هزینه فعال را خوانده و برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده مراکز هزینه فعال</returns>
        // GET: api/ccenters
        [HttpGet]
        [Route(CostCenterApi.CostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCentersAsync()
        {
            var costCenters = await _repository.GetCostCentersAsync(GridOptions);
            return JsonListResult(costCenters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده کلیه مراکز هزینه غیر فعال را خوانده و برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده مراکز هزینه غیر فعال</returns>
        // GET: api/ccenters/inactive
        [HttpGet]
        [Route(CostCenterApi.InactiveCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetInactiveCostCentersAsync()
        {
            var costCenters = await _repository.GetCostCentersAsync(GridOptions, ActiveState.Inactive);
            return JsonListResult(costCenters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده کلیه مراکز هزینه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده مراکز هزینه</returns>
        // GET: api/ccenters/all
        [HttpGet]
        [Route(CostCenterApi.AllCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetAllCostCentersAsync()
        {
            var costCenters = await _repository.GetCostCentersAsync(GridOptions, ActiveState.All);
            return JsonListResult(costCenters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی مرکز هزینه مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <returns>اطلاعات نمایشی مرکز هزینه</returns>
        // GET: api/ccenters/{ccenterId:min(1)}
        [HttpGet]
        [Route(CostCenterApi.CostCenterUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCenterAsync(int ccenterId)
        {
            var costCenter = await _repository.GetCostCenterAsync(ccenterId);
            return JsonReadResult(costCenter);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه جدیدی زیرمجموعه مرکز هزینه والد داده شده برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه والد</param>
        /// <returns>اطلاعات پیشنهادی برای مرکز هزینه جدید</returns>
        // GET: api/ccenters/{ccenterId:int}/children/new
        [HttpGet]
        [Route(CostCenterApi.NewChildCostCenterUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Create)]
        public async Task<IActionResult> GetNewCostCenterAsync(int ccenterId)
        {
            var newCenter = await _repository.GetNewChildCostCenterAsync(
                ccenterId > 0 ? ccenterId : null);
            if (newCenter == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ParentItemNotFound, AppStrings.CostCenter));
            }

            if (newCenter.Level == -1)
            {
                return BadRequestResult(_strings.Format(AppStrings.ChildItemsNotAllowed, AppStrings.CostCenter));
            }

            if (ccenterId > 0 && await _repository.IsUsedCostCenterAsync(ccenterId))
            {
                var parent = await _repository.GetCostCenterAsync(ccenterId);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.CostCenter, parentInfo));
            }

            return Json(newCenter);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه در بالاترین سطح را برمی گرداند
        /// </summary>
        /// <returns>لیست اطلاعات خلاصه مراکز هزینه در بالاترین سطح</returns>
        // GET: api/ccenters/root
        [HttpGet]
        [Route(CostCenterApi.RootCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetRootCostCentersAsync()
        {
            var costCenters = await _repository.GetRootCostCentersAsync();
            return Json(costCenters);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه زیرمجموعه مرکز هزینه داده شده را برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه والد</param>
        /// <returns>لیست اطلاعات خلاصه مراکز هزینه زیرمجموعه</returns>
        // GET: api/ccenters/{ccenterId:min(1)}/children
        [HttpGet]
        [Route(CostCenterApi.CostCenterChildrenUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCenterChildrenAsync(int ccenterId)
        {
            var children = await _repository.GetCostCenterChildrenAsync(ccenterId);
            return Json(children);
        }

        /// <summary>
        /// به روش آسنکرون، کد کامل مرکز هزینه مشخص شده با شناسه را برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <returns>کد کامل مرکز هزینه</returns>
        // GET: api/ccenters/{ccenterId:int}/fullcode
        [HttpGet]
        [Route(CostCenterApi.CostCenterFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Create | (int)CostCenterPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int ccenterId)
        {
            if (ccenterId <= 0)
            {
                return Ok(String.Empty);
            }

            string fullCode = await _repository.GetCostCenterFullCodeAsync(ccenterId);
            return Ok(fullCode);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه داده شده را ایجاد می کند
        /// </summary>
        /// <param name="costCenter">اطلاعات کامل مرکز هزینه جدید</param>
        /// <returns>اطلاعات مرکز هزینه بعد از ایجاد در دیتابیس</returns>
        // POST: api/ccenters
        [HttpPost]
        [Route(CostCenterApi.CostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Create)]
        public async Task<IActionResult> PostNewCostCenterAsync([FromBody] CostCenterViewModel costCenter)
        {
            var message = _checkEdition.ValidateNewModel(costCenter, EditionLimit.CostCenterDepth);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            var result = await ValidationResultAsync(costCenter);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCostCenterAsync(costCenter);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر برای اصلاح</param>
        /// <param name="costCenter">اطلاعات اصلاح شده مرکز هزینه</param>
        /// <returns>اطلاعات مرکز هزینه بعد از اصلاح در دیتابیس</returns>
        // PUT: api/ccenters/{ccenterId:min(1)}
        [HttpPut]
        [Route(CostCenterApi.CostCenterUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Edit)]
        public async Task<IActionResult> PutModifiedCostCenterAsync(
            int ccenterId, [FromBody] CostCenterViewModel costCenter)
        {
            var result = await ValidationResultAsync(costCenter, ccenterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCostCenterAsync(costCenter);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه دیتابیسی را غیرفعال می کند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر برای غیرفعال کردن</param>
        // PUT: api/ccenters/{ccenterId:min(1)}/deactivate
        [HttpPut]
        [Route(CostCenterApi.DeactivateCostCenterUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Deactivate)]
        public async Task<IActionResult> PutCostCenterAsDeactivated(int ccenterId)
        {
            return await UpdateActiveStateAsync(ccenterId, false);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه دیتابیسی را فعال می کند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر برای فعال کردن</param>
        // PUT: api/ccenters/{ccenterId:min(1)}/reactivate
        [HttpPut]
        [Route(CostCenterApi.ReactivateCostCenterUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Reactivate)]
        public async Task<IActionResult> PutCostCenterAsReactivated(int ccenterId)
        {
            return await UpdateActiveStateAsync(ccenterId, true);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/ccenters/{ccenterId:min(1)}
        [HttpDelete]
        [Route(CostCenterApi.CostCenterUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCostCenterAsync(int ccenterId)
        {
            var result = await ValidateDeleteResultAsync(ccenterId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteCostCenterAsync(ccenterId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، مراکز هزینه داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/ccenters
        [HttpPut]
        [Route(CostCenterApi.CostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Delete)]
        public async Task<IActionResult> PutExistingCostCentersAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteCostCentersAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای مرکز هزینه مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی مرکز هزینه مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var costCenter = await _repository.GetCostCenterAsync(item);
            if (costCenter == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.CostCenter, item.ToString());
                return GetGroupActionResult(message, costCenter);
            }

            var result = BranchValidationResult(costCenter);
            if (result is BadRequestObjectResult errorResult)
            {
                return GetGroupActionResult(errorResult.Value.ToString(), costCenter);
            }

            var costCenterInfo = String.Format("'{0} ({1})'", costCenter.Name, costCenter.FullCode);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = _strings.Format(AppStrings.CantDeleteNonLeafItem, AppStrings.CostCenter, costCenterInfo);
            }
            else if (await _repository.IsUsedCostCenterAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteUsedItem, AppStrings.CostCenter, costCenterInfo);
            }
            else if (await _repository.IsRelatedCostCenterAsync(item))
            {
                message = _strings.Format(AppStrings.CantDeleteRelatedItem, AppStrings.CostCenter, costCenterInfo);
            }

            return GetGroupActionResult(message, costCenter);
        }

        private async Task<IActionResult> ValidationResultAsync(CostCenterViewModel costCenter, int ccenterId = 0)
        {
            var result = BasicValidationResult(costCenter, ccenterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateFullCodeAsync(costCenter))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.DuplicateCodeValue, AppStrings.CostCenter, costCenter.FullCode));
            }

            if (await _repository.IsDuplicateNameAsync(costCenter))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.DuplicateNameValue, AppStrings.CostCenter, costCenter.Name));
            }

            if (costCenter.ParentId.HasValue
                && await _repository.IsUsedCostCenterAsync(costCenter.ParentId.Value))
            {
                var parent = await _repository.GetCostCenterAsync(costCenter.ParentId.Value);
                var parentInfo = String.Format("{0} ({1})", parent.Name, parent.FullCode);
                return BadRequestResult(
                    _strings.Format(AppStrings.CantCreateChildForUsedParent, AppStrings.CostCenter, parentInfo));
            }

            result = BranchValidationResult(costCenter);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = ConfigValidationResult(costCenter, _treeConfig.Current);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = _repository as IActiveStateRepository<CostCenterViewModel>;
            if (costCenter.ParentId != null && await repository.IsDeactivatedAsync(costCenter.ParentId.Value))
            {
                var message = _strings.Format(AppStrings.ActiveStateParentError, EntityNameKey);
                return BadRequestResult(message);
            }

            return Ok();
        }

        private async Task<IActionResult> UpdateActiveStateAsync(int costCenterId, bool isActive)
        {
            var costCenter = await _repository.GetCostCenterAsync(costCenterId);
            if (costCenter == null)
            {
                string message = _strings.Format(
                    AppStrings.ItemByIdNotFound, EntityNameKey, costCenterId.ToString());
                return BadRequestResult(message);
            }

            var result = ActiveStateValidationResult(costCenter);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = _repository as IActiveStateRepository<CostCenterViewModel>;
            await repository.SetActiveStatusAsync(costCenter, isActive);
            return Ok();
        }

        private readonly ICostCenterRepository _repository;
        private readonly IConfigRepository _config;
        private readonly ViewTreeFullConfig _treeConfig;
        private readonly ICheckEdition _checkEdition;
    }
}