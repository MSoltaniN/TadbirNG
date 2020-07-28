using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Service.Security;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class CurrenciesController : ValidatingController<CurrencyViewModel>
    {
        public CurrenciesController(ICurrencyRepository repository, ICurrencyRateRepository rateRepository,
            IHostingEnvironment host, ICryptoService crypto, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            _rateRepository = rateRepository;
            _host = host;
            _crypto = crypto;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Currency; }
        }

        // GET: api/currencies
        [HttpGet]
        [Route(CurrencyApi.CurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetCurrenciesAsync()
        {
            var currencies = await _repository.GetCurrenciesAsync(GridOptions);
            foreach (var currency in currencies.Items)
            {
                Localize(currency);
            }

            return JsonListResult(currencies);
        }

        // GET: api/currencies/{currencyId:min(1)}
        [HttpGet]
        [Route(CurrencyApi.CurrencyUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetCurrencyAsync(int currencyId)
        {
            var currency = await _repository.GetCurrencyAsync(currencyId);
            Localize(currency);
            return JsonReadResult(currency);
        }

        // GET: api/currencies/{currencyId:min(1)}/rates
        [HttpGet]
        [Route(CurrencyApi.CurrencyRatesUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.View)]
        public async Task<IActionResult> GetCurrencyRatesAsync(int currencyId)
        {
            var allRates = await _rateRepository.GetCurrencyRatesAsync(currencyId, GridOptions);
            return JsonListResult(allRates);
        }

        // GET: api/currencies/rates/{rateId:min(1)}
        [HttpGet]
        [Route(CurrencyApi.CurrencyRateUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.View)]
        public async Task<IActionResult> GetCurrencyRateAsync(int rateId)
        {
            var currencyRate = await _rateRepository.GetCurrencyRateAsync(rateId);
            return JsonReadResult(currencyRate);
        }

        // GET: api/currencies/info/{nameKey}
        [HttpGet]
        [Route(CurrencyApi.CurrencyInfoByNameUrl)]
        public IActionResult GetCurrencyInfoByName(string nameKey)
        {
            var path = GetLocalCurrencyDbPath();
            var currency = _repository.GetCurrencyByName(path, nameKey);
            Localize(currency);
            currency.BranchId = SecurityContext.User.BranchId;
            currency.BranchName = SecurityContext.User.BranchName;
            return JsonReadResult(currency);
        }

        // GET: api/currencies/names/lookup
        [HttpGet]
        [Route(CurrencyApi.CurrencyNamesLookupUrl)]
        public IActionResult GetCurrencyNamesLookup()
        {
            var path = GetLocalCurrencyDbPath();
            var currencyNames = _repository.GetCurrencyNamesLookup(path);
            Array.ForEach(currencyNames.ToArray(), name => name.Value = _strings[name.Value]);
            SetItemCount(currencyNames.Count);
            var sortedList = currencyNames
                .OrderBy(kv => kv.Value)
                .ToList();
            return Json(sortedList);
        }

        // GET: api/currencies/default/account/{accountId:min(1)}/faccount/{faccountId:min(1)}
        [HttpGet]
        [Route(CurrencyApi.DefaultCurrencyByFullAccountUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetDefaultCurrencyByFullAccountAsync(int accountId, int faccountId)
        {
            var currencyInfo = await _repository.GetDefaultCurrencyAsync(accountId, faccountId);
            return Json(currencyInfo);
        }

        // GET: api/currencies/tax
        [HttpGet]
        [Route(CurrencyApi.TaxCurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetTaxCurrenciesLookupAsync()
        {
            var taxCurrencies = await _repository.GetTaxCurrenciesAsync();
            SetItemCount(taxCurrencies.Count);
            return Json(taxCurrencies);
        }

        // GET: api/currencies/{currencyId:min(1)}/has-rates
        [HttpGet]
        [Route(CurrencyApi.CurrencyHasRatesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.Edit)]
        public async Task<IActionResult> GetCurrencyHasRateAsync(int currencyId)
        {
            bool hasRate = await _rateRepository.CurrencyHasRatesAsync(currencyId);
            return Ok(hasRate);
        }

        // POST: api/currencies/default/{nameKey}
        [HttpPost]
        [Route(CurrencyApi.DefaultCurrencyUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.Create)]
        public async Task<IActionResult> PostDefaultCurrencyAsync(string nameKey)
        {
            var path = GetLocalCurrencyDbPath();
            var currency = _repository.GetCurrencyByName(path, nameKey);
            currency.BranchId = SecurityContext.User.BranchId;
            currency.IsDefaultCurrency = true;

            var outputItem = await _repository.InsertDefaultCurrencyAsync(currency);
            return JsonReadResult(outputItem);
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

            if (await _repository.IsDuplicateCurrencyAsync(currency.Code, currency.Id))
            {
                string message = _strings.Format(AppStrings.CurrencyAlreadyExists, currency.Code);
                return BadRequest(message);
            }

            var outputItem = await _repository.SaveCurrencyAsync(currency);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // POST: api/currencies/{currencyId:min(1)}/rates
        [HttpPost]
        [Route(CurrencyApi.CurrencyRatesUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.Create)]
        public async Task<IActionResult> PostNewCurrencyRateAsync(
            int currencyId, [FromBody] CurrencyRateViewModel currencyRate)
        {
            if (currencyRate.CurrencyId != currencyId)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.Currency));
            }

            var result = BasicValidationResult(currencyRate);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _rateRepository.SaveCurrencyRateAsync(currencyRate);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // POST: api/currencies/tax
        [HttpPost]
        [Route(CurrencyApi.TaxCurrenciesUrl)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PostTaxCurrenciesAsync()
        {
            var file = Request.Form.Files[0];
            string newPath = Path.Combine(_host.WebRootPath, AppConstants.UserUploadFolderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var ticket = Request.Form[AppConstants.ContextHeaderName];
                var context = SecurityContextFromTicket(ticket);
                _repository.CompanyConnection = _crypto.Decrypt(context.User.Connection);
                await _repository.UpdateTaxCurrenciesAsync(fullPath);
            }

            return Ok();
        }

        // POST: api/currencies/tax
        // TODO: temporary
        [HttpPost]
        [Route(CurrencyApi.ZoneUrl)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PostZoneAsync()
        {
            var file = Request.Form.Files[0];
            string newPath = Path.Combine(_host.WebRootPath, AppConstants.UserUploadFolderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var ticket = Request.Form[AppConstants.ContextHeaderName];
                var context = SecurityContextFromTicket(ticket);
                _repository.CompanyConnection = _crypto.Decrypt(context.User.Connection);
                await _repository.UpdateZoneAsync(fullPath);
            }

            return Ok();
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

            if (await _repository.IsDuplicateCurrencyAsync(currency.Code, currency.Id))
            {
                string message = _strings.Format(AppStrings.CurrencyAlreadyExists, currency.Code);
                return BadRequest(message);
            }

            var outputItem = await _repository.SaveCurrencyAsync(currency);
            return OkReadResult(outputItem);
        }

        // PUT: api/currencies/rates/{rateId:min(1)}
        [HttpPut]
        [Route(CurrencyApi.CurrencyRateUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.Edit)]
        public async Task<IActionResult> PutModifiedCurrencyRateAsync(
            int rateId, [FromBody] CurrencyRateViewModel currencyRate)
        {
            var result = BasicValidationResult(currencyRate, rateId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _rateRepository.SaveCurrencyRateAsync(currencyRate);
            return OkReadResult(outputItem);
        }

        // DELETE: api/currencies/{currencyId:min(1)}
        [HttpDelete]
        [Route(CurrencyApi.CurrencyUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCurrencyAsync(int currencyId)
        {
            var result = await ValidateDeleteResultAsync(currencyId);
            if (result != null)
            {
                return BadRequest(result.ErrorMessage);
            }

            await _repository.DeleteCurrencyAsync(currencyId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // DELETE: api/currencies/rates/{rateId:min(1)}
        [HttpDelete]
        [Route(CurrencyApi.CurrencyRateUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCurrencyRateAsync(int rateId)
        {
            var result = await ValidateRateDeleteAsync(rateId);
            if (result != null)
            {
                return BadRequest(result.ErrorMessage);
            }

            await _rateRepository.DeleteCurrencyRateAsync(rateId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/currencies
        [HttpPut]
        [Route(CurrencyApi.CurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.Delete)]
        public async Task<IActionResult> PutExistingCurrenciesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteCurrenciesAsync);
        }

        // PUT: api/currency/rates
        [HttpPut]
        [Route(CurrencyApi.DeleteCurrencyRatesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyRatePermissions.Delete)]
        public async Task<IActionResult> PutExistingCurrencyRatesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _rateRepository.DeleteCurrencyRatesAsync);
        }

        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var currency = await _repository.GetCurrencyAsync(item);
            if (currency == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Currency, item.ToString());
            }
            else if (currency.BranchId != SecurityContext.User.BranchId)
            {
                message = _strings.Format(AppStrings.OtherBranchEditNotAllowed);
            }
            else
            {
                bool canDelete = await _repository.CanDeleteCurrencyAsync(item);
                if (!canDelete)
                {
                    message = _strings.Format(AppStrings.CantDeleteUsedCurrency, _strings[currency.Name]);
                }
            }

            return GetGroupActionResult(message, currency);
        }

        protected async Task<GroupActionResultViewModel> ValidateRateDeleteAsync(int item)
        {
            string message = String.Empty;
            var currencyRate = await _rateRepository.GetCurrencyRateAsync(item);
            if (currencyRate == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.CurrencyRate, item.ToString());
            }
            else if (currencyRate.BranchId != SecurityContext.User.BranchId)
            {
                message = _strings.Format(AppStrings.OtherBranchEditNotAllowed);
            }

            return GetGroupActionResult(message, currencyRate);
        }

        private void Localize(CurrencyViewModel currency)
        {
            currency.Name = _strings[currency.Name];
            currency.MinorUnit = _strings[currency.MinorUnit];
        }

        private string GetLocalCurrencyDbPath()
        {
            return Path.Combine(_host.WebRootPath, "static", "currencies.json");
        }

        private readonly ICurrencyRepository _repository;
        private readonly ICurrencyRateRepository _rateRepository;
        private readonly IHostingEnvironment _host;
        private readonly ICryptoService _crypto;
    }
}