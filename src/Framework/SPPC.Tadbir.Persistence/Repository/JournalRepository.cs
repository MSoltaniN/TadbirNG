using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public class JournalRepository : IJournalRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        /// <param name="configRepository">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        public JournalRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecureRepository repository,
            IConfigRepository configRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _configRepository = configRepository;
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _currentContext = userContext;
            _repository.SetCurrentContext(userContext);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns></returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(
            JournalMode journalMode, DateTime from, DateTime to)
        {
            var journal = default(JournalViewModel);
            switch (journalMode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowAsync(from, to);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerAsync(from, to);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryAsync(from, to);
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
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByDateByBranchAsync(
            JournalMode journalMode, DateTime from, DateTime to)
        {
            var journal = default(JournalViewModel);
            switch (journalMode)
            {
                case JournalMode.ByRows:
                    journal = await GetJournalByDateByRowByBranchAsync(from, to);
                    break;
                case JournalMode.ByLedger:
                    journal = await GetJournalByDateByLedgerByBranchAsync(from, to);
                    break;
                case JournalMode.BySubsidiary:
                    journal = await GetJournalByDateBySubsidiaryByBranchAsync(from, to);
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
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند</returns>
        public async Task<JournalViewModel> GetJournalByNoAsync(
            JournalMode journalMode, int from, int to)
        {
            var journal = default(JournalViewModel);
            if (journalMode == JournalMode.ByRows)
            {
                journal = await GetJournalByNoByRowAsync(from, to);
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="journalMode">حالت مورد نظر برای نمایش و جمع بندی اطلاعات</param>
        /// <param name="from">شماره ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">شماره انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByNoByBranchAsync(
            JournalMode journalMode, int from, int to)
        {
            var journal = default(JournalViewModel);
            if (journalMode == JournalMode.ByRows)
            {
                journal = await GetJournalByNoByRowByBranchAsync(from, to);
            }

            return journal;
        }

        #region Journal Implementation

        #region Journal By Date Implementation

        private async Task<JournalViewModel> GetJournalByDateByRowAsync(
            DateTime from, DateTime to)
        {
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            var journalItems = GetJournalByDateByRowItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<JournalViewModel> GetJournalByDateByLedgerAsync(
            DateTime from, DateTime to)
        {
            var journalItems = new List<JournalItemViewModel>();
            var lines = await GetRawJournalByDateLinesAsync(from, to);
            foreach (var byDateByNo in GetGroupByThenByItems(
                lines, art => art.Voucher.Date, art => Int32.Parse(art.Voucher.No)))
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
                lines, art => art.Voucher.Date, art => Int32.Parse(art.Voucher.No)))
            {
                journalItems.AddRange(await GetJournalBySubsidiaryItemsAsync(byDateByNo));
            }

            return BuildJournal(journalItems);
        }

        private async Task<IList<VoucherLine>> GetRawJournalByDateLinesAsync(
            DateTime from, DateTime to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .ToListAsync();
        }

        private IEnumerable<JournalItemViewModel> GetJournalByDateByRowItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => Int32.Parse(art.Voucher.No))
                .Select(art => _mapper.Map<JournalItemViewModel>(art));
        }

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

        private async Task<JournalItemViewModel> GetJournalItemFromGroup(
            IEnumerable<VoucherLine> lineGroup, string fullCode)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var journalItem = _mapper.Map<JournalItemViewModel>(lineGroup.First());
            journalItem.Id = 0;
            journalItem.AccountFullCode = fullCode;
            journalItem.AccountName = account.Name;
            journalItem.Debit = lineGroup.Sum(art => art.Debit);
            journalItem.Credit = lineGroup.Sum(art => art.Credit);
            journalItem.Description = null;
            return journalItem;
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

        private Task<JournalViewModel> GetJournalByDateByLedgerByBranchAsync(
            DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        private Task<JournalViewModel> GetJournalByDateBySubsidiaryByBranchAsync(
            DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<JournalItemViewModel> GetJournalByDateByRowByBranchItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => Int32.Parse(art.Voucher.No))
                        .ThenBy(art => art.BranchId)
                .Select(art => _mapper.Map<JournalItemViewModel>(art));
        }

        #endregion

        #region Journal By No Implementation

        private async Task<JournalViewModel> GetJournalByNoByRowAsync(int from, int to)
        {
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            var journalItems = GetJournalByNoByRowItems(lines);
            return BuildJournal(journalItems);
        }

        private async Task<IList<VoucherLine>> GetRawJournalByNoLinesAsync(int from, int to)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.Branch)
                .Where(art => Int32.Parse(art.Voucher.No) >= from
                    && Int32.Parse(art.Voucher.No) <= to)
                .ToListAsync();
        }

        private IEnumerable<JournalItemViewModel> GetJournalByNoByRowItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => Int32.Parse(art.Voucher.No))
                .Select(art => _mapper.Map<JournalItemViewModel>(art));
        }

        #endregion

        #region Journal By No By Branch Implementation

        private async Task<JournalViewModel> GetJournalByNoByRowByBranchAsync(int from, int to)
        {
            var lines = await GetRawJournalByNoLinesAsync(from, to);
            var journalItems = GetJournalByNoByRowByBranchItems(lines);
            return BuildJournal(journalItems);
        }

        private IEnumerable<JournalItemViewModel> GetJournalByNoByRowByBranchItems(
            IEnumerable<VoucherLine> voucherLines)
        {
            return voucherLines
                .OrderBy(art => Int32.Parse(art.Voucher.No))
                    .ThenBy(art => art.BranchId)
                .Select(art => _mapper.Map<JournalItemViewModel>(art));
        }

        #endregion

        private JournalViewModel BuildJournal(IEnumerable<JournalItemViewModel> journalItems)
        {
            var journal = new JournalViewModel();
            journal.Items.AddRange(journalItems);
            return journal;
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

        #endregion

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly ISecureRepository _repository;
        private readonly IConfigRepository _configRepository;
        private UserContextViewModel _currentContext;
    }
}
