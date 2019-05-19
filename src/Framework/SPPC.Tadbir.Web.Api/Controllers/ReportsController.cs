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
        [AuthorizeRequest(SecureEntity.UserReport, (int)ReportPermissions.Save)]
        public async Task<IActionResult> PostNewUserReportAsync([FromBody] LocalReportViewModel report)
        {
            if (report == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.UserReport));
            }

            if (report.ReportId == 0)
            {
                return BadRequest(_strings.Format(AppStrings.SourceReportIsRequired));
            }

            _sysRepository.SetCurrentContext(SecurityContext.User);
            report.LocaleId = await GetCurrentLocaleIdAsync();
            await _sysRepository.SaveUserReportAsync(report);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/reports/sys/{reportId:min(1)}
        [HttpPut]
        [Route(ReportApi.ReportUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)ReportPermissions.Save)]
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

            var summary = await _sysRepository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifySystemReport));
            }

            report.LocaleId = await GetCurrentLocaleIdAsync();
            await _sysRepository.SaveUserReportAsync(report);
            return Ok();
        }

        // PUT: api/reports/sys/{reportId:min(1)}/caption
        [HttpPut]
        [Route(ReportApi.ReportCaptionUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)ReportPermissions.Save)]
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

            var summary = await _sysRepository.GetReportSummaryAsync(reportId);
            if (summary == null)
            {
                return BadRequest(_strings.Format(AppStrings.ItemNotFound, AppStrings.UserReport));
            }

            if (summary.IsSystem)
            {
                return BadRequest(_strings.Format(AppStrings.CantModifySystemReport));
            }

            report.LocaleId = await GetCurrentLocaleIdAsync();
            await _sysRepository.SetUserReportCaptionAsync(report);
            return Ok();
        }

        [HttpPut]
        [Route(ReportApi.ReportDefaultUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)ReportPermissions.SetDefault)]
        public async Task<IActionResult> PutExistingReportAsDefaultAsync(int reportId)
        {
            await _sysRepository.SetReportAsDefaultAsync(reportId);
            return Ok();
        }

        // DELETE: api/reports/sys/{reportId:min(1)}
        [HttpDelete]
        [Route(ReportApi.ReportUrl)]
        [AuthorizeRequest(SecureEntity.UserReport, (int)ReportPermissions.Delete)]
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
            bool outOfPage = false;
            quickReport.ReportUnit = StiReportUnitType.Inches;
            var dataSourceName = "root";
            var tableParams = new StiTable();
            tableParams.Columns = 4;

            ////using (StreamReader reader = new StreamReader(
            ////    typeof(Program).Assembly.GetManifestResourceStream("SPPC.Tadbir.Web.Api.Report.cs")))
            ////{
            ////    string reportScript = reader.ReadToEnd();
            ////    quickReport.Script = reportScript;
            ////}

            quickReport = SetPageWidth(quickReport, qr, out outOfPage);
            quickReport = CreateReportFooterBand(quickReport);
            quickReport = CreateReportHeaderBand(quickReport);
            quickReport = CreatePageHeaderBand(quickReport, qr.ReportTitle);
            quickReport = CreatePageFooterBand(quickReport);
            if (qr.Parameters != null)
            {
                quickReport = CreateReportParametersBand(qr, quickReport);
            }

            quickReport = CreateHeaderBand(quickReport, qr.Columns, qr.InchValue, dataSourceName);
            quickReport = CreateDataBand(quickReport, qr, dataSourceName);

            var jsonData = quickReport.SaveToJsonString();

            return Ok(new { designJson = jsonData });
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

        private static StiReport CreateReportHeaderBand(StiReport report)
        {
            StiReportTitleBand reportHeader = new StiReportTitleBand();

            report.Pages[0].Components.Add(reportHeader);

            return report;
        }

        private static StiReport CreateReportFooterBand(StiReport report)
        {
            StiReportSummaryBand reportFooter = new StiReportSummaryBand();
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

        private static StiReport CreateReportParametersBand(QuickReportViewModel quickReportViewModel, StiReport report)
        {
            ////double rightMargin = 1;
            ////double leftMargin = 1;
            ////var paramsWidth = report.Pages[0].Width - leftMargin - rightMargin;
            ////var left = report.Pages[0].Width - rightMargin;
            ////foreach (var param in quickReportViewModel.Parameters)
            ////{
            ////    var txtParamTitle = new StiText();

            ////    txtParamTitle.Text = param.CaptionKey;
            ////    var tsize = GetStringWidth(param.CaptionKey, txtParamTitle.Font);
            ////    txtParamTitle.Width = tsize.Width;
            ////    txtParamTitle.Height = tsize.Height;
            ////    txtParamTitle.Left = left - tsize.Width;
            ////    report.Pages[0].Components.Add(txtParamTitle);

            ////    var txtParamValue = new StiText();
            ////    txtParamValue.Text = param.Value;
            ////    var vsize = GetStringWidth(param.CaptionKey, txtParamValue.Font);
            ////    txtParamValue.Width = vsize.Width;
            ////    txtParamValue.Height = vsize.Height;
            ////    txtParamValue.Left = left - vsize.Width;
            ////    report.Pages[0].Components.Add(txtParamValue);
            ////}
            StiTable table = new StiTable();
            table.AutoWidth = StiTableAutoWidth.Page;
            table.AutoWidthType = StiTableAutoWidthType.FullTable;
            table.Name = "Table1";
            table.Width = report.Pages[0].Width;
            ////table.Height = report.Pages[0].GridSize * 10;
            table.ColumnCount = 4;
            table.RowCount = (int)Math.Ceiling(((double)quickReportViewModel.Parameters.Count * 2 / (double)table.ColumnCount));

            report.Pages[0].Components.Add(table);
            table.CreateCell();

            int i = 0;

            foreach (var param in quickReportViewModel.Parameters)
            {
                StiTableCell headerCell = table.Components[i] as StiTableCell;
                headerCell.Text.Value = param.CaptionKey;
                headerCell.Text = new StiExpression(param.CaptionKey);
                headerCell.HorAlignment = StiTextHorAlignment.Right;
                headerCell.VertAlignment = StiVertAlignment.Center;
                headerCell.RightToLeft = true;
                headerCell.Linked = true;
                headerCell.Margins = new StiMargins(6, 0, 0, 0);
                i++;

                StiTableCell dataCell = table.Components[i] as StiTableCell;
                dataCell.Text.Value = "{" + param.Name + "}";
                dataCell.HorAlignment = StiTextHorAlignment.Left;
                dataCell.VertAlignment = StiVertAlignment.Center;
                dataCell.RightToLeft = true;

                i++;
            }

            table.RightToLeft = true;
            return report;
        }

        private static SizeF GetStringWidth(string text, Font font)
        {
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString("Hello", font);
            return size;
        }

        private static StiReport CreatePageHeaderBand(StiReport report, string header)
        {
            StiPageHeaderBand pageHeader = new StiPageHeaderBand();

            var txtPageHeaderText = new StiText();
            double maxHeight = pageHeader.Height;
            txtPageHeaderText.Width = 2;
            txtPageHeaderText.Name = "txtPageHeaderText";
            txtPageHeaderText.Left = (report.Pages[0].Width / 2) - (txtPageHeaderText.Width / 2);
            txtPageHeaderText.Linked = true;
            txtPageHeaderText.Text = header;
            txtPageHeaderText.Height = 0.8;
            txtPageHeaderText.HorAlignment = StiTextHorAlignment.Center;
            txtPageHeaderText.AutoWidth = true;

            var pfc = new System.Drawing.Text.PrivateFontCollection();
            pfc.AddFontFile(@"J:\SourceCodes\Tadbir\repos\NewRepo\src\Framework\SPPC.Tadbir.Web.New\ClientApp\src\assets\resources\fonts\ReportFont\BTitrBold.ttf");
            txtPageHeaderText.Font = new Font(pfc.Families[0], 14, FontStyle.Bold);
            if (txtPageHeaderText.Height + txtPageHeaderText.Top > maxHeight)
            {
                maxHeight = txtPageHeaderText.Height + txtPageHeaderText.Top;
            }

            pageHeader.Height = maxHeight;
            pageHeader.Components.Add(txtPageHeaderText);
            report.Pages[0].Components.Add(pageHeader);

            return report;
        }

        private static StiReport CreatePageFooterBand(StiReport report)
        {
            StiPageFooterBand reportFooter = new StiPageFooterBand();
            report.Pages[0].Components.Add(reportFooter);
            return report;
        }

        private static StiReport CreateHeaderBand(StiReport report, IList<QuickReportColumnViewModel> columns, int oneInchInPixels, string dataSourceName)
        {
            int visibleColumnCount = columns.Count(c => c.Enabled);
            if (visibleColumnCount == 0)
            {
                return null;
            }

            var orderdColumns = columns.Where(c => c.Enabled).OrderByDescending(c => c.Order).ToList();
            string name = "HeaderBand" + dataSourceName;
            StiColumnHeaderBand headerBand = null;

            double height = double.Parse("0.4", System.Globalization.CultureInfo.InvariantCulture);
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

            for (int i = columns.Count - 1; i >= 0; i--)
            {
                QuickReportColumnViewModel column = columns[i];
                StiText txtHeaderCell = null;
                string ctrlName = "txtTitle_";
                ctrlName += column.Name + column.Index;
                double width = GetSizeInInch(column.Width, oneInchInPixels);
                if (txtHeaderCell == null)
                {
                    txtHeaderCell = new StiText();
                    txtHeaderCell.Name = ctrlName;
                    txtHeaderCell.Left = left;
                    txtHeaderCell.Width = width;
                    txtHeaderCell.Page = report.Pages[0];
                    txtHeaderCell.Parent = headerBand;
                }

                txtHeaderCell.Linked = true;
                txtHeaderCell.Text.Value = !string.IsNullOrEmpty(column.UserText) ? column.UserText : column.DefaultText;
                txtHeaderCell.ClientRectangle = new RectangleD(left, top, width, txtHeaderCell.Height);
                txtHeaderCell.Height = 0.6;
                left = txtHeaderCell.Left + width;

                StiBorder border = new StiBorder();
                border.Side = border.Side | StiBorderSides.Left;
                border.Side = border.Side | StiBorderSides.Right;
                border.Side = border.Side | StiBorderSides.Top;
                border.Side = border.Side | StiBorderSides.Bottom;
                border.Color = Color.FromName("Black");

                txtHeaderCell.CanShrink = true;
                txtHeaderCell.CanGrow = true;
                txtHeaderCell.GrowToHeight = true;
                txtHeaderCell.HorAlignment = StiTextHorAlignment.Center;
                txtHeaderCell.VertAlignment = StiVertAlignment.Center;
                txtHeaderCell.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                txtHeaderCell.Border = border;

                if (txtHeaderCell.Height > maxHeight)
                {
                    headerBand.Height = txtHeaderCell.Height;
                }

                headerBand.Components.Add(txtHeaderCell);
            }

            report.Pages[0].Components.Add(headerBand);
            return report;
        }

        private static StiReport CreateDataBand(StiReport report, QuickReportViewModel quickReportViewModel, string dataSourceName)
        {
            string ctrlName = "dataBand" + dataSourceName;

            List<QuickReportColumnViewModel> orderedColumns = quickReportViewModel.Columns.Where(c => c.Enabled).OrderByDescending(c => c.Order).ToList();

            double pageWidth = report.Pages[0].Width;
            int gridWidth = quickReportViewModel.Columns.Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, quickReportViewModel.InchValue);

            double left = (pageWidth / (double)2) - (tableWidth / (double)2);
            double top = 0;

            StiDataBand dataBand = new StiDataBand();
            dataBand = new StiDataBand();
            dataBand.Name = ctrlName;
            dataBand.DataSourceName = dataSourceName;

            double maxHeight = dataBand.Height;

            for (int i = quickReportViewModel.Columns.Count - 1; i >= 0; i--)
            {
                double width = GetSizeInInch(quickReportViewModel.Columns[i].Width, quickReportViewModel.InchValue);
                StiText txtDataCell = null;
                string name = "txtDataCell_" + quickReportViewModel.Columns[i].Name;

                if (txtDataCell == null)
                {
                    txtDataCell = new StiText();

                    txtDataCell.Name = name;
                    txtDataCell.Left = left;
                    txtDataCell.Parent = dataBand;
                    txtDataCell.Page = report.Pages[0];
                }

                txtDataCell.Height = 0.4;
                txtDataCell.HorAlignment = StiTextHorAlignment.Center;

                txtDataCell.Linked = true;
                txtDataCell.Width = width;
                string functionName = string.Empty;
                if (quickReportViewModel.Columns[i].DataType == "System.DateTime" && quickReportViewModel.ReportLang == "fa")
                {
                    // functionName = "ToShamsi({0})";
                }

                txtDataCell.Text.Value = GetColumnValue(quickReportViewModel.Columns[i], dataSourceName, functionName);
                txtDataCell.ClientRectangle = new RectangleD(left, top, width, txtDataCell.Height);
                left = txtDataCell.Left + width;

                if (txtDataCell.Height > maxHeight)
                {
                    dataBand.Height = txtDataCell.Height;
                }

                StiBorder border = new StiBorder();

                border.Side = border.Side | StiBorderSides.Left;
                border.Side = border.Side | StiBorderSides.Right;
                border.Side = border.Side | StiBorderSides.Top;
                border.Side = border.Side | StiBorderSides.Bottom;
                border.Color = Color.FromName("Black");
                StiBrush brush = null;

                txtDataCell.CanShrink = true;
                txtDataCell.CanGrow = true;
                txtDataCell.GrowToHeight = true;
                txtDataCell.HorAlignment = StiTextHorAlignment.Center;
                txtDataCell.VertAlignment = StiVertAlignment.Center;
                txtDataCell.Font = new System.Drawing.Font("IranSansWeb", 8, FontStyle.Regular);
                txtDataCell.Border = border;

                if (brush != null)
                {
                    txtDataCell.Brush = brush;
                }

                txtDataCell.TextOptions.RightToLeft = true;
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

        private readonly IReportRepository _repository;
        private readonly IReportSystemRepository _sysRepository;
        private readonly IConfigRepository _configRepository;
    }
}