using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class ReportsController : ApiControllerBase
    {
        public ReportsController(IReportRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/reports/sys/tree
        [Route(ReportApi.ReportsHierarchyUrl)]
        public async Task<IActionResult> GetReportTreeAsync()
        {
            string localeCode = GetAcceptLanguages().Substring(0, 2);
            var tree = await _repository.GetReportTreeAsync(localeCode);
            return Json(tree);
        }

        // GET: api/reports/sys/{reportId:min(1)}
        [Route(ReportApi.ReportUrl)]
        public async Task<IActionResult> GetReportAsync(int reportId)
        {
            string localeCode = GetAcceptLanguages().Substring(0, 2);
            var report = await _repository.GetReportAsync(reportId, localeCode);
            Localize(report);
            return JsonReadResult(report);
        }

        // GET: api/reports/sys/{reportId:min(1)}/design
        [Route(ReportApi.ReportDesignUrl)]
        public async Task<IActionResult> GetReportDesignAsync(int reportId)
        {
            string localeCode = GetAcceptLanguages().Substring(0, 2);
            var reportDesign = await _repository.GetReportDesignAsync(reportId, localeCode);
            return JsonReadResult(reportDesign);
        }

        // POST: api/reports/sys
        [HttpPost]
        [Route(ReportApi.ReportsUrl)]
        public async Task<IActionResult> PostNewUserReportAsync([FromBody] LocalReportViewModel report)
        {
            if (report == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserReport));
            }

            if (report.LocaleId == 0)
            {
                return BadRequest(_strings.Format(AppStrings.LocaleIsRequired));
            }

            if (report.ReportId == 0)
            {
                return BadRequest(_strings.Format(AppStrings.SourceReportIsRequired));
            }

            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.SaveUserReportAsync(report);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/reports/sys/{reportId:min(1)}
        [HttpPut]
        [Route(ReportApi.ReportUrl)]
        public async Task<IActionResult> PutModifiedUserReportAsync(
            int reportId, [FromBody] LocalReportViewModel report)
        {
            if (report == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserReport));
            }

            if (report.ReportId != reportId)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.UserReport));
            }

            var summary = await _repository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifySystemReport));
            }

            await _repository.SaveUserReportAsync(report);
            return Ok();
        }

        // PUT: api/reports/sys/{reportId:min(1)}/caption
        [HttpPut]
        [Route(ReportApi.ReportCaptionUrl)]
        public async Task<IActionResult> PutModifiedUserReportCaptionAsync(
            int reportId, [FromBody] LocalReportViewModel report)
        {
            if (report == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserReport));
            }

            if (report.ReportId != reportId)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.UserReport));
            }

            var summary = await _repository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifySystemReport));
            }

            await _repository.SetUserReportCaptionAsync(report);
            return Ok();
        }

        // DELETE: api/reports/sys/{reportId:min(1)}
        [HttpDelete]
        [Route(ReportApi.ReportUrl)]
        public async Task<IActionResult> DeleteExistingUserReportAsync(int reportId)
        {
            var summary = await _repository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifySystemReport));
            }

            await _repository.DeleteUserReportAsync(reportId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // GET: api/reports/voucher/sum-by-date
        [Route(ReportApi.EnvironmentVoucherSummaryByDateUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVoucherSummaryByDateAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            int itemCount = await _repository.GetVoucherSummaryByDateCountAsync(GridOptions);
            SetItemCount(itemCount);
            var report = await _repository.GetVoucherSummaryByDateReportAsync(GridOptions);
            Localize(report);
            return Json(report);
        }

        // GET: api/reports/voucher/std-form
        [Route(ReportApi.VoucherStandardFormUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetStandardVoucherFormAsync()
        {
            var standardForm = await _repository.GetStandardVoucherFormAsync(GridOptions);
            Localize(standardForm);
            return JsonReadResult(standardForm);
        }

        // GET: api/reports/voucher/std-form-detail
        [Route(ReportApi.VoucherStandardFormWithDetailUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetStandardVoucherFormWithDetailAsync()
        {
            var formWithDetail = await _repository.GetStandardVoucherFormAsync(GridOptions, true);
            Localize(formWithDetail);
            return JsonReadResult(formWithDetail);
        }

        private void Localize(IList<VoucherSummaryViewModel> report)
        {
            var now = DateTime.Now;
            var languages = GetAcceptLanguages();
            if (languages.StartsWith("fa"))
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
            var languages = GetAcceptLanguages();
            if (languages.StartsWith("fa"))
            {
                standardVoucher.Date = JalaliDateTime
                    .FromDateTime(now.Parse(standardVoucher.Date, false))
                    .ToShortDateString();
            }
        }

        private void Localize(ReportViewModel report)
        {
            if (report != null)
            {
                if (report.ResourceKeys != null)
                {
                    var keys = report.ResourceKeys.Split(',');
                    Array.ForEach(keys, key => report.ResourceMap.Add(key, _strings[key]));
                }
            }
        }

        private void Localize(PrintInfoViewModel report)
        {
            if (report != null)
            {
                foreach (var param in report.Parameters)
                {
                    param.CaptionKey = _strings[param.CaptionKey];
                    param.DescriptionKey = _strings[param.DescriptionKey];
                }
            }
        }

        private readonly IReportRepository _repository;
    }
}