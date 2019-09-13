using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای تهیه و محاسبه اطلاعات گزارش های برنامه را پیاده سازی می کند
    /// </summary>
    public partial class ReportRepository : RepositoryBase, IReportRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="lookupRepository">امکان خواندن اطلاعات موجود را به صورت لوکاپ فراهم می کند</param>
        public ReportRepository(IRepositoryContext context, ISystemRepository system,
            ILookupRepository lookupRepository)
            : base(context)
        {
            _system = system;
            _lookupRepository = lookupRepository;
        }

        /// <summary>
        /// به روش آسنکرون، مانده حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی حساب مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetAccountBalanceAsync(int accountId, DateTime date)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId);
            Verify.ArgumentNotNull(account, nameof(account));
            return await GetItemBalanceAsync(date, line => line.Account.FullCode.StartsWith(account.FullCode));
        }

        /// <summary>
        /// به روش آسنکرون، مانده حساب مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="accountId">شناسه دیتابیسی حساب مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetAccountBalanceAsync(int accountId, int number)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await repository.GetByIDAsync(accountId);
            Verify.ArgumentNotNull(account, nameof(account));
            return await GetItemBalanceAsync(number, line => line.Account.FullCode.StartsWith(account.FullCode));
        }

        /// <summary>
        /// به روش آسنکرون، مانده تفصیلی شناور مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetDetailAccountBalanceAsync(int faccountId, DateTime date)
        {
            return await GetItemBalanceAsync(date, line => line.DetailId == faccountId);
        }

        /// <summary>
        /// به روش آسنکرون، مانده تفصیلی شناور مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="faccountId">شناسه دیتابیسی تفصیلی شناور مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetDetailAccountBalanceAsync(int faccountId, int number)
        {
            return await GetItemBalanceAsync(number, line => line.DetailId == faccountId);
        }

        /// <summary>
        /// به روش آسنکرون، مانده مرکز هزینه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetCostCenterBalanceAsync(int ccenterId, DateTime date)
        {
            return await GetItemBalanceAsync(date, line => line.CostCenterId == ccenterId);
        }

        /// <summary>
        /// به روش آسنکرون، مانده مرکز هزینه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="ccenterId">شناسه دیتابیسی مرکز هزینه مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetCostCenterBalanceAsync(int ccenterId, int number)
        {
            return await GetItemBalanceAsync(number, line => line.CostCenterId == ccenterId);
        }

        /// <summary>
        /// به روش آسنکرون، مانده پروژه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetProjectBalanceAsync(int projectId, DateTime date)
        {
            return await GetItemBalanceAsync(date, line => line.ProjectId == projectId);
        }

        /// <summary>
        /// به روش آسنکرون، مانده پروژه مشخص شده را محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="projectId">شناسه دیتابیسی پروژه مورد نظر</param>
        /// <param name="number">شماره سندی که مانده با توجه به کلیه سندهای پیش از آن در دوره مالی جاری محاسبه می شود</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetProjectBalanceAsync(int projectId, int number)
        {
            return await GetItemBalanceAsync(number, line => line.ProjectId == projectId);
        }

        /// <summary>
        /// به روش آسنکرون، تاریخ سند سیستمی با نوع داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="type">یکی از انواع تعریف شده برای سندهای سیستمی</param>
        /// <returns>تاریخ سند مورد نظر یا اگر سند مورد نظر پیدا نشود، بدون مقدار</returns>
        public async Task<DateTime?> GetSpecialVoucherDateAsync(VoucherType type)
        {
            DateTime? voucherDate = null;
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var voucher = await repository.GetFirstByCriteriaAsync(v => v.Type == (short)type);
            if (voucher != null)
            {
                voucherDate = voucher.Date;
            }

            return voucherDate;
        }

        /// <summary>
        /// مانده سرفصل حسابداری مشخص شده را در سند مالی از نوع داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="type">نوع سیستمی مورد نظر برای محاسبه مانده</param>
        /// <param name="accountId">شناسه دیتابیسی سرفصل حسابداری مورد نظر</param>
        /// <returns>مانده محاسبه شده برای سرفصل حسابداری</returns>
        public async Task<decimal> GetSpecialVoucherBalanceAsync(VoucherType type, int accountId)
        {
            decimal balance = 0.0M;
            var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
            var account = await accountRepository.GetByIDAsync(accountId);
            if (account != null)
            {
                balance = await Repository
                    .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine)
                    .Where(line => line.Voucher.Type == (short)type
                        && line.FiscalPeriodId == UserContext.FiscalPeriodId
                        && line.Account.FullCode.StartsWith(account.FullCode))
                    .Select(line => line.Debit - line.Credit)
                    .SumAsync();
            }

            return balance;
        }

        /// <summary>
        /// اطلاعات فراداده ای یکی از نماهای اطلاعاتی گزارشی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی نمای اطلاعاتی مورد نظر</param>
        /// <returns>اطلاعات فراداده ای نمای گزارشی</returns>
        public async Task<ViewViewModel> GetReportMetadataByViewAsync(int viewId)
        {
            return await Metadata.GetViewMetadataByIdAsync(viewId);
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
            var vouchers = await Repository
                .GetAllOperationQuery<Voucher>(
                    ViewName.Voucher, voucher => voucher.Lines, voucher => voucher.Status)
                .Select(voucher => Mapper.Map<VoucherSummaryViewModel>(voucher))
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
            int count = await Repository
                .GetAllOperationQuery<Voucher>(ViewName.Voucher, voucher => voucher.Lines)
                .Select(voucher => Mapper.Map<VoucherSummaryViewModel>(voucher))
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

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IMetadataRepository Metadata
        {
            get { return _system.Metadata; }
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

        private async Task<decimal> GetItemBalanceAsync(
            DateTime date, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await Repository
                .GetAllOperationQuery<VoucherLine>(
                    ViewName.VoucherLine, line => line.Voucher, line => line.Account)
                .Where(line => line.Voucher.Date.CompareWith(date) < 0
                    && line.FiscalPeriodId == UserContext.FiscalPeriodId)
                .Where(itemCriteria)
                .Select(line => line.Debit - line.Credit)
                .SumAsync();
        }

        private async Task<decimal> GetItemBalanceAsync(
            int number, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await Repository
                .GetAllOperationQuery<VoucherLine>(
                    ViewName.VoucherLine, line => line.Voucher, line => line.Account)
                .Where(line => line.Voucher.No < number
                    && line.FiscalPeriodId == UserContext.FiscalPeriodId)
                .Where(itemCriteria)
                .Select(line => line.Debit - line.Credit)
                .SumAsync();
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

        private readonly ISystemRepository _system;
        private readonly ILookupRepository _lookupRepository;
    }
}
