using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات منابع و مصارف را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class SourceAppsController : ValidatingController<SourceAppViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات منابع و مصارف در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public SourceAppsController(ISourceAppRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت منبع و مصرف
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.SourceApp; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده منابع و مصارف را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده منابع و مصارف</returns>
        // GET: api/source-apps
        [HttpGet]
        [Route(SourceAppApi.SourceAppsUrl)]
        [AuthorizeRequest(SecureEntity.SourceApp, (int)SourceAppPermissions.View)]
        public async Task<IActionResult> GetSourceAppsAsync()
        {
            var sourceApps = await _repository.GetSourceAppsAsync(GridOptions);
            return JsonListResult(sourceApps);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی منبع و مصرف مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="sourceAppId">شناسه دیتابیسی منبع و مصرف مورد نظر</param>
        /// <returns>اطلاعات نمایشی منبع و مصرف مورد نظر</returns>
        // GET: api/source-apps/{sourceAppId:min(1)}
        [HttpGet]
        [Route(SourceAppApi.SourceAppUrl)]
        [AuthorizeRequest(SecureEntity.SourceApp, (int)SourceAppPermissions.View)]
        public async Task<IActionResult> GetSourceAppAsync(int sourceAppId)
        {
            var sourceApp = await _repository.GetSourceAppAsync(sourceAppId);
            return JsonReadResult(sourceApp);
        }

        /// <summary>
        /// به روش آسنکرون، منبع جدیدی با مقادیر پیشنهادی در دیتابیس ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی منبع جدید با مقادیر پیشنهادی</returns>
        // GET: api/source-apps/new
        [HttpGet]
        [Route(SourceAppApi.NewSourceAppUrl)]
        [AuthorizeRequest(SecureEntity.SourceApp, (int)SourceAppPermissions.Create)]
        public async Task<IActionResult> GetNewSourceAppAsync()
        {
            var newSourceApp = await _repository.GetNewSourceAppAsync();
            return Json(newSourceApp);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک منبع و مصرف جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="sourceApp">اطلاعات نمایشی منبع و مصرف جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای منبع و مصرف</returns>
        // POST: api/source-apps
        [HttpPost]
        [Route(SourceAppApi.SourceAppsUrl)]
        [AuthorizeRequest(SecureEntity.SourceApp, (int)SourceAppPermissions.Create)]
        public async Task<IActionResult> PostNewSourceAppAsync([FromBody] SourceAppViewModel sourceApp)
        {
            var result = await ValidationResultAsync(sourceApp);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveSourceAppAsync(sourceApp);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک منبع و مصرف موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="sourceAppId">شناسه دیتابیسی منبع و مصرف اصلاح شده</param>
        /// <param name="sourceApp">اطلاعات نمایشی اصلاح شده برای منبع و مصرف</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای منبع و مصرف</returns>
        // PUT: api/source-apps/{sourceAppId:min(1)}
        [HttpPut]
        [Route(SourceAppApi.SourceAppUrl)]
        [AuthorizeRequest(SecureEntity.SourceApp, (int)SourceAppPermissions.Edit)]
        public async Task<IActionResult> PutModifiedSourceAppAsync(int sourceAppId, [FromBody] SourceAppViewModel sourceApp)
        {
            var result =await ValidationResultAsync(sourceApp, sourceAppId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveSourceAppAsync(sourceApp);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات منبع و مصرف مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="sourceAppId">شناسه دیتابیسی منبع و مصرف مورد نظر برای حذف</param>
        // DELETE: api/source-apps/{sourceAppId:min(1)}
        [HttpDelete]
        [Route(SourceAppApi.SourceAppUrl)]
        [AuthorizeRequest(SecureEntity.SourceApp, (int)SourceAppPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingSourceAppAsync(int sourceAppId)
        {
            var result = await ValidateDeleteResultAsync(sourceAppId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteSourceAppAsync(sourceAppId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، منابع و مصارف داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/source-apps
        [HttpPut]
        [Route(SourceAppApi.SourceAppsUrl)]
        [AuthorizeRequest(SecureEntity.SourceApp, (int)SourceAppPermissions.Delete)]
        public async Task<IActionResult> PutExistingSourceAppsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteSourceAppsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از منابع و مصارف اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی منبع و مصرف مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var sourceApp = await _repository.GetSourceAppAsync(item);
            if (sourceApp == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.SourceApp, item.ToString());
                return GetGroupActionResult(message, sourceApp);
            }
            var result = BranchValidationResult(sourceApp);
            if (result is BadRequestObjectResult errorResult)
            {
                return GetGroupActionResult(errorResult.Value.ToString(), sourceApp);
            }
            return GetGroupActionResult(message, sourceApp);
        }

        private async Task<IActionResult> ValidationResultAsync(SourceAppViewModel sourceApp, int sourceAppId = 0)
        {
            var result = BasicValidationResult(sourceApp, sourceAppId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateNameAsync(sourceApp))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.Name));
            }

            if (await _repository.IsDuplicateCodeAsync(sourceApp))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.Code));
            }

            result = BranchValidationResult(sourceApp);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            return Ok();
        }

        private readonly ISourceAppRepository _repository;
    }
}