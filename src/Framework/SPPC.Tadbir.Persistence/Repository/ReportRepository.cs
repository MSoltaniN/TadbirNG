using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.ViewModel.Auth;
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
        /// <param name="repository">عملیات مورد نیاز برای اعمال دسترسی امنیتی در سطح سطرهای اطلاعاتی را تعریف می کند</param>
        /// <param name="lookupRepository">امکان خواندن اطلاعات موجود را به صورت لوکاپ فراهم می کند</param>
        public ReportRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper, ISecureRepository repository,
            ILookupRepository lookupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _lookupRepository = lookupRepository;
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
        /// ساختار درختی گزارش های تعریف شده را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        public async Task<IList<TreeItemViewModel>> GetReportTreeAsync(string localeCode)
        {
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var reports = await repository
                .GetEntityQuery()
                .Include(rep => rep.Parent)
                .Include(rep => rep.LocalReports)
                    .ThenInclude(rep => rep.Locale)
                .ToListAsync();
            var tree = reports
                .Select(rep => _mapper.Map<TreeItemViewModel>(rep))
                .ToList();
            Localize(localeCode, reports, tree);
            _unitOfWork.UseCompanyContext();
            return tree;
        }

        /// <summary>
        /// اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش</returns>
        public async Task<PrintInfoViewModel> GetReportAsync(int reportId, string localeCode)
        {
            return await GetReportPrintInfo(reportId, localeCode);
        }

        /// <summary>
        /// اطلاعات مورد نیاز برای طراحی گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeCode">کد استاندارد  دو حرفی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای طراحی گزارش</returns>
        public async Task<PrintInfoViewModel> GetReportDesignAsync(int reportId, string localeCode)
        {
            return await GetReportPrintInfo(reportId, localeCode, false);
        }

        /// <summary>
        /// اطلاعات خلاصه گزارش را برای عملیات اعتبارسنجی خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <returns>اطلاعات خلاصه گزارش موزد نظر</returns>
        public async Task<ReportSummaryViewModel> GetReportSummaryAsync(int reportId)
        {
            _unitOfWork.UseSystemContext();
            var summary = default(ReportSummaryViewModel);
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var report = await repository.GetByIDAsync(reportId);
            if (report != null)
            {
                summary = _mapper.Map<ReportSummaryViewModel>(report);
            }

            _unitOfWork.UseCompanyContext();
            return summary;
        }

        /// <summary>
        /// اطلاعات خلاصه گزارش پیش فرض برای یک فرم را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از فرم های قابل چاپ</param>
        /// <returns>اطلاعات خلاصه گزارش پیش فرض</returns>
        public async Task<ReportSummaryViewModel> GetDefaultReportByViewAsync(int viewId)
        {
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var report = await repository.GetSingleByCriteriaAsync(
                rep => rep.ViewId == viewId && rep.IsDefault);
            _unitOfWork.UseCompanyContext();
            return _mapper.Map<ReportSummaryViewModel>(report);
        }

        public async Task<int> GetLocaleIdAsync(string localeCode)
        {
            Verify.ArgumentNotNullOrEmptyString(localeCode, nameof(localeCode));
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<Locale>();
            var locale = await repository.GetSingleByCriteriaAsync(loc => loc.Code == localeCode);
            _unitOfWork.UseCompanyContext();
            return (locale != null ? locale.Id : 0);
        }

        /// <summary>
        /// اطلاعات یک گزارش ذخیره شده کاربری را ذخیره یا بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری</param>
        public async Task SaveUserReportAsync(LocalReportViewModel report)
        {
            _unitOfWork.UseSystemContext();
            Verify.ArgumentNotNull(report, nameof(report));
            if (String.IsNullOrEmpty(report.Template))
            {
                var repository = _unitOfWork.GetAsyncRepository<Report>();
                var existing = await repository.GetByIDAsync(
                    report.ReportId, rep => rep.LocalReports, rep => rep.Parameters);
                if (existing != null)
                {
                    var userReport = _mapper.Map<Report>(existing);
                    userReport.Id = 0;
                    userReport.IsSystem = false;
                    userReport.IsDefault = false;
                    userReport.CreatedById = _currentContext.Id;
                    repository.Insert(userReport);
                    await _unitOfWork.CommitAsync();

                    Array.ForEach(existing.LocalReports.ToArray(), rep =>
                    {
                        var clone = _mapper.Map<LocalReport>(rep);
                        clone.Id = 0;
                        clone.Caption = (rep.LocaleId == report.LocaleId)
                            ? report.Caption
                            : String.Format("Copy of '{0}'", rep.Caption);
                        clone.ReportId = 0;
                        clone.Locale = null;
                        userReport.LocalReports.Add(clone);
                    });
                    Array.ForEach(existing.Parameters.ToArray(), param =>
                    {
                        var clone = _mapper.Map<Parameter>(param);
                        clone.Id = 0;
                        clone.ReportId = 0;
                        userReport.Parameters.Add(clone);
                    });
                    repository.Update(userReport, rep => rep.LocalReports, rep => rep.Parameters);
                    await _unitOfWork.CommitAsync();
                }
            }
            else
            {
                var repository = _unitOfWork.GetAsyncRepository<LocalReport>();
                var existing = await repository.GetSingleByCriteriaAsync(
                    rep => rep.ReportId == report.ReportId && rep.LocaleId == report.LocaleId);
                if (existing != null)
                {
                    existing.Template = report.Template;
                    repository.Update(existing);
                    await _unitOfWork.CommitAsync();
                }
            }

            _unitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// عنوان یک گزارش ذخیره شده کاربری را بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری شامل عنوان جدید مورد نظر</param>
        public async Task SetUserReportCaptionAsync(LocalReportViewModel report)
        {
            _unitOfWork.UseSystemContext();
            Verify.ArgumentNotNull(report, nameof(report));
            var repository = _unitOfWork.GetAsyncRepository<LocalReport>();
            var existing = await repository.GetSingleByCriteriaAsync(
                rep => rep.ReportId == report.ReportId && rep.LocaleId == report.LocaleId);
            if (existing != null)
            {
                existing.Caption = report.Caption;
                repository.Update(existing);
                await _unitOfWork.CommitAsync();
            }

            _unitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// یک گزارش ذخیر شده کاربری را به همراه کلیه اطلاعات مرتبط حذف می کند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        public async Task DeleteUserReportAsync(int reportId)
        {
            _unitOfWork.UseSystemContext();
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var report = await repository.GetByIDWithTrackingAsync(
                reportId, rep => rep.LocalReports, rep => rep.Parameters);
            if (report != null)
            {
                report.LocalReports.Clear();
                report.Parameters.Clear();
                repository.Delete(report);
                await _unitOfWork.CommitAsync();
            }

            _unitOfWork.UseCompanyContext();
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

        private static void Localize(string localeCode, List<Report> reports, List<TreeItemViewModel> tree)
        {
            foreach (var node in tree)
            {
                var report = reports
                    .Where(rep => rep.Id == node.Id)
                    .Single();
                var localReport = report.LocalReports
                    .Where(rep => localeCode == rep.Locale.Code)
                    .Single();
                node.Caption = localReport.Caption;
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

        private async Task<PrintInfoViewModel> GetReportPrintInfo(
            int reportId, string localeCode, bool withParams = true)
        {
            _unitOfWork.UseSystemContext();
            Verify.ArgumentNotNullOrEmptyString(localeCode, nameof(localeCode));
            var reportView = default(PrintInfoViewModel);
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            IQueryable<Report> reportQuery = repository
                .GetEntityQuery()
                .Include(rep => rep.LocalReports)
                    .ThenInclude(rep => rep.Locale);
            if (withParams)
            {
                reportQuery = reportQuery
                    .Include(rep => rep.Parameters);
            }

            var report = await reportQuery.SingleOrDefaultAsync(rep => rep.Id == reportId);
            if (report != null)
            {
                reportView = _mapper.Map<PrintInfoViewModel>(report);
                var localReport = report.LocalReports
                    .Where(rep => localeCode.StartsWith(rep.Locale.Code))
                    .FirstOrDefault();
                reportView.Template = localReport?.Template;
            }

            _unitOfWork.UseCompanyContext();
            return reportView;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private readonly ISecureRepository _repository;
        private readonly ILookupRepository _lookupRepository;
        private UserContextViewModel _currentContext;
    }
}
