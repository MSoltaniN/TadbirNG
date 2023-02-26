using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Check;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات دسته چک ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class CheckBooksController : ValidatingController<CheckBookViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات دسته چک ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public CheckBooksController(ICheckBookRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت دسته چک
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.CheckBook; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده دسته چک ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده دسته چک ها</returns>
        // GET: api/checkbooks
        [HttpGet]
        [Route(CheckBookApi.CheckBooksUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.View)]
        public async Task<IActionResult> GetCheckBooksAsync()
        {
            var checkbooks = await _repository.GetCheckBooksAsync(GridOptions);
            return JsonListResult(checkbooks);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی دسته چک مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkbookId">شناسه دیتابیسی دسته چک مورد نظر</param>
        /// <returns>اطلاعات نمایشی دسته چک مورد نظر</returns>
        // GET: api/checkbooks/{checkbookId:min(1)}
        [HttpGet]
        [Route(CheckBookApi.CheckBookUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.View)]
        public async Task<IActionResult> GetCheckBookAsync(int checkbookId)
        {
            var checkbook = await _repository.GetCheckBookAsync(checkbookId);
            return JsonReadResult(checkbook);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک دسته چک جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="checkbook">اطلاعات نمایشی دسته چک جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای دسته چک</returns>
        // POST: api/checkbooks
        [HttpPost]
        [Route(CheckBookApi.CheckBooksUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.Create)]
        public async Task<IActionResult> PostNewCheckBookAsync([FromBody] CheckBookViewModel checkbook)
        {
            var result = BasicValidationResult(checkbook);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCheckBookAsync(checkbook);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک دسته چک موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="checkbookId">شناسه دیتابیسی دسته چک اصلاح شده</param>
        /// <param name="checkbook">اطلاعات نمایشی اصلاح شده برای دسته چک</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای دسته چک</returns>
        // PUT: api/checkbooks/{checkbookId:min(1)}
        [HttpPut]
        [Route(CheckBookApi.CheckBookUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.Edit)]
        public async Task<IActionResult> PutModifiedCheckBookAsync(int checkbookId, [FromBody] CheckBookViewModel checkbook)
        {
            var result = BasicValidationResult(checkbook, checkbookId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCheckBookAsync(checkbook);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دسته چک مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="checkbookId">شناسه دیتابیسی دسته چک مورد نظر برای حذف</param>
        // DELETE: api/checkbooks/{checkbookId:min(1)}
        [HttpDelete]
        [Route(CheckBookApi.CheckBookUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCheckBookAsync(int checkbookId)
        {
            string message = await ValidateDeleteAsync(checkbookId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeleteCheckBookAsync(checkbookId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک ها داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/checkbooks
        [HttpPut]
        [Route(CheckBookApi.CheckBooksUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.Delete)]
        public async Task<IActionResult> PutExistingCheckBooksAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteCheckBooksAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از دسته چک ها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی دسته چک مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var checkbook = await _repository.GetCheckBookAsync(item);
            if (checkbook == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.CheckBook, item.ToString());
            }

            return message;
        }

        private readonly ICheckBookRepository _repository;
    }
}