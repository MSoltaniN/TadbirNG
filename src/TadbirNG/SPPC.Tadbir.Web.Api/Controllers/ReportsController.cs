using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
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
        /// <param name="sysRepository"></param>
        /// <param name="system"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public ReportsController(IReportSystemRepository sysRepository,
            ISystemConfigRepository system, IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _sysRepository = sysRepository;
            _configRepository = system;
        }

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
            var metadata = await _sysRepository.GetReportMetadataByViewAsync(viewId);
            return JsonReadResult(metadata);
        }

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

        private readonly IReportSystemRepository _sysRepository;
        private readonly ISystemConfigRepository _configRepository;
    }
}