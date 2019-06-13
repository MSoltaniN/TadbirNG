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

            _sysRepository.SetCurrentContext(SecurityContext.User);
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

        // PUT: api/reports/sys/quickreport
        [HttpPut]
        [Route(ReportApi.EnvironmentQuickReportUrl)]
        public IActionResult PutEnvironmentUserQuickReport([FromBody] QuickReportViewModel qr)
        {
            Stimulsoft.Report.StiReport quickReport = new Stimulsoft.Report.StiReport();
            Stimulsoft.Report.StiReport quickReportTemplate = new Stimulsoft.Report.StiReport();

            bool outOf = false;
            quickReport.ReportUnit = StiReportUnitType.Inches;
            var dataSourceName = "root";
            string reportTemplate = string.Empty;

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
                if (qr.ReportLang != "fa")
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

            quickReport.Styles.AddRange(quickReportTemplate.Styles);

            // load template for adding styles
            quickReport = SetPageWidth(quickReport, qr, out outOf);
            quickReport = CreateReportFooterBand(quickReport, quickReportTemplate);
            quickReport = CreateReportHeaderBand(quickReport, quickReportTemplate);
            quickReport = CreatePageHeaderBand(quickReport, quickReportTemplate);
            quickReport = CreatePageFooterBand(quickReport, quickReportTemplate);
            if (qr.Parameters != null)
            {
                quickReport = CreateReportParametersBand(qr, quickReport, quickReportTemplate);
            }

            quickReport = CreateHeaderBand(quickReport, qr.Columns, qr.InchValue, dataSourceName, quickReportTemplate, qr.ReportLang);
            quickReport = CreateDataBand(quickReport, qr, dataSourceName, quickReportTemplate, qr.ReportLang);
            quickReport = FillLocalVariables(quickReport, qr.ReportTitle);

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

        private static StiReport SetPageWidth(StiReport report, QuickReportViewModel quickReportViewModel, out bool outOfPage)
        {
            double width = report.Pages[0].Width;
            outOfPage = false;
            int gridWidth = quickReportViewModel.Columns.Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, quickReportViewModel.InchValue);

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

        private static StiReport CreateReportParametersBand(QuickReportViewModel quickReportViewModel, StiReport report, StiReport reportTemplate)
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

        private static StiReport CreatePageHeaderBand(StiReport report, StiReport reportTemplate)
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

            StiPageHeaderBand headerParameter = (StiPageHeaderBand)pageHeader.Clone(true);
            headerParameter.Name = "hdrParams";
            headerParameter.Linked = true;
            headerParameter.Height = 0.9;
            headerParameter.CanGrow = true;

            report.Pages[0].Components.Add(headerParameter);

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

        private static StiReport CreateHeaderBand(StiReport report, IList<QuickReportColumnViewModel> columns,
            int oneInchInPixels, string dataSourceName, StiReport reportTemplate, string lang)
        {
            int visibleColumnCount = columns.Count(c => c.Enabled);
            if (visibleColumnCount == 0)
            {
                return null;
            }

            ////var orderdColumns = columns.Where(c => c.Enabled).OrderByDescending(c => c.Order).ToList();
            List<QuickReportColumnViewModel> orderdColumns = null;
            if (lang == "fa")
            {
                orderdColumns = (from c in columns
                                 orderby c.Order ascending
                                 where c.Enabled
                                 select c).ToList();
            }
            else
            {
                orderdColumns = (from c in columns
                                     orderby c.Order descending
                                     where c.Enabled
                                     select c).ToList();
            }

            string name = "HeaderBand" + dataSourceName;
            StiColumnHeaderBand headerBand = null;
            StiText sampleText = null;
            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiColumnHeaderBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiColumnHeaderBand));
                headerBand = (StiColumnHeaderBand)component.Clone(true);
                if (((StiColumnHeaderBand)component).Components.ToList().Any(p => p.GetType() == typeof(StiText)))
                {
                    sampleText = (StiText)((StiColumnHeaderBand)component).Components.ToList().First(p => p.GetType() == typeof(StiText)).Clone(true);
                }

                headerBand.Components.Clear();
            }

            ////double height = double.Parse("0.4", System.Globalization.CultureInfo.InvariantCulture);
            if (headerBand == null)
            {
                headerBand = new StiColumnHeaderBand();
                headerBand.Name = name;
            }

            int dataCellIndex = visibleColumnCount;
            double pageWidth = report.Pages[0].Width;
            int gridWidth = columns.Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, oneInchInPixels);
            double left = (pageWidth / (double)2) - (tableWidth / (double)2);
            double top = 0.06;
            double maxHeight = headerBand.Height;

            for (int i = orderdColumns.Count - 1; i >= 0; i--)
            {
                QuickReportColumnViewModel column = orderdColumns[i];
                StiText txtHeaderCell = null;
                string ctrlName = "txtTitle_";
                ctrlName += column.Name + column.Index;
                double width = GetSizeInInch(column.Width, oneInchInPixels);
                if (txtHeaderCell == null)
                {
                    txtHeaderCell = (StiText)(sampleText.Clone(true));
                    txtHeaderCell.Name = ctrlName;
                    txtHeaderCell.Left = left;
                    txtHeaderCell.Width = width;
                    txtHeaderCell.Page = report.Pages[0];
                    txtHeaderCell.Parent = headerBand;
                }

                ////txtHeaderCell.Linked = true;
                txtHeaderCell.Text.Value = !string.IsNullOrEmpty(column.UserText) ? column.UserText : column.DefaultText;
                txtHeaderCell.ClientRectangle = new RectangleD(left, top, width, txtHeaderCell.Height);
                ////txtHeaderCell.Height = 0.6;
                left = txtHeaderCell.Left + width;

                ////StiBorder border = new StiBorder();
                ////border.Side = border.Side | StiBorderSides.Left;
                ////border.Side = border.Side | StiBorderSides.Right;
                ////border.Side = border.Side | StiBorderSides.Top;
                ////border.Side = border.Side | StiBorderSides.Bottom;
                ////border.Color = Color.FromName("Black");

                ////txtHeaderCell.CanShrink = true;
                ////txtHeaderCell.CanGrow = true;
                ////txtHeaderCell.GrowToHeight = true;
                ////txtHeaderCell.HorAlignment = StiTextHorAlignment.Center;
                ////txtHeaderCell.VertAlignment = StiVertAlignment.Center;
                ////txtHeaderCell.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                ////txtHeaderCell.Border = border;

                ////if (txtHeaderCell.Height > maxHeight)
                ////{
                ////    headerBand.Height = txtHeaderCell.Height;
                ////}

                headerBand.Components.Add(txtHeaderCell);
            }

            report.Pages[0].Components.Add(headerBand);
            return report;
        }

        private static StiReport CreateDataBand(StiReport report, QuickReportViewModel quickReportViewModel,
            string dataSourceName, StiReport reportTemplate, string lang)
        {
            string ctrlName = "dataBand" + dataSourceName;

            ////List<QuickReportColumnViewModel> orderedColumns = quickReportViewModel.Columns.Where(c => c.Enabled).OrderByDescending(c => c.Order).ToList();

            List<QuickReportColumnViewModel> orderdColumns = null;
            if (lang == "fa")
            {
                orderdColumns = (from c in quickReportViewModel.Columns
                                 orderby c.Order ascending
                                 where c.Enabled
                                 select c).ToList();
            }
            else
            {
                orderdColumns = (from c in quickReportViewModel.Columns
                                 orderby c.Order descending
                                 where c.Enabled
                                 select c).ToList();
            }

            double pageWidth = report.Pages[0].Width;
            int gridWidth = quickReportViewModel.Columns.Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, quickReportViewModel.InchValue);

            double left = (pageWidth / (double)2) - (tableWidth / (double)2);
            double top = 0;

            StiDataBand dataBand = new StiDataBand();
            StiText sampleText = null;
            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiDataBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiDataBand));
                dataBand = (StiDataBand)component.Clone(true);
                if (((StiDataBand)component).Components.ToList().Any(p => p.GetType() == typeof(StiText)))
                {
                    sampleText = (StiText)((StiDataBand)component).Components.ToList().First(p => p.GetType() == typeof(StiText)).Clone(true);
                }

                dataBand.Components.Clear();
            }

            dataBand.Name = ctrlName;
            dataBand.DataSourceName = dataSourceName;

            ////double maxHeight = dataBand.Height;

            for (int i = orderdColumns.Count - 1; i >= 0; i--)
            {
                double width = GetSizeInInch(orderdColumns[i].Width, quickReportViewModel.InchValue);
                StiText txtDataCell = null;
                string name = "txtDataCell_" + orderdColumns[i].Name;

                if (txtDataCell == null)
                {
                    txtDataCell = (StiText)(sampleText.Clone(true));
                    txtDataCell.Name = name;
                    txtDataCell.Left = left;
                    txtDataCell.Parent = dataBand;
                    txtDataCell.Page = report.Pages[0];
                }

                if (orderdColumns[i].Type.ToLower() == "money")
                {
                    txtDataCell.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
                }

                txtDataCell.Width = width;
                txtDataCell.Text.Value = GetColumnValue(orderdColumns[i], dataSourceName, string.Empty);
                txtDataCell.ClientRectangle = new RectangleD(left, top, width, txtDataCell.Height);
                left = txtDataCell.Left + width;

                dataBand.Components.Add(txtDataCell);
            }

            report.Pages[0].Components.Add(dataBand);
            return report;
        }

        private static string GetColumnValue(QuickReportColumnViewModel column, string dataSourceName, string functionTamplate)
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