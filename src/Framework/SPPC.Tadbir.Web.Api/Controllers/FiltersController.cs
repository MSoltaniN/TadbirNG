using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Extensions;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class FiltersController : ValidatingController<FilterViewModel>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public FiltersController(IFilterRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Filter; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/filters/views/{viewId:min(1)}
        [HttpGet]
        [Route(FilterApi.FiltersByViewUrl)]
        public async Task<IActionResult> GetFiltersAsync(int viewId)
        {
            var filters = await _repository.GetFiltersAsync(viewId);
            return Json(filters);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filterId"></param>
        /// <returns></returns>
        // GET: api/filters/{filterId:min(1)}
        [HttpGet]
        [Route(FilterApi.FilterUrl)]
        public async Task<IActionResult> GetFilterAsync(int filterId)
        {
            var filter = await _repository.GetFilterAsync(filterId);
            return JsonReadResult(filter);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="filterId"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="filterId"></param>
        /// <returns></returns>
        // DELETE: api/filters/{filterId:min(1)}
        [HttpDelete]
        [Route(FilterApi.FilterUrl)]
        public async Task<IActionResult> DeleteExistingFilterAsync(int filterId)
        {
            string message = await ValidateDeleteAsync(filterId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeleteFilterAsync(filterId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.FilterName));
            }

            return Ok();
        }

        private readonly IFilterRepository _repository;
    }
}