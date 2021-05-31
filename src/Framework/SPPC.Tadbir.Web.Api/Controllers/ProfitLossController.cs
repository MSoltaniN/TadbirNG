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
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class ProfitLossController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="configRepository"></param>
        /// <param name="systemRepository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public ProfitLossController(
            IProfitLossRepository repository, IConfigRepository configRepository,
            ISystemConfigRepository systemRepository, IStringLocalizer<AppStrings> strings,
            ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
            _systemRepository = systemRepository;
            _configRepository = configRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/profit-loss
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossUrl)]
        public async Task<IActionResult> GetProfitLossAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ProfitLossResultAsync(from, to, tax, closing, ccenterId, projectId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/profit-loss/by-fperiods
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossByFiscalPeriodsUrl)]
        public async Task<IActionResult> GetProfitLossByFiscalPeriodsAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ComparativeProfitLossResultAsync(
                from, to, tax, closing, ccenterId, projectId, _repository.GetProfitLossByFiscalPeriodsAsync);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/profit-loss/simple
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleUrl)]
        public async Task<IActionResult> GetSimpleProfitLossAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ProfitLossResultAsync(date, date, tax, closing, ccenterId, projectId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <returns></returns>
        // GET: api/profit-loss/simple/by-projects
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleByProjectsUrl)]
        public async Task<IActionResult> GetSimpleProfitLossByProjectsAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId)
        {
            return await ComparativeProfitLossResultAsync(
                date, date, tax, closing, ccenterId, null, _repository.GetProfitLossByProjectsAsync);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/profit-loss/simple/by-branches
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleByBranchesUrl)]
        public async Task<IActionResult> GetSimpleProfitLossByBranchesAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ComparativeProfitLossResultAsync(
                date, date, tax, closing, ccenterId, projectId, _repository.GetProfitLossByBranchesAsync);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        // GET: api/profit-loss/simple/by-fperiods
        [HttpGet]
        [AuthorizeRequest(SecureEntity.ProfitLoss, (int)ProfitLossPermissions.View)]
        [Route(ProfitLossApi.ProfitLossSimpleByFiscalPeriodsUrl)]
        public async Task<IActionResult> GetSimpleProfitLossByFiscalPeriodsAsync(
            DateTime date, decimal? tax, bool? closing, int? ccenterId, int? projectId)
        {
            return await ComparativeProfitLossResultAsync(
                date, date, tax, closing, ccenterId, projectId, _repository.GetProfitLossByFiscalPeriodsAsync);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <param name="balanceItems"></param>
        /// <returns></returns>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="date"></param>
        /// <param name="tax"></param>
        /// <param name="closing"></param>
        /// <param name="ccenterId"></param>
        /// <param name="projectId"></param>
        /// <param name="balanceItems"></param>
        /// <returns></returns>
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

        private static string LocalizeLabel(IDictionary<string, string> labelMap, string label)
        {
            string localized = label;
            if (!String.IsNullOrEmpty(label) && labelMap.ContainsKey(label))
            {
                localized = labelMap[label];
            }

            return localized;
        }

        private async Task<IActionResult> ProfitLossResultAsync(
            DateTime from, DateTime to, decimal? tax, bool? closing, int? ccenterId, int? projectId,
            IList<StartEndBalanceViewModel> balanceItems = null)
        {
            var parameters = GetParameters(from, to, tax, closing, ccenterId, projectId);
            var profitLoss = await _repository.GetProfitLossAsync(
                parameters, balanceItems ?? new List<StartEndBalanceViewModel>());
            await LocalizeAsync(profitLoss);
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
            await LocalizeAsync(profitLoss);
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

        private async Task LocalizeAsync(ProfitLossViewModel profitLoss)
        {
            var locale = GetPrimaryRequestLanguage();
            int localeId = await _systemRepository.GetLocaleIdAsync(locale);
            var fullConfig = await _configRepository.GetFormLabelConfigAsync(
                CustomFormId.ProfitLoss, localeId);
            var labelConfig = fullConfig.Current;
            foreach (var item in profitLoss.Items)
            {
                item.Group = LocalizeLabel(labelConfig.LabelMap, item.Group);
                item.Account = LocalizeLabel(labelConfig.LabelMap, item.Account);
            }

            foreach (var item in profitLoss.ComparativeItems)
            {
                item.Group = LocalizeLabel(labelConfig.LabelMap, item.Group);
                item.Account = LocalizeLabel(labelConfig.LabelMap, item.Account);
            }
        }

        private readonly IProfitLossRepository _repository;
        private readonly ISystemConfigRepository _systemRepository;
        private readonly IConfigRepository _configRepository;
        private delegate Task<ProfitLossViewModel> ComparativeReportFunction(
            ProfitLossParameters parameters, IEnumerable<StartEndBalanceViewModel> balanceItems);
    }
}