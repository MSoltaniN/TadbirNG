using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]

    public class SystemIssuesController : ValidatingController<SystemIssueViewModel>
    {
        public SystemIssuesController(
            ISystemIssueRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.SystemIssue; }
        }

        // GET: api/sysissues
        [Route(SystemIssueApi.SystemIssuesUrl)]
        [AuthorizeRequest(SecureEntity.SystemIssue, (int)SystemIssuePermissions.View)]
        public async Task<IActionResult> GetSystemIssuesAsync()
        {
            var issues = await _repository.GetUserSystemIssuesAsync(SecurityContext.User.Id);
            Localize(issues);
            return Json(issues);
        }

        private void Localize(IList<SystemIssueViewModel> issues)
        {
            foreach (var item in issues)
            {
                item.Title = _strings[item.Title];
            }
        }

        private readonly ISystemIssueRepository _repository;
    }
}