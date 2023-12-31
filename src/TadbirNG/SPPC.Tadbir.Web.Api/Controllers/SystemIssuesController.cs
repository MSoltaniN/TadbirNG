﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]

    public class SystemIssuesController : ValidatingController<SystemIssueViewModel>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public SystemIssuesController(
            ISystemIssueRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.SystemIssue; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/sys-issues
        [HttpGet]
        [Route(SystemIssueApi.SystemIssuesUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetSystemIssuesAsync()
        {
            var issues = await _repository.GetUserSystemIssuesAsync(SecurityContext.User.Id);
            Localize(issues);
            return Json(issues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/sys-issues/{issueId:min(1)}/summary
        [HttpGet]
        [Route(SystemIssueApi.SystemIssuesSummaryUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetSystemIssueSummariesAsync(int issueId, DateTime from, DateTime to)
        {
            var summaries = await _repository.GetSystemIssueSummaries(issueId, GridOptions, from, to);
            return Json(summaries);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/vouchers/unbalanced
        [HttpGet]
        [Route(SystemIssueApi.UnbalancedVouchers)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetUnbalancedVouchersAsync(DateTime from, DateTime to)
        {
            var vouchers = await _repository.GetUnbalancedVouchersAsync(GridOptions, from, to);
            Localize(vouchers.Items);
            return JsonListResult(vouchers);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/vouchers/no-article
        [HttpGet]
        [Route(SystemIssueApi.VouchersWithNoArticleUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetVouchersWithNoArticleAsync(DateTime from, DateTime to)
        {
            var vouchers = await _repository.GetVouchersWithNoArticleAsync(GridOptions, from, to);
            Localize(vouchers.Items);
            return JsonListResult(vouchers);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // GET: api/vouchers/miss-number
        [HttpGet]
        [Route(SystemIssueApi.MissingVoucherNumbersUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetMissingVoucherNumbersAsync(DateTime from, DateTime to)
        {
            var vouchers = await _repository.GetMissingVoucherNumbersAsync(GridOptions, from, to);
            return JsonListResult(vouchers);
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
        [Route(SystemIssueApi.SystemIssueArticlesUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetSystemIssueArticlesAsync(string issueType, DateTime from, DateTime to)
        {
            var articles = await _repository.GetSystemIssueArticlesAsync(GridOptions, issueType, from, to);
            return JsonListResult(articles);
        }

        private void Localize(IList<SystemIssueViewModel> issues)
        {
            foreach (var item in issues)
            {
                item.Title = _strings[item.Title];
            }
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

        private readonly ISystemIssueRepository _repository;
    }
}