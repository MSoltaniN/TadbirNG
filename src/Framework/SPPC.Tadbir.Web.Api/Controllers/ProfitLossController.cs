using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
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

        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossUrl)]
        public async Task<IActionResult> GetProfitLossAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing)
        {
            return await ProfitLossResultAsync(from, to, tax, closing);
        }

        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleUrl)]
        public async Task<IActionResult> GetSimpleProfitLossAsync(
            DateTime date, decimal? tax, bool? closing)
        {
            return await ProfitLossResultAsync(date, date, tax, closing);
        }

        private async Task<IActionResult> ProfitLossResultAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing)
        {
            var parameters = GetParameters(from, to, tax, closing);
            var profitLoss = await _repository.GetProfitLossAsync(parameters);
            return Json(profitLoss);
        }

        private ProfitLossParameters GetParameters(
            DateTime from, DateTime to, decimal? tax, bool? closing)
        {
            return new ProfitLossParameters()
            {
                FromDate = from,
                ToDate = to,
                TaxAmount = tax ?? 0.0M,
                UseClosingTempVoucher = closing ?? false,
                GridOptions = GridOptions ?? new GridOptions()
            };
        }

        private readonly IProfitLossRepository _repository;
    }
}