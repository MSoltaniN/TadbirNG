using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountGroupsController : ValidatingController<AccountGroupViewModel>
    {
        public AccountGroupsController(IAccountGroupRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.AccountGroup; }
        }

        // GET: api/accgroups
        [Route(AccountGroupApi.AccountGroupsUrl)]
        [AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.View)]
        public async Task<IActionResult> GetAccountGroupsAsync()
        {
            int itemCount = await _repository.GetCountAsync(GridOptions);
            SetItemCount(itemCount);
            var accountGroups = await _repository.GetAccountGroupsAsync();
            Localize(accountGroups);
            return Json(accountGroups);
        }

        private void Localize(IList<AccountGroupViewModel> accountGroups)
        {
            Array.ForEach(accountGroups.ToArray(), grp => grp.Category = _strings[grp.Category]);
        }

        private readonly IAccountGroupRepository _repository;
    }
}