﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز در زیرساخت مدیریت گزارشات را پیاده سازی می کند
    /// </summary>
    public class ReportSystemRepository : IReportSystemRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی</param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public ReportSystemRepository(
            IAppUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWork.UseSystemContext();
        }

        /// <summary>
        /// اطلاعات محیطی و امنیتی کاربر جاری برنامه را برای کنترل قواعد کاری برنامه تنظیم می کند
        /// </summary>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر جاری برنامه</param>
        public void SetCurrentContext(UserContextViewModel userContext)
        {
            _currentContext = userContext;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات قالب گزارش فوری را به زبان مورد نیاز خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان مورد نظر</param>
        /// <returns>اطلاعات قالب گزارش فوری</returns>
        public async Task<LocalReportViewModel> GetQuickReportTemplateAsync(int localeId)
        {
            var repository = _unitOfWork.GetAsyncRepository<LocalReport>();
            var localReport = await repository.GetSingleByCriteriaAsync(
                rep => rep.ReportId == _quickReportId && rep.LocaleId == localeId);
            return _mapper.Map<LocalReportViewModel>(localReport);
        }

        /// <summary>
        /// به روش آسنکرون، ساختار درختی گزارش های تعریف شده را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        public async Task<IList<TreeItemViewModel>> GetReportTreeAsync(int localeId)
        {
            return await GetReportTreeByCriteriaAsync(
                localeId, rep => !rep.IsDynamic && !rep.Code.EndsWith("QReport"));
        }

        /// <summary>
        /// به روش آسنکرون، ساختار درختی گزارش های تعریف شده برای یک فرم را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <param name="viewId">شناسه دیتابیسی فرم مورد نظر</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        public async Task<IList<TreeItemViewModel>> GetReportTreeByViewAsync(int localeId, int viewId)
        {
            var tree = new List<TreeItemViewModel>();
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var reports = await repository
                .GetEntityWithTrackingQuery(rep => rep.Parent, rep => rep.LocalReports)
                .Where(rep => rep.ViewId == viewId)
                .ToListAsync();
            if (reports.Count > 0)
            {
                var first = reports[0];
                var parent = first.Parent;
                while (parent != null)
                {
                    reports.Add(parent);
                    await repository.LoadReferenceAsync(parent, rep => rep.Parent);
                    await repository.LoadCollectionAsync(parent, rep => rep.LocalReports);
                    parent = parent.Parent;
                }

                tree = reports
                    .Select(rep => _mapper.Map<TreeItemViewModel>(rep))
                    .ToList();
                Localize(localeId, reports, tree);
            }

            return tree;
        }

        /// <summary>
        /// به روش آسنکرون، ساختار درختی گزارش های تعریف شده در یک زیرسیستم را به زبان جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <param name="subsystemId">شناسه دیتابیسی زیرسیستم مورد نظر</param>
        /// <returns>ساختار درختی گزارش ها به زبان جاری برنامه</returns>
        public async Task<IList<TreeItemViewModel>> GetReportTreeBySubsystemAsync(int localeId, int subsystemId)
        {
            return await GetReportTreeByCriteriaAsync(localeId, rep => rep.SubsystemId == subsystemId);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای پیش نمایش یا چاپ گزارش</returns>
        public async Task<PrintInfoViewModel> GetReportAsync(int reportId, int localeId)
        {
            return await GetReportPrintInfo(reportId, localeId);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مورد نیاز برای طراحی گزارش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <returns>اطلاعات مورد نیاز برای طراحی گزارش</returns>
        public async Task<PrintInfoViewModel> GetReportDesignAsync(int reportId, int localeId)
        {
            return await GetReportPrintInfo(reportId, localeId, false);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه گزارش را برای عملیات اعتبارسنجی خوانده و برمی گرداند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        /// <returns>اطلاعات خلاصه گزارش موزد نظر</returns>
        public async Task<ReportSummaryViewModel> GetReportSummaryAsync(int reportId)
        {
            var summary = default(ReportSummaryViewModel);
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var report = await repository.GetByIDAsync(reportId);
            if (report != null)
            {
                summary = _mapper.Map<ReportSummaryViewModel>(report);
            }

            return summary;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه گزارش پیش فرض برای یک فرم را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از فرم های قابل چاپ</param>
        /// <returns>اطلاعات خلاصه گزارش پیش فرض</returns>
        public async Task<ReportSummaryViewModel> GetDefaultReportByViewAsync(int viewId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var report = await repository.GetSingleByCriteriaAsync(
                rep => rep.ViewId == viewId && rep.IsDefault);
            return _mapper.Map<ReportSummaryViewModel>(report);
        }

        /// <summary>
        /// به روش آسنکرون، شناسه دیتابیسی متناظر با کد دو حرفی استاندارد یک زبان را خوانده و برمی گرداند
        /// </summary>
        /// <param name="localeCode">کد دو حرفی استاندارد زبان مورد نظر</param>
        /// <returns>شناسه دیتابیسی متناظر با کد زبانی داده شده</returns>
        public async Task<int> GetLocaleIdAsync(string localeCode)
        {
            Verify.ArgumentNotNullOrEmptyString(localeCode, nameof(localeCode));
            var repository = _unitOfWork.GetAsyncRepository<Locale>();
            var locale = await repository.GetSingleByCriteriaAsync(loc => loc.Code == localeCode);
            return (locale != null ? locale.Id : 0);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک گزارش ذخیره شده کاربری را ذخیره یا بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری</param>
        public async Task SaveUserReportAsync(LocalReportViewModel report)
        {
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
        }

        /// <summary>
        /// به روش آسنکرون، عنوان یک گزارش ذخیره شده کاربری را بروزرسانی می کند
        /// </summary>
        /// <param name="report">اطلاعات محلی شده گزارش کاربری شامل عنوان جدید مورد نظر</param>
        public async Task SetUserReportCaptionAsync(LocalReportViewModel report)
        {
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
        }

        /// <summary>
        /// به روش آسنکرون، یک گزارش ذخیر شده کاربری را به همراه کلیه اطلاعات مرتبط حذف می کند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        public async Task DeleteUserReportAsync(int reportId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var report = await repository.GetByIDWithTrackingAsync(
                reportId, rep => rep.LocalReports, rep => rep.Parameters);
            if (report != null)
            {
                report.LocalReports.Clear();
                report.Parameters.Clear();
                repository.Delete(report);

                if (report.IsDefault)
                {
                    var systemReport = await repository.GetSingleByCriteriaAsync(rep => rep.Code == report.Code && rep.IsSystem);
                    systemReport.IsDefault = true;
                    repository.Update(systemReport);
                }

                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// گزارش مشخص شده را به عنوان پیش فرض همه کاربران برای فرم مرتبط تنظیم می کند
        /// </summary>
        /// <param name="reportId">شناسه دیتابیسی گزارش مورد نظر</param>
        public async Task SetReportAsDefaultAsync(int reportId)
        {
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var report = await repository.GetByIDAsync(reportId);
            if (report != null)
            {
                var groupReports = await repository.GetByCriteriaAsync(rep => rep.Code == report.Code);
                foreach (var groupReport in groupReports)
                {
                    groupReport.IsDefault = (groupReport.Id == reportId);
                    repository.Update(groupReport);
                }

                await _unitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا عنوان گزارش محلی داده شده تکراری است یا نه؟
        /// </summary>
        /// <param name="localeId">شناسه دیتابیسی زبان جاری برنامه</param>
        /// <param name="report">گزارش محلی مورد نظر</param>
        /// <returns>در صورت تکراری بودن مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsDuplicateReportCaptionAsync(int localeId, LocalReportViewModel report)
        {
            var repository = _unitOfWork.GetAsyncRepository<LocalReport>();
            int count = await repository.GetCountByCriteriaAsync(
                rep => rep.LocaleId == localeId
                    && rep.Caption == report.Caption);
            return (count > 0);
        }

        private static void Localize(int localeId, List<Report> reports, List<TreeItemViewModel> tree)
        {
            foreach (var node in tree)
            {
                var report = reports
                    .Where(rep => rep.Id == node.Id)
                    .Single();
                var localReport = report.LocalReports
                    .Where(rep => localeId == rep.LocaleId)
                    .Single();
                node.Caption = localReport.Caption;
            }
        }

        private async Task<IList<TreeItemViewModel>> GetReportTreeByCriteriaAsync(
            int localeId, Expression<Func<Report, bool>> criteria)
        {
            var repository = _unitOfWork.GetAsyncRepository<Report>();
            var reports = await repository.GetByCriteriaAsync(
                criteria,
                rep => rep.Parent, rep => rep.LocalReports);
            var tree = reports
                .Select(rep => _mapper.Map<TreeItemViewModel>(rep))
                .ToList();
            Localize(localeId, reports.ToList(), tree);
            return tree;
        }

        private async Task<PrintInfoViewModel> GetReportPrintInfo(
            int reportId, int localeId, bool withParams = true)
        {
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
                    .Where(rep => rep.LocaleId == localeId)
                    .FirstOrDefault();
                reportView.Template = localReport?.Template;
            }

            return reportView;
        }

        private const int _quickReportId = 43;
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;
        private UserContextViewModel _currentContext;
    }
}
