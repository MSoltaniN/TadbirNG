﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class ReportsController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sysRepository"></param>
        /// <param name="system"></param>
        /// <param name="authorize"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public ReportsController(IReportRepository repository, IReportSystemRepository sysRepository,
            ISystemConfigRepository system, IAuthorizeRequest authorize,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
            _sysRepository = sysRepository;
            _configRepository = system;
            _authorize = authorize;
        }

        #region Report Management API

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/sys/tree
        [HttpGet]
        [Route(ReportApi.ReportsHierarchyUrl)]
        public async Task<IActionResult> GetReportTreeAsync()
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var tree = await _sysRepository.GetReportTreeAsync(localeId);
            return Json(tree);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/reports/sys/view/{viewId:min(1)}
        [HttpGet]
        [Route(ReportApi.ReportsByViewUrl)]
        public async Task<IActionResult> GetReportTreeByViewAsync(int viewId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var tree = await _sysRepository.GetReportTreeByViewAsync(localeId, viewId);
            return Json(tree);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="subsysId"></param>
        /// <returns></returns>
        // GET: api/reports/sys/subsys/{subsysId:min(1)}
        [HttpGet]
        [Route(ReportApi.ReportsBySubsystemUrl)]
        public async Task<IActionResult> GetReportTreeBySubsystemAsync(int subsysId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var tree = await _sysRepository.GetReportTreeBySubsystemAsync(localeId, subsysId);
            return Json(tree);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        // GET: api/reports/sys/{reportId:min(1)}
        [HttpGet]
        [Route(ReportApi.ReportUrl)]
        public async Task<IActionResult> GetReportAsync(int reportId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var report = await _sysRepository.GetReportAsync(reportId, localeId);
            Localize(report);
            return JsonReadResult(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        // GET: api/reports/sys/{reportId:min(1)}/design
        [HttpGet]
        [Route(ReportApi.ReportDesignUrl)]
        public async Task<IActionResult> GetReportDesignAsync(int reportId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var reportDesign = await _sysRepository.GetReportDesignAsync(reportId, localeId);
            return JsonReadResult(reportDesign);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ReportApi.ReportsByViewDefaultUrl)]
        public async Task<IActionResult> GetDefaultReportByViewAsync(int viewId)
        {
            var report = await _sysRepository.GetDefaultReportByViewAsync(viewId);
            return JsonReadResult(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ReportApi.ReportsByViewQuickReportUrl)]
        public async Task<IActionResult> GetQuickReportByViewAsync(int viewId)
        {
            var report = await _sysRepository.GetQuickReportByViewAsync(viewId);
            return JsonReadResult(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        // POST: api/reports/sys
        [HttpPost]
        [Route(ReportApi.ReportsUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)UserReportPermissions.Save)]
        public async Task<IActionResult> PostNewUserReportAsync([FromBody] LocalReportViewModel report)
        {
            var result = await ReportValidationResultAsync(report);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            report.LocaleId = await GetCurrentLocaleIdAsync();
            await _sysRepository.SaveUserReportAsync(report);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        // PUT: api/reports/sys/{reportId:min(1)}
        [HttpPut]
        [Route(ReportApi.ReportUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)UserReportPermissions.Save)]
        public async Task<IActionResult> PutModifiedUserReportAsync(
            int reportId, [FromBody] LocalReportViewModel report)
        {
            var result = await ReportValidationResultAsync(report, reportId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            report.LocaleId = await GetCurrentLocaleIdAsync();
            await _sysRepository.SaveUserReportAsync(report);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        // PUT: api/reports/sys/{reportId:min(1)}/caption
        [HttpPut]
        [Route(ReportApi.ReportCaptionUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)UserReportPermissions.Save)]
        public async Task<IActionResult> PutModifiedUserReportCaptionAsync(
            int reportId, [FromBody] LocalReportViewModel report)
        {
            var result = await ReportValidationResultAsync(report, reportId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            report.LocaleId = await GetCurrentLocaleIdAsync();
            await _sysRepository.SetUserReportCaptionAsync(report);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(ReportApi.ReportDefaultUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)UserReportPermissions.SetDefault)]
        public async Task<IActionResult> PutExistingReportAsDefaultAsync(int reportId)
        {
            await _sysRepository.SetReportAsDefaultAsync(reportId);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        // DELETE: api/reports/sys/{reportId:min(1)}
        [HttpDelete]
        [Route(ReportApi.ReportUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)UserReportPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingUserReportAsync(int reportId)
        {
            var summary = await _sysRepository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequestResult(_strings.Format(AppStrings.CantModifySystemReport));
            }

            await _sysRepository.DeleteUserReportAsync(reportId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="qr"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        // PUT: api/reports/sys/quickreport/{unit:min(1)}
        [HttpPut]
        [Route(ReportApi.EnvironmentQuickReportUrl)]
        public async Task<IActionResult> PutEnvironmentUserQuickReportAsync(
            [FromBody] QuickReportConfig qr, int unit)
        {
            var builder = new QuickReportBuilder()
            {
                Template = await GetQuickReportTemplateAsync(),
                Language = GetPrimaryRequestLanguage()
            };
            var quickReport = builder.Build(qr, unit);
            await _configRepository.SaveQuickReportConfigAsync(SecurityContext.User.Id, qr);

            var jsonData = quickReport.SaveToJsonString();

            return Ok(new { designJson = jsonData, outOfPage = false });
        }

        #endregion

        #region Business Reports API

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        // GET: api/reports/metadata/{viewId:min(1)}
        [HttpGet]
        [Route(ReportApi.ReportMetadataByViewUrl)]
        public async Task<IActionResult> GetReportMetadataByViewAsync(int viewId)
        {
            var metadata = await _repository.GetReportMetadataByViewAsync(viewId);
            return JsonReadResult(metadata);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/voucher/sum-by-date
        [HttpGet]
        [Route(ReportApi.EnvironmentVoucherSummaryByDateUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.Print)]
        public async Task<IActionResult> GetEnvironmentVoucherSummaryByDateAsync()
        {
            int itemCount = await _repository.GetVoucherSummaryByDateCountAsync(GridOptions);
            SetItemCount(itemCount);
            var report = await _repository.GetVoucherSummaryByDateReportAsync(GridOptions);
            Localize(report);
            return Json(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/voucher/{voucherNo:min(1)}/std-form
        [HttpGet]
        [Route(ReportApi.VoucherStandardFormUrl)]
        public async Task<IActionResult> GetStandardVoucherFormAsync(int voucherNo)
        {
            return await GetStandardFormAsync(voucherNo);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/voucher/{voucherNo:min(1)}/std-form-detail
        [HttpGet]
        [Route(ReportApi.VoucherStandardFormWithDetailUrl)]
        public async Task<IActionResult> GetStandardVoucherFormWithDetailAsync(int voucherNo)
        {
            return await GetStandardFormAsync(voucherNo, true);
        }

        #endregion

        private async Task<IActionResult> GetStandardFormAsync(int voucherNo, bool withDetail = false)
        {
            var standardForm = await _repository.GetStandardVoucherFormAsync(voucherNo, withDetail);
            if (standardForm == null)
            {
                string message = _strings.Format(
                    AppStrings.ItemByNumberNotFound, AppStrings.Voucher, voucherNo.ToString());
                return BadRequestResult(message);
            }

            var result = GetAuthorizationResult((SubjectType)standardForm.SubjectType);
            if (result != null)
            {
                return result;
            }

            Localize(standardForm);
            return JsonReadResult(standardForm);
        }

        private async Task<int> GetCurrentLocaleIdAsync()
        {
            var localCode = GetPrimaryRequestLanguage();
            return await _configRepository.GetLocaleIdAsync(localCode);
        }

        private async Task<LocalReportViewModel> GetQuickReportTemplateAsync()
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var localReport = await _sysRepository.GetQuickReportTemplateAsync(localeId);
            return localReport;
        }

        private void Localize(IList<VoucherSummaryViewModel> report)
        {
            var now = DateTime.Now;
            var languages = GetPrimaryRequestLanguage();
            if (languages == "fa")
            {
                Array.ForEach(report.ToArray(),
                    summary => summary.Date = JalaliDateTime
                        .FromDateTime(now.Parse(summary.Date, false))
                        .ToShortDateString());
            }

            Array.ForEach(report.ToArray(), summary =>
            {
                summary.BalanceStatus = _strings[summary.BalanceStatus];
                summary.CheckStatus = _strings[summary.CheckStatus];
                summary.Origin = _strings[summary.Origin];
            });
        }

        private void Localize(StandardVoucherViewModel standardVoucher)
        {
            if (standardVoucher == null)
            {
                return;
            }

            var now = DateTime.Now;
            var languages = GetPrimaryRequestLanguage();
            if (languages == "fa")
            {
                standardVoucher.Date = JalaliDateTime
                    .FromDateTime(now.Parse(standardVoucher.Date, false))
                    .ToShortDateString();
            }
        }

        private void Localize(PrintInfoViewModel report)
        {
            if (report != null)
            {
                foreach (var param in report.Parameters)
                {
                    param.CaptionKey = _strings[param.CaptionKey];
                    param.DescriptionKey = _strings[param.CaptionKey];      // Temporary fix
                }
            }
        }

        private async Task<IActionResult> ReportValidationResultAsync(
            LocalReportViewModel report, int reportId = 0)
        {
            if (report == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserReport));
            }

            if (report.ReportId == 0)
            {
                return BadRequestResult(_strings.Format(AppStrings.SourceReportIsRequired));
            }

            int localeId = await GetCurrentLocaleIdAsync();
            if (await _sysRepository.IsDuplicateReportCaptionAsync(localeId, report))
            {
                return BadRequestResult(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.ReportCaption));
            }

            if (reportId == 0)
            {
                return Ok();
            }

            if (report.ReportId != reportId)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.UserReport));
            }

            var summary = await _sysRepository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequestResult(_strings.Format(AppStrings.CantModifySystemReport));
            }

            return Ok();
        }

        private IActionResult GetAuthorizationResult(SubjectType subject)
        {
            var permission = new PermissionBriefViewModel();
            if (subject == SubjectType.Normal)
            {
                permission.EntityName = SecureEntity.Voucher;
                permission.Flags = (int)VoucherPermissions.Print;
            }
            else
            {
                permission.EntityName = SecureEntity.DraftVoucher;
                permission.Flags = (int)DraftVoucherPermissions.Print;
            }

            var permissions = new PermissionBriefViewModel[] { permission };
            _authorize.SetRequiredPermissions(permissions);
            return _authorize.GetAuthorizationResult(Request);
        }

        private readonly IReportRepository _repository;
        private readonly IReportSystemRepository _sysRepository;
        private readonly ISystemConfigRepository _configRepository;
        private readonly IAuthorizeRequest _authorize;
    }
}