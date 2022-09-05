using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه و محاسبه اطلاعات گزارش های زیرسیستم حسابداری را پیاده سازی می کند
    /// </summary>
    public partial class FinanceReportRepository : RepositoryBase, IFinanceReportRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="lookupRepository">امکان خواندن اطلاعات موجود را به صورت لوکاپ فراهم می کند</param>
        /// <param name="utility">امکان اجرای مستقیم دستورات دیتابیسی را فراهم می کند</param>
        public FinanceReportRepository(IRepositoryContext context, ISystemRepository system,
            ILookupRepository lookupRepository, IReportDirectUtility utility)
            : base(context)
        {
            _system = system;
            _lookupRepository = lookupRepository;
            _utility = utility;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش خلاصه اسناد حسابداری بر اساس تاریخ
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش خلاصه اسناد حسابداری</returns>
        public async Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByDateReportAsync(
            GridOptions gridOptions)
        {
            return await GetVoucherSummaryItemsAsync(gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش خلاصه اسناد حسابداری بر اساس شماره سند
        /// را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های برنامه برای فیلتر، مرتب سازی و صفحه بندی اطلاعات</param>
        /// <returns>اطلاعات گزارش خلاصه اسناد حسابداری</returns>
        public async Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryByNoReportAsync(
            GridOptions gridOptions)
        {
            return await GetVoucherSummaryItemsAsync(gridOptions, true);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز در گزارش فرم مرسوم سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند مالی مورد نظر برای چاپ</param>
        /// <param name="withDetail">مشخص می کند که آیا جزییات سطوح شناور نیز مورد نیاز است یا نه</param>
        /// <returns>اطلاعات گزارش فرم مرسوم سند</returns>
        public async Task<StandardVoucherViewModel> GetStandardVoucherFormAsync(
            int voucherNo, bool withDetail = false)
        {
            var standardForm = default(StandardVoucherViewModel);
            var voucher = await GetStandardVoucherFormQuery(withDetail)
                .Where(v => v.FiscalPeriodId == UserContext.FiscalPeriodId && v.No == voucherNo)
                .FirstOrDefaultAsync();
            if (voucher != null)
            {
                standardForm = Mapper.Map<StandardVoucherViewModel>(voucher);
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

                    AddLedgerStandardLineItems(line, lineItems);
                    AddSubsidiaryStandardLineItems(line, lineItems);
                    AddDetailStandardLineItems(line, lineItems);

                    lineItems.Reverse();
                    standardForm.Lines.AddRange(lineItems);
                    lineItems.Clear();
                }

                await SetVoucherDateAsync(standardForm);
            }

            return standardForm;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش چاپی "ساده - در سطح تفصیلی" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند حسابداری مورد نظر</param>
        /// <returns>اطلاعات سند حسابداری</returns>
        public async Task<StandardVoucherViewModel> GetVoucherByDetailAsync(int voucherNo)
        {
            var voucherByDetail = default(StandardVoucherViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository
                .GetEntityQuery()
                .Include(v => v.Lines)
                    .ThenInclude(vl => vl.Account)
                .Where(v => v.FiscalPeriodId == UserContext.FiscalPeriodId && v.No == voucherNo)
                .FirstOrDefaultAsync();
            if (voucher != null)
            {
                voucherByDetail = Mapper.Map<StandardVoucherViewModel>(voucher);
                voucherByDetail.Lines.AddRange(voucher.Lines
                    .Select(line => Mapper.Map<StandardVoucherLineViewModel>(line)));
                await SetVoucherDateAsync(voucherByDetail);
            }

            return voucherByDetail;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش چاپی "مرکب - در سطح کل" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند حسابداری مورد نظر</param>
        /// <returns>اطلاعات سند حسابداری در سطح کل</returns>
        public async Task<StandardVoucherViewModel> GetVoucherByLedgerAsync(int voucherNo)
        {
            return await GetSummaryByLevelAsync(voucherNo, 0);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش چاپی "مرکب - در سطح معین" را خوانده و برمی گرداند
        /// </summary>
        /// <param name="voucherNo">شماره سند حسابداری مورد نظر</param>
        /// <returns>اطلاعات سند حسابداری در سطح معین</returns>
        public async Task<StandardVoucherViewModel> GetVoucherBySubsidiaryAsync(int voucherNo)
        {
            return await GetSummaryByLevelAsync(voucherNo, 1);
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private async Task<IList<VoucherSummaryViewModel>> GetVoucherSummaryItemsAsync(
            GridOptions gridOptions, bool byNo = false)
        {
            var calendar = await _system.Config.GetCurrentCalendarTypeAsync();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var query = byNo
                ? new ReportQuery(VoucherQuery.VoucherSummaryByNo)
                : new ReportQuery(VoucherQuery.VoucherSummaryByDate);
            query.SetFilter(_utility.GetEnvironmentFilters(gridOptions));
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetVoucherSummaryItem(row, calendar))
                .ToList();
        }

        private VoucherSummaryViewModel GetVoucherSummaryItem(DataRow row, CalendarType calendar)
        {
            var date = _utility.ValueOrDefault<DateTime>(row, "Date");
            var summary = new VoucherSummaryViewModel()
            {
                No = _utility.ValueOrDefault<int>(row, "No"),
                Date = calendar == CalendarType.Jalali
                    ? JalaliDateTime.FromDateTime(date).ToShortDateString()
                    : date.ToShortDateString(false),
                DebitSum = _utility.ValueOrDefault<decimal>(row, "DebitSum"),
                CreditSum = _utility.ValueOrDefault<decimal>(row, "CreditSum"),
                Difference = _utility.ValueOrDefault<decimal>(row, "Difference"),
                IssuerName = _utility.ValueOrDefault(row, "IssuerName"),
                StatusName = _utility.ValueOrDefault(row, "StatusName"),
                OriginName = _utility.ValueOrDefault(row, "OriginName")
            };

            summary.BalanceStatus = summary.DebitSum == summary.CreditSum
                ? AppStrings.Balanced
                : AppStrings.Unbalanced;
            return summary;
        }

        #region Standard Voucher Implementation

        private static void AddLedgerStandardLineItems(
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

        private static void AddSubsidiaryStandardLineItems(
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
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
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

        #endregion

        #region Voucher By Level Implementation

        private async Task<StandardVoucherViewModel> GetSummaryByLevelAsync(int voucherNo, int level)
        {
            var summary = default(StandardVoucherViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetSingleByCriteriaAsync(
                v => v.No == voucherNo && v.SubjectType == (short)SubjectType.Normal &&
                v.FiscalPeriodId == UserContext.FiscalPeriodId);
            if (voucher != null)
            {
                summary = Mapper.Map<StandardVoucherViewModel>(voucher);
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                if (level == 0)
                {
                    summary.Lines.AddRange(GetSummaryByLedgerItems(voucherNo));
                }
                else
                {
                    summary.Lines.AddRange(GetSummaryBySubsidiaryItems(voucherNo));
                }

                await SetNameAndDescriptionAsync(summary.Lines);
                await SetVoucherDateAsync(summary);
            }

            return summary;
        }

        private IEnumerable<StandardVoucherLineViewModel> GetSummaryByLedgerItems(int voucherNo)
        {
            int length = _system.Config.GetLevelCodeLength(0);
            var debitItems = GetByLevelItems(voucherNo, length, true);
            var creditItems = GetByLevelItems(voucherNo, length, false);
            return debitItems.Concat(creditItems);
        }

        private IEnumerable<StandardVoucherLineViewModel> GetSummaryBySubsidiaryItems(int voucherNo)
        {
            int ledgerLength = _system.Config.GetLevelCodeLength(0);
            int subsidLength = _system.Config.GetLevelCodeLength(1);
            var subsidDebit = GetByLevelItems(voucherNo, subsidLength, true, "AND acc.Level >= 1 ");
            var ledgerDebit = GetByLevelItems(voucherNo, ledgerLength, true, "AND acc.Level = 0 ");
            var subsidCredit = GetByLevelItems(voucherNo, subsidLength, false, "AND acc.Level >= 1 ");
            var ledgerCredit = GetByLevelItems(voucherNo, ledgerLength, false, "AND acc.Level = 0 ");
            return subsidDebit
                .Concat(ledgerDebit)
                .OrderBy(line => line.AccountFullCode)
                .Concat(subsidCredit
                    .Concat(ledgerCredit)
                    .OrderBy(line => line.AccountFullCode));
        }

        private List<StandardVoucherLineViewModel> GetByLevelItems(
            int voucherNo, int length, bool isDebit, string filter = "")
        {
            var query = GetSummaryByLevelQuery(voucherNo, length, isDebit, filter);
            var result = DbConsole.ExecuteQuery(query.Query);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => GetSummaryItem(row))
                .ToList();
        }

        private StandardVoucherLineViewModel GetSummaryItem(DataRow row)
        {
            return new StandardVoucherLineViewModel()
            {
                AccountFullCode = _utility.ValueOrDefault(row, "FullCode"),
                Debit = _utility.ValueOrDefault<decimal>(row, "Debit"),
                Credit = _utility.ValueOrDefault<decimal>(row, "Credit")
            };
        }

        private ReportQuery GetSummaryByLevelQuery(int voucherNo, int length, bool isDebit, string filter = "")
        {
            ReportQuery query;
            string command = String.Format(VoucherQuery.VoucherSummaryByLevel,
                length, voucherNo, UserContext.FiscalPeriodId, filter);
            if (isDebit)
            {
                query = new ReportQuery(command);
            }
            else
            {
                query = new ReportQuery(command
                    .Replace("Debit", "Credit")
                    .Replace("Credit1", "Debit"));
            }

            return query;
        }

        private async Task SetNameAndDescriptionAsync(List<StandardVoucherLineViewModel> lines)
        {
            var repository = UnitOfWork.GetRepository<Account>();
            var fullCodes = lines
                .Select(item => item.AccountFullCode);
            var accounts = await repository
                .GetEntityQuery()
                .Where(acc => fullCodes.Contains(acc.FullCode))
                .Select(acc => new KeyValuePair<string, string>(acc.FullCode, acc.Name))
                .ToListAsync();
            var accountMap = new Dictionary<string, string>(accounts);
            foreach (var line in lines)
            {
                line.Description = accountMap[line.AccountFullCode];
            }
        }

        #endregion

        private async Task SetVoucherDateAsync(StandardVoucherViewModel voucher)
        {
            var calendar = await _system.Config.GetCurrentCalendarTypeAsync();
            if (calendar == CalendarType.Jalali)
            {
                voucher.Date = JalaliDateTime
                    .FromDateTime(DateTime.Now.Parse(voucher.Date, false))
                    .ToShortDateString();
            }
        }

        private readonly ISystemRepository _system;
        private readonly ILookupRepository _lookupRepository;
        private readonly IReportDirectUtility _utility;
    }
}
