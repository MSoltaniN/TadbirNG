using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.CashFlow;
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
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public PayReceivesController(
            IPayReceiveRepository repository,
            IPayReceiveAccountRepository accountArticleRepository,
            IPayReceiveCashAccountRepository cashAccountArtricleRepository,
            IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _accountArticleRepository = accountArticleRepository;
            _cashAccountArticleRepository = cashAccountArtricleRepository;
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
            if(GridOptions.Operation == (int)OperationId.Print 
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

            var outputItem = await _repository.SavePayReceiveAsync(payment);
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

            var outputItem = await _repository.SavePayReceiveAsync(receipt);
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

            var outputItem = await _repository.SavePayReceiveAsync(payment);
            return OkReadResult(outputItem);
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

            var outputItem = await _repository.SavePayReceiveAsync(receipt);
            return OkReadResult(outputItem);
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
            var result = await PayReceiveActionValidationResultAsync(paymentId, AppStrings.Confirm,
                AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveConfirmationAsync(paymentId, true);
            return Ok();
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
            var result = await PayReceiveActionValidationResultAsync(receiptId, AppStrings.Confirm,
                AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveConfirmationAsync(receiptId, true);
            return Ok();
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
            var result = await PayReceiveActionValidationResultAsync(paymentId, AppStrings.UndoConfirm,
                AppStrings.Payment);
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
            var result = await PayReceiveActionValidationResultAsync(receiptId, AppStrings.UndoConfirm,
                AppStrings.Receipt);
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
            var result = await PayReceiveActionValidationResultAsync(paymentId, AppStrings.Approve,
                AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(paymentId, true);
            return Ok();
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
            var result = await PayReceiveActionValidationResultAsync(receiptId, AppStrings.Approve,
                AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveApprovalAsync(receiptId, true);
            return Ok();
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
            var result = await PayReceiveActionValidationResultAsync(paymentId, AppStrings.UndoApprove,
                AppStrings.Payment);
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
            var result = await PayReceiveActionValidationResultAsync(receiptId, AppStrings.UndoApprove,
                AppStrings.Receipt);
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
                    return BadRequestResult(_strings.Format(AppStrings.CantSaveEntity, entityNameKey));
                }
            }

            var payReceiveNo = payReceive.PayReceiveNo.Trim();
            if (!Int64.TryParse(payReceiveNo, out long numberValue))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.InvalidLetterForStringNumber, AppStrings.Number));
            }

            if(numberValue <= 0)
            {
                var minNumberString = "1";
                return BadRequestResult(_strings.Format(
                   AppStrings.InvalidStringNumber, minNumberString, AppStrings.Number));
            }

            if (await _repository.IsDuplicatePayReceiveNo(payReceive))
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

        private async Task<IActionResult> PayReceiveActionValidationResultAsync(int payReceiveId, string action,
            string entityNameKey)
        {
            var payReceive = await _repository.GetPayReceiveAsync(payReceiveId);
            if (payReceive == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, entityNameKey,
                    payReceiveId.ToString()));
            }

            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (action == AppStrings.Confirm) 
            {
                if(!await _repository.HasAccountArticleAsync(payReceiveId))
                {
                    return BadRequestResult(_strings.Format(AppStrings.InvalidEmptyArticleAction, action,
                            entityNameKey, AppStrings.PayReceiveAccount));
                }

                if (!await _repository.HasCashAccountArticleAsync(payReceiveId))
                {
                    return BadRequestResult(_strings.Format(AppStrings.InvalidEmptyArticleAction, action,
                            entityNameKey, AppStrings.PayReceiveCashAccount));
                }

                if (await _accountArticleRepository.HasAccountArticleInvalidRowsAsync(payReceiveId))
                {
                    return BadRequestResult(_strings.Format(
                        AppStrings.CantConfirmWithInvalidRows, AppStrings.PayReceiveAccount));
                }

                if (await _cashAccountArticleRepository.HasCashAccountArticleInvalidRowsAsync(payReceiveId))
                {
                    return BadRequestResult(_strings.Format(
                        AppStrings.CantConfirmWithInvalidRows, AppStrings.PayReceiveCashAccount));
                }

                if(await _repository.IsUnbalancedPayReceive(payReceiveId))
                {
                    return BadRequestResult(_strings.Format(AppStrings.CantConfirmUnbalancedForm, 
                        AppStrings.PayReceiveAccount, AppStrings.PayReceiveCashAccount, entityNameKey));
                }              
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
        private readonly IPayReceiveAccountRepository _accountArticleRepository;
        private readonly IPayReceiveCashAccountRepository _cashAccountArticleRepository;
    }
}