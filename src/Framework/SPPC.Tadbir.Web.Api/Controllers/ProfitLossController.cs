using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class ProfitLossController : ApiControllerBase
    {
        public ProfitLossController(
            IProfitLossRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/profit-loss
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossUrl)]
        public async Task<IActionResult> GetProfitLossAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ProfitLossResultAsync(from, to, tax, closing, ccenterId, projectId);
        }

        // GET: api/profit-loss/simple
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleUrl)]
        public async Task<IActionResult> GetSimpleProfitLossAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ProfitLossResultAsync(date, date, tax, closing, ccenterId, projectId);
        }

        // PUT: api/profit-loss
        [HttpPut]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossUrl)]
        public async Task<IActionResult> PutPeriodicProfitLossAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId,
            [FromBody] IList<StartEndBalanceViewModel> balanceItems)
        {
            return await ProfitLossResultAsync(from, to, tax, closing, ccenterId, projectId, balanceItems);
        }

        // PUT: api/profit-loss/simple
        [HttpPut]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleUrl)]
        public async Task<IActionResult> PutPeriodicSimpleProfitLossAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId, int? projectId,
            [FromBody] IList<StartEndBalanceViewModel> balanceItems)
        {
            return await ProfitLossResultAsync(date, date, tax, closing, ccenterId, projectId, balanceItems);
        }

        private async Task<IActionResult> ProfitLossResultAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId,
            IList<StartEndBalanceViewModel> balanceItems = null)
        {
            var parameters = GetParameters(from, to, tax, closing, ccenterId, projectId);
            var profitLoss = await _repository.GetProfitLossAsync(parameters, balanceItems);
            Localize(profitLoss);
            return Json(profitLoss);
        }

        private ProfitLossParameters GetParameters(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            var parameters = new ProfitLossParameters()
            {
                FromDate = from,
                ToDate = to,
                TaxAmount = tax ?? 0.0M,
                UseClosingTempVoucher = closing ?? false,
                CostCenterId = ccenterId,
                ProjectId = projectId,
                GridOptions = GridOptions ?? new GridOptions()
            };

            return parameters;
        }

        private void Localize(ProfitLossViewModel profitLoss)
        {
            foreach (var item in profitLoss.Items)
            {
                item.Category = _strings[item.Category ?? String.Empty];
                item.Account = _strings[item.Account ?? String.Empty];
            }
        }

        private readonly IProfitLossRepository _repository;
    }
}