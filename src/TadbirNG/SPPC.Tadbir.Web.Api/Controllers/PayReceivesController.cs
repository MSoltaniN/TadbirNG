using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Localization;
using Microsoft.VisualBasic;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
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
using Stimulsoft.Blockly.Blocks.Lists;
using Stimulsoft.Controls;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات دریافتی ها و پرداختی ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class PayReceivesController : ValidatingController<PayReceiveViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات فرم های دریافت/پرداخت را فراهم می کند</param>
        /// <param name="articleAccountRepository">امکان مدیریت اطلاعات طرف حساب را فراهم می کند</param>
        /// <param name="relationRepository">امکان خواندن ارتباطات موجود در  بردار حساب را فراهم می کند</param>  
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public PayReceivesController(
            IPayReceiveRepository repository,
            IPayReceiveAccountRepository articleAccountRepository,
            IRelationRepository relationRepository,
            IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _accountArticleRepository = articleAccountRepository;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت دریافت و پرداخت
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Receipt; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی فرم پرداخت مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت مورد نظر</returns>
        // GET: api/payments/{payReceiveId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentAsync(int payReceiveId)
        {
            var payment = await _repository.GetPayReceiveAsync(payReceiveId, (int)PayReceiveType.Payment
                , GridOptions);
            return JsonReadResult(payment);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی فرم دریافت مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم دریافت مورد نظر</returns>
        // GET: api/receipts/{payReceiveId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptAsync(int payReceiveId)
        {
            var receipt = await _repository.GetPayReceiveAsync(payReceiveId,
                (int)PayReceiveType.Receipt, GridOptions);
            return JsonReadResult(receipt);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک فرم پرداخت جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceive">اطلاعات نمایشی فرم پرداخت جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم پرداخت</returns>
        // POST: api/payments
        [HttpPost]
        [Route(PayReceiveApi.PaymentsUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Create)]
        public async Task<IActionResult> PostNewPaymentAsync([FromBody] PayReceiveViewModel payReceive)
        {
            var result = await PayReceiveValidationResultAsync(payReceive, AppStrings.Payment);
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
        // POST: api/receipts
        [HttpPost]
        [Route(PayReceiveApi.ReceiptsUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Create)]
        public async Task<IActionResult> PostNewReceiptAsync([FromBody] PayReceiveViewModel payReceive)
        {
            var result = await PayReceiveValidationResultAsync(payReceive, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SavePayReceiveAsync(payReceive);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک فرم پرداخت موجود را
        /// پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت اصلاح شده</param>
        /// <param name="payReceive">اطلاعات نمایشی اصلاح شده برای فرم پرداخت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم پرداخت</returns>
        // PUT: api/payments/{payReceiveId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.PaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutModifiedPaymentAsync(int payReceiveId,
            [FromBody] PayReceiveViewModel payReceive)
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
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک فرم دریافت موجود را
        /// پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت اصلاح شده</param>
        /// <param name="payReceive">اطلاعات نمایشی اصلاح شده برای فرم دریافت</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای فرم دریافت</returns>
        // PUT: api/receipts/{payReceiveId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.ReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutModifiedReceiptAsync(int payReceiveId,
            [FromBody] PayReceiveViewModel payReceive)
        {
            var result = await PayReceiveValidationResultAsync(payReceive, AppStrings.Receipt, payReceiveId);
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
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Delete)]
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
        // DELETE: api/receipts/{payReceiveId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.ReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingReceiptAsync(int payReceiveId)
        {
            string message = await ValidateDeleteAsync(payReceiveId);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequestResult(message);
            }

            await _repository.DeletePayReceiveAsync(payReceiveId, (int)PayReceiveType.Receipt);
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
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Confirm)]
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
        // PUT: api/receipts/{payReceiveId:int}/confirm
        [HttpPut]
        [Route(PayReceiveApi.ConfirmReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Confirm)]
        public async Task<IActionResult> PutExistingReceiptAsConfirmed(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.Confirm,
                AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetPayReceiveConfirmationAsync(payReceiveId, true);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، فرم پرداخت مشخص شده را برگشت از تأیید کرده و وضعیتش را در حالت تأییدنشده قرار می دهد
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر برای برگشت از تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{payReceiveId:int}/confirm/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoConfirmPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.UndoConfirm)]
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
        // PUT: api/receipts/{payReceiveId:int}/confirm/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoConfirmReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingReceiptAsUnconfirmed(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.UndoConfirm,
                AppStrings.Receipt);
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
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Approve)]
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
        // PUT: api/receipts/{payReceiveId:int}/approve
        [HttpPut]
        [Route(PayReceiveApi.ApproveReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Approve)]
        public async Task<IActionResult> PutExistingReceiptAsApproved(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.Approve,
                AppStrings.Receipt);
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
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر برای برگشت از تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/payments/{payReceiveId:int}/approve/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoApprovePaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingPaymentAsUnapproved(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.UndoApprove,
                AppStrings.Payment);
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
        // PUT: api/receipts/{payReceiveId:int}/approve/undo
        [HttpPut]
        [Route(PayReceiveApi.UndoApproveReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingReceiptAsUnapproved(int payReceiveId)
        {
            var result = await PayReceiveActionValidationResultAsync(payReceiveId, AppStrings.UndoApprove,
                AppStrings.Receipt);
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
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentByNoAsync(string payReceiveNo)
        {
            var payReceiveByNo = await _repository.GetPayReceiveNoAsync(payReceiveNo, (int)PayReceiveType.Payment);
            return JsonReadResult(payReceiveByNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="payReceiveNo">شماره فرم دریافت مورد نظر</param>
        /// <returns>اطلاعات نمایشی فرم دریافت مورد نظر</returns>
        // GET: api/receipts/by-no/{payReceiveNo:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceiptByNoUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptByNoAsync(string payReceiveNo)
        {
            var payReceiveByNo = await _repository.GetPayReceiveNoAsync(payReceiveNo, (int)PayReceiveType.Receipt);
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
        /// <param name="payReceiveNo">شماره فرم پرداخت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت قابل دسترسی قبلی</returns>
        // GET: api/payments/{payReceiveNo:min(1)}/previous
        [HttpGet]
        [Route(PayReceiveApi.PreviousPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousPaymentAsync(string payReceiveNo)
        {
            var previous = await _repository.GetPreviousPayReceiveAsync(payReceiveNo,
                (int)PayReceiveType.Payment, GridOptions);
            return JsonReadResult(previous);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت پیش از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="payReceiveNo">شماره فرم دریافت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم دریافت قابل دسترسی قبلی</returns>
        // GET: api/receipts/{payReceiveNo:min(1)}/previous
        [HttpGet]
        [Route(PayReceiveApi.PreviousReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousReceiptAsync(string payReceiveNo)
        {
            var previous = await _repository.GetPreviousPayReceiveAsync(payReceiveNo,
                (int)PayReceiveType.Receipt, GridOptions);
            return JsonReadResult(previous);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم پرداخت بعد از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="payReceiveNo">شماره فرم پرداخت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم پرداخت قابل دسترسی بعدی</returns>
        // GET: api/payments/{payReceiveNo:min(1)}/next
        [HttpGet]
        [Route(PayReceiveApi.NextPaymentUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Navigate)]
        public async Task<IActionResult> GetNextPaymentAsync(string payReceiveNo)
        {
            var next = await _repository.GetNextPayReceiveAsync(payReceiveNo,
                (int)PayReceiveType.Payment, GridOptions);
            return JsonReadResult(next);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات فرم دریافت بعد از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="payReceiveNo">شماره فرم دریافت فعلی</param>
        /// <returns>اطلاعات نمایشی فرم دریافت قابل دسترسی بعدی</returns>
        // GET: api/receipts/{payReceiveNo:min(1)}/next
        [HttpGet]
        [Route(PayReceiveApi.NextReceiptUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Navigate)]
        public async Task<IActionResult> GetNextReceiptAsync(string payReceiveNo)
        {
            var next = await _repository.GetNextPayReceiveAsync(payReceiveNo,
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
            var payReceive = await _repository.GetNewPayReceiveAsync((int)PayReceiveType.Payment);
            return Json(payReceive);
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
            var payReceive = await _repository.GetNewPayReceiveAsync((int)PayReceiveType.Receipt);
            return Json(payReceive);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های طرف حساب شناسه پرداخت داده شده را برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های فرم پرداخت</returns>
        // GET: api/payments/{payReceiveId:min(1)}/account-articles
        [HttpGet]
        [Route(PayReceiveApi.PaymentAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentAccountArticlesAsync(int payReceiveId)
        {
            var articles = await _accountArticleRepository.GetAccountArticlesAsync(payReceiveId, GridOptions);
            return Json(articles.Items);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های طرف حساب شناسه دریافت داده شده را برمی گرداند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های فرم دریافت</returns>
        // GET: api/receipts/{payReceiveId:min(1)}/account-articles
        [HttpGet]
        [Route(PayReceiveApi.ReceiptAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptAccountArticlesAsync(int payReceiveId)
        {
            var articles = await _accountArticleRepository.GetAccountArticlesAsync(payReceiveId, GridOptions);
            return Json(articles.Items);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آرتیکل حساب پرداختی مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="accountArticleId">شناسه دیتابیسی آرتیکل حساب پرداختی مورد نظر</param>
        /// <returns>اطلاعات نمایشی آرتیکل حساب پرداختی</returns>
        // GET: api/payments/account-articles/{accountArticleId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.PaymentAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentAccountArticleAsync(int accountArticleId)
        {
            var article = await _accountArticleRepository.GetAccountArticleAsync(accountArticleId);
            return JsonReadResult(article);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آرتیکل حساب دریافتی مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="accountArticleId">شناسه دیتابیسی آرتیکل حساب دریافتی مورد نظر</param>
        /// <returns>اطلاعات نمایشی آرتیکل حساب دریافتی</returns>
        // GET: api/receipts/account-articles/{accountArticleId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceiptAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptAccountArticleAsync(int accountArticleId)
        {
            var article = await _accountArticleRepository.GetAccountArticleAsync(accountArticleId);
            return JsonReadResult(article);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب پرداختی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <param name="accountArticle">اطلاعات کامل آرتیکل حساب جدید</param>
        /// <returns>اطلاعات آرتیکل حساب بعد از ایجاد در دیتابیس</returns>
        // POST: api/payments/{payReceiveId:min(1)}/account-articles
        [HttpPost]
        [Route(PayReceiveApi.PaymentAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PostNewPaymentAccountArticleAsync(
            int payReceiveId, [FromBody] PayReceiveAccountViewModel accountArticle)
        {
            return await SaveAccountArticleAsync(
                accountArticle, payReceiveId, AppStrings.Payment, (int)PayReceiveType.Payment);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب دریافتی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="payReceiveId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <param name="accountArticle">اطلاعات کامل آرتیکل حساب جدید</param>
        /// <returns>اطلاعات آرتیکل حساب بعد از ایجاد در دیتابیس</returns>
        // POST: api/receipts/{payReceiveId:min(1)}/account-articles
        [HttpPost]
        [Route(PayReceiveApi.ReceiptAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PostNewReceiptAccountArticleAsync(
            int payReceiveId, [FromBody] PayReceiveAccountViewModel accountArticle)
        {
            return await SaveAccountArticleAsync(
                accountArticle, payReceiveId, AppStrings.Receipt, (int)PayReceiveType.Receipt);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب پرداختی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="accountArticleId">شناسه دیتابیسی آرتیکل حساب پرداختی مورد نظر برای اصلاح</param>
        /// <param name="accountArticle">اطلاعات اصلاح شده آرتیکل حساب پرداختی</param>
        /// <returns>اطلاعات آرتیکل حساب پرداختی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/payments/account-articles/{accountArticleId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.PaymentAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutModifiedPaymentAccountArticleAsync(
            int accountArticleId, [FromBody] PayReceiveAccountViewModel accountArticle)
        {
            return await SaveAccountArticleAsync(accountArticle, accountArticle.PayReceiveId, 
                AppStrings.Payment, (int)PayReceiveType.Payment,accountArticleId);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب دریافتی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="accountArticleId">شناسه دیتابیسی آرتیکل حساب دریافتی مورد نظر برای اصلاح</param>
        /// <param name="accountArticle">اطلاعات اصلاح شده آرتیکل حساب دریافتی</param>
        /// <returns>اطلاعات آرتیکل حساب دریافتی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/receipts/account-articles/{payReceiveId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.ReceiptAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutModifiedReceiptAccountArticleAsync(
            int accountArticleId, [FromBody] PayReceiveAccountViewModel accountArticle)
        {
            return await SaveAccountArticleAsync(accountArticle, accountArticle.PayReceiveId, 
                AppStrings.Receipt, (int)PayReceiveType.Receipt, accountArticleId);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب پرداختی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="accountArticleId">شناسه دیتابیسی آرتیکل حساب پرداختی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/payments/account-articles/{accountArticleId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.PaymentAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingPaymentAccountArticleAsync(int accountArticleId)
        {
            return await DeleteAccountArticleAsync(
                accountArticleId, AppStrings.Payment, (int)PayReceiveType.Payment);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب دریافتی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="accountArticleId">شناسه دیتابیسی آرتیکل حساب دریافتی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/receipts/account-articles/{accountArticleId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.ReceiptAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingReceiptAccountArticleAsync(int accountArticleId)
        {
            return await DeleteAccountArticleAsync(
                accountArticleId, AppStrings.Receipt, (int)PayReceiveType.Receipt);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب پرداختی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/payments/account-articles
        [HttpPut]
        [Route(PayReceiveApi.AllPaymentAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutExistingPaymentAccountArticlesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteArticleResultAsync(actionDetail, _accountArticleRepository.DeleteAccountArticlesAsync,
                AppStrings.Payment, (int)PayReceiveType.Payment);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب دریافتی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/receipts/account-articles
        [HttpPut]
        [Route(PayReceiveApi.AllReceiptAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutExistingReceiptAccountArticlesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteArticleResultAsync(actionDetail,
                _accountArticleRepository.DeleteAccountArticlesAsync, AppStrings.Receipt, (int)PayReceiveType.Receipt);
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
                    return BadRequestResult(_strings.Format(AppStrings.CantSaveEntity, entityNameKey));
                }
            }

            int numberLength = AppConstants.DefaultNumberLength;
            var payReceiveNo = payReceive.PayReceiveNo.Trim();
            if (payReceiveNo.Length != numberLength)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.StringNumberHasFixedLength,AppStrings.Number, numberLength.ToString()));
            }

            if (!Int64.TryParse(payReceiveNo, out long numberValue))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.InvalidLetterForStringNumber, AppStrings.Number));
            }

            if(numberValue <= 0)
            {
                var minNumberString = 1.ToString($"D{numberLength}");
                return BadRequestResult(_strings.Format(
                   AppStrings.InvalidStringNumber, minNumberString,AppStrings.Number));
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
                return BadRequestResult(_strings.Format(AppStrings.ItemByIdNotFound, action,
                    payReceiveId.ToString()));
            }

            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
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

        private async Task<IActionResult> GroupDeleteArticleResultAsync(ActionDetailViewModel actionDetail,
            GroupDeleteSpecialAsyncDelegate groupDelete, string entityNameKey, int type)
        {
            if (actionDetail == null || actionDetail.Items.Count == 0)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await ValidateArticleDeleteByPayReceiveResultAsync(entityNameKey, actionDetail.Items.ToArray());
            if(result is BadRequestObjectResult)
            {
                return result;
            }

            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            foreach (int item in actionDetail.Items)
            {
                var error = await ValidateArticleDeleteResultAsync(item);
                if (error == null)
                {
                    validated.Add(item);
                }
                else
                {
                    notValidated.Add(error);
                }
            }

            if (validated.Count > 0)
            {
                await groupDelete(validated, type);
            }

            return Ok(notValidated);
        }

        private async Task<IActionResult> AccountArticleValidationResultAsync(
            PayReceiveViewModel payReceive,PayReceiveAccountViewModel accountArticle, string entityNameKey)
        {
            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (payReceive.IsApproved || payReceive.IsConfirmed)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.CantSaveDetailEntity, entityNameKey, AppStrings.PayReceiveAccount));
            }

            accountArticle.FullAccount.Account = GetNullableItem(accountArticle.FullAccount.Account);
            accountArticle.FullAccount.DetailAccount = GetNullableItem(accountArticle.FullAccount.DetailAccount);
            accountArticle.FullAccount.CostCenter = GetNullableItem(accountArticle.FullAccount.CostCenter);
            accountArticle.FullAccount.Project = GetNullableItem(accountArticle.FullAccount.Project);
            if (accountArticle.FullAccount.Account != null)
            {
                var lookupResult = await FullAccountValidationResultAsync(accountArticle.FullAccount, _relationRepository);
                if (lookupResult is BadRequestObjectResult)
                {
                    return lookupResult;
                }
            }

            if (accountArticle.Amount < decimal.Zero) 
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.NegativeAmountNotAllowed, AppStrings.FullAccount));
            }

            return Ok();
        }

        private async Task<IActionResult> SaveAccountArticleAsync(PayReceiveAccountViewModel accountArticle, 
            int payReceiveId, string entityNameKey, int type, int accountArticleId = 0) 
        {
            int inputId;
            string name;
            if(accountArticleId == 0) 
            {
                inputId = payReceiveId;
                name = nameof(accountArticle.PayReceiveId);
            }
            else
            {
                inputId = accountArticleId;
                name = "Id";
            }

            var result = BasicValidationResult(
                accountArticle, inputId, AppStrings.PayReceiveAccount, name);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var payReceive = await _repository.GetPayReceiveAsync(accountArticle.PayReceiveId);
            result = await AccountArticleValidationResultAsync(payReceive, accountArticle, entityNameKey);
            if (result is BadRequestObjectResult) 
            { 
                return result;
            }

            var outputItem = await _accountArticleRepository.SaveAccountArticleAsync(accountArticle, type);
            if(accountArticleId == 0)
            {
                return StatusCode(StatusCodes.Status201Created, outputItem);
            }
            else
            {
                return JsonReadResult(outputItem);
            }
        }

        private IActionResult BasicValidationResult<TModel>(
            TModel item, int inputId, string entityKey = null, string inputIdName = null)
        {
            string entityNameKey = entityKey ?? EntityNameKey;
            if (item == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, entityNameKey));
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            string name = inputIdName ?? "Id";
            int id = (int)Reflector.GetProperty(item, name);
            if (inputId != id)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, entityNameKey));
            }

            return Ok();
        }

        private static AccountItemBriefViewModel GetNullableItem(AccountItemBriefViewModel item)
        {
            return (item != null && item.Id > 0)
                ? item
                : null;
        }

        private async Task<GroupActionResultViewModel> ValidateArticleDeleteResultAsync(int accountArticleId)
        {
            string message = String.Empty;
            var article = await _accountArticleRepository.GetAccountArticleSummaryAsync(accountArticleId);
            if (article == null)
            {
                message = _strings.Format(
                        AppStrings.ItemByIdNotFound, AppStrings.PayReceiveAccount, accountArticleId.ToString());
            }

            return GetGroupActionResult(message, article);
        }

        private async Task<IActionResult> ValidateArticleDeleteByPayReceiveResultAsync(
            string entityNameKey , params int[] accountArticleIds)
        {
            var payReceive = await _accountArticleRepository.GetPayReceiveAsync(accountArticleIds);

            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (payReceive != null && (payReceive.IsApproved || payReceive.IsConfirmed))
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.CantDeleteDetailEntity, entityNameKey ,AppStrings.PayReceiveAccount));
            }

            return Ok();
        }

        private async Task<IActionResult> DeleteAccountArticleAsync(int accountArticleId, string entityNameKey, int type)
        {
            var result = await ValidateArticleDeleteByPayReceiveResultAsync(entityNameKey, accountArticleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var error = await ValidateArticleDeleteResultAsync(accountArticleId);
            if (error != null)
            {
                return BadRequestResult(error.ErrorMessage);
            }

            await _accountArticleRepository.DeleteAccountArticleAsync(accountArticleId, type);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private readonly IPayReceiveRepository _repository;
        private readonly IPayReceiveAccountRepository _accountArticleRepository;
        private readonly IRelationRepository _relationRepository;
    }
}