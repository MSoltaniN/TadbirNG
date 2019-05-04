using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
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
using Stimulsoft.Report.Dictionary;

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
            quickReport.ReportUnit = StiReportUnitType.Inches;
            var dataSourceName = "root";
            ////StiJsonDatabase database = new StiJsonDatabase("dataset", String.Empty);
            ////database.Alias = "dataset";
            ////StiDataTableSource dataSource = new StiDataTableSource("Vouchers", "Vouchers", "Vouchers");
            ////foreach (var q in qr.Columns)
            ////{
            ////    dataSource.Columns.Add(new StiDataColumn(q.Name, q.Name, Type.GetType(q.DataType)));
            ////}

            ////quickReport.DataSources.Clear();
            ////quickReport.DataSources.Add(dataSource);
            ////quickReport.Dictionary.Databases.Add(database);

            quickReport = CreateReportFooterBand(quickReport);
            quickReport = CreateReportHeaderBand(quickReport);
            quickReport = CreatePageHeaderBand(quickReport, qr.ReportTitle);
            quickReport = CreatePageFooterBand(quickReport);
            quickReport = CreateHeaderBand(quickReport, qr.Columns, qr.InchValue, dataSourceName);
            quickReport = CreateDataBand(quickReport, qr.Columns, qr.InchValue, dataSourceName);

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

        // GET: api/reports/journal/by-date/by-row
        ////[Route(ReportApi.JournalByDateByRowUrl)]
        public async Task<IActionResult> GetJournalByDateByRowAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateByRowAsync(from.Value, to.Value);
            PrepareJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-date/by-row-detail
        [Route(ReportApi.JournalByDateByRowDetailUrl)]
        public async Task<IActionResult> GetJournalByDateByRowWithDetailAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateByRowWithDetailAsync(from.Value, to.Value);
            PrepareJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-date/by-ledger
        [Route(ReportApi.JournalByDateByLedgerUrl)]
        public async Task<IActionResult> GetJournalByDateByLedgerAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateByLedgerAsync(from.Value, to.Value);
            PrepareJournal(journal, gridOptions);
            Localize(journal);
            return Json(journal);
        }

        // GET: api/reports/journal/by-date/by-subsid
        [Route(ReportApi.JournalByDateBySubsidiaryUrl)]
        public async Task<IActionResult> GetJournalByDateBySubsidiaryAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateBySubsidiaryAsync(from.Value, to.Value);
            PrepareJournal(journal, gridOptions);
            Localize(journal);
            return Json(journal);
        }

        // GET: api/reports/journal/by-date/summary
        [Route(ReportApi.JournalByDateLedgerSummaryUrl)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateLedgerSummaryAsync(from.Value, to.Value, gridOptions);
            PrepareSummaryJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-date/summary/by-branch
        [Route(ReportApi.JournalByDateLedgerSummaryByBranchUrl)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByBranchAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateLedgerSummaryByBranchAsync(
                from.Value, to.Value, gridOptions);
            PrepareSummaryJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-date/sum-by-date
        [Route(ReportApi.JournalByDateLedgerSummaryByDateUrl)]
        public async Task<IActionResult> GetJournalByDateLedgerSummaryByDateAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateLedgerSummaryByDateAsync(from.Value, to.Value);
            PrepareJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-date/sum-by-month
        [Route(ReportApi.JournalByDateMonthlyLedgerSummaryUrl)]
        public async Task<IActionResult> GetJournalByDateMonthlyLedgerSummaryAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateMonthlyLedgerSummaryAsync(from.Value, to.Value, gridOptions);
            PrepareSummaryJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-no/by-row
        [Route(ReportApi.JournalByNoByRowUrl)]
        public async Task<IActionResult> GetJournalByNoByRowAsync(int from, int to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByNoByRowAsync(from, to);
            PrepareJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-no/by-row-detail
        [Route(ReportApi.JournalByNoByRowDetailUrl)]
        public async Task<IActionResult> GetJournalByNoByRowWithDetailAsync(int from, int to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByNoByRowWithDetailAsync(from, to);
            PrepareJournal(journal, gridOptions);
            return Json(journal);
        }

        // GET: api/reports/journal/by-no/by-ledger
        [Route(ReportApi.JournalByNoByLedgerUrl)]
        public async Task<IActionResult> GetJournalByNoByLedgerAsync(int from, int to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByNoByLedgerAsync(from, to);
            PrepareJournal(journal, gridOptions);
            Localize(journal);
            return Json(journal);
        }

        // GET: api/reports/journal/by-no/by-subsid
        [Route(ReportApi.JournalByNoBySubsidiaryUrl)]
        public async Task<IActionResult> GetJournalByNoBySubsidiaryAsync(int from, int to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByNoBySubsidiaryAsync(from, to);
            PrepareJournal(journal, gridOptions);
            Localize(journal);
            return Json(journal);
        }

        // GET: api/reports/journal/by-no/summary
        [Route(ReportApi.JournalByNoLedgerSummaryUrl)]
        public async Task<IActionResult> GetJournalByNoLedgerSummaryAsync(int from, int to)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByNoLedgerSummaryAsync(from, to, gridOptions);
            PrepareSummaryJournal(journal, gridOptions);
            return Json(journal);
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

            double height = double.Parse("0.25", System.Globalization.CultureInfo.InvariantCulture);
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
            double top = 0;
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
                txtHeaderCell.Height = 0.8;
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
                txtHeaderCell.Font = new System.Drawing.Font("B Zar", 8, FontStyle.Regular);
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

        private static StiReport CreateDataBand(StiReport report, IList<QuickReportColumnViewModel> columns, int oneInchInPixels, string dataSourceName)
        {
            string ctrlName = "dataBand" + dataSourceName;

            List<QuickReportColumnViewModel> orderedColumns = columns.Where(c => c.Enabled).OrderByDescending(c => c.Order).ToList();

            double pageWidth = report.Pages[0].Width;
            int gridWidth = columns.Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, oneInchInPixels);

            double left = (pageWidth / (double)2) - (tableWidth / (double)2);
            double top = 0;

            StiDataBand dataBand = new StiDataBand();
            dataBand = new StiDataBand();
            dataBand.Name = ctrlName;
            dataBand.DataSourceName = dataSourceName;

            double maxHeight = dataBand.Height;

            for (int i = columns.Count - 1; i >= 0; i--)
            {
                double width = GetSizeInInch(columns[i].Width, oneInchInPixels);
                StiText txtDataCell = null;
                string name = "txtDataCell_" + columns[i].Name;

                if (txtDataCell == null)
                {
                    txtDataCell = new StiText();

                    txtDataCell.Name = name;
                    txtDataCell.Left = left;
                    txtDataCell.Parent = dataBand;
                    txtDataCell.Page = report.Pages[0];
                }

                txtDataCell.Height = 0.8;
                txtDataCell.HorAlignment = StiTextHorAlignment.Center;

                txtDataCell.Linked = true;
                txtDataCell.Width = width;
                txtDataCell.Text.Value = GetColumnValue(columns[i], dataSourceName);
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
                txtDataCell.Font = new System.Drawing.Font("B Zar", 8, FontStyle.Regular);
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

        private static string GetColumnValue(QuickReportColumnViewModel column, string dataSourceName)
        {
            string fullColumnName = "{" + dataSourceName + "." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(column.Name) + "}";
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

        private void PrepareJournal(JournalWithDetailViewModel journal, GridOptions gridOptions)
        {
            var userItems = journal.Items.Apply(gridOptions, false);
            journal.DebitSum = userItems.Select(item => item.Debit).Sum();
            journal.CreditSum = userItems.Select(item => item.Credit).Sum();
            SetItemCount(userItems.Count());
            journal.SetItems(journal.Items.Apply(gridOptions).ToList());
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var journalItem in journal.Items)
            {
                journalItem.RowNo = rowNo++;
            }
        }

        private void PrepareJournal(JournalViewModel journal, GridOptions gridOptions)
        {
            var userItems = journal.Items.Apply(gridOptions, false);
            journal.DebitSum = userItems.Select(item => item.Debit).Sum();
            journal.CreditSum = userItems.Select(item => item.Credit).Sum();
            SetItemCount(userItems.Count());
            journal.SetItems(journal.Items.Apply(gridOptions).ToList());
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var journalItem in journal.Items)
            {
                journalItem.RowNo = rowNo++;
            }
        }

        private void PrepareSummaryJournal(JournalViewModel journal, GridOptions gridOptions)
        {
            journal.DebitSum = journal.Items.Select(item => item.Debit).Sum();
            journal.CreditSum = journal.Items.Select(item => item.Credit).Sum();
            SetItemCount(journal.Items.Count());
            journal.SetItems(journal.Items.Apply(gridOptions).ToList());
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var journalItem in journal.Items)
            {
                journalItem.RowNo = rowNo++;
            }
        }

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

        private void Localize(JournalViewModel journal)
        {
            foreach (var journalItem in journal.Items)
            {
                journalItem.Description = _strings[AppStrings.AsQuotedInVoucherLines];
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

        private void Sanitize(ref DateTime? from, ref DateTime? to)
        {
            if (from == null || to == null)
            {
                DateTime rangeFrom, rangeTo;
                _configRepository.SetCurrentContext(SecurityContext.User);
                _configRepository.GetCurrentFiscalDateRange(out rangeFrom, out rangeTo);
                from = from ?? rangeFrom;
                to = to ?? rangeTo;
            }
        }

        private readonly IReportRepository _repository;
        private readonly IReportSystemRepository _sysRepository;
        private readonly IConfigRepository _configRepository;
    }
}