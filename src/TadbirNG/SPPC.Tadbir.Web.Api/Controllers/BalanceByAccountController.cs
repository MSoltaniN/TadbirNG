using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class BalanceByAccountController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public BalanceByAccountController(IBalanceByAccountRepository repository,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/balancebyacc
        [HttpGet]
        [Route(BalanceByAccountApi.BalanceByAccountUrl)]
        [AuthorizeRequest(SecureEntity.BalanceByAccount, (int)BalanceByAccountPermissions.View)]
        public async Task<IActionResult> GetBalanceByAccountAsync()
        {
            var parameters = GetParameters<BalanceByAccountParameters>();
            if (parameters == null)
            {
                return BadRequestResult(
                    _strings.Format(AppStrings.RequestFailedNoData, AppStrings.ReportParameters));
            }

            var gridOptions = GridOptions ?? new GridOptions();
            parameters.GridOptions = gridOptions;
            var report = await _repository.GetBalanceByAccountAsync(parameters);
            var filtered = report.Items.Apply(gridOptions, false);
            SetItemCount(filtered.Count());
            report.SetItems(filtered
                .ApplyPaging(gridOptions)
                .ToList());
            SetRowNumbers(report.Items);
            return Json(report);
        }

        private readonly IBalanceByAccountRepository _repository;
    }
}