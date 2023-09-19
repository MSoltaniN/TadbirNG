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
    /// عملیات سرویس وب برای مدیریت اطلاعات برندها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class BrandsController : ValidatingController<BrandViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات برندها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public BrandsController(IBrandRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت برند
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Brand; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده برندها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده برندها</returns>
        // GET: api/brands
        [HttpGet]
        [Route(BrandApi.BrandsUrl)]
        [AuthorizeRequest(SecureEntity.Brand, (int)BrandPermissions.View)]
        public async Task<IActionResult> GetBrandsAsync()
        {
            var brands = await _repository.GetBrandsAsync(GridOptions);
            return JsonListResult(brands);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی برند مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="brandId">شناسه دیتابیسی برند مورد نظر</param>
        /// <returns>اطلاعات نمایشی برند مورد نظر</returns>
        // GET: api/brands/{brandId:min(1)}
        [HttpGet]
        [Route(BrandApi.BrandUrl)]
        [AuthorizeRequest(SecureEntity.Brand, (int)BrandPermissions.View)]
        public async Task<IActionResult> GetBrandAsync(int brandId)
        {
            var brand = await _repository.GetBrandAsync(brandId);
            return JsonReadResult(brand);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک برند جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="brand">اطلاعات نمایشی برند جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای برند</returns>
        // POST: api/brands
        [HttpPost]
        [Route(BrandApi.BrandsUrl)]
        [AuthorizeRequest(SecureEntity.Brand, (int)BrandPermissions.Create)]
        public async Task<IActionResult> PostNewBrandAsync([FromBody] BrandViewModel brand)
        {
            var result = BasicValidationResult(brand);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveBrandAsync(brand);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک برند موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="brandId">شناسه دیتابیسی برند اصلاح شده</param>
        /// <param name="brand">اطلاعات نمایشی اصلاح شده برای برند</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای برند</returns>
        // PUT: api/brands/{brandId:min(1)}
        [HttpPut]
        [Route(BrandApi.BrandUrl)]
        [AuthorizeRequest(SecureEntity.Brand, (int)BrandPermissions.Edit)]
        public async Task<IActionResult> PutModifiedBrandAsync(int brandId, [FromBody] BrandViewModel brand)
        {
            var result = BasicValidationResult(brand, brandId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveBrandAsync(brand);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات برند مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="brandId">شناسه دیتابیسی برند مورد نظر برای حذف</param>
        // DELETE: api/brands/{brandId:min(1)}
        [HttpDelete]
        [Route(BrandApi.BrandUrl)]
        [AuthorizeRequest(SecureEntity.Brand, (int)BrandPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingBrandAsync(int brandId)
        {
            string message = await ValidateDeleteAsync(brandId);
            if (!string.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeleteBrandAsync(brandId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، برندها داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/brands
        [HttpPut]
        [Route(BrandApi.BrandsUrl)]
        [AuthorizeRequest(SecureEntity.Brand, (int)BrandPermissions.Delete)]
        public async Task<IActionResult> PutExistingBrandsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteBrandsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از برندها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی برند مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = string.Empty;
            var brand = await _repository.GetBrandAsync(item);
            if (brand == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Brand, item.ToString());
            }

            return message;
        }

        private readonly IBrandRepository _repository;
    }
}