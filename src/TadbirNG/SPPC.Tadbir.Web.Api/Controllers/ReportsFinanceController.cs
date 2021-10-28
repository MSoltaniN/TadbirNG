using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class ReportsFinanceController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="authorize"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public ReportsFinanceController(IFinanceReportRepository repository, IAuthorizeRequest authorize,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
            _authorize = authorize;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/voucher/sum-by-date
        [HttpGet]
        [Route(ReportsFinanceApi.EnvironmentVoucherSummaryByDateUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.Print)]
        public async Task<IActionResult> GetEnvironmentVoucherSummaryByDateAsync()
        {
            int itemCount = await _repository.GetVoucherSummaryByDateCountAsync(GridOptions);
            SetItemCount(itemCount);
            var report = await _repository.GetVoucherSummaryByDateReportAsync(GridOptions);
            Localize(report);
            return Json(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/voucher/{voucherNo:min(1)}/std-form
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherStandardFormUrl)]
        public async Task<IActionResult> GetStandardVoucherFormAsync(int voucherNo)
        {
            return await GetStandardFormAsync(voucherNo);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/voucher/{voucherNo:min(1)}/std-form-detail
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherStandardFormWithDetailUrl)]
        public async Task<IActionResult> GetStandardVoucherFormWithDetailAsync(int voucherNo)
        {
            return await GetStandardFormAsync(voucherNo, true);
        }

        private async Task<IActionResult> GetStandardFormAsync(int voucherNo, bool withDetail = false)
        {
            var standardForm = await _repository.GetStandardVoucherFormAsync(voucherNo, withDetail);
            if (standardForm == null)
            {
                string message = _strings.Format(
                    AppStrings.ItemByNumberNotFound, AppStrings.Voucher, voucherNo.ToString());
                return BadRequestResult(message);
            }

            var result = GetAuthorizationResult((SubjectType)standardForm.SubjectType);
            if (result != null)
            {
                return result;
            }

            Localize(standardForm);
            return JsonReadResult(standardForm);
        }

        private IActionResult GetAuthorizationResult(SubjectType subject)
        {
            var permission = new PermissionBriefViewModel();
            if (subject == SubjectType.Normal)
            {
                permission.EntityName = SecureEntity.Voucher;
                permission.Flags = (int)VoucherPermissions.Print;
            }
            else
            {
                permission.EntityName = SecureEntity.DraftVoucher;
                permission.Flags = (int)DraftVoucherPermissions.Print;
            }

            var permissions = new PermissionBriefViewModel[] { permission };
            _authorize.SetRequiredPermissions(permissions);
            return _authorize.GetAuthorizationResult(Request);
        }

        private void Localize(IList<VoucherSummaryViewModel> report)
        {
            var now = DateTime.Now;
            var languages = GetPrimaryRequestLanguage();
            if (languages == "fa")
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
            var languages = GetPrimaryRequestLanguage();
            if (languages == "fa")
            {
                standardVoucher.Date = JalaliDateTime
                    .FromDateTime(now.Parse(standardVoucher.Date, false))
                    .ToShortDateString();
            }
        }

        private readonly IFinanceReportRepository _repository;
        private readonly IAuthorizeRequest _authorize;
    }
}
