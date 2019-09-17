using System;
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
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.Table;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class ReportsController : ApiControllerBase
    {
        public ReportsController(IReportRepository repository, IReportSystemRepository sysRepository,
            IConfigRepository config, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _sysRepository = sysRepository;
            _configRepository = config;
        }

        #region Report Management API

        // GET: api/reports/sys/tree
        [Route(ReportApi.ReportsHierarchyUrl)]
        public async Task<IActionResult> GetReportTreeAsync()
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var tree = await _sysRepository.GetReportTreeAsync(localeId);
            return Json(tree);
        }

        // GET: api/reports/sys/view/{viewId:min(1)}
        [Route(ReportApi.ReportsByViewUrl)]
        public async Task<IActionResult> GetReportTreeByViewAsync(int viewId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var tree = await _sysRepository.GetReportTreeByViewAsync(localeId, viewId);
            return Json(tree);
        }

        // GET: api/reports/sys/subsys/{subsysId:min(1)}
        [Route(ReportApi.ReportsBySubsystemUrl)]
        public async Task<IActionResult> GetReportTreeBySubsystemAsync(int subsysId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var tree = await _sysRepository.GetReportTreeBySubsystemAsync(localeId, subsysId);
            return Json(tree);
        }

        // GET: api/reports/sys/{reportId:min(1)}
        [Route(ReportApi.ReportUrl)]
        public async Task<IActionResult> GetReportAsync(int reportId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var report = await _sysRepository.GetReportAsync(reportId, localeId);
            Localize(report);
            return JsonReadResult(report);
        }

        // GET: api/reports/sys/{reportId:min(1)}/design
        [Route(ReportApi.ReportDesignUrl)]
        public async Task<IActionResult> GetReportDesignAsync(int reportId)
        {
            int localeId = await GetCurrentLocaleIdAsync();
            var reportDesign = await _sysRepository.GetReportDesignAsync(reportId, localeId);
            return JsonReadResult(reportDesign);
        }

        [Route(ReportApi.ReportsByViewDefaultUrl)]
        public async Task<IActionResult> GetDefaultReportByViewAsync(int viewId)
        {
            var report = await _sysRepository.GetDefaultReportByViewAsync(viewId);
            return JsonReadResult(report);
        }

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

        [HttpPut]
        [Route(ReportApi.ReportDefaultUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)UserReportPermissions.SetDefault)]
        public async Task<IActionResult> PutExistingReportAsDefaultAsync(int reportId)
        {
            await _sysRepository.SetReportAsDefaultAsync(reportId);
            return Ok();
        }

        // DELETE: api/reports/sys/{reportId:min(1)}
        [HttpDelete]
        [Route(ReportApi.ReportUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)UserReportPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingUserReportAsync(int reportId)
        {
            var summary = await _sysRepository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifySystemReport));
            }

            await _sysRepository.DeleteUserReportAsync(reportId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        // PUT: api/reports/sys/quickreport/{unit:min(1)}
        [HttpPut]
        [Route(ReportApi.EnvironmentQuickReportUrl)]
        public IActionResult PutEnvironmentUserQuickReport([FromBody] QuickReportConfig qr, int unit)
        {
            Stimulsoft.Report.StiReport quickReport = new Stimulsoft.Report.StiReport();
            Stimulsoft.Report.StiReport quickReportTemplate = new Stimulsoft.Report.StiReport();

            bool outOf = false;
            quickReport.ReportUnit = StiReportUnitType.Inches;
            var dataSourceName = "root";
            string reportTemplate = string.Empty;
            string reportLang = this.GetAcceptLanguages() == "fa-IR,fa" ? "fa" : "en";

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
            quickReport = SetPageWidth(quickReport, qr, inchValue, out outOf);
            quickReport = CreateReportFooterBand(quickReport, quickReportTemplate);
            quickReport = CreateReportHeaderBand(quickReport, quickReportTemplate);

            bool createParameterHeader = qr.Parameters != null && qr.Parameters.Count > 0;

            quickReport = CreatePageHeaderBand(quickReport, quickReportTemplate, createParameterHeader);
            quickReport = CreatePageFooterBand(quickReport, quickReportTemplate);
            if (qr.Parameters != null)
            {
                quickReport = CreateReportParametersBand(qr, quickReport, quickReportTemplate);
            }

            quickReport = CreateHeaderBand(quickReport, qr.Columns, inchValue, dataSourceName, quickReportTemplate, reportLang);
            quickReport = CreateDataBand(quickReport, qr, dataSourceName, quickReportTemplate, reportLang, inchValue);
            quickReport = FillLocalVariables(quickReport, qr.Title);

            ////SettingsController settingsController = new SettingsController();
            ////settingsController.PutModifiedQReportSettingsByUserAsync(this.SecurityContext.User.Id, qr);
            _configRepository.SaveQuickReportConfigAsync(this.SecurityContext.User.Id, qr);

            var jsonData = quickReport.SaveToJsonString();

            return Ok(new { designJson = jsonData, outOfPage = outOf });
        }

        #endregion

        #region Business Reports API

        // GET: api/reports/metadata/{viewId:min(1)}
        [Route(ReportApi.ReportMetadataByViewUrl)]
        public async Task<IActionResult> GetReportMetadataByViewAsync(int viewId)
        {
            var metadata = await _repository.GetReportMetadataByViewAsync(viewId);
            return JsonReadResult(metadata);
        }

        // GET: api/reports/voucher/sum-by-date
        [Route(ReportApi.EnvironmentVoucherSummaryByDateUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVoucherSummaryByDateAsync()
        {
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

            StiPanel panel = new StiPanel();
            panel.CanGrow = true;
            panel.Width = report.Pages[0].Width;
            panel.PrintOn = table.PrintOnAllPages ? StiPrintOnType.AllPages : StiPrintOnType.OnlyFirstPage;
            panel.Components.Add(table);
            ph.Components.Add(panel);
            table.CreateCell();
            int i = 0;
            foreach (var param in quickReportViewModel.Parameters)
            {
                table.Components[i] = (StiTableCell)titleCell.Clone(true);
                StiTableCell headerCell = table.Components[i] as StiTableCell;
                headerCell.Text.Value = param.CaptionKey;
                ////headerCell.ComponentStyle = titleCell.ComponentStyle;
                headerCell.Name = "cell" + i;
                headerCell.Linked = true;
                ////headerCell.Border = new StiBorder(StiBorderSides.All, Color.Black, 1, StiPenStyle.Dash);
                ////headerCell.CellDockStyle = titleCell.CellDockStyle;
                ////headerCell.TextOptions.RightToLeft = titleCell.TextOptions.RightToLeft;
                i++;

                table.Components[i] = (StiTableCell)valueCell.Clone(true);
                StiTableCell dataCell = table.Components[i] as StiTableCell;
                dataCell.Name = "cell" + i;
                dataCell.Text.Value = "{" + param.Name + "}";
                ////dataCell.ComponentStyle = valueCell.ComponentStyle;
                ////dataCell.Border = new StiBorder(StiBorderSides.All, Color.Black, 1, StiPenStyle.Dash);
                ////dataCell.CellDockStyle = StiDockStyle.Left;
                ////dataCell.TextOptions.RightToLeft = valueCell.TextOptions.RightToLeft;
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

            ////
            ////double maxHeight = pageHeader.Height;
            ////txtPageHeaderText.Width = 2;
            ////txtPageHeaderText.Name = "txtPageHeaderText";
            ////txtPageHeaderText.Left = (report.Pages[0].Width / 2) - (txtPageHeaderText.Width / 2);
            ////txtPageHeaderText.Linked = true;
            ////txtPageHeaderText.Text = header;
            ////txtPageHeaderText.Height = 0.8;
            ////txtPageHeaderText.HorAlignment = StiTextHorAlignment.Center;
            ////txtPageHeaderText.AutoWidth = true;

            //// var pfc = new System.Drawing.Text.PrivateFontCollection();
            //// pfc.AddFontFile(@"J:\SourceCodes\Tadbir\repos\NewRepo\src\Framework\SPPC.Tadbir.Web.New\ClientApp\src\assets\resources\fonts\ReportFont\BTitrBold.ttf");
            //// txtPageHeaderText.Font = new Font(pfc.Families[0], 14, FontStyle.Bold);
            ////if (txtPageHeaderText.Height + txtPageHeaderText.Top > maxHeight)
            ////{
            ////    maxHeight = txtPageHeaderText.Height + txtPageHeaderText.Top;
            ////}

            ////pageHeader.Height = maxHeight;
            ////pageHeader.Components.Add(txtPageHeaderText);
            ////

            ////var pfc = new System.Drawing.Text.PrivateFontCollection();
            ////pfc.AddFontFile(@"J:\SourceCodes\Tadbir\repos\NewRepo\src\Framework\SPPC.Tadbir.Web.New\ClientApp\src\assets\resources\fonts\ReportFont\BTitrBold.ttf");

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
            int oneInchInPixels, string dataSourceName, StiReport reportTemplate, string lang)
        {
            int visibleColumnCount = columns.Count(c => c.Visible);
            if (visibleColumnCount == 0)
            {
                return null;
            }

            bool isGroupExist = columns.Any(c => !string.IsNullOrEmpty(c.GroupName));

            ////var orderdColumns = columns.Where(c => c.Enabled).OrderByDescending(c => c.Order).ToList();
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

            ////double height = double.Parse("0.4", System.Globalization.CultureInfo.InvariantCulture);
            if (headerBand == null)
            {
                headerBand = new StiColumnHeaderBand();
                headerBand.Name = name;
            }
            else
            {
                headerBand.Components.Clear();
            }

            int dataCellIndex = visibleColumnCount;
            double pageWidth = report.Pages[0].Width;
            int gridWidth = columns.Where(p => p.Visible).Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, oneInchInPixels);
            double left = (pageWidth / (double)2) - (tableWidth / (double)2);
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
                        case "date":
                            txtHeaderCell = (StiText)(sampleNumber.Clone(true));
                            break;
                    }

                    txtHeaderCell.Name = ctrlName;
                    txtHeaderCell.Left = left;
                    txtHeaderCell.Width = width;
                    txtHeaderCell.Page = report.Pages[0];
                    txtHeaderCell.Parent = headerBand;
                    if (string.IsNullOrEmpty(orderdColumns[i].GroupName) && isGroupExist)
                    {
                        top = 0;
                        txtHeaderCell.CanGrow = true;
                        txtHeaderCell.Height = txtHeaderCell.Height * 2;
                        txtHeaderCell.GrowToHeight = true;
                        txtHeaderCell.CanShrink = false;
                    }
                    else if (isGroupExist)
                    {
                        top = 0;
                        if (orderdColumns[i].GroupName == lastGroupName)
                        {
                            StiText txtGroupText = (StiText)txtHeaderCell.Clone(true);
                            txtGroupText.Width = txtHeaderCell.Width + lastWidth;
                            txtGroupText.Text.Value = lastGroupName;
                            txtGroupText.CanShrink = false;
                            txtGroupText.ClientRectangle = new RectangleD(left - lastWidth, top, txtGroupText.Width, txtGroupText.Height);
                            headerBand.Components.Add(txtGroupText);

                            lastGroupName = string.Empty;
                        }
                        else if (!string.IsNullOrEmpty(orderdColumns[i].GroupName) && orderdColumns.Count(cc => cc.GroupName == orderdColumns[i].GroupName) == 1)
                        {
                            StiText txtGroupText = (StiText)txtHeaderCell.Clone(true);
                            txtGroupText.Width = txtHeaderCell.Width;
                            txtGroupText.Text.Value = orderdColumns[i].GroupName;
                            txtGroupText.CanShrink = false;
                            txtGroupText.ClientRectangle = new RectangleD(left, top, txtGroupText.Width, txtGroupText.Height);
                            headerBand.Components.Add(txtGroupText);

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
                    }
                }

                txtHeaderCell.Text.Value = !string.IsNullOrEmpty(column.UserTitle) ? column.UserTitle : column.Title;
                txtHeaderCell.ClientRectangle = new RectangleD(left, top, width, txtHeaderCell.Height);
                left = txtHeaderCell.Left + width;
                headerBand.Components.Add(txtHeaderCell);
            }

            report.Pages[0].Components.Add(headerBand);
            return report;
        }

        private static StiReport CreateDataBand(StiReport report, QuickReportConfig quickReportViewModel,
            string dataSourceName, StiReport reportTemplate, string lang, int inchValue)
        {
            string ctrlName = "dataBand" + dataSourceName;

            ////List<QuickReportColumnViewModel> orderedColumns = quickReportViewModel.Columns.Where(c => c.Enabled).OrderByDescending(c => c.Order).ToList();

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

            double left = (pageWidth / (double)2) - (tableWidth / (double)2);
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

            ////double maxHeight = dataBand.Height;

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

                ////txtDataCell.Border = new StiBorder(StiBorderSides.Bottom | StiBorderSides.Left | StiBorderSides.Right, Color.Black, 1, StiPenStyle.Solid);

                txtDataCell.Width = width;
                txtDataCell.Text.Value = GetColumnValue(orderdColumns[i], dataSourceName, string.Empty);
                txtDataCell.ClientRectangle = new RectangleD(left, top, width, txtDataCell.Height);
                left = txtDataCell.Left + width;

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

        private async Task<int> GetCurrentLocaleIdAsync()
        {
            var localCode = GetAcceptLanguages().Substring(0, 2);
            return await _sysRepository.GetLocaleIdAsync(localCode);
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
                    param.DescriptionKey = _strings[param.CaptionKey];      // Temporary fix
                }
            }
        }

        private async Task<IActionResult> ReportValidationResultAsync(
            LocalReportViewModel report, int reportId = 0)
        {
            if (report == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserReport));
            }

            if (report.ReportId == 0)
            {
                return BadRequest(_strings.Format(AppStrings.SourceReportIsRequired));
            }

            int localeId = await GetCurrentLocaleIdAsync();
            if (await _sysRepository.IsDuplicateReportCaptionAsync(localeId, report))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateFieldValue, AppStrings.ReportCaption));
            }

            if (reportId == 0)
            {
                return Ok();
            }

            if (report.ReportId != reportId)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.UserReport));
            }

            var summary = await _sysRepository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifySystemReport));
            }

            return Ok();
        }

        private readonly IReportRepository _repository;
        private readonly IReportSystemRepository _sysRepository;
        private readonly IConfigRepository _configRepository;
    }
}