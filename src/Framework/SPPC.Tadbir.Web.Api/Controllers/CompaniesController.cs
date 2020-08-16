using System;
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
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با شرکت ها را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class CompaniesController : ValidatingController<CompanyDbViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات شرکت ها را در دیتابیس سیستمی فراهم می کند</param>
        /// <param name="host">اطلاعات محیط میزبانی سرویس وب را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را در برنامه فراهم می کند</param>
        public CompaniesController(
            ICompanyRepository repository, IHostingEnvironment host, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            _host = host;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام موجودیت شرکت
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Company; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه شرکت ها را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده شرکت ها</returns>
        // GET: api/companies
        [HttpGet]
        [Route(CompanyApi.CompaniesUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.View)]
        public async Task<IActionResult> GetCompaniesAsync()
        {
            var companies = await _repository.GetCompaniesAsync(GridOptions);
            return JsonListResult(companies);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی شرکت مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی شرکت مورد نظر</param>
        /// <returns>اطلاعات نمایشی شرکت</returns>
        // GET: api/companies/{companyId:min(1)}
        [HttpGet]
        [Route(CompanyApi.CompanyUrl)]
        [AuthorizeRequest(SecureEntity.Company, (int)CompanyPermissions.View)]
        public async Task<IActionResult> GetCompanyAsync(int companyId)
        {
            var company = await _repository.GetCompanyAsync(companyId);
            return JsonReadResult(company);
        }

        /// <summary>
        /// به روش آسنکرون، شرکت داده شده را در دیتابیس سیستمی ثبت و دیتابیس آن را روی سرور ایجاد می کند
        /// </summary>
        /// <param name="company">اطلاعات شرکت جدید</param>
        /// <returns>اطلاعات شرکت بعد از ثبت در دیتابیس سیستمی</returns>
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
        [AuthorizeRequest]
        public async Task<IActionResult> PostValidationCompanyAsync([FromBody]CompanyDbViewModel company)
        {
            var result = await ValidationResultAsync(company);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی شرکت مورد نظر برای اصلاح</param>
        /// <param name="company">اطلاعات اصلاح شده شرکت</param>
        /// <returns>اطلاعات شرکت بعد از اصلاح در دیتابیس</returns>
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

        /// <summary>
        /// به روش آسنکرون، شرکت مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="companyId">شناسه دیتابیسی شرکت مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/companies/{companyId:min(1)}
        [HttpDelete]
        [Route(CompanyApi.CompanyUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> DeleteExistingCompanyAsync(int companyId)
        {
            var result = await ValidateDeleteResultAsync(companyId);
            if (result != null)
            {
                return BadRequest(result.ErrorMessage);
            }

            await _repository.DeleteCompanyAsync(companyId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، شرکت های داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/companies
        [HttpPut]
        [Route(CompanyApi.CompaniesUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutExistingCompaniesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteCompaniesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی شرکت مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رفرنس بدون مقدار در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var company = await _repository.GetCompanyAsync(item);
            if (company == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Company, item.ToString());
            }
            else if (item == SecurityContext.User.CompanyId)
            {
                message = _strings.Format(AppStrings.CantDeleteCurrentItem, EntityNameKey);
            }

            return GetGroupActionResult(message, company);
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