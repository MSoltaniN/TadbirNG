using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// واسط برنامه نویسی با اسناد و آرتیکل های مالی را در برنامه پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class VouchersController : ValidatingController<VoucherViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان مدیریت اطلاعات اسناد مالی را فراهم می کند</param>
        /// <param name="lineRepository">امکان مدیریت اطلاعات آرتیکل های مالی را فراهم می کند</param>
        /// <param name="draftRepository">امکان مدیریت اطلاعات اسناد پیش نویس را فراهم می کند</param>
        /// <param name="draftLineRepository">امکان مدیریت اطلاعات آرتیکل های پیش نویس را فراهم می کند</param>
        /// <param name="relationRepository">امکان خواندن ارتباطات موجود در  بردار حساب را فراهم می کند</param>
        /// <param name="strings">امکان ترجمه متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager"></param>
        public VouchersController(
            IVoucherRepository repository,
            IVoucherLineRepository lineRepository,
            IDraftVoucherRepository draftRepository,
            IDraftVoucherLineRepository draftLineRepository,
            IRelationRepository relationRepository,
            IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _lineRepository = lineRepository;
            _draftRepository = draftRepository;
            _draftLineRepository = draftLineRepository;
            _relationRepository = relationRepository;
        }

        /// <summary>
        /// کلید متن چندزبانه برای نام اسناد مالی
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.Voucher; }
        }

        #region Voucher Resources - Normal

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده اسناد مالی</returns>
        // GET: api/vouchers
        [HttpGet]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVouchersAsync()
        {
            return await GetVoucherListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی (عادی و پیش نویس) قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده اسناد مالی - عادی و پیش نویس</returns>
        // GET: api/vouchers
        [HttpGet]
        [Route(VoucherApi.AllEnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.View)]
        [AuthorizeRequest(SecureEntity.DraftVouchers, (int)ManageDraftVouchersPermissions.View)]
        public async Task<IActionResult> GetAllEnvironmentVouchersAsync()
        {
            return await GetVoucherListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی سند مالی مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر</param>
        /// <returns>اطلاعات نمایشی سند مالی</returns>
        // GET: api/vouchers/{voucherId:int}
        [HttpGet]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherAsync(int voucherId)
        {
            return await GetSingleVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی جدیدی با مقادیر پیشنهادی در دیتابیس ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند مالی جدید با مقادیر پیشنهادی</returns>
        // GET: api/vouchers/new
        [HttpGet]
        [Route(VoucherApi.NewVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public async Task<IActionResult> GetNewVoucherAsync()
        {
            return await GetNewVoucherBySubjectAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی مورد نظر</param>
        /// <returns>اطلاعات نمایشی سند مالی مورد نظر</returns>
        // GET: api/vouchers/by-no/{voucherNo:min(1)}
        [HttpGet]
        [Route(VoucherApi.VoucherByNoUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetVoucherByNoAsync(int voucherNo)
        {
            return await GetVoucherByNoBySubjectAsync(voucherNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین سند مالی قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی اولین سند مالی قابل دسترسی</returns>
        // GET: api/vouchers/first
        [HttpGet]
        [Route(VoucherApi.FirstVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetFirstVoucherAsync()
        {
            return await GetFirstVoucherByTypeAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی پیش از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی فعلی</param>
        /// <returns>اطلاعات نمایشی سند مالی قابل دسترسی قبلی</returns>
        // GET: api/vouchers/{voucherNo:min(1)}/previous
        [HttpGet]
        [Route(VoucherApi.PreviousVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousVoucherAsync(int voucherNo)
        {
            return await GetPreviousVoucherByTypeAsync(voucherNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند مالی بعد از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی فعلی</param>
        /// <returns>اطلاعات نمایشی سند مالی قابل دسترسی بعدی</returns>
        // GET: api/vouchers/{voucherNo:min(1)}/next
        [HttpGet]
        [Route(VoucherApi.NextVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetNextVoucherAsync(int voucherNo)
        {
            return await GetNextVoucherByTypeAsync(voucherNo);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین سند مالی قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی آخرین سند مالی قابل دسترسی</returns>
        // GET: api/vouchers/last
        [HttpGet]
        [Route(VoucherApi.LastVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Navigate)]
        public async Task<IActionResult> GetLastVoucherAsync()
        {
            return await GetLastVoucherByTypeAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/vouchers/opening/query
        [HttpGet]
        [Route(VoucherApi.OpeningVoucherQueryUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOpeningVoucherAsync()
        {
            var openingVoucher = await _repository.GetOpeningVoucherAsync(true);
            if (openingVoucher != null)
            {
                Localize(openingVoucher);
                return Json(openingVoucher);
            }
            else
            {
                var result = await ClosingVoucherValidationResultAsync();
                if (result is BadRequestObjectResult)
                {
                    return result;
                }

                bool hasPrevious = await _repository.HasPreviousClosingVoucherAsync();
                bool needsPrompt = !hasPrevious;
                return Json(needsPrompt);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        // GET: api/vouchers/opening?isDefault={bool}
        [HttpGet]
        [Route(VoucherApi.OpeningVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueOpeningVoucherAsync(bool? isDefault)
        {
            var result = await SpecialVoucherValidationResultAsync(
                AppStrings.OpeningVoucher, AppStrings.IssueOpeningVoucher, VoucherOriginId.OpeningVoucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            bool isDefaultVoucher = isDefault ?? true;
            var openingVoucher = await _repository.GetOpeningVoucherAsync(false, isDefaultVoucher);
            Localize(openingVoucher);
            return Json(openingVoucher);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/vouchers/closing-tmp
        [HttpGet]
        [Route(VoucherApi.ClosingAccountsVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueClosingAccountsVoucherAsync()
        {
            var closingAccountsVoucher = await _repository.GetClosingTempAccountsVoucherAsync(false);
            if (closingAccountsVoucher == null)
            {
                var result = await SpecialVoucherValidationResultAsync(
                    AppStrings.ClosingTempAccounts, AppStrings.IssueClosingTempAccountsVoucher,
                    VoucherOriginId.ClosingTempAccounts);
                if (result is BadRequestObjectResult)
                {
                    return result;
                }

                closingAccountsVoucher = await _repository.GetClosingTempAccountsVoucherAsync();
            }

            Localize(closingAccountsVoucher);
            return Json(closingAccountsVoucher);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="balanceItems"></param>
        /// <returns></returns>
        // PUT: api/vouchers/closing-tmp
        [HttpPut]
        [Route(VoucherApi.ClosingAccountsVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> PutOrIssueClosingAccountsVoucherAsync(
            [FromBody] IList<AccountBalanceViewModel> balanceItems)
        {
            var closingAccountsVoucher = await _repository.GetPeriodicClosingTempAccountsVoucherAsync(
                balanceItems, false);
            if (closingAccountsVoucher == null)
            {
                var result = await SpecialVoucherValidationResultAsync(
                    AppStrings.ClosingTempAccounts, AppStrings.IssueClosingTempAccountsVoucher,
                    VoucherOriginId.ClosingTempAccounts);
                if (result is BadRequestObjectResult)
                {
                    return result;
                }

                closingAccountsVoucher = await _repository.GetPeriodicClosingTempAccountsVoucherAsync(
                    balanceItems ?? new List<AccountBalanceViewModel>());
            }

            Localize(closingAccountsVoucher);
            return Json(closingAccountsVoucher);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/vouchers/closing
        [HttpGet]
        [Route(VoucherApi.ClosingVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetOrIssueClosingVoucherAsync()
        {
            var closingVoucher = await _repository.GetClosingVoucherAsync(false);
            if (closingVoucher == null)
            {
                var result = await SpecialVoucherValidationResultAsync(
                    AppStrings.ClosingVoucher, AppStrings.IssueClosingVoucher, VoucherOriginId.ClosingVoucher);
                if (result is BadRequestObjectResult)
                {
                    return result;
                }

                closingVoucher = await _repository.GetClosingVoucherAsync();
            }

            Localize(closingVoucher);
            return Json(closingVoucher);
        }

        #endregion

        #region Voucher Resources - Draft

        /// <summary>
        /// به روش آسنکرون، کلیه اسناد مالی پیش نویس قابل دسترس در محیط جاری برنامه را برمی گرداند
        /// </summary>
        /// <returns>لیست صفحه بندی شده اسناد مالی پیش نویس</returns>
        // GET: api/vouchers/draft
        [HttpGet]
        [Route(VoucherApi.EnvironmentDraftVouchersUrl)]
        [AuthorizeRequest(SecureEntity.DraftVouchers, (int)ManageDraftVouchersPermissions.View)]
        public async Task<IActionResult> GetEnvironmentDraftVouchersAsync()
        {
            return await GetVoucherListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی سند پیش نویس مشخص شده با شناسه دیتابیسی را برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند پیش نویس مورد نظر</param>
        /// <returns>اطلاعات نمایشی سند پیش نویس</returns>
        // GET: api/vouchers/draft/{voucherId:int}
        [HttpGet]
        [Route(VoucherApi.DraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.View)]
        public async Task<IActionResult> GetDraftVoucherAsync(int voucherId)
        {
            return await GetSingleVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، سند پیش نویس جدیدی با مقادیر پیشنهادی در دیتابیس ایجاد کرده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند پیش نویس جدید با مقادیر پیشنهادی</returns>
        // GET: api/vouchers/draft/new
        [HttpGet]
        [Route(VoucherApi.NewDraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Create)]
        public async Task<IActionResult> GetNewDraftVoucherAsync()
        {
            return await GetNewVoucherBySubjectAsync(SubjectType.Draft);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند پیش نویس مشخص شده با شماره را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند پیش نویس مورد نظر</param>
        /// <returns>اطلاعات نمایشی سند پیش نویس مورد نظر</returns>
        // GET: api/vouchers/draft/by-no/{voucherNo:min(1)}
        [HttpGet]
        [Route(VoucherApi.DraftVoucherByNoUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.View)]
        public async Task<IActionResult> GetDraftVoucherByNoAsync(int voucherNo)
        {
            return await GetVoucherByNoBySubjectAsync(voucherNo, SubjectType.Draft);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات اولین سند پیش نویس قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی اولین سند پیش نویس قابل دسترسی</returns>
        // GET: api/vouchers/first
        [HttpGet]
        [Route(VoucherApi.FirstDraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Navigate)]
        public async Task<IActionResult> GetFirstDraftVoucherAsync()
        {
            return await GetFirstVoucherByTypeAsync(SubjectType.Draft);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند پیش نویس پیش از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند پیش نویس فعلی</param>
        /// <returns>اطلاعات نمایشی سند پیش نویس قابل دسترسی قبلی</returns>
        // GET: api/vouchers/{voucherNo:min(1)}/previous
        [HttpGet]
        [Route(VoucherApi.PreviousDraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Navigate)]
        public async Task<IActionResult> GetPreviousDraftVoucherAsync(int voucherNo)
        {
            return await GetPreviousVoucherByTypeAsync(voucherNo, SubjectType.Draft);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات سند پیش نویس بعد از شماره مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند پیش نویس فعلی</param>
        /// <returns>اطلاعات نمایشی سند پیش نویس قابل دسترسی بعدی</returns>
        // GET: api/vouchers/{voucherNo:min(1)}/next
        [HttpGet]
        [Route(VoucherApi.NextDraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Navigate)]
        public async Task<IActionResult> GetNextDraftVoucherAsync(int voucherNo)
        {
            return await GetNextVoucherByTypeAsync(voucherNo, SubjectType.Draft);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آخرین سند پیش نویس قابل دسترسی را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی آخرین سند پیش نویس قابل دسترسی</returns>
        // GET: api/vouchers/last
        [HttpGet]
        [Route(VoucherApi.LastDraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Navigate)]
        public async Task<IActionResult> GetLastDraftVoucherAsync()
        {
            return await GetLastVoucherByTypeAsync(SubjectType.Draft);
        }

        #endregion

        #region Voucher Articles Resources

        /// <summary>
        /// به روش آسنکرون، کلیه آرتیکل های سند داده شده را برمی گرداند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر</param>
        /// <returns>فهرست صفحه بندی شده آرتیکل های سند</returns>
        // GET: api/vouchers/{voucherId:min(1)}/articles
        [HttpGet]
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticlesAsync(int voucherId)
        {
            var articles = await _lineRepository.GetArticlesAsync(voucherId, GridOptions);
            SetItemCount(articles.TotalCount);
            Localize(articles.Items);
            return Json(articles.Items);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات آرتیکل مالی مشخص شده را برمی گرداند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر</param>
        /// <returns>اطلاعات نمایشی آرتیکل مالی</returns>
        // GET: api/vouchers/articles/{articleId:min(1)}
        [HttpGet]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticleAsync(int articleId)
        {
            var article = await _lineRepository.GetArticleAsync(articleId);
            Localize(article);
            return JsonReadResult(article);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی</param>
        /// <param name="article">اطلاعات کامل آرتیکل مالی جدید</param>
        /// <returns>اطلاعات آرتیکل مالی بعد از ایجاد در دیتابیس</returns>
        // POST: api/vouchers/{voucherId:min(1)}/articles
        [HttpPost]
        [Route(VoucherApi.VoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.CreateLine))]
        public async Task<IActionResult> PostNewArticleAsync(
            int voucherId, [FromBody] VoucherLineViewModel article)
        {
            return await InsertArticleAsync(voucherId, article);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر برای اصلاح</param>
        /// <param name="article">اطلاعات اصلاح شده آرتیکل مالی</param>
        /// <returns>اطلاعات آرتیکل مالی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/vouchers/articles/{articleId:min(1)}
        [HttpPut]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.EditLine))]
        public async Task<IActionResult> PutModifiedArticleAsync(
            int articleId, [FromBody] VoucherLineViewModel article)
        {
            return await UpdateArticleAsync(articleId, article);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات علامتگذاری آرتیکل مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی</param>
        /// <param name="mark">اطلاعات اصلاح شده علامتگذاری آرتیکل مالی</param>
        /// <returns>در صورت بروز خطا، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
        // PUT: api/vouchers/articles/{articleId:min(1)}/mark
        [HttpPut]
        [Route(VoucherApi.VoucherArticleMarkUrl)]
        [AuthorizeRequest(SecureEntity.Journal, (int)JournalPermissions.Mark)]
        public async Task<IActionResult> PutModifiedArticleMarkAsync(
            int articleId, [FromBody] VoucherLineMarkViewModel mark)
        {
            var result = BasicValidationResult(mark, AppStrings.VoucherLineMark, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _lineRepository.SaveArticleMarkAsync(mark);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/vouchers/articles/{articleId:min(1)}
        [HttpDelete]
        [Route(VoucherApi.VoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.DeleteLine))]
        public async Task<IActionResult> DeleteExistingArticleAsync(int articleId)
        {
            return await DeleteArticleAsync(articleId);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/vouchers/articles
        [HttpPut]
        [Route(VoucherApi.AllVoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)(VoucherPermissions.Edit | VoucherPermissions.DeleteLine))]
        public async Task<IActionResult> PutExistingArticlesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await DeleteArticlesAsync(actionDetail);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی داده شده را برای یک سند پیش نویس ایجاد می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی پیش نویس</param>
        /// <param name="article">اطلاعات کامل آرتیکل مالی جدید</param>
        /// <returns>اطلاعات آرتیکل مالی بعد از ایجاد در دیتابیس</returns>
        // POST: api/vouchers/draft/{voucherId:min(1)}/articles
        [HttpPost]
        [Route(VoucherApi.DraftVoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)(DraftVoucherPermissions.Edit | DraftVoucherPermissions.CreateLine))]
        public async Task<IActionResult> PostNewDraftArticleAsync(
            int voucherId, [FromBody] VoucherLineViewModel article)
        {
            return await InsertArticleAsync(voucherId, article);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر برای اصلاح</param>
        /// <param name="article">اطلاعات اصلاح شده آرتیکل مالی</param>
        /// <returns>اطلاعات آرتیکل مالی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/vouchers/draft/articles/{articleId:min(1)}
        [HttpPut]
        [Route(VoucherApi.DraftVoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)(DraftVoucherPermissions.Edit | DraftVoucherPermissions.EditLine))]
        public async Task<IActionResult> PutModifiedDraftArticleAsync(
            int articleId, [FromBody] VoucherLineViewModel article)
        {
            return await UpdateArticleAsync(articleId, article);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="articleId">شناسه دیتابیسی آرتیکل مالی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/vouchers/draft/articles/{articleId:min(1)}
        [HttpDelete]
        [Route(VoucherApi.DraftVoucherArticleUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)(DraftVoucherPermissions.Edit | DraftVoucherPermissions.DeleteLine))]
        public async Task<IActionResult> DeleteExistingDraftArticleAsync(int articleId)
        {
            return await DeleteArticleAsync(articleId);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/vouchers/draft/articles
        [HttpPut]
        [Route(VoucherApi.AllDraftVoucherArticlesUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)(DraftVoucherPermissions.Edit | DraftVoucherPermissions.DeleteLine))]
        public async Task<IActionResult> PutExistingDraftArticlesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await DeleteArticlesAsync(actionDetail);
        }

        #endregion

        #region Voucher Operations - Single

        /// <summary>
        /// به روش آسنکرون، سند مالی پیش نویس مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی پیش نویس مورد نظر برای اصلاح</param>
        /// <param name="voucher">اطلاعات اصلاح شده سند مالی پیش نویس</param>
        /// <returns>اطلاعات سند مالی پیش نویس بعد از اصلاح در دیتابیس</returns>
        // PUT: api/vouchers/draft/{voucherId:int}
        [HttpPut]
        [Route(VoucherApi.DraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Edit)]
        public async Task<IActionResult> PutModifiedDraftVoucherAsync(
            int voucherId, [FromBody] VoucherViewModel voucher)
        {
            return await SaveVoucherAsync(voucherId, voucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی پیش نویس مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی پیش نویس مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/vouchers/draft/{voucherId:int}
        [HttpDelete]
        [Route(VoucherApi.DraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingDraftVoucherAsync(int voucherId)
        {
            return await DeleteVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت ثبت سند پیش نویس مشخص شده را به ثبت عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند پیش نویس مورد نظر برای ثبت</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/draft/{voucherId:int}/check
        [HttpPut]
        [Route(VoucherApi.CheckDraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Check)]
        public async Task<IActionResult> PutExistingDraftVoucherAsChecked(int voucherId)
        {
            return await CheckVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، سند پیش نویس با وضعیت ثبت عادی را برگشت داده و وضعیتش را به ثبت نشده تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند پیش نویس مورد نظر برای برگشت از ثبت</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/draft/{voucherId:int}/check/undo
        [HttpPut]
        [Route(VoucherApi.UndoCheckDraftVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.UndoCheck)]
        public async Task<IActionResult> PutExistingDraftVoucherAsUnchecked(int voucherId)
        {
            return await UncheckVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی داده شده را ایجاد می کند
        /// </summary>
        /// <param name="voucher">اطلاعات کامل سند مالی جدید</param>
        /// <returns>اطلاعات سند مالی بعد از ایجاد در دیتابیس</returns>
        // POST: api/vouchers
        [HttpPost]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Create)]
        public async Task<IActionResult> PostNewVoucherAsync([FromBody] VoucherViewModel voucher)
        {
            var result = await VoucherValidationResultAsync(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = GetVoucherRepository((SubjectType)voucher.SubjectType);
            var outputVoucher = await repository.SaveVoucherAsync(voucher);
            return StatusCode(StatusCodes.Status201Created, outputVoucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را اصلاح می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر برای اصلاح</param>
        /// <param name="voucher">اطلاعات اصلاح شده سند مالی</param>
        /// <returns>اطلاعات سند مالی بعد از اصلاح در دیتابیس</returns>
        // PUT: api/vouchers/{voucherId:int}
        [HttpPut]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Edit)]
        public async Task<IActionResult> PutModifiedVoucherAsync(
            int voucherId, [FromBody] VoucherViewModel voucher)
        {
            return await SaveVoucherAsync(voucherId, voucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند مالی مشخص شده با شناسه دیتابیسی را حذف می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مالی مورد نظر برای حذف</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // DELETE: api/vouchers/{voucherId:int}
        [HttpDelete]
        [Route(VoucherApi.VoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingVoucherAsync(int voucherId)
        {
            return await DeleteVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت ثبت سند مشخص شده را به ثبت عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای ثبت</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/check
        [HttpPut]
        [Route(VoucherApi.CheckVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Check)]
        public async Task<IActionResult> PutExistingVoucherAsChecked(int voucherId)
        {
            return await CheckVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، سند ثبت عادی را برگشت داده و وضعیتش را به ثبت نشده تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از ثبت</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/check/undo
        [HttpPut]
        [Route(VoucherApi.UndoCheckVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoCheck)]
        public async Task<IActionResult> PutExistingVoucherAsUnchecked(int voucherId)
        {
            return await UncheckVoucherAsync(voucherId);
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را در حالت تأییدشده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/confirm
        [HttpPut]
        [Route(VoucherApi.ConfirmVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Confirm)]
        public async Task<IActionResult> PutExistingVoucherAsConfirmed(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Confirm);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherConfirmationAsync(voucherId, true);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را برگشت از تأیید کرده و وضعیتش را در حالت تأییدنشده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از تأیید</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/confirm/undo
        [HttpPut]
        [Route(VoucherApi.UndoConfirmVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoConfirm)]
        public async Task<IActionResult> PutExistingVoucherAsUnconfirmed(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoConfirm);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherConfirmationAsync(voucherId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را در حالت تصویب شده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/approve
        [HttpPut]
        [Route(VoucherApi.ApproveVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Approve)]
        public async Task<IActionResult> PutExistingVoucherAsApproved(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Approve);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherApprovalAsync(voucherId, true);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند مشخص شده را برگشت از تصویب کرده و وضعیتش را در حالت تصویب نشده قرار می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از تصویب</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/approve/undo
        [HttpPut]
        [Route(VoucherApi.UndoApproveVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.UndoApprove)]
        public async Task<IActionResult> PutExistingVoucherAsUnapproved(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoApprove);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherApprovalAsync(voucherId, false);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت ثبت سند مشخص شده را به ثبت قطعی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای ثبت قطعی</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/finalize
        [HttpPut]
        [Route(VoucherApi.FinalizeVoucherUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Finalize)]
        public async Task<IActionResult> PutExistingVoucherAsFinalized(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Finalize);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusId.Finalized);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، سند ثبت قطعی را برگشت داده و وضعیتش را به ثبت عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای برگشت از ثبت قطعی</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/finalize/undo
        [HttpPut]
        [Route(VoucherApi.UndoFinalizeVoucherUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutExistingVoucherAsUnfinalized(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoFinalize);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SetVoucherStatusAsync(voucherId, DocumentStatusId.Checked);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، نوع مفهومی سند مشخص شده را به سند عادی تغییر می دهد
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سند مورد نظر برای تغییر نوع</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/{voucherId:int}/normalize
        [HttpPut]
        [Route(VoucherApi.NormalizeVoucherUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Normalize)]
        public async Task<IActionResult> PutExistingDraftVoucherAsNormalized(int voucherId)
        {
            var result = await ClosingVoucherValidationResultAsync();
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _draftRepository.NormalizeVoucherAsync(voucherId);
            return Ok();
        }

        #endregion

        #region Voucher Operations - Group

        /// <summary>
        /// به روش آسنکرون، اسناد مالی پیش نویس داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/vouchers/draft
        [HttpPut]
        [Route(VoucherApi.EnvironmentDraftVouchersUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Delete)]
        public async Task<IActionResult> PutExistingDraftVouchersAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await DeleteVouchersAsync(actionDetail);
        }

        /// <summary>
        /// ثبت گروهی اسناد پیش نویس
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده </param>
        /// <returns></returns>
        // PUT: api/vouchers/draft/check
        [HttpPut]
        [Route(VoucherApi.CheckDraftVouchersUrl)]
        [AuthorizeRequest(SecureEntity.DraftVouchers, (int)ManageDraftVouchersPermissions.GroupCheck)]
        public async Task<IActionResult> PutExistingDraftVouchersAsChecked([FromBody] ActionDetailViewModel actionDetail)
        {
            return await CheckVouchersAsync(actionDetail);
        }

        /// <summary>
        /// برگشت از ثبت گروهی اسناد پیش نویس
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        // PUT: api/vouchers/draft/check/undo
        [HttpPut]
        [Route(VoucherApi.UndoCheckDraftVouchersUrl)]
        [AuthorizeRequest(SecureEntity.DraftVouchers, (int)ManageDraftVouchersPermissions.GroupUndoCheck)]
        public async Task<IActionResult> PutExistingDraftVouchersAsUnchecked(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await UncheckVouchersAsync(actionDetail);
        }

        /// <summary>
        /// به روش آسنکرون، اسناد مالی داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/vouchers
        [HttpPut]
        [Route(VoucherApi.EnvironmentVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Delete)]
        public async Task<IActionResult> PutExistingVouchersAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await DeleteVouchersAsync(actionDetail);
        }

        /// <summary>
        /// ثبت گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده </param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.CheckVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.GroupCheck)]
        public async Task<IActionResult> PutExistingVouchersAsChecked([FromBody] ActionDetailViewModel actionDetail)
        {
            return await CheckVouchersAsync(actionDetail);
        }

        /// <summary>
        /// برگشت از ثبت گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.UndoCheckVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.GroupUndoCheck)]
        public async Task<IActionResult> PutExistingVouchersAsUnchecked([FromBody] ActionDetailViewModel actionDetail)
        {
            return await UncheckVouchersAsync(actionDetail);
        }

        /// <summary>
        /// تایید گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        // PUT: api/vouchers/confirm
        [HttpPut]
        [Route(VoucherApi.ConfirmVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.GroupConfirm)]
        public async Task<IActionResult> PutExistingVouchersAsConfirmed([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupConfirmApproveResultAsync(
                actionDetail.Items, AppStrings.GroupConfirmApprove);
        }

        /// <summary>
        /// برگشت از تایید گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        // PUT: api/vouchers/confirm/undo
        [HttpPut]
        [Route(VoucherApi.UndoConfirmVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.GroupUndoConfirm)]
        public async Task<IActionResult> PutExistingVouchersAsUnConfirmed([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupConfirmApproveResultAsync(
                actionDetail.Items, AppStrings.GroupUndoConfirmApprove);
        }

        /// <summary>
        /// ثبت قطعی گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.FinalizeVouchersUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.GroupFinalize)]
        public async Task<IActionResult> PutExistingVouchersAsFinalized([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.Finalize, DocumentStatusId.Finalized);
        }

        /// <summary>
        /// برگشت از ثبت قطعی گروهی اسناد
        /// </summary>
        /// <param name="actionDetail">لیست شناسه اسناد انتخاب شده</param>
        /// <returns></returns>
        [HttpPut]
        [Route(VoucherApi.UndoFinalizeVouchersUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutExistingVouchersAsUnfinalized([FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.UndoFinalize, DocumentStatusId.Checked);
        }

        /// <summary>
        /// به روش آسنکرون، نوع مفهومی اسناد پیش نویس مشخص شده را به سند عادی تغییر می دهد
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات تبدیل گروهی</param>
        /// <returns>در صورت وجود خطای اعتبارسنجی، کد وضعیت 400 و
        /// در غیر این صورت، کد وضعیتی 200 (به معنای موفق بودن عملیات) را برمی گرداند</returns>
        // PUT: api/vouchers/normalize
        [HttpPut]
        [Route(VoucherApi.NormalizeVouchersUrl)]
        [AuthorizeRequest(SecureEntity.DraftVoucher, (int)DraftVoucherPermissions.Normalize)]
        public async Task<IActionResult> PutExistingDraftVouchersAsNormalized(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            await _draftRepository.NormalizeVouchersAsync(actionDetail.Items);
            return Ok();
        }

        #endregion

        /// <summary>
        /// به روش آسنکرون، محدوده شماره سندهای قابل دسترسی توسط کاربر جاری را برمی گرداند
        /// </summary>
        /// <returns>اطلاعات محدوده شماره سندها</returns>
        // GET: api/vouchers/range
        [HttpGet]
        [Route(VoucherApi.EnvironmentItemRangeUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVoucherRangeAsync()
        {
            var range = await _repository.GetVoucherRangeInfoAsync();
            return Json(range);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/vouchers/count/by-status
        [HttpGet]
        [Route(VoucherApi.VoucherCountByStatusUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetVouchersCountByStatusIdAsync()
        {
            int itemCount = await _repository.GetCountAsync<VoucherViewModel>(GridOptions);

            return Ok(itemCount);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کل آرتیکل های موجود را برمی گرداند
        /// </summary>
        /// <returns>تعداد کل آرتیکل های موجود</returns>
        // GET: api/vouchers/articles/count
        [HttpGet]
        [Route(VoucherApi.VoucherArticlesCountUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetArticlesCountAsync()
        {
            int itemsCount = await _lineRepository.GetAllArticlesCountAsync();
            return Ok(itemsCount);
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای سند مالی مشخص شده توسط شناسه دیتابیسی اعتبارسنجی می کند
        /// </summary>
        /// <param name="voucherId">شناسه دیتابیسی سطر اطلاعاتی مورد نظر برای حذف</param>
        /// <returns>پیغام خطای به دست آمده از اعتبارسنجی یا رشته خالی در صورت نبود خطا</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int voucherId)
        {
            string message = String.Empty;
            var voucher = await _repository.GetVoucherAsync(voucherId);
            if (voucher == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.Voucher), voucherId);
                return GetGroupActionResult(message, voucher);
            }

            var result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult branchError)
            {
                if (branchError.Value is ErrorViewModel error)
                {
                    message = error.Messages[0];
                }

                return GetGroupActionResult(message, voucher);
            }

            result = CheckedValidationResult(voucher);
            if (result is BadRequestObjectResult statusError)
            {
                if (statusError.Value is ErrorViewModel error)
                {
                    message = error.Messages[0];
                }
            }

            return GetGroupActionResult(message, voucher);
        }

        private async Task<IActionResult> GroupStatusChangeResultAsync(
            IEnumerable<int> items, string action, DocumentStatusId status)
        {
            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            await GroupValidateItemsAsync(items, action, validated, notValidated);
            if (validated.Count > 0)
            {
                var repository = await GetVoucherRepositoryAsync(validated[0]);
                await repository.SetVouchersStatusAsync(validated, status);
            }

            return Ok(notValidated);
        }

        private async Task<IActionResult> GroupConfirmApproveResultAsync(
            IEnumerable<int> items, string action)
        {
            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            await GroupValidateItemsAsync(items, action, validated, notValidated);

            await _repository.SetVouchersConfirmApproveStatusAsync(
                validated, action == AppStrings.GroupConfirmApprove);
            return Ok(notValidated);
        }

        #region Common Voucher/Article Helper Methods

        private async Task<IActionResult> SaveVoucherAsync(int voucherId, VoucherViewModel voucher)
        {
            var result = await VoucherValidationResultAsync(voucher, voucherId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = CheckedValidationResult(voucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (voucher.SaveCount == 0)
            {
                await _repository.SetVoucherDailyNoAsync(voucher);
            }

            var repository = GetVoucherRepository((SubjectType)voucher.SubjectType);
            var outputVoucher = await repository.SaveVoucherAsync(voucher);
            result = (outputVoucher != null)
                ? Ok(outputVoucher)
                : NotFound() as IActionResult;
            return result;
        }

        private async Task<IActionResult> DeleteVoucherAsync(int voucherId)
        {
            var result = await ValidateDeleteResultAsync(voucherId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            var repository = await GetVoucherRepositoryAsync(voucherId);
            await repository.DeleteVoucherAsync(voucherId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private async Task<IActionResult> CheckVoucherAsync(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Check);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = await GetVoucherRepositoryAsync(voucherId);
            await repository.SetVoucherStatusAsync(voucherId, DocumentStatusId.Checked);
            return Ok();
        }

        private async Task<IActionResult> UncheckVoucherAsync(int voucherId)
        {
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoCheck);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = await GetVoucherRepositoryAsync(voucherId);
            await repository.SetVoucherStatusAsync(voucherId, DocumentStatusId.NotChecked);
            return Ok();
        }

        private async Task<IActionResult> DeleteVouchersAsync(ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null || actionDetail.Items.Count == 0)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var repository = await GetVoucherRepositoryAsync(actionDetail.Items[0]);
            return await GroupDeleteResultAsync(actionDetail, repository.DeleteVouchersAsync);
        }

        private async Task<IActionResult> CheckVouchersAsync(ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.Check, DocumentStatusId.Checked);
        }

        private async Task<IActionResult> UncheckVouchersAsync(ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.UndoCheck, DocumentStatusId.NotChecked);
        }

        private async Task<IActionResult> InsertArticleAsync(int voucherId, VoucherLineViewModel article)
        {
            var result =await VoucherLineValidationResultAsync(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            article.FullAccount.DetailAccount =
                (article.FullAccount.DetailAccount != null && article.FullAccount.DetailAccount.Id > 0)
                ? article.FullAccount.DetailAccount
                : null;
            article.FullAccount.CostCenter =
                (article.FullAccount.CostCenter != null && article.FullAccount.CostCenter.Id > 0)
                ? article.FullAccount.CostCenter
                : null;
            article.FullAccount.Project =
                (article.FullAccount.Project != null && article.FullAccount.Project.Id > 0)
                ? article.FullAccount.Project
                : null;
            result = await FullAccountValidationResultAsync(article.FullAccount, _relationRepository);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var lineRepository = await GetLineRepositoryFromVoucherAsync(voucherId);
            var outputLine = await lineRepository.SaveArticleAsync(article);
            return StatusCode(StatusCodes.Status201Created, outputLine);
        }

        private async Task<IActionResult> UpdateArticleAsync(int articleId, VoucherLineViewModel article)
        {
            var result =await VoucherLineValidationResultAsync(article, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = BranchValidationResult(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var lineRepository = await GetLineRepositoryAsync(articleId);
            var outputLine = await lineRepository.SaveArticleAsync(article);
            return JsonReadResult(outputLine);
        }

        private async Task<IActionResult> DeleteArticleAsync(int articleId)
        {
            var result = await ValidateLineDeleteResultAsync(articleId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            var lineRepository = await GetLineRepositoryAsync(articleId);
            await lineRepository.DeleteArticleAsync(articleId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private async Task<IActionResult> DeleteArticlesAsync(ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null || actionDetail.Items.Count == 0)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var lineRepository = await GetLineRepositoryAsync(actionDetail.Items[0]);
            return await GroupDeleteLineResultAsync(actionDetail, lineRepository.DeleteArticlesAsync);
        }

        #endregion

        private async Task GroupValidateItemsAsync(IEnumerable<int> items, string action,
            List<int> validated, List<GroupActionResultViewModel> notValidated)
        {
            foreach (int item in items)
            {
                if (await VoucherActionValidationResultAsync(item, action) is BadRequestObjectResult result)
                {
                    var repository = await GetVoucherRepositoryAsync(item);
                    var voucher = await repository.GetVoucherAsync(item);
                    var error = result.Value as ErrorViewModel;
                    notValidated.Add(GetGroupActionResult(error.Messages[0], voucher));
                }
                else
                {
                    validated.Add(item);
                }
            }
        }

        private IActionResult BasicValidationResult<TModel>(TModel model, string modelType, int modelId = 0)
        {
            if (model == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, modelType));
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            int id = (int)Reflector.GetProperty(model, "Id");
            if (modelId != id)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, modelType));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherValidationResultAsync(VoucherViewModel voucher, int voucherId = 0)
        {
            var result = BasicValidationResult(voucher, AppStrings.Voucher, voucherId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateVoucherNoAsync(voucher))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.VoucherNo));
            }

            if (await _repository.IsDuplicateVoucherDailyNoAsync(voucher))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.DailyNo));
            }

            var fiscalPeriod = await _repository.GetVoucherFiscalPeriodAsync(voucher);
            if (fiscalPeriod == null
                || voucher.Date < fiscalPeriod.StartDate
                || voucher.Date > fiscalPeriod.EndDate)
            {
                return BadRequestResult(_strings.Format(AppStrings.OutOfFiscalPeriodDate));
            }

            if (voucher.Id > 0 && !_repository.CanSaveAsDraftVoucher(voucher))
            {
                return BadRequestResult(_strings[AppStrings.CantSaveAsDraftVoucher]);
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherLineValidationResultAsync(
            VoucherLineViewModel article, int articleId = 0)
        {
            var result = BasicValidationResult(article, AppStrings.VoucherLine, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if ((article.Debit == 0m) && (article.Credit == 0m))
            {
                return BadRequestResult(_strings.Format(AppStrings.ZeroDebitAndCreditNotAllowed));
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequestResult(_strings.Format(AppStrings.DebitAndCreditNotAllowed));
            }

            if (article.SourceAppId.HasValue)
            {
                if(!await _lineRepository.IsAccountBelongsCollectionsCashBankAsync(article.FullAccount.Account.Id))
                {
                    return BadRequestResult(_strings.Format(AppStrings.AccountBelongsCollectionsCashBank));
                }

            }

            return Ok();
        }

        private async Task<IActionResult> SpecialVoucherValidationResultAsync(
            string typeKey, string operationKey, VoucherOriginId origin)
        {
            // Permission check (can't be done with attributes)...
            if (!CanIssueSpecialVoucher(origin))
            {
                string message = _strings.Format(AppStrings.OperationNotAllowed, operationKey);
                return BadRequestResult(message);
            }

            // Rule 1 : Current branch MUST be the highest-level branch in hierarchy...
            if (!await _repository.CanIssueSpecialVoucherAsync(SecurityContext.User.BranchId))
            {
                string message = _strings.Format(AppStrings.CantIssueVoucherFromLowerBranch, typeKey);
                return BadRequestResult(message);
            }

            if (typeKey == AppStrings.ClosingTempAccounts)
            {
                // Rule 2 : Current fiscal period MUST NOT have any unchecked vouchers
                int uncheckedCount = await _repository.GetCountByStatusAsync(DocumentStatusId.NotChecked);
                if (uncheckedCount > 0)
                {
                    return BadRequestResult(_strings[AppStrings.CantIssueClosingVoucherWithUncheckedVouchers]);
                }
            }

            // Rule 3 : Current fiscal period MUST have the closing temp accounts voucher
            if (typeKey == AppStrings.ClosingVoucher
                && !await _repository.IsCurrentSpecialVoucherCheckedAsync(VoucherOriginId.ClosingTempAccounts))
            {
                return BadRequestResult(_strings[AppStrings.ClosingAccountsVoucherNotIssuedOrChecked]);
            }

            return Ok();
        }

        private async Task<GroupActionResultViewModel> ValidateLineDeleteResultAsync(int articleId)
        {
            string message = String.Empty;
            var voucherLine = await _lineRepository.GetArticleAsync(articleId);
            if (voucherLine == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.VoucherLine), articleId);
            }
            else
            {
                var result = BranchValidationResult(voucherLine);
                if (result is BadRequestObjectResult errorResult)
                {
                    message = errorResult.Value.ToString();
                }
            }

            return GetGroupActionResult(message, voucherLine);
        }

        private IActionResult CheckedValidationResult(VoucherViewModel voucher)
        {
            if (voucher.StatusId != (int)DocumentStatusId.NotChecked)
            {
                return BadRequestResult(_strings.Format(AppStrings.CantModifyCheckedDocument, AppStrings.Voucher));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherActionValidationResultAsync(int voucherId, string action)
        {
            var error = await _repository.ValidateVoucherActionAsync(voucherId, action);
            if (error != null)
            {
                return BadRequestResult(error.ErrorMessage);
            }

            if (action == AppStrings.Check)
            {
                int lineCount = await _lineRepository.GetArticleCountAsync<VoucherLineViewModel>(voucherId);
                if (lineCount == 0)
                {
                    return BadRequestResult(_strings.Format(AppStrings.InvalidEmptyVoucherAction, action));
                }
            }

            var voucherInfo = await _repository.GetVoucherInfoAsync(voucherId);
            if (action == AppStrings.UndoCheck && voucherInfo.OriginId == (int)VoucherOriginId.ClosingVoucher)
            {
                bool canUndo = CanUncheckClosingVoucher();
                if (canUndo)
                {
                    return Ok();
                }
                else
                {
                    string message = _strings.Format(AppStrings.OperationNotAllowed, AppStrings.UncheckClosingVoucher);
                    return BadRequestResult(message);
                }
            }
            else
            {
                var result = await ClosingVoucherValidationResultAsync();
                if (result is BadRequestObjectResult)
                {
                    return result;
                }
            }

            return Ok();
        }

        private async Task<IActionResult> ClosingVoucherValidationResultAsync()
        {
            bool isChecked = await _repository.IsCurrentSpecialVoucherCheckedAsync(
                VoucherOriginId.ClosingVoucher);
            if (isChecked)
            {
                return BadRequestResult(_strings[AppStrings.CurrentClosingVoucherIsChecked]);
            }

            return Ok();
        }

        private bool CanUncheckClosingVoucher()
        {
            bool canUncheck = SecurityContext.IsInRole(AppConstants.AdminRoleId);
            if (!canUncheck)
            {
                var permission = new PermissionBriefViewModel(
                    SecureEntity.SpecialVoucher, (int)SpecialVoucherPermissions.UncheckClosingVoucher);
                canUncheck = SecurityContext.HasPermissions(permission);
            }

            return canUncheck;
        }

        private bool CanIssueSpecialVoucher(VoucherOriginId origin)
        {
            int flags = 0;
            switch (origin)
            {
                case VoucherOriginId.OpeningVoucher:
                    flags = (int)SpecialVoucherPermissions.IssueOpeningVoucher;
                    break;
                case VoucherOriginId.ClosingTempAccounts:
                    flags = (int)SpecialVoucherPermissions.IssueClosingTempAccountsVoucher;
                    break;
                case VoucherOriginId.ClosingVoucher:
                    flags = (int)SpecialVoucherPermissions.IssueClosingVoucher;
                    break;
                default:
                    break;
            }

            var permission = new PermissionBriefViewModel()
            {
                EntityName = SecureEntity.SpecialVoucher,
                Flags = flags
            };

            return SecurityContext.IsInRole(AppConstants.AdminRoleId)
                || SecurityContext.HasPermissions(permission);
        }

        private async Task<IActionResult> GroupDeleteLineResultAsync(
            ActionDetailViewModel actionDetail, GroupDeleteAsyncDelegate groupDelete)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            foreach (int item in actionDetail.Items)
            {
                var result = await ValidateLineDeleteResultAsync(item);
                if (result == null)
                {
                    validated.Add(item);
                }
                else
                {
                    notValidated.Add(result);
                }
            }

            if (validated.Count > 0)
            {
                await groupDelete(validated);
            }

            return Ok(notValidated);
        }

        private void Localize(VoucherViewModel voucher)
        {
            if (voucher != null)
            {
                voucher.StatusName = _strings[voucher.StatusName ?? String.Empty];
                voucher.OriginName = _strings[voucher.OriginName ?? String.Empty];
                voucher.TypeName = _strings[voucher.TypeName ?? String.Empty];
                voucher.Description = _strings[voucher.Description ?? String.Empty];
            }
        }

        private void Localize(IEnumerable<VoucherLineViewModel> voucherLines)
        {
            foreach (var voucherLine in voucherLines)
            {
                Localize(voucherLine);
            }
        }

        private void Localize(VoucherLineViewModel voucherLine)
        {
            if (voucherLine != null)
            {
                voucherLine.CurrencyName = _strings[voucherLine.CurrencyName ?? String.Empty];
                voucherLine.Description = _strings[voucherLine.Description ?? String.Empty];
            }
        }

        private async Task<IActionResult> GetVoucherListAsync()
        {
            var repository = GetVoucherRepository();
            var vouchers = await repository.GetVouchersAsync(GridOptions);
            return JsonListResult(vouchers);
        }

        private async Task<IActionResult> GetSingleVoucherAsync(int voucherId)
        {
            var voucher = await _repository.GetVoucherAsync(voucherId, GridOptions);
            Localize(voucher);
            return JsonReadResult(voucher);
        }

        private async Task<IActionResult> GetNewVoucherBySubjectAsync(
            SubjectType subject = SubjectType.Normal)
        {
            bool isChecked = await _repository.IsCurrentSpecialVoucherCheckedAsync(
                VoucherOriginId.ClosingVoucher);
            if (isChecked)
            {
                return BadRequestResult(_strings[AppStrings.CurrentClosingVoucherIsChecked]);
            }

            var repository = GetVoucherRepository(subject);
            var newVoucher = await repository.GetNewVoucherAsync(subject);
            return Json(newVoucher);
        }

        private async Task<IActionResult> GetVoucherByNoBySubjectAsync(
            int voucherNo, SubjectType subject = SubjectType.Normal)
        {
            var voucherByNo = await _repository.GetVoucherByNoAsync(voucherNo, subject);
            Localize(voucherByNo);
            return JsonReadResult(voucherByNo);
        }

        private async Task<IActionResult> GetFirstVoucherByTypeAsync(
            SubjectType subject = SubjectType.Normal)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            var first = !gridOptions.IsEmpty
                ? await _repository.GetFirstVoucherAsync(GridOptions)
                : await _repository.GetFirstVoucherAsync(subject);
            Localize(first);
            return JsonReadResult(first);
        }

        private async Task<IActionResult> GetPreviousVoucherByTypeAsync(
            int currentNo, SubjectType subject = SubjectType.Normal)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            var previous = !gridOptions.IsEmpty
                ? await _repository.GetPreviousVoucherAsync(currentNo, GridOptions)
                : await _repository.GetPreviousVoucherAsync(currentNo, subject);
            Localize(previous);
            return JsonReadResult(previous);
        }

        private async Task<IActionResult> GetNextVoucherByTypeAsync(
            int currentNo, SubjectType subject = SubjectType.Normal)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            var next = !gridOptions.IsEmpty
                ? await _repository.GetNextVoucherAsync(currentNo, GridOptions)
                : await _repository.GetNextVoucherAsync(currentNo, subject);
            Localize(next);
            return JsonReadResult(next);
        }

        private async Task<IActionResult> GetLastVoucherByTypeAsync(
            SubjectType subject = SubjectType.Normal)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            var last = !gridOptions.IsEmpty
                ? await _repository.GetLastVoucherAsync(GridOptions)
                : await _repository.GetLastVoucherAsync(subject);
            Localize(last);
            return JsonReadResult(last);
        }

        private IVoucherRepository GetVoucherRepository()
        {
            var repository = _repository;
            var gridOptions = GridOptions ?? new GridOptions();
            if (gridOptions.Filter != null)
            {
                repository = gridOptions.Filter.ToString().Contains("SubjectType == 1")
                    ? _draftRepository
                    : _repository;
            }

            return repository;
        }

        private async Task<IVoucherRepository> GetVoucherRepositoryAsync(int voucherId)
        {
            var voucherInfo = await _repository.GetVoucherInfoAsync(voucherId);
            SubjectType subject = (SubjectType)voucherInfo.SubjectType;
            return GetVoucherRepository(subject);
        }

        private IVoucherRepository GetVoucherRepository(SubjectType subject)
        {
            return subject == SubjectType.Normal ? _repository : _draftRepository;
        }

        private async Task<IVoucherLineRepository> GetLineRepositoryFromVoucherAsync(int voucherId)
        {
            var voucherInfo = await _repository.GetVoucherInfoAsync(voucherId);
            var subject = (SubjectType)voucherInfo.SubjectType;
            return subject == SubjectType.Draft ? _draftLineRepository : _lineRepository;
        }

        private async Task<IVoucherLineRepository> GetLineRepositoryAsync(int articleId)
        {
            var subject = (SubjectType)await _lineRepository.GetLineSubjectTypeAsync(articleId);
            return subject == SubjectType.Draft ? _draftLineRepository : _lineRepository;
        }

        private readonly IVoucherRepository _repository;
        private readonly IVoucherLineRepository _lineRepository;
        private readonly IDraftVoucherRepository _draftRepository;
        private readonly IDraftVoucherLineRepository _draftLineRepository;
        private readonly IRelationRepository _relationRepository;
    }
}