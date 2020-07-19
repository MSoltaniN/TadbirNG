using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
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
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        public CostCentersController(
            ICostCenterRepository repository, IConfigRepository config, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewName.CostCenter).Result;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام مرکز هزینه
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.CostCenter; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه مراکز هزینه قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده مراکز هزینه</returns>
        // GET: api/ccenters
        [HttpGet]
        [Route(CostCenterApi.EnvironmentCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetEnvironmentCostCentersAsync()
        {
            var costCenters = await _repository.GetCostCentersAsync(GridOptions);
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
                ccenterId > 0 ? ccenterId : (int?)null);
            if (newCenter == null)
            {
                return BadRequest(_strings.Format(AppStrings.ParentItemNotFound, AppStrings.CostCenter));
            }

            if (newCenter.Level == -1)
            {
                return BadRequest(_strings.Format(AppStrings.ChildItemsNotAllowed, AppStrings.CostCenter));
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
        /// <param name="parentId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <returns>کد کامل مرکز هزینه</returns>
        // GET: api/ccenters/{parentId:int}/fullcode
        [HttpGet]
        [Route(CostCenterApi.CostCenterFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Create | (int)CostCenterPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int parentId)
        {
            if (parentId <= 0)
            {
                return Ok(String.Empty);
            }

            string fullCode = await _repository.GetCostCenterFullCodeAsync(parentId);
            return Ok(fullCode);
        }

        /// <summary>
        /// به روش آسنکرون، مرکز هزینه داده شده را ایجاد می کند
        /// </summary>
        /// <param name="costCenter">اطلاعات کامل مرکز هزینه جدید</param>
        /// <returns>اطلاعات مرکز هزینه بعد از ایجاد در دیتابیس</returns>
        // POST: api/ccenters
        [HttpPost]
        [Route(CostCenterApi.EnvironmentCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Create)]
        public async Task<IActionResult> PostNewCostCenterAsync([FromBody] CostCenterViewModel costCenter)
        {
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
            string result = await ValidateDeleteAsync(ccenterId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
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
        [Route(CostCenterApi.EnvironmentCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Delete)]
        public async Task<IActionResult> PutExistingCostCentersAsDeletedAsync(
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

            await _repository.DeleteCostCentersAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای مرکز هزینه مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>پیغام خطای به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var costCenter = await _repository.GetCostCenterAsync(item);
            if (costCenter == null)
            {
                return _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.CostCenter, item.ToString());
            }

            var result = BranchValidationResult(costCenter);
            if (result is BadRequestObjectResult errorResult)
            {
                return errorResult.Value.ToString();
            }

            var costCenterInfo = String.Format("'{0} ({1})'", costCenter.Name, costCenter.Code);
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

            return message;
        }

        private async Task<IActionResult> ValidationResultAsync(CostCenterViewModel costCenter, int ccenterId = 0)
        {
            var result = BasicValidationResult(costCenter, ccenterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateCostCenterAsync(costCenter))
            {
                return BadRequest(_strings.Format(
                    AppStrings.DuplicateCodeValue, AppStrings.CostCenter, costCenter.FullCode));
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

            return Ok();
        }

        private readonly ICostCenterRepository _repository;
        private readonly IConfigRepository _config;
        private readonly ViewTreeFullConfig _treeConfig;
    }
}