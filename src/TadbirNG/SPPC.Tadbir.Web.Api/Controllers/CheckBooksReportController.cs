using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    /// عملیات سرویس وب برای مدیریت اطلاعات دفتر دسته های چک را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class CheckBooksReportController : ValidatingController<CheckBookReportViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات دفتر دسته های چک در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public CheckBooksReportController(ICheckBookReportRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت دفتر دسته چک
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.CheckBookReport; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده دفتر دسته های چک را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده دفتر دسته های چک</returns>
        // GET: api/check-books-report
        [HttpGet]
        [Route(CheckBookReportApi.CheckBooksReportUrl)]
        [AuthorizeRequest(SecureEntity.CheckBookReport, (int)CheckBookReportPermissions.View)]
        public async Task<IActionResult> GetCheckBooksReportAsync()
        {
            var checkBooksReport = await _repository.GetCheckBooksReportAsync(GridOptions);
            return JsonListResult(checkBooksReport);
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک های داده شده را  - در صورت امکان - بایگانی می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات بایگانی گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // Put: api/check-books/archive
        [HttpPut]
        [Route(CheckBookReportApi.ArchiveCheckBooksURL)]
        [AuthorizeRequest(SecureEntity.CheckBookReport, (int)CheckBookReportPermissions.Archive)]
        public async Task<IActionResult> PutExsitingCheckBooksAsArchivedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupArchiveResultAsync(actionDetail, true);
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک های داده شده را  - در صورت امکان - لغو بایگانی می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات لغو بایگانی گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // Put: api/check-books/undo-archive
        [HttpPut]
        [Route(CheckBookReportApi.UndoArchiveCheckBooksURL)]
        [AuthorizeRequest(SecureEntity.CheckBookReport, (int)CheckBookReportPermissions.UndoArchive)]
        public async Task<IActionResult> PutExsitingCheckBooksAsUndoArchivedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupArchiveResultAsync(actionDetail, false);
        }

        private async Task<GroupActionResultViewModel> ValidateArchiveResultAsync(int item, bool archiveValue)
        {
            string message = String.Empty;
            var checkBook = await _repository.GetCheckBookAsync(item); ;
            if (checkBook == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.CheckBook, item.ToString());
            }
            else if(checkBook.IsArchived == archiveValue)
            {
                if(archiveValue==true)
                {
                    message = _strings.Format(AppStrings.NoNeedArchive, AppStrings.CheckBook, checkBook.Name);
                }
                else
                {
                    message = _strings.Format(AppStrings.NoNeedUndoArchive, AppStrings.CheckBook, checkBook.Name);
                }
            }

            return GetGroupActionResult(message, checkBook);
        }

        private async Task<IActionResult> GroupArchiveResultAsync(
            ActionDetailViewModel actionDetail, bool archiveValue)
        {
            if(actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            foreach(int item in actionDetail.Items)
            {
                var result = await ValidateArchiveResultAsync(item, archiveValue);
                if(result == null)
                {
                    validated.Add(item);
                }
                else
                {
                    notValidated.Add(result);
                }
            }

            if(validated.Count > 0)
            {
                await _repository.EditArchiveCheckBooksAsync(validated, archiveValue);
            }

            return Ok(notValidated);
        }
        private readonly ICheckBookReportRepository _repository;
    }
}