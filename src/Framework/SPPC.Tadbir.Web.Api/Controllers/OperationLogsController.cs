using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class OperationLogsController : ApiControllerBase
    {
        public OperationLogsController(IOperationLogRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/system/oplog
        [Route(SystemApi.AllOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.View)]
        public async Task<IActionResult> GetAllOperationLogsAsync()
        {
            int itemCount = await _repository.GetLogCountAsync(GridOptions);
            SetItemCount(itemCount);
            var operationLogs = await _repository.GetLogsAsync(null, null, GridOptions);
            Localize(operationLogs);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/company/{companyId:int}
        [Route(SystemApi.CompanyOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.View)]
        public async Task<IActionResult> GetCompanyOperationLogsAsync(int companyId)
        {
            var operationLogs = await _repository.GetLogsAsync(null, companyId, GridOptions);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/user/{userId:int}
        [Route(SystemApi.UserOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.View)]
        public async Task<IActionResult> GetUserOperationLogsAsync(int userId)
        {
            var operationLogs = await _repository.GetLogsAsync(userId, null, GridOptions);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/user/{userId:int}/company/{companyId:int}
        [Route(SystemApi.UserCompanyOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.View)]
        public async Task<IActionResult> GetUserCompanyOperationLogsAsync(int userId, int companyId)
        {
            var operationLogs = await _repository.GetLogsAsync(userId, companyId, GridOptions);
            return Json(operationLogs);
        }

        private void Localize(IList<SysOperationLogViewModel> logs)
        {
            // TODO: Re-implement this method
        }

        private readonly IOperationLogRepository _repository;
    }
}