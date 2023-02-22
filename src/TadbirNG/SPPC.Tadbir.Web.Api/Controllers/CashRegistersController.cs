using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات صندوق ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class CashRegistersController : ValidatingController<CashRegisterViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات صندوق ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public CashRegistersController(ICashRegisterRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت صندوق
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.CashRegister; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده صندوق ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده صندوق ها</returns>
        // GET: api/cashregisters
        [HttpGet]
        [Route(CashRegisterApi.CashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.View)]
        public async Task<IActionResult> GetCashRegistersAsync()
        {
            var cashregisters = await _repository.GetCashRegistersAsync(GridOptions);
            return JsonListResult(cashregisters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی صندوق مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashregisterId">شناسه دیتابیسی صندوق مورد نظر</param>
        /// <returns>اطلاعات نمایشی صندوق مورد نظر</returns>
        // GET: api/cashregisters/{cashregisterId:min(1)}
        [HttpGet]
        [Route(CashRegisterApi.CashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.View)]
        public async Task<IActionResult> GetCashRegisterAsync(int cashregisterId)
        {
            var cashregister = await _repository.GetCashRegisterAsync(cashregisterId);
            return JsonReadResult(cashregister);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک صندوق جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="cashregister">اطلاعات نمایشی صندوق جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای صندوق</returns>
        // POST: api/cashregisters
        [HttpPost]
        [Route(CashRegisterApi.CashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Create)]
        public async Task<IActionResult> PostNewCashRegisterAsync([FromBody] CashRegisterViewModel cashregister)
        {
            var result = BasicValidationResult(cashregister);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCashRegisterAsync(cashregister);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک صندوق موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="cashregisterId">شناسه دیتابیسی صندوق اصلاح شده</param>
        /// <param name="cashregister">اطلاعات نمایشی اصلاح شده برای صندوق</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای صندوق</returns>
        // PUT: api/cashregisters/{cashregisterId:min(1)}
        [HttpPut]
        [Route(CashRegisterApi.CashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Edit)]
        public async Task<IActionResult> PutModifiedCashRegisterAsync(int cashregisterId, [FromBody] CashRegisterViewModel cashregister)
        {
            var result = BasicValidationResult(cashregister, cashregisterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCashRegisterAsync(cashregister);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صندوق مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="cashregisterId">شناسه دیتابیسی صندوق مورد نظر برای حذف</param>
        // DELETE: api/cashregisters/{cashregisterId:min(1)}
        [HttpDelete]
        [Route(CashRegisterApi.CashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCashRegisterAsync(int cashregisterId)
        {
            string message = await ValidateDeleteAsync(cashregisterId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeleteCashRegisterAsync(cashregisterId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق ها داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/cashregisters
        [HttpPut]
        [Route(CashRegisterApi.CashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Delete)]
        public async Task<IActionResult> PutExistingCashRegistersAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteCashRegistersAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از صندوق ها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی صندوق مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var cashregister = await _repository.GetCashRegisterAsync(item);
            if (cashregister == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.CashRegister, item.ToString());
            }

            return message;
        }

        private readonly ICashRegisterRepository _repository;
    }
}