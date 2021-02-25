using System;
using System.Collections.Generic;
using System.Linq;
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
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Inventory;
using SPPC.Tadbir.Web.Api.Extensions;
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
        public VouchersController(
            IVoucherRepository repository,
            IVoucherLineRepository lineRepository,
            IDraftVoucherRepository draftRepository,
            IDraftVoucherLineRepository draftLineRepository,
            IRelationRepository relationRepository,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
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
            var result = await SpecialVoucherValidationResultAsync(AppStrings.OpeningVoucher);
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
            var result = await SpecialVoucherValidationResultAsync(AppStrings.ClosingTempAccounts);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var closingAccountsVoucher = await _repository.GetClosingTempAccountsVoucherAsync();
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
            var result = await SpecialVoucherValidationResultAsync(AppStrings.ClosingTempAccounts);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var closingAccountsVoucher = await _repository.GetPeriodicClosingTempAccountsVoucherAsync(balanceItems);
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
            var result = await ClosingVoucherValidationResultAsync();
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var closingVoucher = await _repository.GetClosingVoucherAsync();
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
            var result = VoucherLineValidationResultAsync(article);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = await FullAccountValidationResult(article.FullAccount, _relationRepository);
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
            var result = VoucherLineValidationResultAsync(article, articleId);
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
            var result = await ValidateLineDeleteResultAsync(articleId);
            if (result != null)
            {
                return BadRequest(result.ErrorMessage);
            }

            var lineRepository = await GetLineRepositoryAsync(articleId);
            await lineRepository.DeleteArticleAsync(articleId);
            return StatusCode(StatusCodes.Status204NoContent);
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
            if (actionDetail == null || actionDetail.Items.Count == 0)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var lineRepository = await GetLineRepositoryAsync(actionDetail.Items[0]);
            return await GroupDeleteLineResultAsync(actionDetail, lineRepository.DeleteArticlesAsync);
        }

        #endregion

        #region Voucher Operations - Single

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
            var result = await ValidateDeleteResultAsync(voucherId);
            if (result != null)
            {
                return BadRequest(result.ErrorMessage);
            }

            var repository = await GetVoucherRepositoryAsync(voucherId);
            await repository.DeleteVoucherAsync(voucherId);
            return StatusCode(StatusCodes.Status204NoContent);
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
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.Check);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = await GetVoucherRepositoryAsync(voucherId);
            await repository.SetVoucherStatusAsync(voucherId, DocumentStatusId.Checked);
            return Ok();
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
            var result = await VoucherActionValidationResultAsync(voucherId, AppStrings.UndoCheck);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = await GetVoucherRepositoryAsync(voucherId);
            await repository.SetVoucherStatusAsync(voucherId, DocumentStatusId.NotChecked);
            return Ok();
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
            await _draftRepository.NormalizeVoucherAsync(voucherId);
            return Ok();
        }

        #endregion

        #region Voucher Operations - Group

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
            if (actionDetail == null || actionDetail.Items.Count == 0)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var repository = await GetVoucherRepositoryAsync(actionDetail.Items[0]);
            return await GroupDeleteResultAsync(actionDetail, repository.DeleteVouchersAsync);
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
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.Check, DocumentStatusId.Checked);
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
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            return await GroupStatusChangeResultAsync(
                actionDetail.Items, AppStrings.UndoCheck, DocumentStatusId.NotChecked);
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
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
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            await _draftRepository.NormalizeVouchersAsync(actionDetail.Items);
            return Ok();
        }

        #endregion

        #region System Issue Resources

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/vouchers/no-article
        [HttpGet]
        [Route(VoucherApi.VoucherWithNoArticleUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetVouchersWithNoArticleAsync(DateTime from, DateTime to)
        {
            var (vouchers, itemCount) = await _repository.GetVouchersWithNoArticleAsync(GridOptions, from, to);
            SetItemCount(itemCount);
            Localize(vouchers.ToArray());
            SetRowNumbers(vouchers);
            return Json(vouchers);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/vouchers/unbalanced
        [HttpGet]
        [Route(VoucherApi.UnbalancedVouchers)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetUnbalancedVouchersAsync(DateTime from, DateTime to)
        {
            var (vouchers, itemCount) = await _repository.GetUnbalancedVouchersAsync(GridOptions, from, to);
            SetItemCount(itemCount);
            Localize(vouchers.ToArray());
            SetRowNumbers(vouchers);
            return Json(vouchers);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/vouchers/miss-number
        [HttpGet]
        [Route(VoucherApi.MissingVoucherNumberUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetMissingVoucherNumbersAsync(DateTime from, DateTime to)
        {
            var (voucherNumbers, itemCount) = await _repository.GetMissingVoucherNumbersAsync(GridOptions, from, to);
            SetItemCount(itemCount);
            SetRowNumbers(voucherNumbers);
            return Json(voucherNumbers);
        }

        /// <summary>
        /// به روش آسنکرون، آرتیکل های مالی دارای اشکال داده شده را برمی گرداند
        /// </summary>
        /// <param name="issueType">نوع اشکال سیستمی مورد نظر برای آرتیکل های مالی</param>
        /// <param name="from">تاریخ ابتدای دوره گزارشگیری</param>
        /// <param name="to">تاریخ انتهای دوره گزارشگیری</param>
        /// <returns>اطلاعات نمایشی آرتیکل های مالی دارای مشکل داده شده</returns>
        // GET: api/vouchers/articles/sys-issue/{issueType}
        [HttpGet]
        [Route(VoucherApi.SystemIssueArticlesUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetSystemIssueArticlesAsync(string issueType, DateTime from, DateTime to)
        {
            var (articles, itemCount) = await _lineRepository.GetSystemIssueArticlesAsync(
                GridOptions, issueType, from, to);
            SetItemCount(itemCount);
            if (issueType != "invalid-acc")
            {
                SetRowNumbers(articles);
            }

            return Json(articles);
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
                message = branchError.Value.ToString();
                return GetGroupActionResult(message, voucher);
            }

            result = CheckedValidationResult(voucher);
            if (result is BadRequestObjectResult statusError)
            {
                message = statusError.Value.ToString();
            }

            return GetGroupActionResult(message, voucher);
        }

        private static bool IsVoucherMainAction(string action)
        {
            return action == AppStrings.Check
                || action == AppStrings.Confirm
                || action == AppStrings.Approve
                || action == AppStrings.Finalize
                || action == AppStrings.UndoFinalize;
        }

        private async Task<IActionResult> GroupStatusChangeResultAsync(
            IEnumerable<int> items, string action, DocumentStatusId status)
        {
            var validated = new List<int>();
            var notValidated = new List<GroupActionResultViewModel>();
            foreach (int item in items)
            {
                var result = await _repository.ValidateVoucherActionAsync(item, action);
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
            bool isConfirmed = (action == AppStrings.GroupConfirmApprove);
            foreach (int item in items)
            {
                var result = await _repository.ValidateVoucherActionAsync(item, action);
                if (result == null)
                {
                    validated.Add(item);
                }
                else
                {
                    notValidated.Add(result);
                }
            }

            await _repository.SetVouchersConfirmApproveStatusAsync(validated, isConfirmed);
            return Ok(notValidated);
        }

        private IActionResult BasicValidationResult<TModel>(TModel model, string modelType, int modelId = 0)
        {
            if (model == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, modelType));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = (int)Reflector.GetProperty(model, "Id");
            if (modelId != id)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, modelType));
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
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.VoucherNo));
            }

            if (await _repository.IsDuplicateVoucherDailyNoAsync(voucher))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.DailyNo));
            }

            var fiscalPeriod = await _repository.GetVoucherFiscalPeriodAsync(voucher);
            if (fiscalPeriod == null
                || voucher.Date < fiscalPeriod.StartDate
                || voucher.Date > fiscalPeriod.EndDate)
            {
                return BadRequest(_strings.Format(AppStrings.OutOfFiscalPeriodDate));
            }

            if (voucher.Id > 0 && !_repository.CanSaveAsDraftVoucher(voucher))
            {
                return BadRequest(_strings[AppStrings.CantSaveAsDraftVoucher]);
            }

            return Ok();
        }

        private IActionResult VoucherLineValidationResultAsync(
            VoucherLineViewModel article, int articleId = 0)
        {
            var result = BasicValidationResult(article, AppStrings.VoucherLine, articleId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if ((article.Debit > 0m) && (article.Credit > 0m))
            {
                return BadRequest(_strings.Format(AppStrings.DebitAndCreditNotAllowed));
            }

            return Ok();
        }

        private async Task<IActionResult> ClosingVoucherValidationResultAsync()
        {
            var result = await SpecialVoucherValidationResultAsync(AppStrings.ClosingVoucher);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            // Current fiscal period MUST have the closing temp accounts voucher
            if (!await _repository.IsCurrentSpecialVoucherCheckedAsync(
                VoucherOriginId.ClosingTempAccounts))
            {
                return BadRequest(_strings[AppStrings.ClosingAccountsVoucherNotIssuedOrChecked]);
            }

            return Ok();
        }

        private async Task<IActionResult> SpecialVoucherValidationResultAsync(string typeKey)
        {
            // Rule 1 : Current branch MUST be the highest-level branch in hierarchy...
            if (!await _repository.CanIssueSpecialVoucherAsync(SecurityContext.User.BranchId))
            {
                string message = _strings.Format(AppStrings.CantIssueVoucherFromLowerBranch, typeKey);
                return BadRequest(message);
            }

            if (typeKey == AppStrings.ClosingTempAccounts)
            {
                // Rule 2 : Current fiscal period MUST NOT have any unchecked vouchers
                int uncheckedCount = await _repository.GetCountByStatusAsync(DocumentStatusId.NotChecked);
                if (uncheckedCount > 0)
                {
                    return BadRequest(_strings[AppStrings.CantIssueClosingVoucherWithUncheckedVouchers]);
                }
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
                return BadRequest(_strings.Format(AppStrings.CantModifyCheckedDocument, AppStrings.Voucher));
            }

            return Ok();
        }

        private async Task<IActionResult> VoucherActionValidationResultAsync(int voucherId, string action)
        {
            var error = await _repository.ValidateVoucherActionAsync(voucherId, action);
            if (error != null)
            {
                return BadRequest(error.ErrorMessage);
            }

            if (IsVoucherMainAction(action))
            {
                int lineCount = await _lineRepository.GetArticleCountAsync<VoucherLineViewModel>(voucherId);
                if (lineCount == 0)
                {
                    return BadRequest(_strings.Format(AppStrings.InvalidEmptyVoucherAction, action));
                }
            }

            return Ok();
        }

        private async Task<IActionResult> GroupDeleteLineResultAsync(
            ActionDetailViewModel actionDetail, GroupDeleteAsyncDelegate groupDelete)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
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

        private void Localize(IEnumerable<VoucherViewModel> vouchers)
        {
            foreach (var voucher in vouchers)
            {
                Localize(voucher);
            }
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
            Localize(vouchers.Items);
            return JsonListResult(vouchers);
        }

        private async Task<IActionResult> GetSingleVoucherAsync(int voucherId)
        {
            var voucher = await _repository.GetVoucherAsync(voucherId);
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
                return BadRequest(_strings[AppStrings.CurrentClosingVoucherIsChecked]);
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
            var first = await _repository.GetFirstVoucherAsync(subject);
            Localize(first);
            return JsonReadResult(first);
        }

        private async Task<IActionResult> GetPreviousVoucherByTypeAsync(
            int currentNo, SubjectType subject = SubjectType.Normal)
        {
            var previous = await _repository.GetPreviousVoucherAsync(currentNo, subject);
            Localize(previous);
            return JsonReadResult(previous);
        }

        private async Task<IActionResult> GetNextVoucherByTypeAsync(
            int currentNo, SubjectType subject = SubjectType.Normal)
        {
            var next = await _repository.GetNextVoucherAsync(currentNo, subject);
            Localize(next);
            return JsonReadResult(next);
        }

        private async Task<IActionResult> GetLastVoucherByTypeAsync(
            SubjectType subject = SubjectType.Normal)
        {
            var last = await _repository.GetLastVoucherAsync(subject);
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
            SubjectType subject = (SubjectType)await _repository.GetSubjectTypeAsync(voucherId);
            return GetVoucherRepository(subject);
        }

        private IVoucherRepository GetVoucherRepository(SubjectType subject)
        {
            return subject == SubjectType.Normal ? _repository : _draftRepository;
        }

        private async Task<IVoucherLineRepository> GetLineRepositoryFromVoucherAsync(int voucherId)
        {
            var subject = (SubjectType)await _repository.GetSubjectTypeAsync(voucherId);
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