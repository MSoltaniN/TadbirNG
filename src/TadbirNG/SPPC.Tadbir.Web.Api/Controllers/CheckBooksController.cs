using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Check;
using SPPC.Tadbir.Web.Api.Filters;
using System;
using System.Threading.Tasks;

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
        /// <param name="pageRepository">.امکان ذخیره و بازیابی اطلاعات برگه های چک در دیتابیس فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public CheckBooksController(ICheckBookRepository repository, IStringLocalizer<AppStrings> strings,
            ICheckBookPageRepository pageRepository,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _pageRepository = pageRepository;
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
        public async Task<IActionResult> GetCheckBookByNoAsync(string checkBookNo)
        {
            var checkBookByNo = await _repository.GetCheckBookByNoAsync(checkBookNo);
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

            if (await _repository.IsDuplicateCheckBookNameAsync(checkbook))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue,
                    AppStrings.CheckBookName));
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
        public async Task<IActionResult> PutModifiedCheckBookAsync(int checkbookId,
            [FromBody] CheckBookViewModel checkbook)
        {
            var result = BasicValidationResult(checkbook, checkbookId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.HasChildrenAsync(checkbookId))
            {
                string msg = _strings[AppStrings.InabilityEditCheckbook];
                return BadRequestResult(msg);
            }

            if (await _repository.IsDuplicateCheckBookNameAsync(checkbook))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue,
                    AppStrings.CheckBookName));
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
            var isExistCheckbook = await _repository.HasParentAsync(item);
            if (!isExistCheckbook)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound,
                    AppStrings.CheckBook, item.ToString());
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
            var pages = await _pageRepository.GetPagesAsync(checkBookId, GridOptions);
            return Json(pages.Items);
        }

        /// <summary>
        ///  به روش آسنکرون، اطلاعات نمایشی برگه های یک دسته چک جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="checkBookId">شناسه دیتابیسی دسته چک مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده برگه های دسته چک</returns>
        // GET: api/check-book/{checkBookId:min(1)}/pages
        [HttpPost]
        [Route(CheckBookApi.CheckBookPagesUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.CreatePages)]
        public async Task<IActionResult> CreatePagesAsync(int checkBookId)
        {
            if (!await _repository.HasParentAsync(checkBookId))
            {
                string result = _strings[AppStrings.CheckBookParntInfoNotExist];
                return BadRequestResult(result);
            }

            if (await _repository.HasChildrenAsync(checkBookId))
            {
                string result = _strings[AppStrings.CheckBookPagesExist];
                return BadRequestResult(result);
            }

            var pages = await _pageRepository.CreatePagesAsync(checkBookId);
            return Json(pages.Items);
        }

        /// <summary>
        ///  به روش آسنکرون، برگه های دسته چک مشخص شده با شناسه دسته چک را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="checkBookId">شناسه دیتابیسی دسته چک مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده برگه های دسته چک</returns>
        // GET: api/check-book/{checkBookId:min(1)}/pages
        [HttpDelete]
        [Route(CheckBookApi.CheckBookPagesUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.DeletePages)]
        public async Task<IActionResult> DeletePagesAsync(int checkBookId)
        {
            string message = await ValidateDeleteAsync(checkBookId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            if (await _repository.HasConnectedToCheckAsync(checkBookId))
            {
                string msg = _strings[AppStrings.HasConnectedToCheck];
                return BadRequestResult(msg);
            }

            await _pageRepository.DeleteCheckBookPagesAsync(checkBookId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        ///  به روش آسنکرون، برگه دسته چک مشخص شده با شناسه دیتابیسی را ابطال می کند
        /// </summary>
        /// <param name="checkBookPageId">شناسه عددی یکی از برگه های چک موجود</param>
        /// <returns>مشخصات برگه چک ابطال شده</returns>
        [HttpPut]
        [Route(CheckBookApi.CheckBookPageCancelPageUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.CancelPage)]
        public async Task<IActionResult> CancelPageAsync(int checkBookPageId)
        {
            string description = _strings.Format(AppStrings.CancelPageLog);
            var outputItem = await _pageRepository.ChangeStateCheckAsync(checkBookPageId,
                CheckBookPageState.Cancelled, description);
            return OkReadResult(outputItem);
        }

        /// <summary>
        ///  به روش آسنکرون، برگه دسته چک مشخص شده با شناسه دیتابیسی را برگشت از ابطال می کند
        /// </summary>
        /// <param name="checkBookPageId">شناسه عددی یکی از برگه های چک موجود</param>
        /// <returns>مشخصات برگه چک برگشت شده از ابطال </returns>
        [HttpPut]
        [Route(CheckBookApi.CheckBookPageUndoCancelPageUrl)]
        [AuthorizeRequest(SecureEntity.CheckBook, (int)CheckBookPermissions.UndoCancelPage)]
        public async Task<IActionResult> UndoCancelPageAsync(int checkBookPageId)
        {
            string description = _strings.Format(AppStrings.UndoCancelPageLog);
            var outputItem = await _pageRepository.ChangeStateCheckAsync(checkBookPageId,
                CheckBookPageState.Blank, description);
            return OkReadResult(outputItem);
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
        private readonly ICheckBookPageRepository _pageRepository;
    }
}