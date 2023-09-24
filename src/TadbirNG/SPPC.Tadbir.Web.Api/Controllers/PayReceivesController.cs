using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات فرم های پایه دریافت و پرداخت را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class PayReceivesController : ValidatingController<PayReceiveViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات فرم های دریافت/پرداخت را فراهم می کند</param>
        /// <param name="accountArticleRepository">امکان مدیریت اطلاعات طرف حساب را فراهم می کند</param>
        /// <param name="cashAccountArtricleRepository">امکان مدیریت اطلاعات حساب نقدی را فراهم می کند</param>
        /// <param name="voucherRepository">امکان مدیریت اطلاعات فرم سند را فراهم می‌کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="config">امکان مدیریت اطلاعات تنظیمات برنامه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public PayReceivesController(
            IPayReceiveRepository repository,
            IPayReceiveAccountRepository accountArticleRepository,
            IPayReceiveCashAccountRepository cashAccountArtricleRepository,
            IVoucherRepository voucherRepository,
            IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager,
            IConfigRepository config)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _accountArticleRepository = accountArticleRepository;
            _cashAccountArticleRepository = cashAccountArtricleRepository;
            _voucherRepository = voucherRepository;
            _config = config;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت فرم دریافت/پرداخت
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Receipt; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی فرم پرداخت مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت مورد نظر</returns>
        // GET: api/payments/{paymentId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentAsync(int paymentId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId, (int)PayReceiveType.Payment
                , GridOptions);
            if (GridOptions.Operation == (int)OperationId.Print
                || GridOptions.Operation == (int)OperationId.PrintPreview)
            {
                return Ok();
            }

            return JsonReadResult(payment);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی فرم دریافت مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم دریافت مورد نظر</returns>
        // GET: api/receipts/{receiptId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptAsync(int receiptId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId,
                (int)PayReceiveType.Receipt, GridOptions);
            if (GridOptions.Operation == (int)OperationId.Print
                || GridOptions.Operation == (int)OperationId.PrintPreview)
            {
                return Ok();
            }

            return JsonReadResult(receipt);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک فرم پرداخت جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payment">اطلاعات نمایشی فرم پرداخت جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم پرداخت</returns>
        // POST: api/payments
        [HttpPost]
        [Route(PayReceiveApi.PaymentsUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Create)]
        public async Task<IActionResult> PostNewPaymentAsync([FromBody] PayReceiveViewModel payment)
        {
            var result = await PayReceiveValidationResultAsync(payment, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payment, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک فرم دریافت جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="receipt">اطلاعات نمایشی فرم دریافت جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم دریافت</returns>
        // POST: api/receipts
        [HttpPost]
        [Route(PayReceiveApi.ReceiptsUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Create)]
        public async Task<IActionResult> PostNewReceiptAsync([FromBody] PayReceiveViewModel receipt)
        {
            var result = await PayReceiveValidationResultAsync(receipt, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(receipt, (int)PayReceiveType.Receipt);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک فرم پرداخت موجود را
        /// پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت اصلاح شده</param>
        /// <param name="payment">اطلاعات نمایشی اصلاح شده برای فرم پرداخت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم پرداخت</returns>
        // PUT: api/payments/{paymentId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutModifiedPaymentAsync(int paymentId,
            [FromBody] PayReceiveViewModel payment)
        {
            var result = await PayReceiveValidationResultAsync(payment, AppStrings.Payment, paymentId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payment, (int)PayReceiveType.Payment);
            return await ExecuteAutomaticStepsAsync(outputItem, AppStrings.Payment, OperationId.Edit);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک فرم دریافت موجود را
        /// پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت اصلاح شده</param>
        /// <param name="receipt">اطلاعات نمایشی اصلاح شده برای فرم دریافت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم دریافت</returns>
        // PUT: api/receipts/{receiptId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.ReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutModifiedReceiptAsync(int receiptId,
            [FromBody] PayReceiveViewModel receipt)
        {
            var result = await PayReceiveValidationResultAsync(receipt, AppStrings.Receipt, receiptId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(receipt, (int)PayReceiveType.Receipt);
            return await ExecuteAutomaticStepsAsync(outputItem, AppStrings.Receipt, OperationId.Edit);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم پرداخت مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای حذف</param>
        // DELETE: api/payments/{paymentId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingPaymentAsync(int paymentId)
        {
            string message = await ValidateDeleteAsync(paymentId, AppStrings.Payment);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeletePayReceiveAsync(paymentId, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر برای حذف</param>
        // DELETE: api/receipts/{receiptId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.ReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingReceiptAsync(int receiptId)
        {
            string message = await ValidateDeleteAsync(receiptId, AppStrings.Receipt);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeletePayReceiveAsync(receiptId, (int)PayReceiveType.Receipt);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را در حالت تأییدشده قرار می دهد
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{paymentId:int}/confirm
        [HttpPut]
        [Route(PayReceiveApi.ConfirmPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Confirm)]
        public async Task<IActionResult> PutExistingPaymentAsConfirmed(int paymentId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId);
            if (payment == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Payment,
                    paymentId.ToString()));
            }

            var result = await PayReceiveActionCommonValidationResultAsync(payment, AppStrings.Confirm,
                AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            payment = await _repository.SetPayReceiveConfirmationAsync(paymentId, true);
            return await ExecuteAutomaticStepsAsync(payment, AppStrings.Payment, OperationId.Confirm);
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را در حالت تأییدشده قرار می دهد
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر برای تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/receipts/{receiptId:int}/confirm
        [HttpPut]
        [Route(PayReceiveApi.ConfirmReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Confirm)]
        public async Task<IActionResult> PutExistingReceiptAsConfirmed(int receiptId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId);
            if (receipt == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Receipt,
                    receiptId.ToString()));
            }

            var result = await PayReceiveActionCommonValidationResultAsync(receipt, AppStrings.Confirm,
                AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            receipt = await _repository.SetPayReceiveConfirmationAsync(receiptId, true);
            return await ExecuteAutomaticStepsAsync(receipt, AppStrings.Receipt, OperationId.Confirm);
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را برگشت از تأیید کرده و وضعیتش را در حالت تأییدنشده قرار می دهد
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای برگشت از تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{paymentId:int}/confirm/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoConfirmPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingPaymentAsUnconfirmed(int paymentId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId);
            if (payment == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Payment,
                    paymentId.ToString()));
            }

            var result = PayReceiveUnConfirmValidation(payment, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(payment, AppStrings.UndoConfirm, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveConfirmationAsync(paymentId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را برگشت از تأیید کرده و وضعیتش را در حالت تأییدنشده قرار می دهد
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر برای برگشت از تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/receipts/{receiptId:int}/confirm/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoConfirmReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingReceiptAsUnconfirmed(int receiptId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId);
            if (receipt == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Receipt,
                    receiptId.ToString()));
            }

            var result = PayReceiveUnConfirmValidation(receipt, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(receipt, AppStrings.UndoConfirm, AppStrings.Receipt);

            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveConfirmationAsync(receiptId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را در حالت تصویب شده قرار می دهد
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{paymentId:int}/approve
        [HttpPut]
        [Route(PayReceiveApi.ApprovePaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Approve)]
        public async Task<IActionResult> PutExistingPaymentAsApproved(int paymentId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId);
            if (payment == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Payment,
                    paymentId.ToString()));
            }

            var result = PayReceiveApproveValidation(payment, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(payment, AppStrings.Approve, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            payment = await _repository.SetPayReceiveApprovalAsync(paymentId, true);
            return await ExecuteAutomaticStepsAsync(payment, AppStrings.Payment, OperationId.Approve);
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را در حالت تصویب شده قرار می دهد
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر برای تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/receipts/{receiptId:int}/approve
        [HttpPut]
        [Route(PayReceiveApi.ApproveReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Approve)]
        public async Task<IActionResult> PutExistingReceiptAsApproved(int receiptId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId);
            if (receipt == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Receipt,
                    receiptId.ToString()));
            }

            var result = PayReceiveApproveValidation(receipt, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(receipt, AppStrings.Approve, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            receipt = await _repository.SetPayReceiveApprovalAsync(receiptId, true);
            return await ExecuteAutomaticStepsAsync(receipt, AppStrings.Receipt, OperationId.Approve);
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را برگشت از تصویب کرده و وضعیتش را در حالت تصویب نشده قرار می دهد
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای برگشت از تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{paymentId:int}/approve/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoApprovePaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingPaymentAsUnapproved(int paymentId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId);
            if (payment == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Payment,
                    paymentId.ToString()));
            }

            var result = PayReceiveUnApproveValidation(payment, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(payment, AppStrings.UndoApprove, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(paymentId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت مشخص شده را برگشت از تصویب کرده و وضعیتش را در حالت تصویب نشده قرار می دهد
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر برای برگشت از تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/receipts/{receiptId:int}/approve/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoApproveReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingReceiptAsUnapproved(int receiptId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId);
            if (receipt == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Receipt,
                    receiptId.ToString()));
            }

            var result = PayReceiveUnApproveValidation(receipt, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(receipt, AppStrings.UndoApprove, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(receiptId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم پرداخت مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="paymentNo">شماره فرم پرداخت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت مورد نظر</returns>
        // GET: api/payments/by-no/{paymentNo:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.PaymentByNoUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentByNoAsync(string paymentNo)
        {
            var payReceiveByNo = await _repository.GetPayReceiveByNoAsync(paymentNo, (int)PayReceiveType.Payment);
            return JsonReadResult(payReceiveByNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="receiptNo">شماره فرم دریافت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم دریافت مورد نظر</returns>
        // GET: api/receipts/by-no/{receiptNo:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceiptByNoUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptByNoAsync(string receiptNo)
        {
            var payReceiveByNo = await _repository.GetPayReceiveByNoAsync(receiptNo, (int)PayReceiveType.Receipt);
            return JsonReadResult(payReceiveByNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین فرم پرداخت قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی اولین فرم پرداخت قابل دسترسی</returns>
        // GET: api/payments/first
        [HttpGet]
        [Route(PayReceiveApi.FirstPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Navigate)]
        public async Task<IActionResult> GetFirstPaymentAsync()
        {
            var first = await _repository.GetFirstPayReceiveAsync((int)PayReceiveType.Payment, GridOptions);
            return JsonReadResult(first);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین فرم دریافت قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی اولین فرم دریافت قابل دسترسی</returns>
        // GET: api/receipts/first
        [HttpGet]
        [Route(PayReceiveApi.FirstReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Navigate)]
        public async Task<IActionResult> GetFirstReceiptAsync()
        {
            var first = await _repository.GetFirstPayReceiveAsync((int)PayReceiveType.Receipt, GridOptions);
            return JsonReadResult(first);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین فرم پرداخت قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی آخرین فرم پرداخت قابل دسترسی</returns>
        // GET: api/payments/last
        [HttpGet]
        [Route(PayReceiveApi.LastPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Navigate)]
        public async Task<IActionResult> GetLastPaymentAsync()
        {
            var last = await _repository.GetLastPayReceiveAsync((int)PayReceiveType.Payment, GridOptions);
            return JsonReadResult(last);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین فرم دریافت قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی آخرین فرم دریافت قابل دسترسی</returns>
        // GET: api/receipts/last
        [HttpGet]
        [Route(PayReceiveApi.LastReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Navigate)]
        public async Task<IActionResult> GetLastReceiptAsync()
        {
            var last = await _repository.GetLastPayReceiveAsync((int)PayReceiveType.Receipt, GridOptions);
            return JsonReadResult(last);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم پرداخت پیش از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="paymentNo">شماره فرم پرداخت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت قابل دسترسی قبلی</returns>
        // GET: api/payments/{paymentNo:min(1)}/previous
        [HttpGet]
        [Route(PayReceiveApi.PreviousPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousPaymentAsync(string paymentNo)
        {
            var previous = await _repository.GetPreviousPayReceiveAsync(paymentNo,
                (int)PayReceiveType.Payment, GridOptions);
            return JsonReadResult(previous);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت پیش از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="receiptNo">شماره فرم دریافت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم دریافت قابل دسترسی قبلی</returns>
        // GET: api/receipts/{receiptNo:min(1)}/previous
        [HttpGet]
        [Route(PayReceiveApi.PreviousReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousReceiptAsync(string receiptNo)
        {
            var previous = await _repository.GetPreviousPayReceiveAsync(receiptNo,
                (int)PayReceiveType.Receipt, GridOptions);
            return JsonReadResult(previous);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم پرداخت بعد از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="paymentNo">شماره فرم پرداخت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت قابل دسترسی بعدی</returns>
        // GET: api/payments/{paymentNo:min(1)}/next
        [HttpGet]
        [Route(PayReceiveApi.NextPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Navigate)]
        public async Task<IActionResult> GetNextPaymentAsync(string paymentNo)
        {
            var next = await _repository.GetNextPayReceiveAsync(paymentNo,
                (int)PayReceiveType.Payment, GridOptions);
            return JsonReadResult(next);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت بعد از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="receiptNo">شماره فرم دریافت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم دریافت قابل دسترسی بعدی</returns>
        // GET: api/receipts/{receiptNo:min(1)}/next
        [HttpGet]
        [Route(PayReceiveApi.NextReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Navigate)]
        public async Task<IActionResult> GetNextReceiptAsync(string receiptNo)
        {
            var next = await _repository.GetNextPayReceiveAsync(receiptNo,
                (int)PayReceiveType.Receipt, GridOptions);
            return JsonReadResult(next);
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت جدیدی با مقادیر پیشنهادی در دیتابیس ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی فرم پرداخت جدید با مقادیر پیشنهادی</returns>
        // GET: api/payments/new
        [HttpGet]
        [Route(PayReceiveApi.NewPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Create)]
        public async Task<IActionResult> GetNewPaymentAsync()
        {
            var payment = await _repository.GetNewPayReceiveAsync((int)PayReceiveType.Payment);
            return Json(payment);
        }

        /// <summary>
        /// به روش آسنکرون، فرم دریافت جدیدی با مقادیر پیشنهادی در دیتابیس ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی فرم دریافت جدید با مقادیر پیشنهادی</returns>
        // GET: api/receipts/new
        [HttpGet]
        [Route(PayReceiveApi.NewReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Create)]
        public async Task<IActionResult> GetNewReceiptAsync()
        {
            var receipt = await _repository.GetNewPayReceiveAsync((int)PayReceiveType.Receipt);
            return Json(receipt);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل‌های فرم پرداخت را ثبت مالی می‌کند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای ثبت مالی</param>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر برای ثبت مالی</param>
        /// <returns>اطلاعات نمایشی سند ثبت شده مرتبط با فرم پرداخت</returns>
        // Post: api/payments/{paymentId:min(1)}/register/vouchers/{voucherId:int}
        [HttpPost]
        [Route(PayReceiveApi.RegisterPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Register)]
        public async Task<IActionResult> PostNewRegisterPaymentArticlesAsync(int paymentId, int voucherId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId);
            if (payment == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Payment,
                    paymentId.ToString()));
            }

            var result = await PayReceiveRegisterValidationAsync(payment, voucherId, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(payment, AppStrings.Register, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.RegisterAsync(payment.Id, voucherId);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل‌های فرم دریافت را ثبت مالی می‌کند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم پرداخت مورد نظر برای ثبت مالی</param>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر برای ثبت مالی</param>
        /// <returns>اطلاعات نمایشی سند ثبت شده مرتبط با فرم دریافت</returns>
        // Post: api/receipts/{receiptId:min(1)}/register/vouchers/{voucherId:int}
        [HttpPost]
        [Route(PayReceiveApi.RegisterReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Register)]
        public async Task<IActionResult> PostNewRegisterReceiptArticlesAsync(int receiptId, int voucherId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId);
            if (receipt == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Receipt,
                    receiptId.ToString()));
            }

            var result = await PayReceiveRegisterValidationAsync(receipt, voucherId, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(receipt, AppStrings.Register, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.RegisterAsync(receipt.Id, voucherId);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل‌های مالی فرم پرداخت را بر اساس تنظیمات خودکار ایجاد می‌کند 
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای ثبت مالی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // Post: api/payments/{paymentId:min(1)}/register/automatic
        [HttpPost]
        [Route(PayReceiveApi.AutomaticeRegisterPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Register)]
        public async Task<IActionResult> PostNewAutomaticeRegisterPaymentArticlesAsync(int paymentId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId);
            if (payment == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Payment,
                    paymentId.ToString()));
            }

            var result = await PayReceiveRegisterValidationAsync(payment, 0, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(payment, AppStrings.Register, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var config = await _config.GetConfigByTypeAsync<PaymentConfig>();
            var outputItem = await RegisterAsync(payment, config);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل‌های مالی را بر اساس تنظیمات خودکار ایجاد می کند.
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر برای ثبت مالی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // Post: api/receipts/{receiptId:min(1)}/register/automatic
        [HttpPost]
        [Route(PayReceiveApi.AutomaticeRegisterReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Register)]
        public async Task<IActionResult> PostNewAutomaticeRegisterReceiptArticlesAsync(int receiptId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId);
            if (receipt == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Receipt,
                    receiptId.ToString()));
            }

            var result = await PayReceiveRegisterValidationAsync(receipt, 0, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(receipt, AppStrings.Register, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var config = await _config.GetConfigByTypeAsync<PaymentConfig>();
            var outputItem = await RegisterAsync(receipt, config);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل‌های مالی فرم پرداخت را حذف می‌کند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر برای برگشت از ثبت مالی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // Delete: api/payments/{paymentId:min(1)}/register/undo
        [HttpDelete]
        [Route(PayReceiveApi.UndoRegisterPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.UndoRegister)]
        public async Task<IActionResult> DeleteRegisteredPaymentArticlesAsync(int paymentId)
        {
            var payment = await _repository.GetPayReceiveAsync(paymentId);
            if (payment == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Payment,
                    paymentId.ToString()));
            }

            var result = await PayReceiveUnRegisterValidationAsync(payment, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(payment, AppStrings.UndoRegister, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.UndoRegisterAsync(paymentId, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل‌های مالی فرم دریافت را حذف می‌کند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر برای برگشت از ثبت مالی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // Delete: api/receipts/{receiptId:min(1)}/register/undo
        [HttpDelete]
        [Route(PayReceiveApi.UndoRegisterReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.UndoRegister)]
        public async Task<IActionResult> DeleteRegisteredReceiptArticlesAsync(int receiptId)
        {
            var receipt = await _repository.GetPayReceiveAsync(receiptId);
            if (receipt == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Receipt,
                    receiptId.ToString()));
            }

            var result = await PayReceiveUnRegisterValidationAsync(receipt, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await PayReceiveActionCommonValidationResultAsync(receipt, AppStrings.UndoRegister, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.UndoRegisterAsync(receiptId, (int)PayReceiveType.Receipt);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، سندی که فرم دریافت/پرداخت ورودی روی آن ثبت مالی شده را برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت/پرداخت مورد نظر</param>
        /// <returns>سند مرتبط با فرم دریافت/پرداخت</returns>        
        // GET: api/pay-receives/{payReceiveId:min(1)}/voucher
        [HttpGet]
        [Route(PayReceiveApi.RelatedVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherOfRegisterAsync(int payReceiveId)
        {
            var voucher = await _repository.GetVoucherOfRegisterAsync(payReceiveId);
            return JsonReadResult(voucher);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از فرم های دریافت یا پرداخت اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی فرم دریافت یا پرداخت مورد نظر برای حذف</param>
        /// <param name="entityNameKey">عنوان کلیدی انتیتی برای فرم های با چند حالت انتیتی</param> 
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<string> ValidateDeleteAsync(int item, string entityNameKey)
        {
            string message = String.Empty;
            var payReceive = await _repository.GetPayReceiveAsync(item);
            if (payReceive == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, entityNameKey, item.ToString());
            }
            else
            {
                var result = BranchValidationResult(payReceive);
                if (result is BadRequestObjectResult errorResult)
                {
                    message = errorResult.Value.ToString();
                }
                else if (payReceive.IsRegistered)
                {
                    message = _strings.Format(AppStrings.RegisteredFormBlocked, entityNameKey);
                }
                else if (payReceive.IsConfirmed || payReceive.IsApproved)
                {
                    message = _strings.Format(AppStrings.CantDeleteEntity, entityNameKey);
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

                var currPayReceive = await _repository.GetPayReceiveAsync(payReceiveId);

                if (currPayReceive.IsRegistered)
                {
                    return BadRequestResult(_strings.Format(AppStrings.RegisteredFormBlocked, entityNameKey));
                }

                if (currPayReceive.IsConfirmed || currPayReceive.IsApproved)
                {
                    return BadRequestResult(_strings.Format(AppStrings.CantSaveEntity, entityNameKey));
                }
            }

            var textNo = payReceive.TextNo.Trim();
            if (!Int64.TryParse(textNo, out long numberValue))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.InvalidLetterForStringNumber, AppStrings.Number));
            }

            if (numberValue <= 0)
            {
                var minNumberString = "1";
                return BadRequestResult(_strings.Format(
                   AppStrings.InvalidStringNumber, minNumberString, AppStrings.Number));
            }

            var type = entityNameKey == AppStrings.Payment
                ? (int)PayReceiveType.Payment
                : (int)PayReceiveType.Receipt;
            if (await _repository.IsDuplicateTextNoAsync(payReceive, type))
            {
                string fieldTitle = entityNameKey == AppStrings.Payment
                    ? AppStrings.PaymentNo
                    : AppStrings.ReceiptNo;

                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, fieldTitle));
            }

            if (payReceive.CurrencyId > 0 && payReceive.CurrencyRate <= Decimal.Zero)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.ZeroOrNegativeAmountNotAllowed, AppStrings.CurrencyRate));
            }

            return Ok();
        }

        private async Task<IActionResult> PayReceiveActionCommonValidationResultAsync(PayReceiveViewModel payReceive, string action,
            string entityNameKey)
        {
            if (payReceive.IsRegistered && action != AppStrings.UndoRegister)
            {
                return BadRequestResult(_strings.Format(AppStrings.RegisteredFormBlocked, entityNameKey));
            }

            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if ((action == AppStrings.Confirm && payReceive.IsConfirmed)
                || (action == AppStrings.Approve && payReceive.IsApproved)
                || (action == AppStrings.Register && payReceive.IsRegistered))
            {
                return BadRequestResult(_strings.Format(AppStrings.RepeatedEntityActionMessage, action,
                    entityNameKey));
            }

            if (action == AppStrings.Confirm || action == AppStrings.Approve || action == AppStrings.Register)
            {
                if (!await _repository.HasAccountArticleAsync(payReceive.Id))
                {
                    return BadRequestResult(_strings.Format(AppStrings.InvalidEmptyArticleAction, action,
                            entityNameKey, AppStrings.PayReceiveAccount));
                }

                if (!await _repository.HasCashAccountArticleAsync(payReceive.Id))
                {
                    return BadRequestResult(_strings.Format(AppStrings.InvalidEmptyArticleAction, action,
                            entityNameKey, AppStrings.PayReceiveCashAccount));
                }

                if (await _accountArticleRepository.HasAccountArticleInvalidRowsAsync(payReceive.Id))
                {
                    return BadRequestResult(_strings.Format(
                        AppStrings.CantActionWithInvalidRows, action, AppStrings.PayReceiveAccount));
                }

                if (await _cashAccountArticleRepository.HasCashAccountArticleInvalidRowsAsync(payReceive.Id))
                {
                    return BadRequestResult(_strings.Format(
                        AppStrings.CantActionWithInvalidRows, action, AppStrings.PayReceiveCashAccount));
                }

                if (payReceive.AccountAmountsSum != payReceive.CashAmountsSum)
                {
                    return BadRequestResult(_strings.Format(AppStrings.CantActionUnbalancedForm,
                        AppStrings.PayReceiveAccount, AppStrings.PayReceiveCashAccount, action, entityNameKey));
                }
            }

            return Ok();
        }

        private IActionResult PayReceiveUnConfirmValidation(PayReceiveViewModel payReceive, string entityNameKey)
        {
            if (!payReceive.IsConfirmed)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, AppStrings.UndoConfirm,
                    entityNameKey, AppStrings.Confirm));
            }

            if (payReceive.IsApproved)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, AppStrings.UndoConfirm,
                    entityNameKey, AppStrings.UndoApprove));
            }

            return Ok();
        }

        private IActionResult PayReceiveApproveValidation(PayReceiveViewModel payReceive, string entityNameKey)
        {
            if (!payReceive.IsConfirmed)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, AppStrings.Approve,
                    entityNameKey, AppStrings.Confirm));
            }

            return Ok();
        }

        private IActionResult PayReceiveUnApproveValidation(PayReceiveViewModel payReceive, string entityNameKey)
        {
            if (!payReceive.IsApproved)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, AppStrings.UndoApprove,
                    entityNameKey, AppStrings.Approve));
            }

            return Ok();
        }

        private async Task<IActionResult> PayReceiveRegisterValidationAsync(
            PayReceiveViewModel payReceive, int voucherId, string entityNameKey)
        {
            if (!payReceive.IsConfirmed || !payReceive.IsApproved)
            {
                return BadRequestResult(_strings.Format(AppStrings.ImpossibleRegisterBeforeConfirmAndApprove, entityNameKey));
            }

            if (voucherId > 0)
            {
                if (!await _repository.IsValidVoucherForRegisterAsync(voucherId, payReceive.Date))
                {
                    return BadRequestResult(_strings.Format(AppStrings.NotValidVoucherForRegister));
                }
            }

            return Ok();
        }

        private async Task<IActionResult> PayReceiveUnRegisterValidationAsync(
            PayReceiveViewModel payReceive, string entityNameKey)
        {
            if (!payReceive.IsRegistered)
            {
                return BadRequestResult(_strings.Format(AppStrings.InvalidEntityActionMessage, AppStrings.UndoRegister,
                    entityNameKey, AppStrings.Register));
            }

            var voucher = await _repository.GetVoucherOfRegisterAsync(payReceive.Id);
            if (voucher.StatusId != (int)DocumentStatusId.NotChecked)
            {
                return BadRequestResult(_strings.Format(AppStrings.UndoRegisterEnabledForNotCheckedVouchers));
            }

            return Ok();
        }
        private async Task<IActionResult> ExecuteAutomaticStepsAsync(PayReceiveViewModel payReceive, string entityNameKey, OperationId operationId)
        {
            OperationalFormsConfig config = entityNameKey == AppStrings.Receipt
                ? await _config.GetConfigByTypeAsync<ReceiptConfig>()
                : await _config.GetConfigByTypeAsync<PaymentConfig>();
            if ((operationId == OperationId.Edit && config.RegisterFlowConfig.ConfirmAfterSave)
                || (operationId == OperationId.Confirm && config.RegisterFlowConfig.ApproveAfterConfirm)
                || (operationId == OperationId.Approve && config.RegisterFlowConfig.RegisterAfterApprove))
            {
                if (operationId == OperationId.Edit)
                {
                    var result = await PayReceiveActionCommonValidationResultAsync(payReceive, AppStrings.Confirm,
                        entityNameKey);
                    if (result is BadRequestObjectResult)
                    {
                        return result;
                    }

                    int payReceiveId = payReceive.Id;
                    payReceive = await _repository.SetPayReceiveConfirmationAsync(payReceiveId, true);
                }

                if (config.RegisterFlowConfig.ApproveAfterConfirm
                    && (operationId == OperationId.Edit
                    || operationId == OperationId.Confirm))
                {
                    var result = PayReceiveApproveValidation(payReceive, entityNameKey);
                    if (result is BadRequestObjectResult)
                    {
                        return result;
                    }

                    payReceive = await _repository.SetPayReceiveApprovalAsync(payReceive.Id, true);
                }

                if (config.RegisterFlowConfig.RegisterAfterApprove 
                    && (operationId == OperationId.Confirm 
                    || operationId == OperationId.Approve
                    || (operationId == OperationId.Edit 
                    && config.RegisterFlowConfig.ApproveAfterConfirm)))
                {
                    int voucherId = 0;
                    var result = await PayReceiveRegisterValidationAsync(payReceive, voucherId, entityNameKey);
                    if (result is BadRequestObjectResult)
                    {
                        return result;
                    }

                    if (config.RegisterConfig.RegisterWithLastValidVoucher || config.RegisterConfig.RegisterWithNewCreatedVoucher)
                    {
                        var outputItem = await RegisterAsync(payReceive, config);
                        return StatusCode(StatusCodes.Status201Created, outputItem);
                    }
                }
            }

            if(operationId == OperationId.Edit && !config.RegisterFlowConfig.ConfirmAfterSave)
            {
                return OkReadResult(payReceive);
            }

            return Ok();
        }

        private async Task<VoucherViewModel> RegisterAsync(PayReceiveViewModel payReceive, OperationalFormsConfig config)
        {
            VoucherViewModel outputItem = null;
            if (config.RegisterConfig.RegisterWithLastValidVoucher)
            {
                var voucherId = await _repository.GetLastVoucherforRegisterAsync(payReceive.Date);
                if(voucherId > 0)
                {
                    outputItem = await _repository.RegisterAsync(payReceive.Id, voucherId);
                }
            }
            else if (config.RegisterConfig.RegisterWithNewCreatedVoucher)
            {
                outputItem = await _repository.RegisterAsync(payReceive.Id);
                if (config.RegisterConfig.CheckedVoucher)
                {
                    outputItem = await _voucherRepository.SetVoucherStatusAsync(outputItem.Id, DocumentStatusId.Checked);
                }
            }

            return outputItem;
        }

        private readonly IPayReceiveRepository _repository;
        private readonly IPayReceiveAccountRepository _accountArticleRepository;
        private readonly IPayReceiveCashAccountRepository _cashAccountArticleRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IConfigRepository _config;
    }
}
