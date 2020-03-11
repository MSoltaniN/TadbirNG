using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class CompaniesController : ValidatingController<CompanyDbViewModel>
    {
        public CompaniesController(
            ICompanyRepository repository, IHostingEnvironment host, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            _host = host;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.Company; }
        }

        // GET: api/companies
        [Route(CompanyApi.CompaniesUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.View)]
        public async Task<IActionResult> GetCompaniesAsync()
        {
            int itemCount = await _repository.GetCountAsync(GridOptions);
            SetItemCount(itemCount);
            var companies = await _repository.GetCompaniesAsync(GridOptions);
            SetRowNumbers(companies);
            return Json(companies);
        }

        // GET: api/companies/{companyId:min(1)}
        [Route(CompanyApi.CompanyUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.View)]
        public async Task<IActionResult> GetCompanyAsync(int companyId)
        {
            var company = await _repository.GetCompanyAsync(companyId);
            return JsonReadResult(company);
        }

        // POST: api/companies
        [HttpPost]
        [Route(CompanyApi.CompaniesUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PostNewCompanyAsync([FromBody] CompanyDbViewModel company)
        {
            var result = await ValidationResultAsync(company);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCompanyAsync(company, _host.WebRootPath);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // POST: api/companies/validation
        [HttpPost]
        [Route(CompanyApi.CompanyValidationUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.Create)]
        public async Task<IActionResult> PostValidationCompanyAsync([FromBody]CompanyDbViewModel company)
        {
            var result = await ValidationResultAsync(company);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            return Ok();
        }

        // PUT: api/companies/{companyId:min(1)}
        [HttpPut]
        [Route(CompanyApi.CompanyUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutModifiedCompanyAsync(
            int companyId, [FromBody] CompanyDbViewModel company)
        {
            var result = BasicValidationResult(company, companyId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCompanyAsync(company, _host.WebRootPath);
            return OkReadResult(outputItem);
        }

        // DELETE: api/companies/{companyId:min(1)}
        [HttpDelete]
        [Route(CompanyApi.CompanyUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> DeleteExistingCompanyAsync(int companyId)
        {
            string result = await ValidateDeleteAsync(companyId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteCompanyAsync(companyId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/companies
        [HttpPut]
        [Route(CompanyApi.CompaniesUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.Delete)]
        public async Task<IActionResult> PutExistingCompaniesAsDeletedAsync(
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

            await _repository.DeleteCompaniesAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            if (item == SecurityContext.User.CompanyId)
            {
                return _strings.Format(AppStrings.CantDeleteCurrentItem, EntityNameKey);
            }

            var company = await _repository.GetCompanyAsync(item);
            if (company == null)
            {
                return _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Company, item.ToString());
            }

            return String.Empty;
        }

        private async Task<IActionResult> ValidationResultAsync(CompanyDbViewModel company, int companyId = 0)
        {
            var result = BasicValidationResult(company, companyId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateCompanyAsync(company))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.DbName));
            }

            if (_repository.IsDuplicateCompanyUserNameAsync(company))
            {
                return BadRequest(_strings.Format(AppStrings.InvalidDatabaseUserName));
            }

            return Ok();
        }

        private readonly IHostingEnvironment _host;
        private ICompanyRepository _repository;
    }
}