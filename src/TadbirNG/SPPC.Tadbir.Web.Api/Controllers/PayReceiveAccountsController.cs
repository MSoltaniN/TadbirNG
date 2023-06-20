using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
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
    /// عملیات سرویس وب برای مدیریت اطلاعات طرف‌های حساب فرم دریافت/پرداخت را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class PayReceiveAccountsController : ValidatingController<PayReceiveViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات فرم های دریافت/پرداخت را فراهم می کند</param>
        /// <param name="articleAccountRepository">امکان مدیریت اطلاعات طرف حساب را فراهم می کند</param>
        /// <param name="relationRepository">امکان خواندن ارتباطات موجود در  بردار حساب را فراهم می کند</param>  
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public PayReceiveAccountsController(
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
        /// به روش آسنکرون، کلیه آرتیکل های طرف حساب شناسه پرداخت داده شده را برمی گرداند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های فرم پرداخت</returns>
        // GET: api/payments/{paymentId:min(1)}/account-articles
        [HttpGet]
        [Route(PayReceiveApi.PaymentAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentAccountArticlesAsync(int paymentId)
        {
            var articles = await _accountArticleRepository.GetAccountArticlesAsync(paymentId, GridOptions);
            return Json(articles.Items);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های طرف حساب شناسه دریافت داده شده را برمی گرداند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های فرم دریافت</returns>
        // GET: api/receipts/{receiptId:min(1)}/account-articles
        [HttpGet]
        [Route(PayReceiveApi.ReceiptAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptAccountArticlesAsync(int receiptId)
        {
            var articles = await _accountArticleRepository.GetAccountArticlesAsync(receiptId, GridOptions);
            return Json(articles.Items);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آرتیکل حساب پرداختی داده شده را برمی گرداند
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
        /// به روش آسنکرون، اطلاعات آرتیکل حساب دریافتی داده شده را برمی گرداند
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
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <param name="accountArticle">اطلاعات کامل آرتیکل حساب جدید</param>
        /// <returns>اطلاعات آرتیکل حساب بعد از ایجاد در دیتابیس</returns>
        // POST: api/payments/{paymentId:min(1)}/account-articles
        [HttpPost]
        [Route(PayReceiveApi.PaymentAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PostNewPaymentAccountArticleAsync(
            int paymentId, [FromBody] PayReceiveAccountViewModel accountArticle)
        {
            return await SaveAccountArticleAsync(
                accountArticle, paymentId, AppStrings.Payment, (int)PayReceiveType.Payment);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب دریافتی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <param name="accountArticle">اطلاعات کامل آرتیکل حساب جدید</param>
        /// <returns>اطلاعات آرتیکل حساب بعد از ایجاد در دیتابیس</returns>
        // POST: api/receipts/{receiptId:min(1)}/account-articles
        [HttpPost]
        [Route(PayReceiveApi.ReceiptAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PostNewReceiptAccountArticleAsync(
            int receiptId, [FromBody] PayReceiveAccountViewModel accountArticle)
        {
            return await SaveAccountArticleAsync(
                accountArticle, receiptId, AppStrings.Receipt, (int)PayReceiveType.Receipt);
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
        // PUT: api/receipts/account-articles/{receiptId:min(1)}
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
        /// به روش آسنکرون، آرتیکل های حساب نامعتبر فرم پرداخت داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="paymentId">شناسه فرم پرداخت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/payments/{paymentId:min(1)}/account-articles/remove-Invalid-rows
        [HttpDelete]
        [Route(PayReceiveApi.RemovePaymentAccountInvalidRowsUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingInvalidPaymentAccountArticlesAsync(int paymentId)
        {
            var result = await ValidateRemoveInvalidRowsAsync(paymentId, AppStrings.Payment);
            if(result is BadRequestObjectResult)
            {
                return result;
            }

            await _accountArticleRepository.DeleteInvalidRowsAccountArticleAsync(
                paymentId, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب نامعتبر فرم دریافت داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="receiptId">شناسه فرم دریافت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/receipts/{receiptId:min(1)}/account-articles/remove-Invalid-rows
        [HttpDelete]
        [Route(PayReceiveApi.RemoveReceiptAccountInvalidRowsUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingInvalidReceiptAccountArticlesAsync(int receiptId)
        {
            var result = await ValidateRemoveInvalidRowsAsync(receiptId, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _accountArticleRepository.DeleteInvalidRowsAccountArticleAsync(
                receiptId, (int)PayReceiveType.Receipt);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب فرم پرداخت داده شده را - در صورت امکان - تجمیع می کند
        /// </summary>
        /// <param name="paymentId">شناسه فرم پرداخت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/payments/{paymentId:min(1)}/account-articles/aggregate-rows
        [HttpPut]
        [Route(PayReceiveApi.AggregatePaymentAccountArticleRowsUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutExistingPaymentAccountArticlesAsAggregateAsync(int paymentId)
        {
            var result = await ValidateAggregateRowsAsync(paymentId, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _accountArticleRepository.AggregateAccountArticleRowsAsync(
                paymentId, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب فرم دریافت داده شده را - در صورت امکان - تجمیع می کند
        /// </summary>
        /// <param name="receiptId">شناسه فرم دریافت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/receipts/{receiptId:min(1)}/account-articles/aggregate-rows
        [HttpPut]
        [Route(PayReceiveApi.AggregateReceiptAccountArticleRowsUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutExistingReceiptAccountArticlesAsAggregateAsync(int receiptId)
        {
            var result = await ValidateAggregateRowsAsync(receiptId, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _accountArticleRepository.AggregateAccountArticleRowsAsync(
                receiptId, (int)PayReceiveType.Receipt);
            return StatusCode(StatusCodes.Status204NoContent);
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
            PayReceiveViewModel payReceive, PayReceiveAccountViewModel accountArticle, string entityNameKey)
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

            if (accountArticle.Amount < Decimal.Zero) 
            {
                return BadRequestResult(_strings[AppStrings.NegativeAmountNotAllowed]);
            }

            return Ok();
        }

        private async Task<IActionResult> SaveAccountArticleAsync(PayReceiveAccountViewModel accountArticle, 
            int payReceiveId, string entityNameKey, int type, int accountArticleId = 0) 
        {
            var result = GetBasicValidation(accountArticleId, payReceiveId, accountArticle);
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

        private IActionResult GetBasicValidation(
            int accountArticleId, int payReceiveId, PayReceiveAccountViewModel accountArticle)
        {
            if (accountArticleId == 0)
            {
                return BasicValidationResult(
                    accountArticle, payReceiveId, AppStrings.PayReceiveAccount, nameof(accountArticle.PayReceiveId));
            }
            else
            {
                return BasicValidationResult(accountArticle, accountArticleId, AppStrings.PayReceiveAccount);
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
            string entityNameKey, params int[] accountArticleIds)
        {
            var payReceive = await _accountArticleRepository.GetPayReceiveAsync(accountArticleIds);
            if (payReceive != null) 
            { 
                var result = BranchValidationResult(payReceive);
                if (result is BadRequestObjectResult)
                {
                    return result;
                }

                if (payReceive.IsApproved || payReceive.IsConfirmed)
                {
                    return BadRequestResult(_strings.Format(
                        AppStrings.CantDeleteDetailEntity, entityNameKey, AppStrings.PayReceiveAccount));
                }
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

        private async Task<IActionResult> ValidateRemoveInvalidRowsAsync(int payReceiveId, string entityNameKey)
        {
            var payReceive = await _repository.GetPayReceiveAsync(payReceiveId);
            if (payReceive == null) 
            {
                return BadRequestResult(
                    _strings.Format(AppStrings.ItemByIdNotFound, entityNameKey, payReceiveId.ToString()));
            }

            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!await _accountArticleRepository.HasAccountArticleInvalidRowsAsync(payReceiveId))
            {
                return BadRequestResult(
                    _strings.Format(AppStrings.NotFoundInvalidRows, AppStrings.PayReceiveAccount, entityNameKey));
            }

            return Ok();
        }

        private async Task<IActionResult> ValidateAggregateRowsAsync(int payReceiveId, string entityNameKey)
        {
            var payReceive = await _repository.GetPayReceiveAsync(payReceiveId);
            if (payReceive == null)
            {
                return BadRequestResult(
                    _strings.Format(AppStrings.ItemByIdNotFound, entityNameKey, payReceiveId.ToString()));
            }

            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (!await _accountArticleRepository.HasAccountArticlestoAggregateAsync(payReceiveId))
            {
                return BadRequestResult(
                    _strings.Format(AppStrings.NotFoundAggregateRows, AppStrings.PayReceiveAccount, entityNameKey));
            }

            return Ok();

        }

        private readonly IPayReceiveRepository _repository;
        private readonly IPayReceiveAccountRepository _accountArticleRepository;
        private readonly IRelationRepository _relationRepository;
    }
}