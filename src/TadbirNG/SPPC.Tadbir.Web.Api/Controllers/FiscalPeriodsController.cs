using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با دوره های مالی را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class FiscalPeriodsController : ValidatingController<FiscalPeriodViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات دوره های مالی در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        public FiscalPeriodsController(
            IFiscalPeriodRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام موجودیت دوره مالی
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.FiscalPeriod; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه دوره های مالی در شرکت جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده دوره های مالی</returns>
        // GET: api/fperiods/company/{companyId:min(1)}
        [HttpGet]
        [Route(FiscalPeriodApi.CompanyFiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodsAsync()
        {
            var gridOptions = GridOptions ?? new GridOptions();
            var fiscalPeriods = await _repository.GetFiscalPeriodsAsync(gridOptions);
            return JsonListResult(fiscalPeriods);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی دوره مالی مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اطلاعات نمایشی دوره مالی</returns>
        // GET: api/fperiods/{fpId:min(1)}
        [HttpGet]
        [Route(FiscalPeriodApi.FiscalPeriodUrl)]
        public async Task<IActionResult> GetFiscalPeriodAsync(int fpId)
        {
            var fiscalPeriod = await _repository.GetFiscalPeriodAsync(fpId);
            return JsonReadResult(fiscalPeriod);
        }

        /// <summary>
        /// به روش آسنکرون، دوره مالی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="fiscalPeriod">اطلاعات دوره مالی جدید</param>
        /// <returns>اطلاعات دوره مالی بعد از ایجاد در دیتابیس</returns>
        // POST: api/fperiods
        [HttpPost]
        [Route(FiscalPeriodApi.FiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Create)]
        public async Task<IActionResult> PostNewFiscalPeriodAsync([FromBody] FiscalPeriodViewModel fiscalPeriod)
        {
            var result = await ValidationResultAsync(fiscalPeriod);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFiscalPeriodAsync(fiscalPeriod);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر برای اصلاح</param>
        /// <param name="fiscalPeriod">اطلاعات اصلاح شده دوره مالی</param>
        /// <returns>اطلاعات دوره مالی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/fperiods/{fpId:min(1)}
        [HttpPut]
        [Route(FiscalPeriodApi.FiscalPeriodUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Edit)]
        public async Task<IActionResult> PutModifiedFiscalPeriodAsync(
            int fpId, [FromBody] FiscalPeriodViewModel fiscalPeriod)
        {
            var result = await ValidationResultAsync(fiscalPeriod, fpId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveFiscalPeriodAsync(fiscalPeriod);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/fperiods/{fpId:min(1)}
        [HttpDelete]
        [Route(FiscalPeriodApi.FiscalPeriodUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingFiscalPeriodAsync(int fpId)
        {
            var result = await ValidateDeleteResultAsync(fpId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteFiscalPeriodAsync(fpId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، دوره مالی مشخص شده با شناسه دیتابیسی را به همراه کلیه اطلاعات وابسته حذف می کند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/fperiods/{fpId:min(1)}/data
        [HttpDelete]
        [Route(FiscalPeriodApi.FiscalPeriodDataUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> DeleteExistingFiscalPeriodWithDataAsync(int fpId)
        {
            string result = await BasicValidateDeleteAsync(fpId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequestResult(result);
            }

            if (!await _repository.CanDeleteFiscalPeriodWithDataAsync(fpId))
            {
                result = _strings[AppStrings.CantDeleteMiddleFiscalPeriods];
                return BadRequestResult(result);
            }

            if (await _repository.HasCommittedVouchersAsync(fpId))
            {
                result = _strings[AppStrings.CantDeleteCommittedVouchers];
                return BadRequestResult(result);
            }

            await _repository.DeleteFiscalPeriodWithDataAsync(fpId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، دوره های مالی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/fperiods
        [HttpPut]
        [Route(FiscalPeriodApi.FiscalPeriodsUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.Delete)]
        public async Task<IActionResult> PutExistingFiscalPeriodsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteFiscalPeriodsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه برای نقش های دارای دسترسی به دوره مالی داده شده را برمی گرداند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <returns>اطلاعات خلاصه برای نقش های دارای دسترسی به دوره مالی</returns>
        // GET: api/fperiods/{fpId:min(1)}/roles
        [HttpGet]
        [Route(FiscalPeriodApi.FiscalPeriodRolesUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.View)]
        public async Task<IActionResult> GetFiscalPeriodRolesAsync(int fpId)
        {
            var roles = await _repository.GetFiscalPeriodRolesAsync(fpId);
            Localize(roles);
            return JsonReadResult(roles);
        }

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به دوره مالی داده شده را در دیتابیس اصلاح می کند
        /// </summary>
        /// <param name="fpId">شناسه دیتابیسی دوره مالی مورد نظر</param>
        /// <param name="fiscalPeriodRoles">اطلاعات جدید برای نقش های دارای دسترسی به دوره مالی</param>
        /// <returns>در صورت بروز خطا، کد وضعیت 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
        // PUT: api/fperiods/{fpId:min(1)}/roles
        [HttpPut]
        [Route(FiscalPeriodApi.FiscalPeriodRolesUrl)]
        [AuthorizeRequest(SecureEntity.FiscalPeriod, (int)FiscalPeriodPermissions.AssignRoles)]
        public async Task<IActionResult> PutModifiedFiscalPeriodRolesAsync(
            int fpId, [FromBody] RelatedItemsViewModel fiscalPeriodRoles)
        {
            var result = BasicValidationResult(fiscalPeriodRoles, fpId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveFiscalPeriodRolesAsync(fiscalPeriodRoles);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سطر مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی دوره مالی مورد نظر برای حذف</param>
        /// <returns>نتیجه به دست آمده از اعتبارسنجی یا رفرنس بدون مقدار در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            var fiscalPeriod = await _repository.GetFiscalPeriodAsync(item);
            string error = await BasicValidateDeleteAsync(item);
            if (!String.IsNullOrEmpty(error))
            {
                return GetGroupActionResult(error, fiscalPeriod);
            }

            bool canDelete = await _repository.CanDeleteFiscalPeriodAsync(item);
            if (!canDelete)
            {
                string name = _strings[EntityNameKey].ToString().ToLower();
                error = _strings.Format(AppStrings.CantDeleteItemWithData, name, fiscalPeriod.Name);
            }

            return GetGroupActionResult(error, fiscalPeriod);
        }

        private async Task<string> BasicValidateDeleteAsync(int item)
        {
            if (item == SecurityContext.User.FiscalPeriodId)
            {
                string name = _strings[EntityNameKey].ToString().ToLower();
                return _strings.Format(AppStrings.CantDeleteCurrentItem, name);
            }

            var fperiod = await _repository.GetFiscalPeriodAsync(item);
            if (fperiod == null)
            {
                return _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.FiscalPeriod, item.ToString());
            }

            return String.Empty;
        }

        private async Task<IActionResult> ValidationResultAsync(FiscalPeriodViewModel fiscalPeriod, int fperiodId = 0)
        {
            var result = BasicValidationResult(fiscalPeriod, fperiodId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (_repository.IsStartDateAfterEndDate(fiscalPeriod))
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidDatePeriod, AppStrings.FiscalPeriod));
            }

            if (await _repository.IsOverlapFiscalPeriodAsync(fiscalPeriod))
            {
                return BadRequestResult(_strings.Format(AppStrings.DateOverlap));
            }

            if (!await _repository.IsProgressiveFiscalPeriodAsync(fiscalPeriod))
            {
                return BadRequestResult(_strings.Format(AppStrings.FiscalPeriodMustBeProgressive));
            }

            return Ok();
        }

        private void Localize(RelatedItemsViewModel roles)
        {
            Array.ForEach(roles.RelatedItems.ToArray(), item => item.Name = _strings[item.Name]);
        }

        private readonly IFiscalPeriodRepository _repository;
    }
}