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
    /// عملیات سرویس وب برای مدیریت اطلاعات فایل ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class FilesController : ValidatingController<FileViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات فایل ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public FilesController(IFileRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت فایل
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.File; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده فایل ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده فایل ها</returns>
        // GET: api/files
        [HttpGet]
        [Route(FileApi.FilesUrl)]
        [AuthorizeRequest(SecureEntity.File, (int)FilePermissions.View)]
        public async Task<IActionResult> GetFilesAsync()
        {
            var files = await _repository.GetFilesAsync(GridOptions);
            return JsonListResult(files);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی فایل مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="fileId">شناسه دیتابیسی فایل مورد نظر</param>
        /// <returns>اطلاعات نمایشی فایل مورد نظر</returns>
        // GET: api/files/{fileId:min(1)}
        [HttpGet]
        [Route(FileApi.FileUrl)]
        [AuthorizeRequest(SecureEntity.File, (int)FilePermissions.View)]
        public async Task<IActionResult> GetFileAsync(int fileId)
        {
            var file = await _repository.GetFileAsync(fileId);
            return JsonReadResult(file);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک فایل جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="file">اطلاعات نمایشی فایل جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فایل</returns>
        // POST: api/files
        [HttpPost]
        [Route(FileApi.FilesUrl)]
        [AuthorizeRequest(SecureEntity.File, (int)FilePermissions.Create)]
        public async Task<IActionResult> PostNewFileAsync([FromForm] FileViewModel file)
        {
            var result = BasicValidationResult(file);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFileAsync(file);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک فایل موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="fileId">شناسه دیتابیسی فایل اصلاح شده</param>
        /// <param name="file">اطلاعات نمایشی اصلاح شده برای فایل</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فایل</returns>
        // PUT: api/files/{fileId:min(1)}
        [HttpPut]
        [Route(FileApi.FileUrl)]
        [AuthorizeRequest(SecureEntity.File, (int)FilePermissions.Edit)]
        public async Task<IActionResult> PutModifiedFileAsync(int fileId, [FromForm] FileViewModel file)
        {
            var result = BasicValidationResult(file, fileId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFileAsync(file);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فایل مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="fileId">شناسه دیتابیسی فایل مورد نظر برای حذف</param>
        // DELETE: api/files/{fileId:min(1)}
        [HttpDelete]
        [Route(FileApi.FileUrl)]
        [AuthorizeRequest(SecureEntity.File, (int)FilePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingFileAsync(int fileId)
        {
            string message = await ValidateDeleteAsync(fileId);
            if (!string.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeleteFileAsync(fileId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، فایل ها داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/files
        [HttpPut]
        [Route(FileApi.FilesUrl)]
        [AuthorizeRequest(SecureEntity.File, (int)FilePermissions.Delete)]
        public async Task<IActionResult> PutExistingFilesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteFilesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از فایل ها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی فایل مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = string.Empty;
            var file = await _repository.GetFileAsync(item);
            if (file == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.File, item.ToString());
            }

            return message;
        }

        private readonly IFileRepository _repository;
    }
}