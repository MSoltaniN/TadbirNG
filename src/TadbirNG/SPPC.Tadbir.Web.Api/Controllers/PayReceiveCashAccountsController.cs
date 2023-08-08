using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
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
    /// عملیات سرویس وب برای مدیریت اطلاعات حساب‌های نقدیی را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class PayReceiveCashAccountsController : ValidatingController<PayReceiveCashAccountViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات فرم های دریافت/پرداخت را فراهم می کند</param>
        /// <param name="cashAccountArtricleRepository">امکان مدیریت اطلاعات حساب نقدی را فراهم می کند</param>
        /// <param name="relationRepository">امکان خواندن ارتباطات موجود در  بردار حساب را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public PayReceiveCashAccountsController(IPayReceiveRepository repository,
            IPayReceiveCashAccountRepository cashAccountArtricleRepository,
            IRelationRepository relationRepository,
            IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _cashAccountArticleRepository = cashAccountArtricleRepository;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت فرم دریافت/پرداخت
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Receipt; }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های حساب نقدی شناسه پرداخت داده شده را برمی گرداند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های فرم پرداخت</returns>
        // GET: api/payments/{paymentId:min(1)}/cash/articles
        [HttpGet]
        [Route(PayReceiveApi.PaymentCashAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentCashAccountArticlesAsync(int paymentId)
        {
            var articles = await _cashAccountArticleRepository.GetCashAccountArticlesAsync(
                paymentId, (int)PayReceiveType.Payment, GridOptions);
            return JsonListResult(articles);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های حساب نقدی شناسه دریافت داده شده را برمی گرداند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های فرم دریافت</returns>
        // GET: api/receipts/{receiptId:min(1)}/cash/articles
        [HttpGet]
        [Route(PayReceiveApi.ReceiptCashAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptCashAccountArticlesAsync(int receiptId)
        {
            var articles = await _cashAccountArticleRepository.GetCashAccountArticlesAsync(
                receiptId, (int)PayReceiveType.Receipt, GridOptions);
            return JsonListResult(articles);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آرتیکل حساب نقدی پرداختی داده شده را برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل حساب نقدی پرداختی مورد نظر</param>
        /// <returns>اطلاعات نمایشی آرتیکل حساب نقدی پرداختی</returns>
        // GET: api/payments/cash/articles/{articleId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.PaymentCashAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.View)]
        public async Task<IActionResult> GetPaymentCashAccountArticleAsync(int articleId)
        {
            var article = await _cashAccountArticleRepository.GetCashAccountArticleAsync(articleId);
            return JsonReadResult(article);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آرتیکل حساب نقدی دریافتی داده شده را برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل حساب نقدی دریافتی مورد نظر</param>
        /// <returns>اطلاعات نمایشی آرتیکل حساب نقدی دریافتی</returns>
        // GET: api/receipts/cash/articles/{articleId:min(1)}
        [HttpGet]
        [Route(PayReceiveApi.ReceiptCashAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.View)]
        public async Task<IActionResult> GetReceiptCashAccountArticleAsync(int articleId)
        {
            var article = await _cashAccountArticleRepository.GetCashAccountArticleAsync(articleId);
            return JsonReadResult(article);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب نقدی پرداختی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="paymentId">شناسه دیتابیسی فرم پرداخت مورد نظر</param>
        /// <param name="cashAccountArticle">اطلاعات کامل آرتیکل حساب نقدی جدید</param>
        /// <returns>اطلاعات آرتیکل حساب نقدی بعد از ایجاد در دیتابیس</returns>
        // POST: api/payments/{paymentId:min(1)}/cash/articles
        [HttpPost]
        [Route(PayReceiveApi.PaymentCashAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PostNewPaymentCashAccountArticleAsync(
            int paymentId, [FromBody] PayReceiveCashAccountViewModel cashAccountArticle)
        {
            return await SaveCashAccountArticleAsync(
                cashAccountArticle, paymentId, AppStrings.Payment, (int)PayReceiveType.Payment);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب نقدی دریافتی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="receiptId">شناسه دیتابیسی فرم دریافت مورد نظر</param>
        /// <param name="cashAccountArticle">اطلاعات کامل آرتیکل حساب جدید</param>
        /// <returns>اطلاعات آرتیکل حساب بعد از ایجاد در دیتابیس</returns>
        // POST: api/receipts/{receiptId:min(1)}/cash/articles
        [HttpPost]
        [Route(PayReceiveApi.ReceiptCashAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PostNewReceiptCashAccountArticleAsync(
            int receiptId, [FromBody] PayReceiveCashAccountViewModel cashAccountArticle)
        {
            return await SaveCashAccountArticleAsync(
                cashAccountArticle, receiptId, AppStrings.Receipt, (int)PayReceiveType.Receipt);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب نقدی پرداختی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل حساب نقدی پرداختی مورد نظر برای اصلاح</param>
        /// <param name="cashAccountArticle">اطلاعات اصلاح شده آرتیکل حساب نقدی پرداختی</param>
        /// <returns>اطلاعات آرتیکل حساب نقدی پرداختی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/payments/cash/articles/{articleId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.PaymentCashAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutModifiedPaymentCashAccountArticleAsync(
            int articleId, [FromBody] PayReceiveCashAccountViewModel cashAccountArticle)
        {
            return await SaveCashAccountArticleAsync(cashAccountArticle, cashAccountArticle.PayReceiveId,
                AppStrings.Payment, (int)PayReceiveType.Payment, articleId);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب نقدی دریافتی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="cashAccountArticleId">شناسه دیتابیسی آرتیکل حساب نقدی دریافتی مورد نظر برای اصلاح</param>
        /// <param name="cashAccountArticle">اطلاعات اصلاح شده آرتیکل حساب نقدی دریافتی</param>
        /// <returns>اطلاعات آرتیکل حساب نقدی دریافتی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/receipts/cash/articles/{receiptId:min(1)}
        [HttpPut]
        [Route(PayReceiveApi.ReceiptCashAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutModifiedReceiptCashAccountArticleAsync(
            int cashAccountArticleId, [FromBody] PayReceiveCashAccountViewModel cashAccountArticle)
        {
            return await SaveCashAccountArticleAsync(cashAccountArticle, cashAccountArticle.PayReceiveId,
                AppStrings.Receipt, (int)PayReceiveType.Receipt, cashAccountArticleId);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب نقدی پرداختی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل حساب نقدی پرداختی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/payments/cash/articles/{articleId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.PaymentCashAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingPaymentCashAccountArticleAsync(int articleId)
        {
            return await DeleteCashAccountArticleAsync(
                articleId, AppStrings.Payment, (int)PayReceiveType.Payment);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل حساب نقدی دریافتی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل حساب نقدی دریافتی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/receipts/cash/articles/{articleId:min(1)}
        [HttpDelete]
        [Route(PayReceiveApi.ReceiptCashAccountArticleUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingReceiptCashAccountArticleAsync(int articleId)
        {
            return await DeleteCashAccountArticleAsync(
                articleId, AppStrings.Receipt, (int)PayReceiveType.Receipt);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب نقدی پرداختی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/payments/cash/articles
        [HttpPut]
        [Route(PayReceiveApi.AllPaymentCashAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutExistingPaymentCashAccountArticlesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteArticleResultAsync(actionDetail, _cashAccountArticleRepository.DeleteCashAccountArticlesAsync,
                AppStrings.Payment, (int)PayReceiveType.Payment);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب نقدی دریافتی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/receipts/cash/articles
        [HttpPut]
        [Route(PayReceiveApi.AllReceiptCashAccountArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutExistingReceiptCashAccountArticlesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteArticleResultAsync(actionDetail,_cashAccountArticleRepository.DeleteCashAccountArticlesAsync, 
                AppStrings.Receipt, (int)PayReceiveType.Receipt);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب نقدی نامعتبر فرم پرداخت داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="paymentId">شناسه فرم پرداخت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/payments/{paymentId:min(1)}/cash/articles/prune
        [HttpDelete]
        [Route(PayReceiveApi.RemovePaymentCashAccountInvalidRowsUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingInvalidPaymentCashAccountArticlesAsync(int paymentId)
        {
            var result = await ValidateRemoveInvalidRowsAsync(paymentId, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _cashAccountArticleRepository.DeleteInvalidRowsCashAccountArticleAsync(
                paymentId, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب نقدی نامعتبر فرم دریافت داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="receiptId">شناسه فرم دریافت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/receipts/{receiptId:min(1)}/cash/articles/prune
        [HttpDelete]
        [Route(PayReceiveApi.RemoveReceiptCashAccountInvalidRowsUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> DeleteExistingInvalidReceiptCashAccountArticlesAsync(int receiptId)
        {
            var result = await ValidateRemoveInvalidRowsAsync(receiptId, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _cashAccountArticleRepository.DeleteInvalidRowsCashAccountArticleAsync(
                receiptId, (int)PayReceiveType.Receipt);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب نقدی فرم پرداخت داده شده را - در صورت امکان - تجمیع می کند
        /// </summary>
        /// <param name="paymentId">شناسه فرم پرداخت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/payments/{paymentId:min(1)}/cash/articles/aggregate
        [HttpPut]
        [Route(PayReceiveApi.AggregatePaymentCashAccountArticleRowsUrl)]
        [AuthorizeRequest(SecureEntity.Payment, (int)PaymentPermissions.Edit)]
        public async Task<IActionResult> PutExistingPaymentCashAccountArticlesAsAggregateAsync(int paymentId)
        {
            var result = await ValidateAggregateRowsAsync(paymentId, AppStrings.Payment);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _cashAccountArticleRepository.AggregateCashAccountArticleRowsAsync(
                paymentId, (int)PayReceiveType.Payment);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های حساب نقدی فرم دریافت داده شده را - در صورت امکان - تجمیع می کند
        /// </summary>
        /// <param name="receiptId">شناسه فرم دریافت مورد نظر</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/receipts/{receiptId:min(1)}/cash/articles/aggregate
        [HttpPut]
        [Route(PayReceiveApi.AggregateReceiptCashAccountArticleRowsUrl)]
        [AuthorizeRequest(SecureEntity.Receipt, (int)ReceiptPermissions.Edit)]
        public async Task<IActionResult> PutExistingReceiptCashAccountArticlesAsAggregateAsync(int receiptId)
        {
            var result = await ValidateAggregateRowsAsync(receiptId, AppStrings.Receipt);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _cashAccountArticleRepository.AggregateCashAccountArticleRowsAsync(
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
            if (result is BadRequestObjectResult)
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

        private async Task<IActionResult> CashAccountArticleValidationResultAsync(
            PayReceiveViewModel payReceive, PayReceiveCashAccountViewModel cashAccountArticle, string entityNameKey)
        {
            var result = BranchValidationResult(payReceive);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (payReceive.IsApproved || payReceive.IsConfirmed || payReceive.IsRegistered)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.CantSaveDetailEntity, entityNameKey, AppStrings.PayReceiveCashAccount));
            }

            cashAccountArticle.FullAccount.Account = GetNullableItem(cashAccountArticle.FullAccount.Account);
            cashAccountArticle.FullAccount.DetailAccount = GetNullableItem(cashAccountArticle.FullAccount.DetailAccount);
            cashAccountArticle.FullAccount.CostCenter = GetNullableItem(cashAccountArticle.FullAccount.CostCenter);
            cashAccountArticle.FullAccount.Project = GetNullableItem(cashAccountArticle.FullAccount.Project);
            if (cashAccountArticle.FullAccount.Account != null)
            {
                var lookupResult = await FullAccountValidationResultAsync(cashAccountArticle.FullAccount, _relationRepository);
                if (lookupResult is BadRequestObjectResult)
                {
                    return lookupResult;
                }

                int accountId = cashAccountArticle.FullAccount.Account.Id;
                if (cashAccountArticle.IsBank)
                {
                    if(!await _cashAccountArticleRepository.IsBankCashAccountAsync(accountId))
                    {
                        return BadRequestResult(_strings.Format(
                            AppStrings.SelectedAccountNotInCollection, AppStrings.Bank));
                    }
                }
                else 
                {
                    if (!await _cashAccountArticleRepository.IsCashierCashAccountAsync(accountId))
                    {
                        return BadRequestResult(_strings.Format(
                            AppStrings.SelectedAccountNotInCollection, AppStrings.CashRegister));
                    }
                }
            }

            if (cashAccountArticle.Amount < Decimal.Zero)
            {
                return BadRequestResult(_strings[AppStrings.NegativeAmountNotAllowed]);
            }
            
            if (cashAccountArticle.SourceAppId > 0) 
            {
                int sourceAppId = (int)cashAccountArticle.SourceAppId;
                if (payReceive.Type == (int)PayReceiveType.Payment)
                {
                    if(!await _cashAccountArticleRepository.IsAppCashAccountAsync(sourceAppId))
                    {
                        return BadRequestResult(_strings.Format(AppStrings.InvalidSourceAppInForm, 
                            AppStrings.Applications, AppStrings.Source, entityNameKey));
                    }
                }
                else if (payReceive.Type == (int)PayReceiveType.Receipt)
                {
                    if (!await _cashAccountArticleRepository.IsSourceCashAccountAsync(sourceAppId))
                    {
                        return BadRequestResult(_strings.Format(AppStrings.InvalidSourceAppInForm, 
                            AppStrings.Sources, AppStrings.Application, entityNameKey));
                    }
                }
            }

            return Ok();
        }

        private async Task<IActionResult> SaveCashAccountArticleAsync(PayReceiveCashAccountViewModel cashAccountArticle,
            int payReceiveId, string entityNameKey, int type, int cashAccountArticleId = 0)
        {
            var result = GetBasicValidation(cashAccountArticleId, payReceiveId, cashAccountArticle);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var payReceive = await _repository.GetPayReceiveAsync(cashAccountArticle.PayReceiveId);
            result = await CashAccountArticleValidationResultAsync(payReceive, cashAccountArticle, entityNameKey);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _cashAccountArticleRepository.SaveCashAccountArticleAsync(cashAccountArticle, type);
            if (cashAccountArticleId == 0)
            {
                return StatusCode(StatusCodes.Status201Created, outputItem);
            }
            else
            {
                return JsonReadResult(outputItem);
            }
        }

        private IActionResult GetBasicValidation(
            int cashAccountArticleId, int payReceiveId, PayReceiveCashAccountViewModel cashAccountArticle)
        {
            if (cashAccountArticleId == 0)
            {
                return BasicValidationResult(
                    cashAccountArticle, payReceiveId, AppStrings.PayReceiveCashAccount, nameof(cashAccountArticle.PayReceiveId));
            }
            else
            {
                return BasicValidationResult(cashAccountArticle, cashAccountArticleId, AppStrings.PayReceiveCashAccount);
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

        private async Task<GroupActionResultViewModel> ValidateArticleDeleteResultAsync(int cashAccountArticleId)
        {
            string message = String.Empty;
            var article = await _cashAccountArticleRepository.GetCashAccountArticleSummaryAsync(cashAccountArticleId);
            if (article == null)
            {
                message = _strings.Format(
                        AppStrings.ItemByIdNotFound, AppStrings.PayReceiveCashAccount, cashAccountArticleId.ToString());
            }

            return GetGroupActionResult(message, article);
        }

        private async Task<IActionResult> ValidateArticleDeleteByPayReceiveResultAsync(
            string entityNameKey, params int[] cashAccountArticleIds)
        {
            var payReceive = await _cashAccountArticleRepository.GetPayReceiveAsync(cashAccountArticleIds);
            if (payReceive != null)
            {
                var result = BranchValidationResult(payReceive);
                if (result is BadRequestObjectResult)
                {
                    return result;
                }

                if (payReceive.IsApproved || payReceive.IsConfirmed || payReceive.IsRegistered)
                {
                    return BadRequestResult(_strings.Format(
                        AppStrings.CantDeleteDetailEntity, entityNameKey, AppStrings.PayReceiveCashAccount));
                }
            }

            return Ok();
        }

        private async Task<IActionResult> DeleteCashAccountArticleAsync(int cashAccountArticleId, string entityNameKey, int type)
        {
            var result = await ValidateArticleDeleteByPayReceiveResultAsync(entityNameKey, cashAccountArticleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var error = await ValidateArticleDeleteResultAsync(cashAccountArticleId);
            if (error != null)
            {
                return BadRequestResult(error.ErrorMessage);
            }

            await _cashAccountArticleRepository.DeleteCashAccountArticleAsync(cashAccountArticleId, type);
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

            if (payReceive.IsApproved || payReceive.IsConfirmed || payReceive.IsRegistered)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.CantDeleteDetailEntity, entityNameKey, AppStrings.PayReceiveCashAccount));
            }

            if (!await _cashAccountArticleRepository.HasCashAccountArticleInvalidRowsAsync(payReceiveId))
            {
                return BadRequestResult(
                    _strings.Format(AppStrings.NotFoundInvalidRows, AppStrings.PayReceiveCashAccount, entityNameKey));
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

            if (payReceive.IsApproved || payReceive.IsConfirmed || payReceive.IsRegistered)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.CantChangeDetailEntity, entityNameKey, AppStrings.PayReceiveCashAccount));
            }

            if (!await _cashAccountArticleRepository.HasCashAccountArticlesToAggregateAsync(payReceiveId))
            {
                return BadRequestResult(
                    _strings.Format(AppStrings.NotFoundAggregateRows, AppStrings.PayReceiveCashAccount, entityNameKey));
            }

            return Ok();
        }

        private readonly IPayReceiveRepository _repository;
        private readonly IPayReceiveCashAccountRepository _cashAccountArticleRepository;
        private readonly IRelationRepository _relationRepository;
    }
}