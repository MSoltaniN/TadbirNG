using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
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
            get { return String.Empty; }
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
        [Route(PayReceiveApi.ReceivalUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.View)]
        public async Task<IActionResult> GetReceivalAsync(int payReceiveId)
        {
            var receival = await _repository.GetPayReceiveAsync(payReceiveId);
            return JsonReadResult(receival);
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
            var result = await PayReceiveValidationResultAsync(payReceive,AppStrings.Payment);
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
        // POST: api/receivals
        [HttpPost]
        [Route(PayReceiveApi.ReceivalsUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.Create)]
        public async Task<IActionResult> PostNewReceivalAsync([FromBody] PayReceiveViewModel payReceive)
        {
            var result = await PayReceiveValidationResultAsync(payReceive, AppStrings.Receival);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payReceive);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک فرم پرداخت موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
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
            var result = await PayReceiveValidationResultAsync(payReceive, AppStrings.Payment, payReceiveId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payReceive);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک فرم دریافت موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت اصلاح شده</param>
        /// <param name="payReceive">اطلاعات نمایشی اصلاح شده برای فرم دریافت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم دریافت</returns>
        // PUT: api/receivals/{payReceiveId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.ReceivalUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.Edit)]
        public async Task<IActionResult> PutModifiedReceivalAsync(int payReceiveId, [FromBody] PayReceiveViewModel payReceive)
        {
            var result = await PayReceiveValidationResultAsync(payReceive, AppStrings.Receival, payReceiveId);
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

            await _repository.DeletePayReceiveAsync(payReceiveId, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر برای حذف</param>
        // DELETE: api/payments/{payReceiveId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.ReceivalUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingReceivalAsync(int payReceiveId)
        {
            string message = await ValidateDeleteAsync(payReceiveId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeletePayReceiveAsync(payReceiveId, (int)PayReceiveType.Receival);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را در حالت تأییدشده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر برای تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{payReceiveId:int}/confirm
        [HttpPut]
        [Route(PayReceiveApi.ConfirmPaymentUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.Confirm)]
        public async Task<IActionResult> PutExistingPaymentAsConfirmed(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.Confirm,
                AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }
            await _repository.SetPayReceiveConfirmationAsync(payReceiveId, true);

            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را در حالت تأییدشده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر برای تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/reseivals/{payReceiveId:int}/confirm
        [HttpPut]
        [Route(PayReceiveApi.ConfirmReceivalUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.Confirm)]
        public async Task<IActionResult> PutExistingReceivalAsConfirmed(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.Confirm,
                AppStrings.Receival);
            if (result is BadRequestObjectResult)
            {
                return result;
            }
            await _repository.SetPayReceiveConfirmationAsync(payReceiveId, true);

            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخع مشخص شده را برگشت از تأیید کرده و وضعیتش را در حالت تأییدنشده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر برای برگشت از تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{payReceiveId:int}/confirm/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoConfirmPaymentUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingPaymentAsUnconfirmed(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.UndoConfirm,
                AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveConfirmationAsync(payReceiveId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را برگشت از تأیید کرده و وضعیتش را در حالت تأییدنشده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر برای برگشت از تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/receivals/{payReceiveId:int}/confirm/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoConfirmReceivalUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingReceivalAsUnconfirmed(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.UndoConfirm,
                AppStrings.Receival);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveConfirmationAsync(payReceiveId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را در حالت تصویب شده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر برای تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{payReceiveId:int}/approve
        [HttpPut]
        [Route(PayReceiveApi.ApprovePaymentUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.Approve)]
        public async Task<IActionResult> PutExistingPaymentAsApproved(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.Approve,
                AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(payReceiveId, true);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را در حالت تصویب شده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر برای تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/receivals/{payReceiveId:int}/approve
        [HttpPut]
        [Route(PayReceiveApi.ApproveReceivalUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.Approve)]
        public async Task<IActionResult> PutExistingReceivalAsApproved(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.Approve,
                AppStrings.Receival);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(payReceiveId, true);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را برگشت از تصویب کرده و وضعیتش را در حالت تصویب نشده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی سند مورد نظر برای برگشت از تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{payReceiveId:int}/approve/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoApprovePaymentUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingPaymentAsUnapproved(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.UndoApprove,
                AppStrings.Payments);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(payReceiveId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را برگشت از تصویب کرده و وضعیتش را در حالت تصویب نشده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر برای برگشت از تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{payReceiveId:int}/approve/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoApproveReceivalUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingReceivalAsUnapproved(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.UndoApprove,
                AppStrings.Receivals);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(payReceiveId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم پرداخت مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="payReceiveNo">شماره فرم پرداخت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت مورد نظر</returns>
        // GET: api/payments/by-no/{payReceiveNo:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.PaymentByNoUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentByNoAsync(string payReceiveNo)
        {
            var payReceiveByNo = await _repository.GetPayReceiveNoAsync(payReceiveNo, PayReceiveType.Payment);
            return JsonReadResult(payReceiveByNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="payReceiveNo">شماره فرم دریافت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم دریافت مورد نظر</returns>
        // GET: api/receivals/by-no/{payReceiveNo:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceivalByNoUrl)]
        [AuthorizeRequest(SecureEntity.PayReceive, (int)ReceivalPermissions.View)]
        public async Task<IActionResult> GetReceivalByNoAsync(string payReceiveNo)
        {
            var payReceiveByNo = await _repository.GetPayReceiveNoAsync(payReceiveNo, PayReceiveType.Receival);
            return JsonReadResult(payReceiveByNo);
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
            var payReceive = await _repository.GetPayReceiveAsync(item);
            if (payReceive == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.PayReceive, item.ToString());
            }
            else
            {
                var result = BranchValidationResult(payReceive);
                if (result is BadRequestObjectResult errorResult)
                {
                    message = errorResult.Value.ToString();
                }
            }

            return message;
        }

        private async Task<IActionResult> PayReceiveValidationResultAsync(PayReceiveViewModel payReceive,
            string entityNameKey, int payReceiveId = 0)
        {
            var result = BasicValidationResult(payReceive, payReceiveId, entityNameKey);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (payReceiveId > 0)
            {
                result = BranchValidationResult(payReceive);
                if (result is BadRequestObjectResult)
                {
                    return result;
                }

                if (payReceive.IsConfirmed)
                {
                    return BadRequestResult(_strings.Format(AppStrings.CantSaveAsDraft, entityNameKey));
                }
            }

            if (await _repository.IsDuplicatePayReceiveNo(payReceive))
            {
                string fieldTitle = payReceive.Type == (int)PayReceiveType.Payment
                    ? AppStrings.PaymentNo
                    : AppStrings.ReceivalNo;

                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, fieldTitle));
            }

            return Ok();
        }

        private async Task<IActionResult> PayReceiveActionValidationResultAsync(int payReceiveId, string action,
            string entityNameKey)
        {
            var payReceive = await _repository.GetPayReceiveAsync(payReceiveId);
            if (payReceive == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, action,
                    payReceiveId.ToString()));
            }

            if ((action == AppStrings.Confirm && payReceive.IsConfirmed)
                || (action == AppStrings.Approve && payReceive.IsApproved))
            {
                return BadRequestResult(_strings.Format(AppStrings.RepeatedEntityActionMessage, action,
                    entityNameKey));
            }

            if (action == AppStrings.UndoConfirm)
            {
                if (!payReceive.IsConfirmed)
                {
                    return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, action,
                        entityNameKey, AppStrings.Confirm));
                }

                if (payReceive.IsApproved)
                {
                    return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, action,
                        entityNameKey, AppStrings.UndoApprove));
                }
            }

            if (action == AppStrings.Approve && !payReceive.IsConfirmed)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, action,
                       entityNameKey, AppStrings.Confirm));
            }

            if (action == AppStrings.UndoApprove && !payReceive.IsApproved)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, action,
                       entityNameKey, AppStrings.Approve));
            }

            return Ok();
        }

        private readonly IPayReceiveRepository _repository;
    }
}