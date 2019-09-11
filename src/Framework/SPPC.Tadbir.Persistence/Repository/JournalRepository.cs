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
using SPPC.Tadbir.ViewModel.Auth;
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
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        /// <param name="configRepository">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        public JournalRepository(IRepositoryContext context, ISecureRepository repository,
            IConfigRepository configRepository)
            : base(context)
        {
            _repository = repository;
            _configRepository = configRepository;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ</returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(
            JournalMode journalMode, DateTime from, DateTime to, GridOptions gridOptions = null)
        {
            var journal = default(JournalViewModel);
            switch (journalMode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowAsync(from, to);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByDateByRowWithDetailAsync(from, to);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerAsync(from, to);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryAsync(from, to);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByDateLedgerSummaryAsync(from, to, gridOptions);
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalByDateLedgerSummaryByDateAsync(from, to);
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalByDateMonthlyLedgerSummaryAsync(from, to, gridOptions);
                    break;
                default:
                    break;
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByDateByBranchAsync(
            JournalMode journalMode, DateTime from, DateTime to, GridOptions gridOptions = null)
        {
            var journal = default(JournalViewModel);
            switch (journalMode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowByBranchAsync(from, to);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByDateByRowDetailByBranchAsync(from, to);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerByBranchAsync(from, to);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryByBranchAsync(from, to);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByDateLedgerSummaryByBranchAsync(
                        from, to, gridOptions);
                    break;
                case JournalMode.LedgerSummaryByDate:
                    journal = await GetJournalByDateLedgerSummaryByDateByBranchAsync(from, to);
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    journal = await GetJournalByDateMonthlyLedgerSummaryAsync(from, to, gridOptions);
                    break;
                default:
                    break;
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">شماره ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">شماره انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند</returns>
        public async Task<JournalViewModel> GetJournalByNoAsync(
            JournalMode journalMode, int from, int to, GridOptions gridOptions = null)
        {
            var journal = default(JournalViewModel);
            switch (journalMode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByNoByRowAsync(from, to);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByNoByRowDetailAsync(from, to);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByNoByLedgerAsync(from, to);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByNoBySubsidiaryAsync(from, to);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByNoLedgerSummaryAsync(from, to, gridOptions);
                    break;
                default:
                    break;
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">شماره ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">شماره انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByNoByBranchAsync(
            JournalMode journalMode, int from, int to, GridOptions gridOptions = null)
        {
            var journal = default(JournalViewModel);
            switch (journalMode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByNoByRowByBranchAsync(from, to);
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = await GetJournalByNoByRowDetailByBranchAsync(from, to);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByNoByLedgerByBranchAsync(from, to);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByNoBySubsidiaryByBranchAsync(from, to);
                    break;
                case JournalMode.LedgerSummary:
                    journal = await GetJournalByNoLedgerSummaryByBranchAsync(from, to, gridOptions);
                    break;
                default:
                    break;
            }

            return journal;
        }

        #region Journal Implementation

        private static JournalViewModel BuildJournal(IEnumerable<JournalItemViewModel> journalItems)
        {
            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        #region Journal By Date Implementation

        private async Task<JournalViewModel> GetJournalByDateByRowAsync(
            DateTime from, DateTime to)
        {
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var journalItems = GetJournalByDateByRowItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateByRowWithDetailAsync(
            DateTime from, DateTime to)
        {
            var lines = await GetRawJournalByDateWithDetailLinesAsync(from, to);
            var journalItems = GetJournalByDateByRowItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateByLedgerAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDateByNo in GetGroupByThenByItems(
                lines, art => art.Voucher.Date, art => art.Voucher.No))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateBySubsidiaryAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDateByNo in GetGroupByThenByItems(
                lines, art => art.Voucher.Date, art => art.Voucher.No))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryAsync(
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var journalItems = await GetJournalLedgerSummaryItemsAsync(lines, gridOptions);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryByDateAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDateByNo in GetGroupByThenByItems(
                lines, art => art.Voucher.Date))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateMonthlyLedgerSummaryAsync(
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var journalItems = new List<JournalItemViewModel>();
            var monthJournal = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var monthEnum = new MonthEnumerator(from, to, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = lines
                    .Where(art => art.Voucher.Date.IsBetween(month.Start, month.End));
                monthJournal.AddRange(await GetJournalLedgerSummaryItemsAsync(monthLines, gridOptions));
                Array.ForEach(monthJournal.ToArray(), item => item.VoucherDate = month.End);
                journalItems.AddRange(monthJournal);
                monthJournal.Clear();
            }

            return BuildJournal(journalItems);
        }

        private async Task<IList<VoucherLine>> GetRawJournalByDateLinesAsync(
            DateTime from, DateTime to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .ToListAsync();
        }

        private async Task<IList<VoucherLine>> GetRawJournalByDateWithDetailLinesAsync(
            DateTime from, DateTime to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch)
                .Where(art => art.Voucher.Date.IsBetween(from, to))
                .ToListAsync();
        }

        private IEnumerable<JournalItemViewModel> GetJournalByDateByRowItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                .Select(art => Mapper.Map<JournalItemViewModel>(art));
        }

        #endregion

        #region Journal By Date By Branch Implementation

        private async Task<JournalViewModel> GetJournalByDateByRowByBranchAsync(
            DateTime from, DateTime to)
        {
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var journalItems = GetJournalByDateByRowByBranchItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateByRowDetailByBranchAsync(
            DateTime from, DateTime to)
        {
            var lines = await GetRawJournalByDateWithDetailLinesAsync(from, to);
            var journalItems = GetJournalByDateByRowByBranchItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateByLedgerByBranchAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDateByNoByBranch in GetGroupByThenByItems(
                lines, art => art.Voucher.Date, art => art.Voucher.No,
                art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNoByBranch));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateBySubsidiaryByBranchAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDateByNoByBranch in GetGroupByThenByItems(
                lines, art => art.Voucher.Date, art => art.Voucher.No,
                art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byDateByNoByBranch));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryByBranchAsync(
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var journalItems = await GetJournalLedgerSummaryByBranchItemsAsync(lines, gridOptions);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateLedgerSummaryByDateByBranchAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDateByNo in GetGroupByThenByItems(
                lines, art => art.Voucher.Date, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateMonthlyLedgerSummaryByBranchAsync(
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var journalItems = new List<JournalItemViewModel>();
            var monthJournal = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var monthEnum = new MonthEnumerator(from, to, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = lines
                    .Where(art => art.Voucher.Date.IsBetween(month.Start, month.End));
                foreach (var byBranch in GetGroupByThenByItems(monthLines, art => art.BranchId))
                {
                    monthJournal.AddRange(await GetJournalLedgerSummaryItemsAsync(byBranch, gridOptions));
                    Array.ForEach(monthJournal.ToArray(), item => item.VoucherDate = month.End);
                    journalItems.AddRange(monthJournal);
                    monthJournal.Clear();
                }
            }

            return BuildJournal(journalItems);
        }

        private IEnumerable<JournalItemViewModel> GetJournalByDateByRowByBranchItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                        .ThenBy(art => art.BranchId)
                .Select(art => Mapper.Map<JournalItemViewModel>(art));
        }

        #endregion

        #region Journal By No Implementation

        private async Task<JournalViewModel> GetJournalByNoByRowAsync(int from, int to)
        {
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            var journalItems = GetJournalByNoByRowItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoByRowDetailAsync(int from, int to)
        {
            var lines = await GetRawJournalByNoWithDetailLinesAsync(from, to);
            var journalItems = GetJournalByNoByRowItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoByLedgerAsync(
            int from, int to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            foreach (var byNo in GetGroupByThenByItems(
                lines, art => art.Voucher.No))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoBySubsidiaryAsync(
            int from, int to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            foreach (var byNo in GetGroupByThenByItems(
                lines, art => art.Voucher.No))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoLedgerSummaryAsync(
            int from, int to, GridOptions gridOptions)
        {
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            var journalItems = await GetJournalLedgerSummaryItemsAsync(lines, gridOptions);
            return BuildJournal(journalItems);
        }

        private async Task<IList<VoucherLine>> GetRawJournalByNoLinesAsync(int from, int to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.No >= from
                    && art.Voucher.No <= to)
                .ToListAsync();
        }

        private async Task<IList<VoucherLine>> GetRawJournalByNoWithDetailLinesAsync(int from, int to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch)
                .Where(art => art.Voucher.No >= from && art.Voucher.No <= to)
                .ToListAsync();
        }

        private IEnumerable<JournalItemViewModel> GetJournalByNoByRowItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => art.Voucher.No)
                .Select(art => Mapper.Map<JournalItemViewModel>(art));
        }

        #endregion

        #region Journal By No By Branch Implementation

        private async Task<JournalViewModel> GetJournalByNoByRowByBranchAsync(int from, int to)
        {
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            var journalItems = GetJournalByNoByRowByBranchItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoByRowDetailByBranchAsync(int from, int to)
        {
            var lines = await GetRawJournalByNoWithDetailLinesAsync(from, to);
            var journalItems = GetJournalByNoByRowByBranchItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoByLedgerByBranchAsync(
            int from, int to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            foreach (var byNo in GetGroupByThenByItems(
                lines, art => art.Voucher.No, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalByLedgerItemsAsync(byNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoBySubsidiaryByBranchAsync(
            int from, int to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            foreach (var byNo in GetGroupByThenByItems(
                lines, art => art.Voucher.No, art => art.BranchId))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByNoLedgerSummaryByBranchAsync(
            int from, int to, GridOptions gridOptions)
        {
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            var journalItems = await GetJournalLedgerSummaryByBranchItemsAsync(lines, gridOptions);
            return BuildJournal(journalItems);
        }

        private IEnumerable<JournalItemViewModel> GetJournalByNoByRowByBranchItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => art.Voucher.No)
                    .ThenBy(art => art.BranchId)
                .Select(art => Mapper.Map<JournalItemViewModel>(art));
        }

        #endregion

        private async Task<IList<JournalItemViewModel>> GetJournalByLedgerItemsAsync(
            IEnumerable<VoucherLine> lines)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<VoucherLine, bool> allFilter = art => true;
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
            IEnumerable<VoucherLine> lines)
        {
            Func<VoucherLine, bool> ledgerFilter = art => art.Account.Level == 0;
            Func<VoucherLine, bool> subsidiaryFilter = art => art.Account.Level >= 1;
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
            IEnumerable<VoucherLine> lines, GridOptions gridOptions)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<JournalItemViewModel, bool> allFilter = art => true;
            var items = lines
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .Apply(gridOptions, false)
                .ToList();
            foreach (var byLedger in GetAccountTurnoverGroups(items, true, 0, allFilter))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            foreach (var byLedger in GetAccountTurnoverGroups(items, false, 0, allFilter))
            {
                journalItems.Add(await GetJournalItemFromGroup(byLedger, byLedger.Key));
            }

            return journalItems;
        }

        private async Task<IList<JournalItemViewModel>> GetJournalLedgerSummaryByBranchItemsAsync(
            IEnumerable<VoucherLine> lines, GridOptions gridOptions)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<JournalItemViewModel, bool> allFilter = art => true;
            var items = lines
                .Select(art => Mapper.Map<JournalItemViewModel>(art))
                .Apply(gridOptions, false)
                .ToList();
            foreach (var byLedger in GetAccountTurnoverGroups(items, true, 0, allFilter))
            {
                string ledgerCode = byLedger.Key;
                foreach (var byBranch in GetGroupByThenByItems(byLedger, art => art.BranchId))
                {
                    journalItems.Add(await GetJournalItemFromGroup(byBranch, ledgerCode));
                }
            }

            foreach (var byLedger in GetAccountTurnoverGroups(items, false, 0, allFilter))
            {
                string ledgerCode = byLedger.Key;
                foreach (var byBranch in GetGroupByThenByItems(byLedger, art => art.BranchId))
                {
                    journalItems.Add(await GetJournalItemFromGroup(byBranch, ledgerCode));
                }
            }

            return journalItems;
        }

        private IEnumerable<IGrouping<string, VoucherLine>> GetAccountTurnoverGroups(
            IEnumerable<VoucherLine> lines, bool isDebit, int groupLevel,
            Func<VoucherLine, bool> lineFilter)
        {
            int codeLength = GetLevelCodeLength(groupLevel);
            Func<VoucherLine, bool> turnoverCriteria = art => art.Credit > 0;
            if (isDebit)
            {
                turnoverCriteria = art => art.Debit > 0;
            }

            var turnoverGroups = lines
                .Where(turnoverCriteria)
                .Where(lineFilter)
                .OrderBy(art => art.Account.FullCode)
                .GroupBy(art => art.Account.FullCode.Substring(0, codeLength));
            return turnoverGroups;
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
            IEnumerable<VoucherLine> lineGroup, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var journalItem = Mapper.Map<JournalItemViewModel>(lineGroup.First());
            journalItem.Id = 0;
            journalItem.AccountFullCode = fullCode;
            journalItem.AccountName = account.Name;
            journalItem.Debit = lineGroup.Sum(art => art.Debit);
            journalItem.Credit = lineGroup.Sum(art => art.Credit);
            journalItem.Description = null;
            return journalItem;
        }

        private async Task<JournalItemViewModel> GetJournalItemFromGroup(
            IEnumerable<JournalItemViewModel> itemGroup, string fullCode)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var item = itemGroup.First();
            var journalItem = new JournalItemViewModel()
            {
                AccountFullCode = fullCode,
                AccountName = account.Name,
                BranchId = item.BranchId,
                BranchName = item.BranchName,
                VoucherStatusId = item.VoucherStatusId,
                Debit = itemGroup.Sum(art => art.Debit),
                Credit = itemGroup.Sum(art => art.Credit),
                Description = item.AccountName
            };

            return journalItem;
        }

        private IEnumerable<IEnumerable<VoucherLine>> GetGroupByThenByItems<TKey1>(
            IEnumerable<VoucherLine> lines, Func<VoucherLine, TKey1> firstSelector)
        {
            foreach (var byFirst in lines
                .OrderBy(firstSelector)
                .GroupBy(firstSelector))
            {
                yield return byFirst;
            }
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

        private IEnumerable<IEnumerable<VoucherLine>> GetGroupByThenByItems<TKey1, TKey2>(
            IEnumerable<VoucherLine> lines, Func<VoucherLine, TKey1> firstSelector,
            Func<VoucherLine, TKey2> secondSelector)
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

        private IEnumerable<IEnumerable<VoucherLine>> GetGroupByThenByItems<TKey1, TKey2, TKey3>(
            IEnumerable<VoucherLine> lines, Func<VoucherLine, TKey1> firstSelector,
            Func<VoucherLine, TKey2> secondSelector, Func<VoucherLine, TKey3> thirdSelector)
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
            var fullConfig = _configRepository
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

        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}
