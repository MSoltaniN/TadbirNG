using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class BalanceSheetController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public BalanceSheetController(IBalanceSheetRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/bal-sheet
        [HttpGet]
        [AuthorizeRequest(SecureEntity.BalanceSheet, (int)BalanceSheetPermissions.View)]
        [Route(BalanceSheetApi.BalanceSheetUrl)]
        public async Task<IActionResult> GetBalanceSheetAsync(
            DateTime date, bool? closing, int? ccenterId, int? projectId)
        {
            return await BalanceSheetResultAsync(date, closing, ccenterId, projectId);
        }

        private async Task<IActionResult> BalanceSheetResultAsync(
            DateTime date, bool? closing, int? ccenterId, int? projectId)
        {
            var parameters = GetParameters(date, closing, ccenterId, projectId);
            var balanceSheet = await _repository.GetBalanceSheetAsync(parameters);
            Localize(balanceSheet);
            return Json(balanceSheet);
        }

        private BalanceSheetParameters GetParameters(
            DateTime date, bool? closing, int? ccenterId, int? projectId)
        {
            var parameters = new BalanceSheetParameters()
            {
                Date = date,
                UseClosingVoucher = closing ?? false,
                CostCenterId = ccenterId,
                ProjectId = projectId,
                GridOptions = GridOptions ?? new GridOptions()
            };

            return parameters;
        }

        private void Localize(BalanceSheetViewModel balanceSheet)
        {
            foreach (var item in balanceSheet.Items)
            {
                item.Assets = GetReportLabel(item.Assets);
                item.Liabilities = GetReportLabel(item.Liabilities);
            }
        }

        private string GetReportLabel(string label)
        {
            return !String.IsNullOrEmpty(label)
                ? String.Format("{0}:", _strings[label])
                : String.Empty;
        }

        private readonly IBalanceSheetRepository _repository;
    }
}