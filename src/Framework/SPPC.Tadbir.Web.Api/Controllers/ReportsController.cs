using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Report;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class ReportsController : ApiControllerBase
    {
        public ReportsController(IReportRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/reports/{baseId:min(1)}
        [Route(ReportApi.DefaultSystemReportUrl)]
        public async Task<IActionResult> GetDefaultSystemReportAsync(int baseId)
        {
            var report = await _repository.GetDefaultSystemReportAsync(baseId);
            Localize(report);
            return JsonReadResult(report);
        }

        // GET: api/reports/voucher/sum-by-date
        [Route(ReportApi.EnvironmentVoucherSummaryByDateUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetEnvironmentVoucherSummaryByDateAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            int itemCount = await _repository.GetVoucherSummaryByDateCountAsync(GridOptions);
            SetItemCount(itemCount);
            var report = await _repository.GetVoucherSummaryByDateReportAsync(GridOptions);
            Localize(report);
            return Json(report);
        }

        // GET: api/reports/voucher/std-form
        [Route(ReportApi.VoucherStandardFormUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetStandardVoucherFormAsync()
        {
            var standardForm = await _repository.GetStandardVoucherFormAsync(GridOptions);
            Localize(standardForm);
            return JsonReadResult(standardForm);
        }

        // GET: api/reports/voucher/std-form-detail
        [Route(ReportApi.VoucherStandardFormWithDetailUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.View)]
        public async Task<IActionResult> GetStandardVoucherFormWithDetailAsync()
        {
            var formWithDetail = await _repository.GetStandardVoucherFormAsync(GridOptions, true);
            Localize(formWithDetail);
            return JsonReadResult(formWithDetail);
        }

        // GET: api/reports/tree
        [Route(ReportApi.ReportsHierarchyUrl)]
        public async Task<IActionResult> GetReportTreeAsync()
        {
            var tree = await _repository.GetReportTreeAsync();
            Localize(tree);
            return Json(tree);
        }

        private void Localize(IList<CoreReportViewModel> reports)
        {
            foreach (var report in reports)
            {
                report.Name = _strings[report.Name];
            }
        }

        private void Localize(IList<VoucherSummaryViewModel> report)
        {
            var now = DateTime.Now;
            var languages = GetAcceptLanguages();
            if (languages.StartsWith("fa"))
            {
                Array.ForEach(report.ToArray(),
                    summary => summary.Date = JalaliDateTime
                        .FromDateTime(now.Parse(summary.Date, false))
                        .ToShortDateString());
            }

            Array.ForEach(report.ToArray(), summary =>
            {
                summary.BalanceStatus = _strings[summary.BalanceStatus];
                summary.CheckStatus = _strings[summary.CheckStatus];
                summary.Origin = _strings[summary.Origin];
            });
        }

        private void Localize(StandardVoucherViewModel standardVoucher)
        {
            if (standardVoucher == null)
            {
                return;
            }

            var now = DateTime.Now;
            var languages = GetAcceptLanguages();
            if (languages.StartsWith("fa"))
            {
                standardVoucher.Date = JalaliDateTime
                    .FromDateTime(now.Parse(standardVoucher.Date, false))
                    .ToShortDateString();
            }
        }

        private void Localize(ReportViewModel report)
        {
            if (report != null)
            {
                if (report.BaseResourceKeys != null)
                {
                    var keys = report.BaseResourceKeys.Split(',');
                    Array.ForEach(keys, key => report.ResourceMap.Add(key, _strings[key]));
                }
            }
        }

        private readonly IReportRepository _repository;
    }
}