using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class CompaniesController : ValidatingController<CompanyDbViewModel>
    {
        public CompaniesController(
            ICompanyRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
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

        // GET: api/companies/metadata
        [Route(CompanyApi.CompanyMetadataUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.View)]
        public async Task<IActionResult> GetCompanyMetadataAsync()
        {
            var metadata = await _repository.GetCompanyMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/companies
        [HttpPost]
        [Route(CompanyApi.CompaniesUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.Create)]
        public async Task<IActionResult> PostNewCompanyAsync([FromBody] CompanyDbViewModel company)
        {
            var result = BasicValidationResult(company);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SetCurrentContext(SecurityContext.User);
            var outputItem = await _repository.SaveCompanyAsync(company);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/companies/{companyId:min(1)}
        [HttpPut]
        [Route(CompanyApi.CompanyUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.Edit)]
        public async Task<IActionResult> PutModifiedCompanyAsync(
            int companyId, [FromBody] CompanyDbViewModel company)
        {
            var result = BasicValidationResult(company, companyId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            _repository.SetCurrentContext(SecurityContext.User);
            var outputItem = await _repository.SaveCompanyAsync(company);
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/companies/{companyId:min(1)}
        [HttpDelete]
        [Route(CompanyApi.CompanyUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCompanyAsync(int companyId)
        {
            string result = await ValidateDeleteAsync(companyId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.DeleteCompanyAsync(companyId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var company = await _repository.GetCompanyAsync(item);
            if (company == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Company), item);
            }

            return message;
        }

        private ICompanyRepository _repository;
    }
}