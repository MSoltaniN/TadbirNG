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
    /// عملیات سرویس وب برای مدیریت اطلاعات دریافت ها و پرداخت ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class PayReceivesController : ValidatingController<PayReceiveViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات دریافت ها و پرداخت ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public PayReceivesController(IPayReceiveRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت دریافت و پرداخت
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Payment; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی فرم پرداخت مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت مورد نظر</returns>
        // GET: api/payments/{payReceiveId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentAsync(int payReceiveId)
        {
            var payment = await _repository.GetPayReceiveAsync(payReceiveId);
            return JsonReadResult(payment);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی فرم دریافت مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم دریافت مورد نظر</returns>
        // GET: api/payments/{payReceiveId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceiveUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivePermissions.View)]
        public async Task<IActionResult> GetReceiveAsync(int payReceiveId)
        {
            var receive = await _repository.GetPayReceiveAsync(payReceiveId);
            return JsonReadResult(receive);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک فرم پرداخت جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceive">اطلاعات نمایشی فرم پرداخت جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم پرداخت</returns>
        // POST: api/payments
        [HttpPost]
        [Route(PayReceiveApi.PaymentsUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.Create)]
        public async Task<IActionResult> PostNewPaymentAsync([FromBody] PayReceiveViewModel payReceive)
        {
            var result = BasicValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }
            
            var outputItem = await _repository.SavePayReceiveAsync(payReceive);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک فرم دریافت جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceive">اطلاعات نمایشی فرم دریافت جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم دریافت</returns>
        // POST: api/receives
        [HttpPost]
        [Route(PayReceiveApi.ReceivesUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivePermissions.Create)]
        public async Task<IActionResult> PostNewReceiveAsync([FromBody] PayReceiveViewModel payReceive)
        {
            var result = BasicValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payReceive);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک یک فرم پرداخت موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت اصلاح شده</param>
        /// <param name="payReceive">اطلاعات نمایشی اصلاح شده برای فرم پرداخت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم پرداخت</returns>
        // PUT: api/payments/{payReceiveId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutModifiedPaymentAsync(int payReceiveId, [FromBody] PayReceiveViewModel payReceive)
        {
            var result = BasicValidationResult(payReceive, payReceiveId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payReceive);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک یک فرم دریافت موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت اصلاح شده</param>
        /// <param name="payReceive">اطلاعات نمایشی اصلاح شده برای فرم دریافت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم دریافت</returns>
        // PUT: api/receives/{payReceiveId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.ReceiveUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivePermissions.Edit)]
        public async Task<IActionResult> PutModifiedReceiveAsync(int payReceiveId, [FromBody] PayReceiveViewModel payReceive)
        {
            var result = BasicValidationResult(payReceive, payReceiveId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payReceive);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم پرداخت مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر برای حذف</param>
        // DELETE: api/payments/{payReceiveId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingPaymentAsync(int payReceiveId)
        {
            string message = await ValidateDeleteAsync(payReceiveId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeletePayReceiveAsync(payReceiveId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر برای حذف</param>
        // DELETE: api/payments/{payReceiveId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.ReceiveUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivePermissions.Delete)]
        public async Task<IActionResult> DeleteExistingReceiveAsync(int payReceiveId)
        {
            string message = await ValidateDeleteAsync(payReceiveId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeletePayReceiveAsync(payReceiveId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، فرم های پرداخت داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/payments
        [HttpPut]
        [Route(PayReceiveApi.PaymentsUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.Delete)]
        public async Task<IActionResult> PutExistingPaymentsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeletePayReceivesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، فرم های دریافت داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/receives
        [HttpPut]
        [Route(PayReceiveApi.ReceivesUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivePermissions.Delete)]
        public async Task<IActionResult> PutExistingReceivesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeletePayReceivesAsync);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از فرم های دریافت یا پرداخت اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی فرم دریافت یا پرداخت مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var payreceive = await _repository.GetPayReceiveAsync(item);
            if (payreceive == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.PayReceive, item.ToString());
            }

            return message;
        }

        private readonly IPayReceiveRepository _repository;
    }
}