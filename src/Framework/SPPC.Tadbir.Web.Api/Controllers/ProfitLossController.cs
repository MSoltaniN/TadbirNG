using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
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

        // GET: api/profit-loss/by-ccenters
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossByCostCentersUrl)]
        public async Task<IActionResult> GetProfitLossByCostCentersAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? projectId)
        {
            return await ComparativeProfitLossResultAsync(
                from, to, tax, closing, null, projectId, _repository.GetProfitLossByCostCentersAsync);
        }

        // GET: api/profit-loss/by-projects
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossByProjectsUrl)]
        public async Task<IActionResult> GetProfitLossByProjectsAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId)
        {
            return await ComparativeProfitLossResultAsync(
                from, to, tax, closing, ccenterId, null, _repository.GetProfitLossByProjectsAsync);
        }

        // GET: api/profit-loss/by-branches
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossByBranchesUrl)]
        public async Task<IActionResult> GetProfitLossByBranchesAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ComparativeProfitLossResultAsync(
                from, to, tax, closing, ccenterId, projectId, _repository.GetProfitLossByBranchesAsync);
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

        // GET: api/profit-loss/simple/by-ccenters
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleByCostCentersUrl)]
        public async Task<IActionResult> GetSimpleProfitLossByCostCentersAsync(
            DateTime date, decimal? tax, bool? closing, int? projectId)
        {
            return await ComparativeProfitLossResultAsync(
                date, date, tax, closing, null, projectId, _repository.GetProfitLossByCostCentersAsync);
        }

        // GET: api/profit-loss/simple/by-projects
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleByProjectsUrl)]
        public async Task<IActionResult> GetSimpleProfitLossByProjectsAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId)
        {
            return await ComparativeProfitLossResultAsync(
                date, date, tax, closing, ccenterId, null, _repository.GetProfitLossByCostCentersAsync);
        }

        // GET: api/profit-loss/simple/by-branches
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleByBranchesUrl)]
        public async Task<IActionResult> GetSimpleProfitLossByBranchesAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId)
        {
            return await ComparativeProfitLossResultAsync(
                date, date, tax, closing, ccenterId, null, _repository.GetProfitLossByBranchesAsync);
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

        private async Task<IActionResult> ComparativeProfitLossResultAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId,
            ComparativeReportFunction compareFunction)
        {
            var actionDetail = GetParameters<ActionDetailViewModel>();
            if (actionDetail == null)
            {
                return await ProfitLossResultAsync(from, to, tax, closing, ccenterId, projectId);
            }

            var parameters = GetParameters(from, to, tax, closing, ccenterId, projectId);
            parameters.CompareItems.AddRange(actionDetail.Items);
            var profitLoss = await compareFunction(parameters, null);
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
                item.Group = _strings[item.Group ?? String.Empty];
                item.Account = _strings[item.Account ?? String.Empty];
            }

            foreach (var item in profitLoss.ItemsByCostCenters)
            {
                item.Group = _strings[item.Group ?? String.Empty];
                item.Account = _strings[item.Account ?? String.Empty];
            }

            foreach (var item in profitLoss.ItemsByProjects)
            {
                item.Group = _strings[item.Group ?? String.Empty];
                item.Account = _strings[item.Account ?? String.Empty];
            }

            foreach (var item in profitLoss.ItemsByBranches)
            {
                item.Group = _strings[item.Group ?? String.Empty];
                item.Account = _strings[item.Account ?? String.Empty];
            }

            foreach (var item in profitLoss.ItemsByFiscalPeriods)
            {
                item.Group = _strings[item.Group ?? String.Empty];
                item.Account = _strings[item.Account ?? String.Empty];
            }
        }

        private readonly IProfitLossRepository _repository;
        private delegate Task<ProfitLossViewModel> ComparativeReportFunction(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems);
    }
}