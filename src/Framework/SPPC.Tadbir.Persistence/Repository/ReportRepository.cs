using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه و محاسبه اطلاعات گزارش های برنامه را پیاده سازی می کند
    /// </summary>
    public class ReportRepository : IReportRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        /// <param name="metadata">امکان خواندن متادیتا برای نماهای اطلاعاتی گزارشی را فراهم می کند</param>
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        /// <param name="lookupRepository">امکان خواندن اطلاعات موجود را به صورت لوکاپ فراهم می کند</param>
        /// <param name="configRepository">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        public ReportRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, IMetadataRepository metadata, ISecureRepository repository,
            ILookupRepository lookupRepository, IConfigRepository configRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _metadata = metadata;
            _repository = repository;
            _lookupRepository = lookupRepository;
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
        /// اطلاعات فراداده ای یکی از نماهای اطلاعاتی گزارشی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر</param>
        /// <returns>اطلاعات فراداده ای نمای گزارشی</returns>
        public async Task<ViewViewModel> GetReportMetadataByViewAsync(int viewId)
        {
            return await _metadata.GetViewMetadataByIdAsync(viewId);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش خلاصه اسناد حسابداری</returns>
        public async Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var userMap = await _lookupRepository.GetUserPersonsAsync();
            var vouchers = await _repository
                .GetAllOperationQuery<Voucher>(
                    ViewName.Voucher, voucher => voucher.Lines, voucher => voucher.Status)
                .Select(voucher => _mapper.Map<VoucherSummaryViewModel>(voucher))
                .Apply(gridOptions)
                .ToListAsync();
            Array.ForEach(vouchers.ToArray(),
                voucher => voucher.PreparedBy = userMap[voucher.PreparedById]);
            return vouchers;
        }

        /// <summary>
        /// به روش آسنکرون، نعداد سطرهای اطلاعاتی در گزارش خلاصه اسناد حسابداری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>تعداد سطرهای گزارش خلاصه اسناد حسابداری</returns>
        public async Task<int> GetVoucherSummaryByDateCountAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            int count = await _repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher, voucher => voucher.Lines)
                .Select(voucher => _mapper.Map<VoucherSummaryViewModel>(voucher))
                .Apply(gridOptions, false)
                .CountAsync();
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش فرم مرسوم سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <param name="withDetail">مشخص می کند که آیا جزییات سطوح شناور نیز مورد نیاز است یا نه</param>
        /// <returns>اطلاعات گزارش فرم مرسوم سند</returns>
        public async Task<StandardVoucherViewModel> GetStandardVoucherFormAsync(
            GridOptions gridOptions, bool withDetail = false)
        {
            var standardForm = default(StandardVoucherViewModel);
            var voucher = await GetStandardVoucherFormQuery(withDetail)
                .Apply(gridOptions)
                .FirstOrDefaultAsync();
            if (voucher != null)
            {
                standardForm = _mapper.Map<StandardVoucherViewModel>(voucher);
                var lineItems = new List<StandardVoucherLineViewModel>();
                foreach (var line in voucher.Lines)
                {
                    lineItems.Add(new StandardVoucherLineViewModel()
                    {
                        AccountFullCode = String.Empty,
                        Description = line.Description,
                        PartialAmount = Math.Max(line.Debit, line.Credit)
                    });
                    if (withDetail)
                    {
                        AddFloatingStandardLineItems(line, lineItems);
                    }

                    AddGeneralStandardLineItems(line, lineItems);
                    AddAuxiliaryStandardLineItems(line, lineItems);
                    AddDetailStandardLineItems(line, lineItems);

                    lineItems.Reverse();
                    standardForm.Lines.AddRange(lineItems);
                    lineItems.Clear();
                }
            }

            return standardForm;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و مطابق با ردیف های سند
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<IList<JournalViewModel>> GetJournalByDateByRowAsync(DateTime from, DateTime to)
        {
            var journalQuery = GetJournalByDateByRowQuery(from, to);
            var journal = await journalQuery
                .ToListAsync();

            int rowNo = 1;
            foreach (var journalItem in journal)
            {
                journalItem.RowNo = rowNo++;
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و مطابق با ردیف های سند با سطوح شناور
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<IList<JournalWithDetailViewModel>> GetJournalByDateByRowWithDetailAsync(
            DateTime from, DateTime to)
        {
            var journalQuery = GetJournalByDateByRowDetailQuery(from, to);
            var journal = await journalQuery
                .ToListAsync();

            int rowNo = 1;
            foreach (var journalItem in journal)
            {
                journalItem.RowNo = rowNo++;
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و حسابهای کل
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<IList<JournalViewModel>> GetJournalByDateByLedgerAsync(
            DateTime from, DateTime to)
        {
            var journal = new List<JournalViewModel>();
            int rowNo = 1;
            Func<VoucherLine, bool> allFilter = art => true;
            var lines = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, art => art.Voucher, art => art.Account)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .ToListAsync();
            foreach (var byNo in lines
                .OrderBy(art => Int32.Parse(art.Voucher.No))
                .GroupBy(art => art.Voucher.No))
            {
                foreach (var byLedger in GetAccountTurnoverGroups(byNo, true, 0, allFilter))
                {
                    var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                    journalItem.Debit = byLedger.Sum(art => art.Debit);
                    journal.Add(journalItem);
                }

                foreach (var byLedger in GetAccountTurnoverGroups(byNo, false, 0, allFilter))
                {
                    var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                    journalItem.Credit = byLedger.Sum(art => art.Credit);
                    journal.Add(journalItem);
                }
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و حسابهای معین
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<IList<JournalViewModel>> GetJournalByDateBySubsidiaryAsync(
            DateTime from, DateTime to)
        {
            var journal = new List<JournalViewModel>();
            int rowNo = 1;
            Func<VoucherLine, bool> ledgerFilter = art => art.Account.Level == 0;
            Func<VoucherLine, bool> subsidiaryFilter = art => art.Account.Level >= 1;
            var lines = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, art => art.Voucher, art => art.Account)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .ToListAsync();
            foreach (var byNo in lines
                .OrderBy(art => Int32.Parse(art.Voucher.No))
                .GroupBy(art => art.Voucher.No))
            {
                var debitLines = new List<JournalViewModel>();
                foreach (var bySubsidiary in GetAccountTurnoverGroups(byNo, true, 1, subsidiaryFilter))
                {
                    var journalItem = await GetNewJournalItem(bySubsidiary.First(), rowNo++, bySubsidiary.Key);
                    journalItem.Debit = bySubsidiary.Sum(art => art.Debit);
                    debitLines.Add(journalItem);
                }

                foreach (var byLedger in GetAccountTurnoverGroups(byNo, true, 0, ledgerFilter))
                {
                    var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                    journalItem.Debit = byLedger.Sum(art => art.Debit);
                    debitLines.Add(journalItem);
                }

                journal.AddRange(debitLines.OrderBy(item => item.AccountFullCode));

                var creditLines = new List<JournalViewModel>();
                foreach (var bySubsidiary in GetAccountTurnoverGroups(byNo, false, 1, subsidiaryFilter))
                {
                    var journalItem = await GetNewJournalItem(bySubsidiary.First(), rowNo++, bySubsidiary.Key);
                    journalItem.Credit = bySubsidiary.Sum(art => art.Credit);
                    creditLines.Add(journalItem);
                }

                foreach (var byLedger in GetAccountTurnoverGroups(byNo, false, 0, ledgerFilter))
                {
                    var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                    journalItem.Credit = byLedger.Sum(art => art.Credit);
                    creditLines.Add(journalItem);
                }

                journal.AddRange(creditLines.OrderBy(item => item.AccountFullCode));
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و سند خلاصه
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<IList<JournalViewModel>> GetJournalByDateLedgerSummaryAsync(
            DateTime from, DateTime to)
        {
            var journal = new List<JournalViewModel>();
            int rowNo = 1;
            Func<VoucherLine, bool> allFilter = art => true;
            var lines = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, art => art.Account)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .ToListAsync();
            foreach (var byLedger in GetAccountTurnoverGroups(lines, true, 0, allFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                journalItem.Description = journalItem.AccountName;
                journalItem.Debit = byLedger.Sum(art => art.Debit);
                journal.Add(journalItem);
            }

            foreach (var byLedger in GetAccountTurnoverGroups(lines, false, 0, allFilter))
            {
                var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                journalItem.Description = journalItem.AccountName;
                journalItem.Credit = byLedger.Sum(art => art.Credit);
                journal.Add(journalItem);
            }

            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و سند خلاصه به تفکیک تاریخ
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="from">تاریخ ابتدا در دوره گزارشگیری مورد نظر</param>
        /// <param name="to">تاریخ انتها در دوره گزارشگیری مورد نظر</param>
        /// <returns>اطلاعات گزارش دفتر روزنامه</returns>
        public async Task<IList<JournalViewModel>> GetJournalByDateLedgerSummaryByDateAsync(
            DateTime from, DateTime to)
        {
            var journal = new List<JournalViewModel>();
            int rowNo = 1;
            Func<VoucherLine, bool> allFilter = art => true;
            var lines = await _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, art => art.Voucher, art => art.Account)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .ToListAsync();
            foreach (var byDate in lines
                .OrderBy(art => art.Voucher.Date)
                .GroupBy(art => art.Voucher.Date))
            {
                foreach (var byLedger in GetAccountTurnoverGroups(byDate, true, 0, allFilter))
                {
                    var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                    journalItem.Debit = byLedger.Sum(art => art.Debit);
                    journal.Add(journalItem);
                }

                foreach (var byLedger in GetAccountTurnoverGroups(byDate, false, 0, allFilter))
                {
                    var journalItem = await GetNewJournalItem(byLedger.First(), rowNo++, byLedger.Key);
                    journalItem.Credit = byLedger.Sum(art => art.Credit);
                    journal.Add(journalItem);
                }
            }

            return journal;
        }

        private static void AddGeneralStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Account.Level == 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.FullCode,
                    Description = line.Account.Name,
                    Debit = line.Debit,
                    Credit = line.Credit
                });
            }
        }

        private static void AddAuxiliaryStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Account.Level == 1)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.FullCode,
                    Description = line.Account.Name
                });
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.Parent.FullCode,
                    Description = line.Account.Parent.Name,
                    Debit = line.Debit,
                    Credit = line.Credit
                });
            }
        }

        private static void AddDetailStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Account.Level == 2)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.FullCode,
                    Description = line.Account.Name
                });
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.Parent.FullCode,
                    Description = line.Account.Parent.Name
                });
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Account.Parent.Parent.FullCode,
                    Description = line.Account.Parent.Parent.Name,
                    Debit = line.Debit,
                    Credit = line.Credit
                });
            }
        }

        private static void AddFloatingStandardLineItems(
            VoucherLine line, IList<StandardVoucherLineViewModel> lineItems)
        {
            if (line.Project?.Id > 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.Project.FullCode,
                    Description = line.Project.Name
                });
            }

            if (line.CostCenter?.Id > 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.CostCenter.FullCode,
                    Description = line.CostCenter.Name
                });
            }

            if (line.DetailAccount?.Id > 0)
            {
                lineItems.Add(new StandardVoucherLineViewModel()
                {
                    AccountFullCode = line.DetailAccount.FullCode,
                    Description = line.DetailAccount.Name
                });
            }
        }

        private IQueryable<Voucher> GetStandardVoucherFormQuery(bool withDetail = false)
        {
            var repository = _unitOfWork.GetAsyncRepository<Voucher>();
            IQueryable<Voucher> query = repository
                .GetEntityQuery()
                .Include(v => v.Lines)
                    .ThenInclude(vl => vl.Account)
                        .ThenInclude(acc => acc.Parent)
                            .ThenInclude(acc => acc.Parent);
            if (withDetail)
            {
                query = query
                    .Include(v => v.Lines)
                        .ThenInclude(vl => vl.DetailAccount)
                    .Include(v => v.Lines)
                        .ThenInclude(vl => vl.CostCenter)
                    .Include(v => v.Lines)
                        .ThenInclude(vl => vl.Project);
            }

            return query;
        }

        private IQueryable<JournalViewModel> GetJournalByDateByRowQuery(DateTime from, DateTime to)
        {
            var journalQuery = _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine, art => art.Voucher, art => art.Account)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<JournalViewModel>(art));
            return journalQuery;
        }

        private IQueryable<JournalWithDetailViewModel> GetJournalByDateByRowDetailQuery(DateTime from, DateTime to)
        {
            var journalQuery = _repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                    art => art.Voucher, art => art.Account, art => art.DetailAccount,
                    art => art.CostCenter, art => art.Project)
                .Where(art => art.Voucher.Date >= from && art.Voucher.Date <= to)
                .OrderBy(art => art.Voucher.Date)
                    .ThenBy(art => art.Voucher.No)
                .Select(art => _mapper.Map<JournalWithDetailViewModel>(art));
            return journalQuery;
        }

        private IEnumerable<IGrouping<string, VoucherLine>> GetAccountTurnoverGroups(
            IEnumerable<VoucherLine> lines, bool isDebit, int groupLevel, Func<VoucherLine, bool> lineFilter)
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

        private async Task<JournalViewModel> GetNewJournalItem(
            VoucherLine voucherLine, int rowNo, string fullCode)
        {
            var repository = _unitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetSingleByCriteriaAsync(acc => acc.FullCode == fullCode);
            var journalItem = _mapper.Map<JournalViewModel>(voucherLine);
            journalItem.Id = 0;
            journalItem.Description = null;
            journalItem.RowNo = rowNo;
            journalItem.AccountFullCode = fullCode;
            journalItem.AccountName = account.Name;
            journalItem.Credit = 0.0M;
            journalItem.Debit = 0.0M;
            return journalItem;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly IMetadataRepository _metadata;
        private readonly ISecureRepository _repository;
        private readonly ILookupRepository _lookupRepository;
        private readonly IConfigRepository _configRepository;
        private UserContextViewModel _currentContext;
    }
}
