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
    public class FiscalPeriodsController : ApiControllerBase<FiscalPeriodViewModel>
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

        // GET: api/fperiods/metadata
        [Route(FiscalPeriodApi.FiscalPeriodMetadataUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodMetadataAsync()
        {
            var metadata = await _repository.GetFiscalPeriodMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/fperiods
        [HttpPost]
        [Route(FiscalPeriodApi.FiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Create)]
        public async Task<IActionResult> PostNewFiscalPeriodAsync([FromBody] FiscalPeriodViewModel fiscalPeriod)
        {
            var result = BasicValidationResult(fiscalPeriod);
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
            var result = BasicValidationResult(fiscalPeriod, fpId);
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
            var fperiod = _repository.GetFiscalPeriodAsync(fpId);
            if (fperiod == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.FiscalPeriod));
            }

            await _repository.DeleteFiscalPeriodAsync(fpId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private IFiscalPeriodRepository _repository;
    }
}