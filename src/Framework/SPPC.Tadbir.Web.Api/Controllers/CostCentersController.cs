using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class CostCentersController : ValidatingController<CostCenterViewModel>
    {
        public CostCentersController(
            ICostCenterRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.CostCenter; }
        }

        // GET: api/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(CostCenterApi.FiscalPeriodBranchCostCentersUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCentersAsync(int fpId, int branchId)
        {
            int itemCount = await _repository.GetCountAsync(UserAccess, fpId, branchId, GridOptions);
            SetItemCount(itemCount);
            var costCenters = await _repository.GetCostCentersAsync(UserAccess, fpId, branchId, GridOptions);
            return Json(costCenters);
        }

        // GET: api/ccenters/lookup/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(CostCenterApi.FiscalPeriodBranchCostCentersLookupUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCentersLookupAsync(int fpId, int branchId)
        {
            var lookup = await _repository.GetCostCentersLookupAsync(UserAccess, fpId, branchId, GridOptions);
            return Json(lookup);
        }

        // GET: api/ccenters/{ccenterId:min(1)}
        [Route(CostCenterApi.CostCenterUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCenterAsync(int ccenterId)
        {
            var costCenter = await _repository.GetCostCenterAsync(ccenterId);
            return JsonReadResult(costCenter);
        }

        // GET: api/ccenters/{ccenterId:min(1)}/children
        [Route(CostCenterApi.CostCenterChildrenUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCenterChildrenAsync(int ccenterId)
        {
            var children = await _repository.GetCostCenterChildrenAsync(ccenterId);
            return JsonReadResult(children);
        }

        // GET: api/ccenters/metadata
        [Route(CostCenterApi.CostCenterMetadataUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.View)]
        public async Task<IActionResult> GetCostCenterMetadataAsync()
        {
            var metadata = await _repository.GetCostCenterMetadataAsync();
            return JsonReadResult(metadata);
        }

        // GET: api/ccenters/fullcode/{parentId}
        [HttpGet]
        [Route(CostCenterApi.CostCenterFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.CostCenter, (int)CostCenterPermissions.Create | (int)CostCenterPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int parentId)
        {
            if (parentId <= 0)
            {
                return Ok(string.Empty);
            }

            string fullCode = await _repository.GetCostCenterFullCodeAsync(parentId);

            return Ok(fullCode);
        }

        // POST: api/ccenters
        [HttpPost]
        [Route(CostCenterApi.CostCentersUrl)]
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
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
        }

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

        private async Task<IActionResult> ValidationResultAsync(CostCenterViewModel costCenter, int ccenterId = 0)
        {
            var result = BasicValidationResult(costCenter, ccenterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateCostCenterAsync(costCenter))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateCodeValue, AppStrings.CostCenter));
            }

            return Ok();
        }

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var costCenter = await _repository.GetCostCenterAsync(item);
            if (costCenter == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.CostCenter), item);
            }

            var costCenterInfo = String.Format("'{0} ({1})'", costCenter.Name, costCenter.Code);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteNonLeafItem], _strings[AppStrings.CostCenter], costCenterInfo);
            }
            else if (await _repository.IsUsedCostCenterAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteUsedItem], _strings[AppStrings.CostCenter], costCenterInfo);
            }
            else if (await _repository.IsRelatedCostCenterAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteRelatedItem], _strings[AppStrings.CostCenter], costCenterInfo);
            }

            return message;
        }

        private ICostCenterRepository _repository;
    }
}