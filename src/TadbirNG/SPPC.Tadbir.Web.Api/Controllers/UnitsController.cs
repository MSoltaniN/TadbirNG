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

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات واحدها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class UnitsController : ValidatingController<UnitViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات واحدها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public UnitsController(IUnitRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت واحد
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Unit; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده واحدها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده واحدها</returns>
        // GET: api/units
        [HttpGet]
        [Route(UnitApi.UnitsUrl)]
        [AuthorizeRequest(SecureEntity.Unit, (int)UnitPermissions.View)]
        public async Task<IActionResult> GetUnitsAsync()
        {
            var units = await _repository.GetUnitsAsync(GridOptions);
            return JsonListResult(units);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی واحد مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="unitId">شناسه دیتابیسی واحد مورد نظر</param>
        /// <returns>اطلاعات نمایشی واحد مورد نظر</returns>
        // GET: api/units/{unitId:min(1)}
        [HttpGet]
        [Route(UnitApi.UnitUrl)]
        [AuthorizeRequest(SecureEntity.Unit, (int)UnitPermissions.View)]
        public async Task<IActionResult> GetUnitAsync(int unitId)
        {
            var unit = await _repository.GetUnitAsync(unitId);
            return JsonReadResult(unit);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک واحد جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="unit">اطلاعات نمایشی واحد جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای واحد</returns>
        // POST: api/units
        [HttpPost]
        [Route(UnitApi.UnitsUrl)]
        [AuthorizeRequest(SecureEntity.Unit, (int)UnitPermissions.Create)]
        public async Task<IActionResult> PostNewUnitAsync([FromBody] UnitViewModel unit)
        {
            var result = BasicValidationResult(unit);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveUnitAsync(unit);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک واحد موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="unitId">شناسه دیتابیسی واحد اصلاح شده</param>
        /// <param name="unit">اطلاعات نمایشی اصلاح شده برای واحد</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای واحد</returns>
        // PUT: api/units/{unitId:min(1)}
        [HttpPut]
        [Route(UnitApi.UnitUrl)]
        [AuthorizeRequest(SecureEntity.Unit, (int)UnitPermissions.Edit)]
        public async Task<IActionResult> PutModifiedUnitAsync(int unitId, [FromBody] UnitViewModel unit)
        {
            var result = BasicValidationResult(unit, unitId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveUnitAsync(unit);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات واحد مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="unitId">شناسه دیتابیسی واحد مورد نظر برای حذف</param>
        // DELETE: api/units/{unitId:min(1)}
        [HttpDelete]
        [Route(UnitApi.UnitUrl)]
        [AuthorizeRequest(SecureEntity.Unit, (int)UnitPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingUnitAsync(int unitId)
        {
            string message = await ValidateDeleteAsync(unitId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeleteUnitAsync(unitId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، واحدها داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/units
        [HttpPut]
        [Route(UnitApi.UnitsUrl)]
        [AuthorizeRequest(SecureEntity.Unit, (int)UnitPermissions.Delete)]
        public async Task<IActionResult> PutExistingUnitsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteUnitsAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از واحدها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی واحد مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var unit = await _repository.GetUnitAsync(item);
            if (unit == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Unit, item.ToString());
            }

            return message;
        }

        private readonly IUnitRepository _repository;
    }
}