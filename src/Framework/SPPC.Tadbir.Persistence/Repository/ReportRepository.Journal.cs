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
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public partial class ReportRepository
    {
        #region Journal Methods

        #region By Date Methods

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و مطابق با ردیف های سند
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByDateByRowAsync(DateTime from, DateTime to)
        {
            var journal = new JournalViewModel();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            journal.Items.AddRange(lines
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => Int32.Parse(art.Voucher.No))
                .Select(art => _mapper.Map<JournalItemViewModel>(art)));
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و مطابق با ردیف های سند با سطوح شناور
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalWithDetailViewModel> GetJournalByDateByRowWithDetailAsync(
            DateTime from, DateTime to)
        {
            var itemsQuery = GetJournalByDateByRowDetailQuery(from, to);
            var journal = new JournalWithDetailViewModel();
            journal.Items.AddRange(await itemsQuery.ToListAsync());
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و حسابهای کل
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByDateByLedgerAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDate in lines
                .OrderBy(art => art.Voucher.Date)
                .GroupBy(art => art.Voucher.Date))
            {
                foreach (var byDateByNo in byDate
                    .OrderBy(art => art.Voucher.No)
                    .GroupBy(art => art.Voucher.No))
                {
                    await AddJournalByLedgerItemsAsync(byDateByNo, journalItems);
                }
            }

            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و حسابهای معین
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByDateBySubsidiaryAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDate in lines
                .OrderBy(art => art.Voucher.Date)
                .GroupBy(art => art.Voucher.Date))
            {
                foreach (var byDateByNo in byDate
                    .OrderBy(art => art.Voucher.No)
                    .GroupBy(art => art.Voucher.No))
                {
                    await AddJournalBySubsidiaryItemsAsync(byDateByNo, journalItems);
                }
            }

            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و سند خلاصه
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByDateLedgerSummaryAsync(
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            await AddJournalLedgerSummaryItemsAsync(lines, journalItems, gridOptions);
            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و سند خلاصه به تفکیک تاریخ
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByDateLedgerSummaryByDateAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            Func<VoucherLine, bool> allFilter = art => true;
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDate in lines
                .OrderBy(art => art.Voucher.Date)
                .GroupBy(art => art.Voucher.Date))
            {
                await AddJournalByLedgerItemsAsync(byDate, journalItems);
            }

            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و سند خلاصه به تفکیک ماه
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByDateMonthlyLedgerSummaryAsync(
            DateTime from, DateTime to, GridOptions gridOptions)
        {
            var journalItems = new List<JournalItemViewModel>();
            var monthJournal = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var monthEnum = new MonthEnumerator(from, to, new PersianCalendar());
            foreach (var month in monthEnum.GetMonths())
            {
                var monthLines = lines
                    .Where(art => art.Voucher.Date >= month.Start
                        && art.Voucher.Date <= month.End);
                await AddJournalLedgerSummaryItemsAsync(monthLines, monthJournal, gridOptions);
                Array.ForEach(monthJournal.ToArray(), item => item.VoucherDate = month.End);
                journalItems.AddRange(monthJournal);
                monthJournal.Clear();
            }

            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        #endregion

        #region By Number Methods

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و مطابق با ردیف های سند
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">شماره اولین سند مورد نظر برای گزارشگیری</param>
        /// <param name="to">شماره آخرین سند مورد نظر برای گزارشگیری</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByNoByRowAsync(int from, int to)
        {
            var journal = new JournalViewModel();
            var lines = await GetRawJournalByNumberLinesAsync(from, to);
            journal.Items.AddRange(lines
                .OrderBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<JournalItemViewModel>(art)));
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و مطابق با ردیف های سند با سطوح شناور
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">شماره اولین سند مورد نظر برای گزارشگیری</param>
        /// <param name="to">شماره آخرین سند مورد نظر برای گزارشگیری</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalWithDetailViewModel> GetJournalByNoByRowWithDetailAsync(
            int from, int to)
        {
            var itemsQuery = GetJournalByNoByRowDetailQuery(from, to);
            var journal = new JournalWithDetailViewModel();
            journal.Items.AddRange(await itemsQuery.ToListAsync());
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و حسابهای کل
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">شماره اولین سند مورد نظر برای گزارشگیری</param>
        /// <param name="to">شماره آخرین سند مورد نظر برای گزارشگیری</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByNoByLedgerAsync(
            int from, int to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNumberLinesAsync(from, to);
            foreach (var byNo in lines
                .OrderBy(art => Int32.Parse(art.Voucher.No))
                .GroupBy(art => Int32.Parse(art.Voucher.No)))
            {
                await AddJournalByLedgerItemsAsync(byNo, journalItems);
            }

            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و حسابهای معین
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">شماره اولین سند مورد نظر برای گزارشگیری</param>
        /// <param name="to">شماره آخرین سند مورد نظر برای گزارشگیری</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByNoBySubsidiaryAsync(
            int from, int to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNumberLinesAsync(from, to);
            foreach (var byNo in lines
                .OrderBy(art => Int32.Parse(art.Voucher.No))
                .GroupBy(art => Int32.Parse(art.Voucher.No)))
            {
                await AddJournalBySubsidiaryItemsAsync(byNo, journalItems);
            }

            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و سند خلاصه
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">شماره اولین سند مورد نظر برای گزارشگیری</param>
        /// <param name="to">شماره آخرین سند مورد نظر برای گزارشگیری</param>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<JournalViewModel> GetJournalByNoLedgerSummaryAsync(
            int from, int to, GridOptions gridOptions)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByNumberLinesAsync(from, to);
            await AddJournalLedgerSummaryItemsAsync(lines, journalItems, gridOptions);
            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
        }

        #endregion

        #endregion

        #region Journal Implementation Methods

        private IQueryable<JournalWithDetailItemViewModel> GetJournalByDateByRowDetailQuery(DateTime from, DateTime to)
        {
            var journalQuery = _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => Int32.Parse(art.Voucher.No))
                .Select(art => _mapper.Map<JournalWithDetailItemViewModel>(art));
            return journalQuery;
        }

        private IQueryable<JournalWithDetailItemViewModel> GetJournalByNoByRowDetailQuery(
            int from, int to)
        {
            var journalQuery = _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project, art => art.Branch)
                .Where(art => Int32.Parse(art.Voucher.No) >= from && Int32.Parse(art.Voucher.No) <= to)
                .OrderBy(art => Int32.Parse(art.Voucher.No))
                .Select(art => _mapper.Map<JournalWithDetailItemViewModel>(art));
            return journalQuery;
        }

        private async Task<IList<VoucherLine>> GetRawJournalByDateLinesAsync(DateTime from, DateTime to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .ToListAsync();
        }

        private async Task<IList<VoucherLine>> GetRawJournalByNumberLinesAsync(int from, int to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => Int32.Parse(art.Voucher.No) >= from && Int32.Parse(art.Voucher.No) <= to)
                .ToListAsync();
        }

        private async Task AddJournalByLedgerItemsAsync(
            IEnumerable<VoucherLine> lines, IList<JournalItemViewModel> journalItems)
        {
            Func<VoucherLine, bool> allFilter = art => true;
            foreach (var byLedger in GetAccountTurnoverGroups(lines, true, 0, allFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), byLedger.Key);
                journalItem.Debit = byLedger.Sum(art => art.Debit);
                journalItems.Add(journalItem);
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, false, 0, allFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), byLedger.Key);
                journalItem.Credit = byLedger.Sum(art => art.Credit);
                journalItems.Add(journalItem);
            }
        }

        private async Task AddJournalBySubsidiaryItemsAsync(
            IEnumerable<VoucherLine> lines, List<JournalItemViewModel> journalItems)
        {
            Func<VoucherLine, bool> ledgerFilter = art => art.Account.Level == 0;
            Func<VoucherLine, bool> subsidiaryFilter = art => art.Account.Level >= 1;
            var debitLines = new List<JournalItemViewModel>();
            foreach (var bySubsidiary in GetAccountTurnoverGroups(lines, true, 1, subsidiaryFilter))
            {
                var journalItem = await GetNewJournalItem(bySubsidiary.First(), bySubsidiary.Key);
                journalItem.Debit = bySubsidiary.Sum(art => art.Debit);
                debitLines.Add(journalItem);
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, true, 0, ledgerFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), byLedger.Key);
                journalItem.Debit = byLedger.Sum(art => art.Debit);
                debitLines.Add(journalItem);
            }

            journalItems.AddRange(debitLines.OrderBy(item => item.AccountFullCode));

            var creditLines = new List<JournalItemViewModel>();
            foreach (var bySubsidiary in GetAccountTurnoverGroups(lines, false, 1, subsidiaryFilter))
            {
                var journalItem = await GetNewJournalItem(bySubsidiary.First(), bySubsidiary.Key);
                journalItem.Credit = bySubsidiary.Sum(art => art.Credit);
                creditLines.Add(journalItem);
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, false, 0, ledgerFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), byLedger.Key);
                journalItem.Credit = byLedger.Sum(art => art.Credit);
                creditLines.Add(journalItem);
            }

            journalItems.AddRange(creditLines.OrderBy(item => item.AccountFullCode));
        }

        private async Task AddJournalLedgerSummaryItemsAsync(
            IEnumerable<VoucherLine> lines, List<JournalItemViewModel> journalItems,
            GridOptions gridOptions)
        {
            Func<JournalItemViewModel, bool> allFilter = art => true;
            var items = lines
                .Select(art => _mapper.Map<JournalItemViewModel>(art))
                .Apply(gridOptions, false)
                .ToList();
            foreach (var byLedger in GetAccountTurnoverGroups(items, true, 0, allFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), byLedger.Key);
                journalItem.Description = journalItem.AccountName;
                journalItem.Debit = byLedger.Sum(art => art.Debit);
                journalItems.Add(journalItem);
            }

            foreach (var byLedger in GetAccountTurnoverGroups(items, false, 0, allFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), byLedger.Key);
                journalItem.Description = journalItem.AccountName;
                journalItem.Credit = byLedger.Sum(art => art.Credit);
                journalItems.Add(journalItem);
            }
        }

        private IEnumerable<IGrouping<string, VoucherLine>> GetAccountTurnoverGroups(
            IEnumerable<VoucherLine> lines, bool isDebit, int groupLevel,
            Func<VoucherLine, bool> lineFilter)
        {
            var fullConfig = _configRepository
                .GetViewTreeConfigByViewAsync(ViewName.Account)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(level => level.No <= groupLevel + 1)
                .Select(level => (int)level.CodeLength)
                .Sum();
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
            var fullConfig = _configRepository
                .GetViewTreeConfigByViewAsync(ViewName.Account)
                .Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(level => level.No <= groupLevel + 1)
                .Select(level => (int)level.CodeLength)
                .Sum();
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

        private async Task<JournalItemViewModel> GetNewJournalItem(JournalItemViewModel voucherLine, string fullCode)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var journalItem = new JournalItemViewModel()
            {
                AccountFullCode = fullCode,
                AccountName = account.Name,
                BranchId = voucherLine.BranchId,
                BranchName = voucherLine.BranchName,
                VoucherStatusId = voucherLine.VoucherStatusId
            };

            return journalItem;
        }

        private async Task<JournalItemViewModel> GetNewJournalItem(VoucherLine voucherLine, string fullCode)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var journalItem = _mapper.Map<JournalItemViewModel>(voucherLine);
            journalItem.Id = 0;
            journalItem.Description = null;
            journalItem.AccountFullCode = fullCode;
            journalItem.AccountName = account.Name;
            journalItem.Credit = 0.0M;
            journalItem.Debit = 0.0M;
            return journalItem;
        }

        #endregion
    }
}
