using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Check;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
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
        /// به روش آسنکرون، اطلاعات نمایشی دسته چک مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkbookId">شناسه دیتابیسی دسته چک مورد نظر</param>
        /// <returns>اطلاعات نمایشی دسته چک مورد نظر</returns>
        // GET: api/check-books/{checkbookId:min(1)}
        [HttpGet]
        [Route(CheckBookApi.CheckBookUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.View)]
        public async Task<IActionResult> GetCheckBookAsync(int checkbookId)
        {
            var checkbook = await _repository.GetCheckBookAsync(checkbookId);
            Localize(checkbook);
            return JsonReadResult(checkbook);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دسته چک مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="checkBookNo">شماره دسته چک مورد نظر</param>
        /// <returns>اطلاعات نمایشی دسته چک مورد نظر</returns>
        // GET: api/check-Books/by-no/{checkBookNo:min(1)}
        [HttpGet]
        [Route(CheckBookApi.CheckBookByNoUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.View)]
        public async Task<IActionResult> GetCheckBookByNoAsync(int checkBookNo)
        {
            var checkBookByNo = await _repository.GetCheckBookAsync(checkBookNo);
            Localize(checkBookByNo);
            return JsonReadResult(checkBookByNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک دسته چک جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="checkbook">اطلاعات نمایشی دسته چک جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای دسته چک</returns>
        // POST: api/check-books
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
        // PUT: api/check-books/{checkbookId:min(1)}
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
        // DELETE: api/check-books/{checkbookId:min(1)}
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

        #region CheckBook Pages
        /// <summary>
        /// به روش آسنکرون، کلیه برگه های چک ثبت شده را برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه دیتابیسی دسته چک مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده برگه های دسته چک</returns>
        // GET: api/check-Books/{checkBookId:min(1)}/pages
        [HttpGet]
        [Route(CheckBookApi.CheckBookPagesUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.View)]
        public async Task<IActionResult> GetPagesAsync(int checkBookId)
        {
            var pages = await _repository.GetPagesAsync(checkBookId, GridOptions);
            SetItemCount(pages.TotalCount);
            return Json(pages.Items);
        }
        
        #endregion

        private void Localize(CheckBookViewModel checkBook)
        {
            if (checkBook != null)
            {
                checkBook.BankName = _strings[checkBook.BankName ?? String.Empty];
                checkBook.Name = _strings[checkBook.Name ?? String.Empty];
            }
        }

        

        private readonly ICheckBookRepository _repository;
    }
}