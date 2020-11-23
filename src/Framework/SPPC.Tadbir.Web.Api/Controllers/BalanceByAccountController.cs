﻿using System.Linq;
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
    [Produces("application/json")]
    public class BalanceByAccountController : ApiControllerBase
    {
        public BalanceByAccountController(IBalanceByAccountRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/balancebyacc
        [HttpGet]
        [Route(BalanceByAccountApi.BalanceByAccountUrl)]
        [AuthorizeRequest(SecureEntity.BalanceByAccount, (int)BalanceByAccountPermissions.View)]
        public async Task<IActionResult> GetBalanceByAccountAsync()
        {
            var parameters = GetParameters<BalanceByAccountParameters>();
            if (parameters == null)
            {
                return BadRequest();
            }

            var gridOptions = GridOptions ?? new GridOptions();
            parameters.GridOptions = gridOptions;
            var report = await _repository.GetBalanceByAccountAsync(parameters);

            SetItemCount(report.Items.Count);
            report.SetItems(report.Items
                .ApplyPaging(gridOptions)
                .ToList());
            SetRowNumbers(report.Items);
            return Json(report);
        }

        private readonly IBalanceByAccountRepository _repository;
    }
}