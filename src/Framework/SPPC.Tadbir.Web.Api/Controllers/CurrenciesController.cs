﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class CurrenciesController : ValidatingController<CurrencyViewModel>
    {
        public CurrenciesController(ICurrencyRepository repository, ICurrencyRateRepository rateRepository,
            IHostingEnvironment host, IHostingEnvironment hostingEnvironment, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            _rateRepository = rateRepository;
            _host = host;

            _hostingEnvironment = hostingEnvironment;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Currency; }
        }

        #region برای تست آپلود فایل میباشد
        private IHostingEnvironment _hostingEnvironment;

        [Route("currencies/test-upload")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> PostUploadAsync()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
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
                    _repository.CompanyConnection = context.User.Connection;
                    await _repository.UpdateTaxCurrenciesAsync(fullPath);
                }

                return Json("انجام شد");
            }
            catch (Exception ex)
            {
                return Json("خطا: " + ex.Message);
            }
        }
        #endregion

        // GET: api/currencies
        [Route(CurrencyApi.CurrenciesUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetCurrenciesAsync()
        {
            int itemCount = await _repository.GetCountAsync(GridOptions);
            SetItemCount(itemCount);
            var currencies = await _repository.GetCurrenciesAsync(GridOptions);
            foreach (var currency in currencies)
            {
                Localize(currency);
            }

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

        // GET: api/currencies/{currencyId:min(1)}/rates
        [Route(CurrencyApi.CurrencyRatesUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.View)]
        public async Task<IActionResult> GetCurrencyRatesAsync(int currencyId)
        {
            var allRates = await _rateRepository.GetCurrencyRatesAsync(currencyId);
            SetItemCount(allRates.Count);
            var gridOptions = GridOptions ?? new GridOptions();
            var rates = allRates
                .Apply(gridOptions)
                .ToList();
            return Json(rates);
        }

        // GET: api/currencies/rates/{rateId:min(1)}
        [Route(CurrencyApi.CurrencyRateUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.View)]
        public async Task<IActionResult> GetCurrencyRateAsync(int rateId)
        {
            var currencyRate = await _rateRepository.GetCurrencyRateAsync(rateId);
            return JsonReadResult(currencyRate);
        }

        // GET: api/currencies/info/{nameKey}
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
        [Route(CurrencyApi.DefaultCurrencyByFullAccountUrl)]
        [AuthorizeRequest(SecureEntity.Currency, (int)CurrencyPermissions.View)]
        public async Task<IActionResult> GetDefaultCurrencyByFullAccountAsync(int accountId, int faccountId)
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var currencyInfo = await _repository.GetDefaultCurrencyAsync(accountId, faccountId);
            return Json(currencyInfo);
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

            _repository.SetCurrentContext(SecurityContext.User);
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

            _rateRepository.SetCurrentContext(SecurityContext.User);
            var outputItem = await _rateRepository.SaveCurrencyRateAsync(currencyRate);
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

            if (await _repository.IsDuplicateCurrencyAsync(currency.Code, currency.Id))
            {
                string message = _strings.Format(AppStrings.CurrencyAlreadyExists, currency.Code);
                return BadRequest(message);
            }

            _repository.SetCurrentContext(SecurityContext.User);
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

            _rateRepository.SetCurrentContext(SecurityContext.User);
            var outputItem = await _rateRepository.SaveCurrencyRateAsync(currencyRate);
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

        // DELETE: api/currencies/rates/{rateId:min(1)}
        [HttpDelete]
        [Route(CurrencyApi.CurrencyRateUrl)]
        [AuthorizeRequest(SecureEntity.CurrencyRate, (int)CurrencyRatePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCurrencyRateAsync(int rateId)
        {
            string message = await ValidateRateDeleteAsync(rateId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            _rateRepository.SetCurrentContext(SecurityContext.User);
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
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await ValidateGroupDeleteAsync(actionDetail.Items);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.DeleteCurrenciesAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        protected override async Task<string> ValidateDeleteAsync(int item)
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

            return message;
        }

        protected async Task<string> ValidateRateDeleteAsync(int item)
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

            return message;
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
    }
}