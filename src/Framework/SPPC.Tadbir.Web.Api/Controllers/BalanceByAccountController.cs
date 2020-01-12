using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

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
            var parameters = GetHeaderParameters<BalanceByAccountParameters>();

            if (parameters == null)
            {
                return BadRequest("اطلاعات ارسالی به سرور معتبر نیست");
            }

            var report = await _repository.GetBalanceByAccountAsync(parameters);

            return Json(report);
        }

        private async Task<IActionResult> BalanceByAccountResultAsync(BalanceByAccountParameters parameters)
        {
            if (parameters == null)
            {
                return BadRequest("اطلاعات ارسالی به سرور معتبر نیست");
            }

            switch (parameters.ViewId)
            {
                case ViewName.Account:
                    {
                        break;
                    }

                case ViewName.DetailAccount:
                    {
                        break;
                    }

                case ViewName.CostCenter:
                    {
                        break;
                    }

                case ViewName.Project:
                    {
                        break;
                    }

                default:
                    break;
            }

            return Ok();
        }

        private readonly IBalanceByAccountRepository _repository;
    }
}