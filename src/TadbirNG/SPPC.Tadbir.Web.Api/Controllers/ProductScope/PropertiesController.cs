using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.ProductScope;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers.ProductScope
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات ویژگی ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class PropertiesController : ValidatingController<PropertyViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات ویژگی ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public PropertiesController(IPropertyRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت ویژگی
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Property; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده ویژگی ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده ویژگی ها</returns>
        // GET: api/properties
        [HttpGet]
        [Route(PropertyApi.PropertiesUrl)]
        [AuthorizeRequest(SecureEntity.Property, (int)PropertyPermissions.View)]
        public async Task<IActionResult> GetPropertiesAsync()
        {
            var properties = await _repository.GetPropertiesAsync(GridOptions);
            return JsonListResult(properties);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی ویژگی مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="propertyId">شناسه دیتابیسی ویژگی مورد نظر</param>
        /// <returns>اطلاعات نمایشی ویژگی مورد نظر</returns>
        // GET: api/properties/{propertyId:min(1)}
        [HttpGet]
        [Route(PropertyApi.PropertyUrl)]
        [AuthorizeRequest(SecureEntity.Property, (int)PropertyPermissions.View)]
        public async Task<IActionResult> GetPropertyAsync(int propertyId)
        {
            var property = await _repository.GetPropertyAsync(propertyId);
            return JsonReadResult(property);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک ویژگی جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="property">اطلاعات نمایشی ویژگی جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای ویژگی</returns>
        // POST: api/properties
        [HttpPost]
        [Route(PropertyApi.PropertiesUrl)]
        [AuthorizeRequest(SecureEntity.Property, (int)PropertyPermissions.Create)]
        public async Task<IActionResult> PostNewPropertyAsync([FromBody] PropertyViewModel property)
        {
            var result = BasicValidationResult(property);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePropertyAsync(property);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک ویژگی موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="propertyId">شناسه دیتابیسی ویژگی اصلاح شده</param>
        /// <param name="property">اطلاعات نمایشی اصلاح شده برای ویژگی</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای ویژگی</returns>
        // PUT: api/properties/{propertyId:min(1)}
        [HttpPut]
        [Route(PropertyApi.PropertyUrl)]
        [AuthorizeRequest(SecureEntity.Property, (int)PropertyPermissions.Edit)]
        public async Task<IActionResult> PutModifiedPropertyAsync(int propertyId, [FromBody] PropertyViewModel property)
        {
            var result = BasicValidationResult(property, propertyId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePropertyAsync(property);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویژگی مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="propertyId">شناسه دیتابیسی ویژگی مورد نظر برای حذف</param>
        // DELETE: api/properties/{propertyId:min(1)}
        [HttpDelete]
        [Route(PropertyApi.PropertyUrl)]
        [AuthorizeRequest(SecureEntity.Property, (int)PropertyPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingPropertyAsync(int propertyId)
        {
            string message = await ValidateDeleteAsync(propertyId);
            if (!string.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeletePropertyAsync(propertyId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، ویژگی ها داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/properties
        [HttpPut]
        [Route(PropertyApi.PropertiesUrl)]
        [AuthorizeRequest(SecureEntity.Property, (int)PropertyPermissions.Delete)]
        public async Task<IActionResult> PutExistingPropertiesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeletePropertiesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از ویژگی ها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی ویژگی مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = string.Empty;
            var property = await _repository.GetPropertyAsync(item);
            if (property == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Property, item.ToString());
            }

            return message;
        }

        private readonly IPropertyRepository _repository;
    }
}