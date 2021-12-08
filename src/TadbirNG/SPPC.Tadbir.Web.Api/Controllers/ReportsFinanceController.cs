using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Reporting;
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
        /// <param name="tokenManager"></param>
        public ReportsFinanceController(IFinanceReportRepository repository, IAuthorizeRequest authorize,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _authorize = authorize;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/finance/vouchers/sum-by-date
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherSummaryByDateUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.Print)]
        public async Task<IActionResult> GetVoucherSummaryByDateAsync()
        {
            var report = await _repository.GetVoucherSummaryByDateReportAsync(GridOptions);
            SetItemCount(report.Count);
            Localize(report);
            return Json(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/finance/vouchers/sum-by-no
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherSummaryByNoUrl)]
        [AuthorizeRequest(SecureEntity.Vouchers, (int)ManageVouchersPermissions.Print)]
        public async Task<IActionResult> GetVoucherSummaryByNoAsync()
        {
            var report = await _repository.GetVoucherSummaryByNoReportAsync(GridOptions);
            SetItemCount(report.Count);
            Localize(report);
            return Json(report);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/reports/finance/voucher-by-no/{voucherNo:min(1)}/std-form
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
        // GET: api/reports/finance/voucher-by-no/{voucherNo:min(1)}/std-form-detail
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherStandardFormWithDetailUrl)]
        public async Task<IActionResult> GetStandardVoucherFormWithDetailAsync(int voucherNo)
        {
            return await GetStandardFormAsync(voucherNo, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="voucherNo"></param>
        /// <returns></returns>
        // GET: api/reports/finance/voucher-by-no/{voucherNo:min(1)}/by-detail
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherByDetailUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Print)]
        public async Task<IActionResult> GetVoucherByDetailAsync(int voucherNo)
        {
            var voucher = await _repository.GetVoucherByDetailAsync(voucherNo);
            return JsonReadResult(voucher);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="voucherNo"></param>
        /// <returns></returns>
        // GET: api/reports/finance/voucher-by-no/{voucherNo:min(1)}/by-ledger
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherByLedgerUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Print)]
        public async Task<IActionResult> GetVoucherByLedgerAsync(int voucherNo)
        {
            var voucher = await _repository.GetVoucherByLedgerAsync(voucherNo);
            return JsonReadResult(voucher);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="voucherNo"></param>
        /// <returns></returns>
        // GET: api/reports/finance/voucher-by-no/{voucherNo:min(1)}/by-subsid
        [HttpGet]
        [Route(ReportsFinanceApi.VoucherBySubsidiaryUrl)]
        [AuthorizeRequest(SecureEntity.Voucher, (int)VoucherPermissions.Print)]
        public async Task<IActionResult> GetVoucherBySubsidiaryAsync(int voucherNo)
        {
            var voucher = await _repository.GetVoucherBySubsidiaryAsync(voucherNo);
            return JsonReadResult(voucher);
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
            Array.ForEach(report.ToArray(), summary =>
            {
                summary.BalanceStatus = _strings[summary.BalanceStatus];
                summary.StatusName = _strings[summary.StatusName];
                summary.OriginName = _strings[summary.OriginName];
            });
        }

        private readonly IFinanceReportRepository _repository;
        private readonly IAuthorizeRequest _authorize;
    }
}
