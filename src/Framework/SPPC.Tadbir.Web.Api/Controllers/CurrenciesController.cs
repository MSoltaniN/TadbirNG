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
    public class CurrenciesController : ValidatingController<CurrencyViewModel>
    {
        public CurrenciesController(ICurrencyRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Currency; }
        }

        // GET: api/currencies
        [Route(CurrencyApi.CurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetCurrenciesAsync()
        {
            int itemCount = await _repository.GetCountAsync(GridOptions);
            SetItemCount(itemCount);
            var currencies = await _repository.GetCurrenciesAsync(GridOptions);
            return Json(currencies);
        }

        // GET: api/currencies/{currencyId:min(1)}
        [Route(CurrencyApi.CurrencyUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetCurrencyAsync(int currencyId)
        {
            var currency = await _repository.GetCurrencyAsync(currencyId);
            return JsonReadResult(currency);
        }

        // POST: api/currencies
        [HttpPost]
        [Route(CurrencyApi.CurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.Create)]
        public async Task<IActionResult> PostNewCurrencyAsync([FromBody] CurrencyViewModel currency)
        {
            var result = BasicValidationResult(currency);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SetCurrentContext(SecurityContext.User);
            var outputItem = await _repository.SaveCurrencyAsync(currency);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/currencies/{currencyId:min(1)}
        [HttpPut]
        [Route(CurrencyApi.CurrencyUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.Edit)]
        public async Task<IActionResult> PutModifiedCurrencyAsync(int currencyId, [FromBody] CurrencyViewModel currency)
        {
            var result = BasicValidationResult(currency, currencyId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SetCurrentContext(SecurityContext.User);
            var outputItem = await _repository.SaveCurrencyAsync(currency);
            return OkReadResult(outputItem);
        }

        // DELETE: api/currencies/{currencyId:min(1)}
        [HttpDelete]
        [Route(CurrencyApi.CurrencyUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCurrencyAsync(int currencyId)
        {
            string message = await ValidateDeleteAsync(currencyId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.DeleteCurrencyAsync(currencyId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var currency = await _repository.GetCurrencyAsync(item);
            if (currency == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Currency, item.ToString());
            }

            return message;
        }

        private readonly ICurrencyRepository _repository;
    }
}