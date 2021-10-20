﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.Table;

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
        public IActionResult PutEnvironmentUserQuickReport([FromBody] QuickReportConfig qr, int unit)
        {
            StiReport quickReport = new StiReport();
            StiReport quickReportTemplate = new StiReport();
            bool fitToPage = qr.ReportPageSetting.ColumnFitPage;
            bool outOf = false;
            quickReport.ReportUnit = StiReportUnitType.Inches;
            var dataSourceName = "root";
            string reportTemplate = string.Empty;
            string reportLang = GetPrimaryRequestLanguage();

            // load template for adding styles
            var qtemplate = GetQuickReportTemplateAsync().Result;
            if (qtemplate != null && !string.IsNullOrEmpty(qtemplate.Template))
            {
                reportTemplate = qtemplate.Template;
                quickReportTemplate.LoadFromJson(reportTemplate);
            }
            else
            {
                string templateName = "SPPC.Tadbir.Web.Api.Resources.Reporting.Template.QuickReport.A4.Rtl.mrt";
                if (reportLang != "fa")
                {
                    templateName = "SPPC.Tadbir.Web.Api.Resources.Reporting.Template.QuickReport.A4.Rtl.mrt";
                }

                using (StreamReader reader = new StreamReader(
                    typeof(Program).Assembly.GetManifestResourceStream(templateName)))
                {
                    reportTemplate = reader.ReadToEnd();
                }

                quickReportTemplate.LoadFromString(reportTemplate);
            }

            int inchValue = unit;
            quickReport.Styles.AddRange(quickReportTemplate.Styles);

            // load template for adding styles
            quickReport = SetPageSetting(quickReport, qr);
            quickReport = CreateReportFooterBand(quickReport, quickReportTemplate);
            quickReport = CreateReportHeaderBand(quickReport, quickReportTemplate);

            bool createParameterHeader = qr.Parameters != null && qr.Parameters.Count > 0;

            quickReport = CreatePageHeaderBand(quickReport, quickReportTemplate, createParameterHeader);
            quickReport = CreatePageFooterBand(quickReport, quickReportTemplate);
            if (qr.Parameters != null)
            {
                quickReport = CreateReportParametersBand(qr, quickReport, quickReportTemplate);
            }

            quickReport = CreateHeaderBand(quickReport, qr.Columns, inchValue, dataSourceName, quickReportTemplate, reportLang, fitToPage);
            quickReport = CreateDataBand(quickReport, qr, dataSourceName, quickReportTemplate, reportLang, inchValue, fitToPage);
            quickReport = FillLocalVariables(quickReport, qr.Title);

            ////SettingsController settingsController = new SettingsController();
            ////settingsController.PutModifiedQReportSettingsByUserAsync(this.SecurityContext.User.Id, qr);
            _configRepository.SaveQuickReportConfigAsync(this.SecurityContext.User.Id, qr);

            var jsonData = quickReport.SaveToJsonString();

            return Ok(new { designJson = jsonData, outOfPage = outOf });
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

        #region Quick Report Methods

        private static StiReport CreateReportHeaderBand(StiReport report, StiReport reportTemplate)
        {
            StiReportTitleBand reportHeader = new StiReportTitleBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiReportTitleBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiReportTitleBand));
                reportHeader = (StiReportTitleBand)component;
                reportHeader = (StiReportTitleBand)ChangeLeftPositions(report, reportHeader, ReportBandType.ReportHeader, reportTemplate);
            }

            report.Pages[0].Components.Add(reportHeader);

            return report;
        }

        private enum ReportBandType
        {
            ReportHeader,
            ReportFooter,
            PageHeader,
            PageFooter
        }

        private const double LandscapeWidth = 11.69;
        private const double PortraitWidth = 8.27;

        private static StiReport FillLocalVariables(StiReport report, string header)
        {
            report.Dictionary.Variables.Add("vReportFirstTitle", string.Empty);
            report.Dictionary.Variables.Add("vReportSummaryTitle", string.Empty);
            report.Dictionary.Variables.Add("vReportTitle", header);

            return report;
        }

        private static StiComponent ChangeLeftPositions(StiReport stiReport, StiComponent component, ReportBandType reportBandType, StiReport reportTemplate)
        {
            StiComponentsCollection collection = new StiComponentsCollection();
            double diff = 0;

            switch (reportBandType)
            {
                case ReportBandType.PageFooter:
                    collection = ((StiPageFooterBand)component).Components;
                    break;
                case ReportBandType.ReportHeader:
                    collection = ((StiReportTitleBand)component).Components;
                    break;
                case ReportBandType.ReportFooter:
                    collection = ((StiReportSummaryBand)component).Components;
                    break;
                case ReportBandType.PageHeader:
                    collection = ((StiPageHeaderBand)component).Components;
                    break;
            }

            if (reportTemplate.Pages[0].Orientation == StiPageOrientation.Portrait && stiReport.Pages[0].Orientation == StiPageOrientation.Landscape)
            {
                diff = 1;
            }
            else if (reportTemplate.Pages[0].Orientation == StiPageOrientation.Landscape && stiReport.Pages[0].Orientation == StiPageOrientation.Portrait)
            {
                diff = -1;
            }

            if (diff != 0)
            {
                foreach (StiComponent stiComponent in collection)
                {
                    var newLeft = stiComponent.Left;
                    if (diff > 0)
                    {
                        newLeft = (LandscapeWidth * stiComponent.Left) / PortraitWidth;
                    }

                    if (diff < 0)
                    {
                        newLeft = (PortraitWidth * stiComponent.Left) / LandscapeWidth;
                    }

                    stiComponent.Left = newLeft;
                }
            }

            return component;
        }

        private static StiReport CreateReportFooterBand(StiReport report, StiReport reportTemplate)
        {
            StiReportSummaryBand reportFooter = new StiReportSummaryBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiReportSummaryBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiReportSummaryBand));
                reportFooter = (StiReportSummaryBand)component;
                reportFooter = (StiReportSummaryBand)ChangeLeftPositions(report, reportFooter, ReportBandType.ReportFooter, reportTemplate);
            }

            report.Pages[0].Components.Add(reportFooter);
            return report;
        }

        private static StiReport SetPageWidth(StiReport report, QuickReportConfig quickReportViewModel, int inchValue, out bool outOfPage)
        {
            double width = report.Pages[0].Width;
            outOfPage = false;
            int gridWidth = quickReportViewModel.Columns.Where(c => c.Visible).Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, inchValue);

            // check page width in portrait
            if (tableWidth > report.Pages[0].Width)
            {
                report.Pages[0].Orientation = StiPageOrientation.Landscape;
            }

            // check page width in landescape
            if (tableWidth > report.Pages[0].Width)
            {
                outOfPage = true;
            }

            return report;
        }

        private static StiReport SetPageSetting(StiReport report, QuickReportConfig quickReportViewModel)
        {
            switch (quickReportViewModel.ReportPageSetting.PageSize)
            {
                case "A4":
                    report.Pages[0].Width = 8.27;
                    report.Pages[0].Height = 11.69;
                    break;
            }

            switch (quickReportViewModel.ReportPageSetting.PageOrientation)
            {
                case "Portrait":
                    report.Pages[0].Orientation = StiPageOrientation.Portrait;
                    break;
                case "Landscape":
                    report.Pages[0].Orientation = StiPageOrientation.Landscape;
                    break;
            }

            report.Pages[0].Margins.Left = quickReportViewModel.ReportPageSetting.MarginLeft;
            report.Pages[0].Margins.Right = quickReportViewModel.ReportPageSetting.MarginRight;
            report.Pages[0].Margins.Top = quickReportViewModel.ReportPageSetting.MarginTop;
            report.Pages[0].Margins.Bottom = quickReportViewModel.ReportPageSetting.MarginBottom;

            return report;
        }

        private static StiReport CreateReportParametersBand(QuickReportConfig quickReportViewModel, StiReport report, StiReport reportTemplate)
        {
            // read tblParameter from reportTemplate
            StiTable table = new StiTable();
            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiTable)))
            {
                table = (StiTable)componentsList.First(p => p.GetType() == typeof(StiTable));
            }

            string titleStyle = string.Empty, valueStyle = string.Empty;
            if (!string.IsNullOrEmpty(table.Components[0].ComponentStyle))
            {
                titleStyle = table.Components[0].ComponentStyle;
            }

            if (!string.IsNullOrEmpty(table.Components[1].ComponentStyle))
            {
                titleStyle = table.Components[1].ComponentStyle;
            }

            StiTableCell titleCell = (StiTableCell)table.Components[0].Clone(true);
            StiTableCell valueCell = (StiTableCell)table.Components[1].Clone(true);

            table.RowCount = (int)Math.Ceiling(((double)quickReportViewModel.Parameters.Count * 2 / table.ColumnCount));
            var ph = (StiPageHeaderBand)report.Pages[0].Components.ToList().Last(p => p.GetType() == typeof(StiPageHeaderBand));
            ph.Components.Clear();

            StiPanel panel = new StiPanel
            {
                CanGrow = true,
                Width = report.Pages[0].Width,
                PrintOn = table.PrintOnAllPages ? StiPrintOnType.AllPages : StiPrintOnType.OnlyFirstPage
            };
            panel.Components.Add(table);
            ph.Components.Add(panel);
            table.CreateCell();
            int i = 0;
            foreach (var param in quickReportViewModel.Parameters)
            {
                table.Components[i] = (StiTableCell)titleCell.Clone(true);
                StiTableCell headerCell = table.Components[i] as StiTableCell;
                headerCell.Text.Value = param.CaptionKey;
                headerCell.Name = "cell" + i;
                headerCell.Linked = true;
                i++;
                table.Components[i] = (StiTableCell)valueCell.Clone(true);
                StiTableCell dataCell = table.Components[i] as StiTableCell;
                dataCell.Name = "cell" + i;
                dataCell.Text.Value = "{" + param.Name + "}";
                i++;
            }

            table.Linked = true;
            return report;
        }

        private static SizeF GetStringWidth(string text, Font font)
        {
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(text, font);
            return size;
        }

        private static StiReport CreatePageHeaderBand(StiReport report, StiReport reportTemplate, bool createParameterHeader)
        {
            StiPageHeaderBand pageHeader = new StiPageHeaderBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiPageHeaderBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiPageHeaderBand));
                pageHeader = (StiPageHeaderBand)component;
                pageHeader = (StiPageHeaderBand)ChangeLeftPositions(report, pageHeader, ReportBandType.PageHeader, reportTemplate);
            }

            report.Pages[0].Components.Add(pageHeader);

            if (createParameterHeader)
            {
                StiPageHeaderBand headerParameter = (StiPageHeaderBand)pageHeader.Clone(true);
                headerParameter.Name = "hdrParams";
                headerParameter.Linked = true;
                headerParameter.Height = 0.9;
                headerParameter.CanGrow = true;
                headerParameter.Components.Clear();
                report.Pages[0].Components.Add(headerParameter);
            }

            return report;
        }

        private static StiReport CreatePageFooterBand(StiReport report, StiReport reportTemplate)
        {
            StiPageFooterBand reportFooter = new StiPageFooterBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiPageFooterBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiPageFooterBand));
                reportFooter = (StiPageFooterBand)component;
                reportFooter = (StiPageFooterBand)ChangeLeftPositions(report, reportFooter, ReportBandType.PageFooter, reportTemplate);
            }

            report.Pages[0].Components.Add(reportFooter);
            return report;
        }

        private static StiReport CreateHeaderBand(StiReport report, IList<QuickReportColumnConfig> columns,
    int oneInchInPixels, string dataSourceName, StiReport reportTemplate, string lang, bool fitToPage)
        {
            int visibleColumnCount = columns.Count(c => c.Visible);
            if (visibleColumnCount == 0)
            {
                return null;
            }

            bool isGroupExist = columns.Any(c => !string.IsNullOrEmpty(c.GroupName));
            List<QuickReportColumnConfig> orderdColumns = null;
            if (lang == "fa")
            {
                orderdColumns = (from c in columns
                                 orderby c.DisplayIndex ascending
                                 where c.Visible
                                 select c).ToList();
            }
            else
            {
                orderdColumns = (from c in columns
                                 orderby c.DisplayIndex descending
                                 where c.Visible
                                 select c).ToList();
            }

            string name = "HeaderBand" + dataSourceName;
            StiColumnHeaderBand headerBand = null;
            StiText sampleText = null;
            StiText sampleNumber = null;
            StiComponent component = null;

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiColumnHeaderBand)))
            {
                component = componentsList.First(p => p.GetType() == typeof(StiColumnHeaderBand));
                headerBand = (StiColumnHeaderBand)component.Clone(true);
                if (isGroupExist)
                {
                    headerBand.Height = headerBand.Height * 2;
                    headerBand.CanGrow = false;
                }
            }

            // copy text style
            if (((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnTextHeader"))
            {
                sampleText = (StiText)((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnTextHeader").Clone(true);
            }

            // copy number style
            if (((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnNumberHeader"))
            {
                sampleNumber = (StiText)((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnNumberHeader").Clone(true);
            }

            if (headerBand == null)
            {
                headerBand = new StiColumnHeaderBand
                {
                    Name = name
                };
            }
            else
            {
                headerBand.Components.Clear();
            }

            int dataCellIndex = visibleColumnCount;
            double pageWidth = report.Pages[0].Width;
            int gridWidth = columns.Where(p => p.Visible).Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, oneInchInPixels);
            double left = 0;
            double top = 0;
            double maxHeight = headerBand.Height;

            string lastGroupName = string.Empty;
            double lastWidth = 0;

            for (int i = orderdColumns.Count - 1; i >= 0; i--)
            {
                QuickReportColumnConfig column = orderdColumns[i];
                StiText txtHeaderCell = null;
                string ctrlName = "txtTitle_";
                ctrlName += column.Name + column.DisplayIndex;
                double width = GetSizeInInch(column.Width, oneInchInPixels);

                if (txtHeaderCell == null)
                {
                    txtHeaderCell = (StiText)(sampleText.Clone(true));

                    switch (orderdColumns[i].Type.ToLower())
                    {
                        case "string":
                            txtHeaderCell = (StiText)(sampleText.Clone(true));
                            break;
                        case "number":
                        case "money":
                            txtHeaderCell = (StiText)(sampleNumber.Clone(true));
                            break;
                        case "gregorian":
                        case "jalali":
                            txtHeaderCell = (StiText)(sampleNumber.Clone(true));
                            break;
                    }

                    txtHeaderCell.Name = ctrlName;
                    txtHeaderCell.Left = left;
                    txtHeaderCell.Width = width;
                    txtHeaderCell.Page = report.Pages[0];
                    txtHeaderCell.Parent = headerBand;

                    txtHeaderCell.CanGrow = true;
                    txtHeaderCell.WordWrap = true;

                    if (string.IsNullOrEmpty(orderdColumns[i].GroupName) && isGroupExist)
                    {
                        top = 0;
                        txtHeaderCell.CanGrow = true;
                        txtHeaderCell.Height = txtHeaderCell.Height * 2;
                        txtHeaderCell.GrowToHeight = true;
                        txtHeaderCell.CanShrink = false;
                        lastWidth = 0;
                    }
                    else if (isGroupExist)
                    {
                        top = 0;
                        if (orderdColumns[i].GroupName == lastGroupName)
                        {
                            StiText txtGroupText = (StiText)txtHeaderCell.Clone(true);
                            var txtHeaderCellWidth = txtHeaderCell.Width;
                            if (fitToPage)
                            {
                                var widthPercent = txtHeaderCellWidth / tableWidth * 100;
                                txtHeaderCellWidth = (widthPercent * pageWidth) / 100;
                            }

                            txtGroupText.Width = txtHeaderCellWidth + lastWidth;
                            txtGroupText.Text.Value = lastGroupName;
                            txtGroupText.CanShrink = false;
                            txtGroupText.ClientRectangle = new RectangleD(left - lastWidth, top, txtGroupText.Width, txtGroupText.Height);
                            headerBand.Components.Add(txtGroupText);
                            lastWidth = 0;
                            lastGroupName = string.Empty;
                        }
                        else if (!string.IsNullOrEmpty(orderdColumns[i].GroupName) && orderdColumns.Count(cc => cc.GroupName == orderdColumns[i].GroupName) == 1)
                        {
                            StiText txtGroupText = (StiText)txtHeaderCell.Clone(true);
                            var txtHeaderCellWidth = txtHeaderCell.Width;
                            if (fitToPage)
                            {
                                var widthPercent = txtHeaderCellWidth / tableWidth * 100;
                                txtHeaderCellWidth = (widthPercent * pageWidth) / 100;
                            }

                            txtGroupText.Width = txtHeaderCellWidth;
                            txtGroupText.Text.Value = orderdColumns[i].GroupName;
                            txtGroupText.CanShrink = false;
                            txtGroupText.ClientRectangle = new RectangleD(left, top, txtGroupText.Width, txtGroupText.Height);
                            headerBand.Components.Add(txtGroupText);
                            lastWidth = 0;
                            lastGroupName = string.Empty;
                        }
                        else
                        {
                            lastWidth = txtHeaderCell.Width;
                            lastGroupName = orderdColumns[i].GroupName;
                        }

                        txtHeaderCell.CanShrink = false;
                        top = txtHeaderCell.Height;
                    }
                    else
                    {
                        top = 0;
                        lastWidth = 0;
                    }
                }

                txtHeaderCell.Text.Value = !string.IsNullOrEmpty(column.UserTitle) ? column.UserTitle : column.Title;
                if (isGroupExist && lastWidth > 0)
                {
                    width = lastWidth;
                }

                if (fitToPage)
                {
                    var widthPercent = width / tableWidth * 100;
                    width = (widthPercent * pageWidth) / 100;

                    lastWidth = width;
                    txtHeaderCell.ClientRectangle = new RectangleD(left, top, width, txtHeaderCell.Height);
                    left = txtHeaderCell.Left + width;
                }
                else
                {
                    txtHeaderCell.ClientRectangle = new RectangleD(left, top, width, txtHeaderCell.Height);
                    left = txtHeaderCell.Left + width;
                    lastWidth = width;
                }

                txtHeaderCell.GrowToHeight = true;
                headerBand.Components.Add(txtHeaderCell);
            }

            report.Pages[0].Components.Add(headerBand);
            return report;
        }

        private static StiReport CreateDataBand(StiReport report, QuickReportConfig quickReportViewModel,
            string dataSourceName, StiReport reportTemplate, string lang, int inchValue, bool fitToPage)
        {
            string ctrlName = "dataBand" + dataSourceName;

            List<QuickReportColumnConfig> orderdColumns = null;
            if (lang == "fa")
            {
                orderdColumns = (from c in quickReportViewModel.Columns
                                 orderby c.DisplayIndex ascending
                                 where c.Visible
                                 select c).ToList();
            }
            else
            {
                orderdColumns = (from c in quickReportViewModel.Columns
                                 orderby c.DisplayIndex descending
                                 where c.Visible
                                 select c).ToList();
            }

            double pageWidth = report.Pages[0].Width;
            int gridWidth = quickReportViewModel.Columns.Where(c => c.Visible).Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, inchValue);
            double left = 0;
            double top = 0;

            StiDataBand dataBand = new StiDataBand();
            StiText sampleText = null;
            StiText sampleNumber = null;
            StiText sampleDate = null;
            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            StiComponent component = null;

            if (componentsList.Any(p => p.GetType() == typeof(StiDataBand)))
            {
                component = componentsList.First(p => p.GetType() == typeof(StiDataBand));
                dataBand = (StiDataBand)component.Clone(true);
            }

            // copy text style
            if (((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnTextData"))
            {
                sampleText = (StiText)((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnTextData").Clone(true);
            }

            // copy date style
            if (((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnNumberData"))
            {
                sampleNumber = (StiText)((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnNumberData").Clone(true);
            }

            // copy number style
            if (((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnDateData"))
            {
                sampleDate = (StiText)((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnDateData").Clone(true);
            }

            dataBand.Components.Clear();
            dataBand.Name = ctrlName;
            dataBand.DataSourceName = dataSourceName;

            for (int i = orderdColumns.Count - 1; i >= 0; i--)
            {
                double width = GetSizeInInch(orderdColumns[i].Width, inchValue);
                StiText txtDataCell = null;
                string name = "txtDataCell_" + orderdColumns[i].Name;

                if (txtDataCell == null)
                {
                    if (orderdColumns[i].Name.ToLower() != "rowno")
                    {
                        switch (orderdColumns[i].Type.ToLower())
                        {
                            case "number":
                            case "money":
                                txtDataCell = (StiText)(sampleNumber.Clone(true));
                                break;
                            case "date":
                                txtDataCell = (StiText)(sampleDate.Clone(true));
                                break;
                            case "string":
                            default:
                                txtDataCell = (StiText)(sampleText.Clone(true));
                                break;
                        }
                    }
                    else
                    {
                        txtDataCell = (StiText)(sampleDate.Clone(true));
                    }

                    txtDataCell.Name = name;
                    txtDataCell.Left = left;
                    txtDataCell.Parent = dataBand;
                    txtDataCell.Page = report.Pages[0];
                }

                if (orderdColumns[i].Type.ToLower() == "money")
                {
                    txtDataCell.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
                }

                if (fitToPage)
                {
                    var widthPercent = width / tableWidth * 100;
                    width = (widthPercent * pageWidth) / 100;
                }

                txtDataCell.Width = width;
                txtDataCell.Text.Value = GetColumnValue(orderdColumns[i], dataSourceName, string.Empty);
                txtDataCell.ClientRectangle = new RectangleD(left, top, width, txtDataCell.Height);
                left = txtDataCell.Left + width;

                if (quickReportViewModel.ReportViewSetting != null)
                {
                    if (quickReportViewModel.ReportViewSetting.HideHorizontalLine && quickReportViewModel.ReportViewSetting.HideVerticalLine)
                    {
                        txtDataCell.Border = new StiBorder(StiBorderSides.None, txtDataCell.Border.Color, txtDataCell.Border.Size, txtDataCell.Border.Style);
                    }

                    if (quickReportViewModel.ReportViewSetting.HideHorizontalLine && !quickReportViewModel.ReportViewSetting.HideVerticalLine)
                    {
                        txtDataCell.Border = new StiBorder(StiBorderSides.Left | StiBorderSides.Right, txtDataCell.Border.Color, txtDataCell.Border.Size, txtDataCell.Border.Style);
                    }

                    if (!quickReportViewModel.ReportViewSetting.HideHorizontalLine && quickReportViewModel.ReportViewSetting.HideVerticalLine)
                    {
                        txtDataCell.Border = new StiBorder(StiBorderSides.Bottom, txtDataCell.Border.Color, txtDataCell.Border.Size, txtDataCell.Border.Style);
                    }
                }

                dataBand.Components.Add(txtDataCell);
            }

            report.Pages[0].Components.Add(dataBand);
            return report;
        }

        private static string GetColumnValue(QuickReportColumnConfig column, string dataSourceName, string functionTamplate)
        {
            string fullColumnName = "{0}";
            if (!string.IsNullOrEmpty(functionTamplate))
            {
                fullColumnName = functionTamplate;
            }

            fullColumnName = "{" + string.Format(fullColumnName, dataSourceName + "." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(column.Name)) + "}";
            return fullColumnName;
        }

        private static double GetSizeInInch(int pixel, int oneInchInPixel)
        {
            if (oneInchInPixel == 0)
            {
                return 0;
            }

            double inchValue = (double)pixel / (double)oneInchInPixel;

            return inchValue;
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