using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر روزنامه را پیاده سازی می کند
    /// </summary>
    public class JournalRepository : LoggingRepository<Account, object>, IJournalRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="report">امکانات عمومی مورد نیاز برای گزارشگیری را فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات جاری ایجاد لاگ را فراهم می کند</param>
        public JournalRepository(IRepositoryContext context, ISystemRepository system,
            IReportUtility report, ILogConfigRepository config)
            : base(context, config, system.Logger)
        {
            _system = system;
            _report = report;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ</returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowAsync(parameters);
                    sourceList = SourceListId.JournalByDateByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByDateByRowWithDetailAsync(parameters);
                    sourceList = SourceListId.JournalByDateByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerAsync(parameters);
                    sourceList = SourceListId.JournalByDateByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryAsync(parameters);
                    sourceList = SourceListId.JournalByDateBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByDateLedgerSummaryAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummary;
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalByDateLedgerSummaryByDateAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummaryByDate;
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalByDateMonthlyLedgerSummaryAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummaryByMonth;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByDateByBranchAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByDateByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByDateByRowDetailByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByDateByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByDateByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByDateBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByDateLedgerSummaryByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummary;
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalByDateLedgerSummaryByDateByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummaryByDate;
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalByDateMonthlyLedgerSummaryByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByDateSummaryByMonth;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند</returns>
        public async Task<JournalViewModel> GetJournalByNoAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByNoByRowAsync(parameters);
                    sourceList = SourceListId.JournalByNoByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByNoByRowDetailAsync(parameters);
                    sourceList = SourceListId.JournalByNoByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByNoByLedgerAsync(parameters);
                    sourceList = SourceListId.JournalByNoByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByNoBySubsidiaryAsync(parameters);
                    sourceList = SourceListId.JournalByNoBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByNoLedgerSummaryAsync(parameters);
                    sourceList = SourceListId.JournalByNoSummary;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByNoByBranchAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByNoByRowByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByNoByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByNoByRowDetailByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByNoByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByNoByLedgerByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByNoByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByNoBySubsidiaryByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByNoBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByNoLedgerSummaryByBranchAsync(parameters);
                    sourceList = SourceListId.JournalByNoSummary;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.Journal; }
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        #region Journal Implementation

        private static JournalViewModel BuildJournal(
            IEnumerable<JournalItemViewModel> journalItems, GridOptions gridOptions)
        {
            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems.Apply(gridOptions, false));
            journal.DebitSum = journal.Items.Sum(item => item.Debit);
            journal.CreditSum = journal.Items.Sum(item => item.Credit);
            return journal;
        }

        #region Journal By Date Implementation

        private async Task<JournalViewModel> GetJournalByDateByRowAsync(
            JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByDateLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateByRowWithDetailAsync(
            JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByDateWithDetailLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateByLedgerAsync(
            JournalParameters parameters)
        {
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            var journalItems = new List<JournalItemViewModel>();
            foreach (var byDateByNo in _report.GetGroupByItems(
                lines, art => art.VoucherDate.Date, art => art.VoucherNo))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateBySubsidiaryAsync(
            JournalParameters parameters)
        {
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            var journalItems = new List<JournalItemViewModel>();
            foreach (var byDateByNo in _report.GetGroupByItems(
                lines, art => art.VoucherDate.Date, art => art.VoucherNo))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryAsync(
            JournalParameters parameters)
        {
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            var journalItems = await GetJournalByLedgerItemsAsync(lines);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryByDateAsync(
            JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            foreach (var byDateByNo in _report.GetGroupByItems(
                lines, art => art.VoucherDate.Date))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateMonthlyLedgerSummaryAsync(
            JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var monthJournal = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            var monthEnum = new MonthEnumerator(parameters.FromDate, parameters.ToDate, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = lines
                    .Where(art => art.VoucherDate.IsBetween(month.Start, month.End));
                monthJournal.AddRange(await GetJournalByLedgerItemsAsync(monthLines));
                Array.ForEach(monthJournal.ToArray(), item => item.VoucherDate = month.End);
                journalItems.AddRange(monthJournal);
                monthJournal.Clear();
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<List<JournalItemViewModel>> GetRawJournalByDateLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch);
            return await _report.GetRawReportByDateLinesAsync<JournalItemViewModel>(
                query, parameters.FromDate, parameters.ToDate, parameters.GridOptions);
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByDateWithDetailLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch);
            return await _report.GetRawReportByDateLinesAsync<JournalItemViewModel>(
                query, parameters.FromDate, parameters.ToDate, parameters.GridOptions);
        }

        #endregion

        #region Journal By Date By Branch Implementation

        private async Task<JournalViewModel> GetJournalByDateByRowByBranchAsync(
            JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByDateByBranchLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateByRowDetailByBranchAsync(
            JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByDateByBranchWithDetailLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateByLedgerByBranchAsync(
            JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            foreach (var byDateByNoByBranch in _report.GetGroupByItems(
                lines, art => art.VoucherDate.Date, art => art.VoucherNo, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNoByBranch));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateBySubsidiaryByBranchAsync(
            JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            foreach (var byDateByNoByBranch in _report.GetGroupByItems(
                lines, art => art.VoucherDate.Date, art => art.VoucherNo, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byDateByNoByBranch));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryByBranchAsync(
            JournalParameters parameters)
        {
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            var journalItems = await GetJournalLedgerSummaryByBranchItemsAsync(lines);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryByDateByBranchAsync(
            JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            foreach (var byDateByNo in _report.GetGroupByItems(
                lines, art => art.VoucherDate.Date, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateMonthlyLedgerSummaryByBranchAsync(
            JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var monthJournal = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            var monthEnum = new MonthEnumerator(parameters.FromDate, parameters.ToDate, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = lines
                    .Where(art => art.VoucherDate.IsBetween(month.Start, month.End));
                foreach (var byBranch in _report.GetGroupByItems(monthLines, art => art.BranchId))
                {
                    monthJournal.AddRange(await GetJournalByLedgerItemsAsync(byBranch));
                    Array.ForEach(monthJournal.ToArray(), item => item.VoucherDate = month.End);
                    journalItems.AddRange(monthJournal);
                    monthJournal.Clear();
                }
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<List<JournalItemViewModel>> GetRawJournalByDateByBranchLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch);
            return await _report.GetRawReportByDateByBranchLinesAsync<JournalItemViewModel>(
                query, parameters.FromDate, parameters.ToDate, parameters.GridOptions);
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByDateByBranchWithDetailLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch);
            return await _report.GetRawReportByDateByBranchLinesAsync<JournalItemViewModel>(
                query, parameters.FromDate, parameters.ToDate, parameters.GridOptions);
        }

        #endregion

        #region Journal By No Implementation

        private async Task<JournalViewModel> GetJournalByNoByRowAsync(JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByNoLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoByRowDetailAsync(JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByNoWithDetailLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoByLedgerAsync(JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(parameters);
            foreach (var byNo in _report.GetGroupByItems(
                lines, art => art.VoucherNo))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoBySubsidiaryAsync(JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(parameters);
            foreach (var byNo in _report.GetGroupByItems(
                lines, art => art.VoucherNo))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoLedgerSummaryAsync(JournalParameters parameters)
        {
            var lines = await GetRawJournalByNoLinesAsync(parameters);
            var journalItems = await GetJournalByLedgerItemsAsync(lines);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByNoLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch);
            return await _report.GetRawReportByNumberLinesAsync<JournalItemViewModel>(
                query, parameters.FromNo, parameters.ToNo, parameters.GridOptions);
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByNoWithDetailLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch);
            return await _report.GetRawReportByNumberLinesAsync<JournalItemViewModel>(
                query, parameters.FromNo, parameters.ToNo, parameters.GridOptions);
        }

        #endregion

        #region Journal By No By Branch Implementation

        private async Task<JournalViewModel> GetJournalByNoByRowByBranchAsync(JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByNoByBranchLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoByRowDetailByBranchAsync(JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByNoByBranchWithDetailLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoByLedgerByBranchAsync(JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoByBranchLinesAsync(parameters);
            foreach (var byNo in _report.GetGroupByItems(
                lines, art => art.VoucherNo, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoBySubsidiaryByBranchAsync(JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoByBranchLinesAsync(parameters);
            foreach (var byNo in _report.GetGroupByItems(
                lines, art => art.VoucherNo, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoLedgerSummaryByBranchAsync(JournalParameters parameters)
        {
            var lines = await GetRawJournalByNoByBranchLinesAsync(parameters);
            var journalItems = await GetJournalLedgerSummaryByBranchItemsAsync(lines);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByNoByBranchLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch);
            return await _report.GetRawReportByNumberByBranchLinesAsync<JournalItemViewModel>(
                query, parameters.FromNo, parameters.ToNo, parameters.GridOptions);
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByNoByBranchWithDetailLinesAsync(
            JournalParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch);
            return await _report.GetRawReportByNumberByBranchLinesAsync<JournalItemViewModel>(
                query, parameters.FromNo, parameters.ToNo, parameters.GridOptions);
        }

        #endregion

        private async Task<IList<JournalItemViewModel>> GetJournalByLedgerItemsAsync(
            IEnumerable<JournalItemViewModel> lines)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<JournalItemViewModel, bool> allFilter = art => true;
            foreach (var byLedger in _report.GetTurnoverGroups(lines, 0, art => art.Debit > 0))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            foreach (var byLedger in _report.GetTurnoverGroups(lines, 0, art => art.Credit > 0))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            return journalItems;
        }

        private async Task<IList<JournalItemViewModel>> GetJournalBySubsidiaryItemsAsync(
            IEnumerable<JournalItemViewModel> lines)
        {
            Func<JournalItemViewModel, bool> ledgerFilter =
                art => art.AccountLevel == 0 && art.Debit > 0;
            Func<JournalItemViewModel, bool> subsidiaryFilter =
                art => art.AccountLevel >= 1 && art.Debit > 0;
            var debitLines = new List<JournalItemViewModel>();
            foreach (var bySubsidiary in _report.GetTurnoverGroups(lines, 1, subsidiaryFilter))
            {
                debitLines.Add(await GetJournalItemFromGroup(bySubsidiary, bySubsidiary.Key));
            }

            foreach (var byLedger in _report.GetTurnoverGroups(lines, 0, ledgerFilter))
            {
                debitLines.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            var creditLines = new List<JournalItemViewModel>();
            ledgerFilter = art => art.AccountLevel == 0 && art.Credit > 0;
            subsidiaryFilter = art => art.AccountLevel >= 1 && art.Credit > 0;
            foreach (var bySubsidiary in _report.GetTurnoverGroups(lines, 1, subsidiaryFilter))
            {
                creditLines.Add(await GetJournalItemFromGroup(bySubsidiary, bySubsidiary.Key));
            }

            foreach (var byLedger in _report.GetTurnoverGroups(lines, 0, ledgerFilter))
            {
                creditLines.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            return debitLines
                .OrderBy(item => item.AccountFullCode)
                .Concat(creditLines
                    .OrderBy(item => item.AccountFullCode))
                .ToList();
        }

        private async Task<IList<JournalItemViewModel>> GetJournalLedgerSummaryByBranchItemsAsync(
            IEnumerable<JournalItemViewModel> lines)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<JournalItemViewModel, bool> allFilter = art => true;
            foreach (var byLedger in _report.GetTurnoverGroups(lines, 0, art => art.Debit > 0))
            {
                string ledgerCode = byLedger.Key;
                foreach (var byBranch in _report.GetGroupByItems(byLedger, art => art.BranchId))
                {
                    journalItems.Add(await GetJournalItemFromGroup(byBranch, ledgerCode));
                }
            }

            foreach (var byLedger in _report.GetTurnoverGroups(lines, 0, art => art.Credit > 0))
            {
                string ledgerCode = byLedger.Key;
                foreach (var byBranch in _report.GetGroupByItems(byLedger, art => art.BranchId))
                {
                    journalItems.Add(await GetJournalItemFromGroup(byBranch, ledgerCode));
                }
            }

            return journalItems;
        }

        private async Task<JournalItemViewModel> GetJournalItemFromGroup(
            IEnumerable<JournalItemViewModel> itemGroup, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var item = itemGroup.First();
            var journalItem = new JournalItemViewModel()
            {
                VoucherDate = item.VoucherDate,
                VoucherNo = item.VoucherNo,
                AccountFullCode = fullCode,
                AccountName = account.Name,
                BranchName = item.BranchName,
                Debit = itemGroup.Sum(art => art.Debit),
                Credit = itemGroup.Sum(art => art.Credit),
                Description = item.AccountName
            };

            return journalItem;
        }

        #endregion

        private readonly ISystemRepository _system;
        private readonly IReportUtility _report;
    }
}
