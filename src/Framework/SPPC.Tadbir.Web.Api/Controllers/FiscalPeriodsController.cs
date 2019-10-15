using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class FiscalPeriodsController : ValidatingController<FiscalPeriodViewModel>
    {
        public FiscalPeriodsController(
            IFiscalPeriodRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.FiscalPeriod; }
        }

        // GET: api/fperiods/company/{companyId:min(1)}
        [Route(FiscalPeriodApi.CompanyFiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodsAsync(int companyId)
        {
            int itemCount = await _repository.GetCountAsync(companyId, GridOptions);
            SetItemCount(itemCount);
            var fiscalPeriods = await _repository.GetFiscalPeriodsAsync(companyId, GridOptions);
            return Json(fiscalPeriods);
        }

        // GET: api/fperiods/{fpId:min(1)}
        [Route(FiscalPeriodApi.FiscalPeriodUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodAsync(int fpId)
        {
            var fiscalPeriod = await _repository.GetFiscalPeriodAsync(fpId);
            return JsonReadResult(fiscalPeriod);
        }

        // POST: api/fperiods
        [HttpPost]
        [Route(FiscalPeriodApi.FiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Create)]
        public async Task<IActionResult> PostNewFiscalPeriodAsync([FromBody] FiscalPeriodViewModel fiscalPeriod)
        {
            var result = await ValidationResultAsync(fiscalPeriod);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFiscalPeriodAsync(fiscalPeriod);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // POST: api/fperiods/validation
        [HttpPost]
        [Route(FiscalPeriodApi.FiscalPeriodValidationUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Create)]
        public async Task<IActionResult> PostFiscalPeriodValidationAsync([FromBody]FiscalPeriodViewModel fiscalPeriod)
        {
            var result = BasicValidationResult(fiscalPeriod);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (_repository.IsStartDateAfterEndDate(fiscalPeriod))
            {
                return BadRequest(_strings.Format(AppStrings.InvalidDatePeriod, AppStrings.FiscalPeriod));
            }

            return Ok();
        }

        // PUT: api/fperiods/{fpId:min(1)}
        [HttpPut]
        [Route(FiscalPeriodApi.FiscalPeriodUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Edit)]
        public async Task<IActionResult> PutModifiedFiscalPeriodAsync(
            int fpId, [FromBody] FiscalPeriodViewModel fiscalPeriod)
        {
            var result = await ValidationResultAsync(fiscalPeriod, fpId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFiscalPeriodAsync(fiscalPeriod);
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/fperiods/{fpId:min(1)}
        [HttpDelete]
        [Route(FiscalPeriodApi.FiscalPeriodUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingFiscalPeriodAsync(int fpId)
        {
            string result = await ValidateDeleteAsync(fpId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteFiscalPeriodAsync(fpId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/fperiods
        [HttpPut]
        [Route(FiscalPeriodApi.FiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Delete)]
        public async Task<IActionResult> PutExistingFiscalPeriodsAsDeletedAsync(
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

            await _repository.DeleteFiscalPeriodsAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/fperiods/{fpId:min(1)}/roles
        [Route(FiscalPeriodApi.FiscalPeriodRolesUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodRolesAsync(int fpId)
        {
            var roles = await _repository.GetFiscalPeriodRolesAsync(fpId);
            Localize(roles);
            return JsonReadResult(roles);
        }

        // PUT: api/fperiods/{fpId:min(1)}/roles
        [HttpPut]
        [Route(FiscalPeriodApi.FiscalPeriodRolesUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.AssignRoles)]
        public async Task<IActionResult> PutModifiedFiscalPeriodRolesAsync(
            int fpId, [FromBody] RelatedItemsViewModel fiscalPeriodRoles)
        {
            var result = BasicValidationResult(fiscalPeriodRoles, fpId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveFiscalPeriodRolesAsync(fiscalPeriodRoles);
            return Ok();
        }

        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var fperiod = await _repository.GetFiscalPeriodAsync(item);
            if (fperiod == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.FiscalPeriod, item.ToString());
            }
            else
            {
                bool canDelete = await _repository.CanDeleteFiscalPeriodAsync(item);
                if (!canDelete)
                {
                    message = _strings.Format(AppStrings.CantDeleteFiscalPeriodWithData, fperiod.Name);
                }
            }

            return message;
        }

        private async Task<IActionResult> ValidationResultAsync(FiscalPeriodViewModel fiscalPeriod, int fperiodId = 0)
        {
            var result = BasicValidationResult(fiscalPeriod, fperiodId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (_repository.IsStartDateAfterEndDate(fiscalPeriod))
            {
                return BadRequest(_strings.Format(AppStrings.InvalidDatePeriod, AppStrings.FiscalPeriod));
            }

            if (await _repository.IsOverlapFiscalPeriodAsync(fiscalPeriod))
            {
                return BadRequest(_strings.Format(AppStrings.DateOverlap));
            }

            if (!await _repository.IsProgressiveFiscalPeriodAsync(fiscalPeriod))
            {
                return BadRequest(_strings.Format(AppStrings.FiscalPeriodMustBeProgressive));
            }

            return Ok();
        }

        private void Localize(RelatedItemsViewModel roles)
        {
            Array.ForEach(roles.RelatedItems.ToArray(), item => item.Name = _strings[item.Name]);
        }

        private IFiscalPeriodRepository _repository;
    }
}