using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Extensions;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class FiltersController : ValidatingController<FilterViewModel>
    {
        public FiltersController(IFilterRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Filter; }
        }

        // GET: api/filters/views/{viewId:min(1)}
        [Route(FilterApi.FiltersByViewUrl)]
        public async Task<IActionResult> GetFiltersAsync(int viewId)
        {
            var filters = await _repository.GetFiltersAsync(viewId);
            return Json(filters);
        }

        // GET: api/filters/{filterId:min(1)}
        [Route(FilterApi.FilterUrl)]
        public async Task<IActionResult> GetFilterAsync(int filterId)
        {
            var filter = await _repository.GetFilterAsync(filterId);
            return JsonReadResult(filter);
        }

        // POST: api/filters
        [HttpPost]
        [Route(FilterApi.FiltersUrl)]
        public async Task<IActionResult> PostNewFilterAsync([FromBody] FilterViewModel filter)
        {
            var result = await FilterValidationResultAsync(filter);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFilterAsync(filter);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/filters/{filterId:min(1)}
        [HttpPut]
        [Route(FilterApi.FilterUrl)]
        public async Task<IActionResult> PutModifiedFilterAsync(int filterId, [FromBody] FilterViewModel filter)
        {
            var result = BasicValidationResult(filter, filterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFilterAsync(filter);
            return OkReadResult(outputItem);
        }

        // DELETE: api/filters/{filterId:min(1)}
        [HttpDelete]
        [Route(FilterApi.FilterUrl)]
        public async Task<IActionResult> DeleteExistingFilterAsync(int filterId)
        {
            string message = await ValidateDeleteAsync(filterId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            await _repository.DeleteFilterAsync(filterId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var filter = await _repository.GetFilterAsync(item);
            if (filter == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Filter, item.ToString());
            }

            return message;
        }

        private async Task<IActionResult> FilterValidationResultAsync(FilterViewModel filter)
        {
            var result = BasicValidationResult(filter);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateFilterAsync(filter))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.FilterName));
            }

            return Ok();
        }

        private readonly IFilterRepository _repository;
    }
}