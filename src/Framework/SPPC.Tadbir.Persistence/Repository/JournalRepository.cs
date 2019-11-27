using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Helpers;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای محاسبه اطلاعات گزارش دفتر روزنامه را پیاده سازی می کند
    /// </summary>
    public class JournalRepository : RepositoryBase, IJournalRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public JournalRepository(IRepositoryContext context, ISystemRepository system)
            : base(context)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ</returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowAsync(parameters);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByDateByRowWithDetailAsync(parameters);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerAsync(parameters);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryAsync(parameters);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByDateLedgerSummaryAsync(parameters);
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalByDateLedgerSummaryByDateAsync(parameters);
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalByDateMonthlyLedgerSummaryAsync(parameters);
                    break;
                default:
                    break;
            }

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
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowByBranchAsync(parameters);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByDateByRowDetailByBranchAsync(parameters);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerByBranchAsync(parameters);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryByBranchAsync(parameters);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByDateLedgerSummaryByBranchAsync(parameters);
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalByDateLedgerSummaryByDateByBranchAsync(parameters);
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalByDateMonthlyLedgerSummaryByBranchAsync(parameters);
                    break;
                default:
                    break;
            }

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
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByNoByRowAsync(parameters);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByNoByRowDetailAsync(parameters);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByNoByLedgerAsync(parameters);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByNoBySubsidiaryAsync(parameters);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByNoLedgerSummaryAsync(parameters);
                    break;
                default:
                    break;
            }

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
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByNoByRowByBranchAsync(parameters);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByNoByRowDetailByBranchAsync(parameters);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByNoByLedgerByBranchAsync(parameters);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByNoBySubsidiaryByBranchAsync(parameters);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByNoLedgerSummaryByBranchAsync(parameters);
                    break;
                default:
                    break;
            }

            return journal;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
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
            foreach (var byDateByNo in GetGroupByThenByItems(
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
            foreach (var byDateByNo in GetGroupByThenByItems(
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
            var journalItems = await GetJournalLedgerSummaryItemsAsync(lines);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryByDateAsync(
            JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(parameters);
            foreach (var byDateByNo in GetGroupByThenByItems(
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
                monthJournal.AddRange(await GetJournalLedgerSummaryItemsAsync(monthLines));
                Array.ForEach(monthJournal.ToArray(), item => item.VoucherDate = month.End);
                journalItems.AddRange(monthJournal);
                monthJournal.Clear();
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<List<JournalItemViewModel>> GetRawJournalByDateLinesAsync(
            JournalParameters parameters)
        {
            var lines = await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(parameters.FromDate, parameters.ToDate))
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(parameters.GridOptions)
                .OrderBy(art => art.VoucherDate)
                    .ThenBy(art => art.VoucherNo)
                .ToList();
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByDateWithDetailLinesAsync(
            JournalParameters parameters)
        {
            var lines = await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(parameters.FromDate, parameters.ToDate))
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(parameters.GridOptions)
                .OrderBy(art => art.VoucherDate)
                    .ThenBy(art => art.VoucherNo)
                .ToList();
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
            foreach (var byDateByNoByBranch in GetGroupByThenByItems(
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
            foreach (var byDateByNoByBranch in GetGroupByThenByItems(
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
            foreach (var byDateByNo in GetGroupByThenByItems(
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
                foreach (var byBranch in GetGroupByThenByItems(monthLines, art => art.BranchId))
                {
                    monthJournal.AddRange(await GetJournalLedgerSummaryItemsAsync(byBranch));
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
            var lines = await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(parameters.FromDate, parameters.ToDate))
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(parameters.GridOptions)
                .OrderBy(art => art.VoucherDate)
                    .ThenBy(art => art.VoucherNo)
                        .ThenBy(art => art.BranchId)
                .ToList();
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByDateByBranchWithDetailLinesAsync(
            JournalParameters parameters)
        {
            var lines = await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(parameters.FromDate, parameters.ToDate))
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(parameters.GridOptions)
                .OrderBy(art => art.VoucherDate)
                    .ThenBy(art => art.VoucherNo)
                        .ThenBy(art => art.BranchId)
                .ToList();
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
            foreach (var byNo in GetGroupByThenByItems(
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
            foreach (var byNo in GetGroupByThenByItems(
                lines, art => art.VoucherNo))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoLedgerSummaryAsync(JournalParameters parameters)
        {
            var lines = await GetRawJournalByNoLinesAsync(parameters);
            var journalItems = await GetJournalLedgerSummaryItemsAsync(lines);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByNoLinesAsync(
            JournalParameters parameters)
        {
            var lines = await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.No >= parameters.FromNo
                    && art.Voucher.No <= parameters.ToNo)
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(parameters.GridOptions)
                .OrderBy(art => art.VoucherNo)
                .ToList();
        }

        private async Task<IList<JournalItemViewModel>> GetRawJournalByNoWithDetailLinesAsync(
            JournalParameters parameters)
        {
            var lines = await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch)
                .Where(art => art.Voucher.No >= parameters.FromNo
                    && art.Voucher.No <= parameters.ToNo)
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .ToListAsync();
            return lines
                .ApplyQuickFilter(parameters.GridOptions)
                .OrderBy(art => art.VoucherNo)
                .ToList();
        }

        #endregion

        #region Journal By No By Branch Implementation

        private async Task<JournalViewModel> GetJournalByNoByRowByBranchAsync(JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByNoLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoByRowDetailByBranchAsync(JournalParameters parameters)
        {
            var journalItems = await GetRawJournalByNoWithDetailLinesAsync(parameters);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoByLedgerByBranchAsync(JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(parameters);
            foreach (var byNo in GetGroupByThenByItems(
                lines, art => art.VoucherNo, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoBySubsidiaryByBranchAsync(JournalParameters parameters)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(parameters);
            foreach (var byNo in GetGroupByThenByItems(
                lines, art => art.VoucherNo, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byNo));
            }

            return BuildJournal(journalItems, parameters.GridOptions);
        }

        private async Task<JournalViewModel> GetJournalByNoLedgerSummaryByBranchAsync(JournalParameters parameters)
        {
            var lines = await GetRawJournalByNoLinesAsync(parameters);
            var journalItems = await GetJournalLedgerSummaryByBranchItemsAsync(lines);
            return BuildJournal(journalItems, parameters.GridOptions);
        }

        #endregion

        private async Task<IList<JournalItemViewModel>> GetJournalByLedgerItemsAsync(
            IEnumerable<JournalItemViewModel> lines)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<JournalItemViewModel, bool> allFilter = art => true;
            foreach (var byLedger in GetAccountTurnoverGroups(lines, true, 0, allFilter))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, false, 0, allFilter))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            return journalItems;
        }

        private async Task<IList<JournalItemViewModel>> GetJournalBySubsidiaryItemsAsync(
            IEnumerable<JournalItemViewModel> lines)
        {
            Func<JournalItemViewModel, bool> ledgerFilter = art => art.AccountLevel == 0;
            Func<JournalItemViewModel, bool> subsidiaryFilter = art => art.AccountLevel >= 1;
            var debitLines = new List<JournalItemViewModel>();
            foreach (var bySubsidiary in GetAccountTurnoverGroups(lines, true, 1, subsidiaryFilter))
            {
                debitLines.Add(await GetJournalItemFromGroup(bySubsidiary, bySubsidiary.Key));
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, true, 0, ledgerFilter))
            {
                debitLines.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            var creditLines = new List<JournalItemViewModel>();
            foreach (var bySubsidiary in GetAccountTurnoverGroups(lines, false, 1, subsidiaryFilter))
            {
                creditLines.Add(await GetJournalItemFromGroup(bySubsidiary, bySubsidiary.Key));
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, false, 0, ledgerFilter))
            {
                creditLines.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            return debitLines
                .OrderBy(item => item.AccountFullCode)
                .Concat(creditLines
                    .OrderBy(item => item.AccountFullCode))
                .ToList();
        }

        private async Task<IList<JournalItemViewModel>> GetJournalLedgerSummaryItemsAsync(
            IEnumerable<JournalItemViewModel> lines)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<JournalItemViewModel, bool> allFilter = art => true;
            foreach (var byLedger in GetAccountTurnoverGroups(lines, true, 0, allFilter))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, false, 0, allFilter))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            return journalItems;
        }

        private async Task<IList<JournalItemViewModel>> GetJournalLedgerSummaryByBranchItemsAsync(
            IEnumerable<JournalItemViewModel> lines)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<JournalItemViewModel, bool> allFilter = art => true;
            foreach (var byLedger in GetAccountTurnoverGroups(lines, true, 0, allFilter))
            {
                string ledgerCode = byLedger.Key;
                foreach (var byBranch in GetGroupByThenByItems(byLedger, art => art.BranchId))
                {
                    journalItems.Add(await GetJournalItemFromGroup(byBranch, ledgerCode));
                }
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, false, 0, allFilter))
            {
                string ledgerCode = byLedger.Key;
                foreach (var byBranch in GetGroupByThenByItems(byLedger, art => art.BranchId))
                {
                    journalItems.Add(await GetJournalItemFromGroup(byBranch, ledgerCode));
                }
            }

            return journalItems;
        }

        private IEnumerable<IGrouping<string, JournalItemViewModel>> GetAccountTurnoverGroups(
            IEnumerable<JournalItemViewModel> lines, bool isDebit, int groupLevel,
            Func<JournalItemViewModel, bool> lineFilter)
        {
            int codeLength = GetLevelCodeLength(groupLevel);
            Func<JournalItemViewModel, bool> turnoverCriteria = art => art.Credit > 0;
            if (isDebit)
            {
                turnoverCriteria = art => art.Debit > 0;
            }

            var turnoverGroups = lines
                .Where(turnoverCriteria)
                .Where(lineFilter)
                .OrderBy(art => art.AccountFullCode)
                .GroupBy(art => art.AccountFullCode.Substring(0, codeLength));
            return turnoverGroups;
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

        private IEnumerable<IEnumerable<JournalItemViewModel>> GetGroupByThenByItems<TKey1>(
            IEnumerable<JournalItemViewModel> lines, Func<JournalItemViewModel, TKey1> firstSelector)
        {
            foreach (var byFirst in lines
                .OrderBy(firstSelector)
                .GroupBy(firstSelector))
            {
                yield return byFirst;
            }
        }

        private IEnumerable<IEnumerable<JournalItemViewModel>> GetGroupByThenByItems<TKey1, TKey2>(
            IEnumerable<JournalItemViewModel> lines, Func<JournalItemViewModel, TKey1> firstSelector,
            Func<JournalItemViewModel, TKey2> secondSelector)
        {
            foreach (var byFirst in lines
                .OrderBy(firstSelector)
                .GroupBy(firstSelector))
            {
                foreach (var bySecond in byFirst
                    .OrderBy(secondSelector)
                    .GroupBy(secondSelector))
                {
                    yield return bySecond;
                }
            }
        }

        private IEnumerable<IEnumerable<JournalItemViewModel>> GetGroupByThenByItems<TKey1, TKey2, TKey3>(
            IEnumerable<JournalItemViewModel> lines, Func<JournalItemViewModel, TKey1> firstSelector,
            Func<JournalItemViewModel, TKey2> secondSelector, Func<JournalItemViewModel, TKey3> thirdSelector)
        {
            foreach (var byFirst in lines
                .OrderBy(firstSelector)
                .GroupBy(firstSelector))
            {
                foreach (var bySecond in byFirst
                    .OrderBy(secondSelector)
                    .GroupBy(secondSelector))
                {
                    foreach (var byThird in bySecond
                        .OrderBy(thirdSelector)
                        .GroupBy(thirdSelector))
                    {
                        yield return byThird;
                    }
                }
            }
        }

        private int GetLevelCodeLength(int level)
        {
            var fullConfig = Config
                .GetViewTreeConfigByViewAsync(ViewName.Account)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
        }

        #endregion

        private readonly ISystemRepository _system;
    }
}
