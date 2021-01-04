using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class BalanceSheetController : ApiControllerBase
    {
        public BalanceSheetController(IBalanceSheetRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

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

        private readonly IBalanceSheetRepository _repository;
    }
}