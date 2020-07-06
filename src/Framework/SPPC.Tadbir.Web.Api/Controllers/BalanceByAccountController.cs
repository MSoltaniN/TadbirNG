using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class BalanceByAccountController : ApiControllerBase
    {
        public BalanceByAccountController(IBalanceByAccountRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/balancebyacc
        [Route(BalanceByAccountApi.BalanceByAccountUrl)]
        [AuthorizeRequest(SecureEntity.BalanceByAccount, (int)BalanceByAccountPermissions.View)]
        public async Task<IActionResult> GetBalanceByAccountAsync()
        {
            var parameters = GetParameters<BalanceByAccountParameters>();

            if (parameters == null)
            {
                return BadRequest("اطلاعات ارسالی به سرور معتبر نیست");
            }

            var report = await _repository.GetBalanceByAccountAsync(parameters);

            SetItemCount(report.Items.Count);
            report.SetItems(report.Items.ApplyPaging(parameters.GridOptions).ToList());
            int rowNo = (parameters.GridOptions.Paging.PageSize * (parameters.GridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var item in report.Items)
            {
                item.RowNo = rowNo++;
            }

            return Json(report);
        }

        private readonly IBalanceByAccountRepository _repository;
    }
}