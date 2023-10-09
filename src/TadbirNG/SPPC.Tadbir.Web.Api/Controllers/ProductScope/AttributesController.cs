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
    /// عملیات سرویس وب برای مدیریت اطلاعات خصوصیت ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class AttributesController : ValidatingController<AttributeViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات خصوصیت ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public AttributesController(IAttributeRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت خصوصیت
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Attribute; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده خصوصیت ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده خصوصیت ها</returns>
        // GET: api/attributes
        [HttpGet]
        [Route(AttributeApi.AttributesUrl)]
        [AuthorizeRequest(SecureEntity.Attribute, (int)AttributePermissions.View)]
        public async Task<IActionResult> GetAttributesAsync()
        {
            var attributes = await _repository.GetAttributesAsync(GridOptions);
            return JsonListResult(attributes);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی خصوصیت مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="attributeId">شناسه دیتابیسی خصوصیت مورد نظر</param>
        /// <returns>اطلاعات نمایشی خصوصیت مورد نظر</returns>
        // GET: api/attributes/{attributeId:min(1)}
        [HttpGet]
        [Route(AttributeApi.AttributeUrl)]
        [AuthorizeRequest(SecureEntity.Attribute, (int)AttributePermissions.View)]
        public async Task<IActionResult> GetAttributeAsync(int attributeId)
        {
            var attribute = await _repository.GetAttributeAsync(attributeId);
            return JsonReadResult(attribute);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک خصوصیت جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="attribute">اطلاعات نمایشی خصوصیت جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای خصوصیت</returns>
        // POST: api/attributes
        [HttpPost]
        [Route(AttributeApi.AttributesUrl)]
        [AuthorizeRequest(SecureEntity.Attribute, (int)AttributePermissions.Create)]
        public async Task<IActionResult> PostNewAttributeAsync([FromBody] AttributeViewModel attribute)
        {
            var result = BasicValidationResult(attribute);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveAttributeAsync(attribute);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک خصوصیت موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="attributeId">شناسه دیتابیسی خصوصیت اصلاح شده</param>
        /// <param name="attribute">اطلاعات نمایشی اصلاح شده برای خصوصیت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای خصوصیت</returns>
        // PUT: api/attributes/{attributeId:min(1)}
        [HttpPut]
        [Route(AttributeApi.AttributeUrl)]
        [AuthorizeRequest(SecureEntity.Attribute, (int)AttributePermissions.Edit)]
        public async Task<IActionResult> PutModifiedAttributeAsync(int attributeId, [FromBody] AttributeViewModel attribute)
        {
            var result = BasicValidationResult(attribute, attributeId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveAttributeAsync(attribute);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خصوصیت مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="attributeId">شناسه دیتابیسی خصوصیت مورد نظر برای حذف</param>
        // DELETE: api/attributes/{attributeId:min(1)}
        [HttpDelete]
        [Route(AttributeApi.AttributeUrl)]
        [AuthorizeRequest(SecureEntity.Attribute, (int)AttributePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingAttributeAsync(int attributeId)
        {
            string message = await ValidateDeleteAsync(attributeId);
            if (!string.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeleteAttributeAsync(attributeId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، خصوصیت ها داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/attributes
        [HttpPut]
        [Route(AttributeApi.AttributesUrl)]
        [AuthorizeRequest(SecureEntity.Attribute, (int)AttributePermissions.Delete)]
        public async Task<IActionResult> PutExistingAttributesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteAttributesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از خصوصیت ها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی خصوصیت مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = string.Empty;
            var attribute = await _repository.GetAttributeAsync(item);
            if (attribute == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Attribute, item.ToString());
            }

            return message;
        }

        private readonly IAttributeRepository _repository;
    }
}