using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel;
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
            _repository.SetCurrentContext(SecurityContext.User);
            var result = await ValidationResultAsync(fiscalPeriod);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFiscalPeriodAsync(fiscalPeriod);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/fperiods/{fpId:min(1)}
        [HttpPut]
        [Route(FiscalPeriodApi.FiscalPeriodUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Edit)]
        public async Task<IActionResult> PutModifiedFiscalPeriodAsync(
            int fpId, [FromBody] FiscalPeriodViewModel fiscalPeriod)
        {
            _repository.SetCurrentContext(SecurityContext.User);
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
            bool canDelete = await _repository.CanDeleteFiscalPeriodAsync(fpId);
            if (!canDelete)
            {
                return BadRequest(_strings.Format(AppStrings.CantDeleteFiscalPeriodWithData));
            }

            var fperiod = await _repository.GetFiscalPeriodAsync(fpId);
            if (fperiod == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.FiscalPeriod));
            }

            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.DeleteFiscalPeriodAsync(fpId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/fperiods/{fpId:min(1)}/roles
        [Route(FiscalPeriodApi.FiscalPeriodRolesUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodRolesAsync(int fpId)
        {
            var roles = await _repository.GetFiscalPeriodRolesAsync(fpId);
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

        private async Task<IActionResult> ValidationResultAsync(FiscalPeriodViewModel fiscalPeriod, int fperiodId = 0)
        {
            var result = BasicValidationResult(fiscalPeriod, fperiodId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.ExistsFiscalPeriodInRange(fiscalPeriod.StartDate, fiscalPeriod.EndDate))
            {
                return BadRequest(_strings.Format(AppStrings.FiscalPeriodAlreadyDefined));
            }

            if (_repository.IsStartDateAfterEndDate(fiscalPeriod))
            {
                return BadRequest(_strings.Format(AppStrings.InvalidDatePeriod, AppStrings.FiscalPeriod));
            }

            if (await _repository.IsOverlapFiscalPeriodAsync(fiscalPeriod))
            {
                return BadRequest(_strings.Format(AppStrings.DateOverlap));
            }

            return Ok();
        }

        private IFiscalPeriodRepository _repository;
    }
}