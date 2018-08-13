using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class OperationLogsController : ApiControllerBase
    {
        public OperationLogsController(IOperationLogRepository repository)
        {
            _repository = repository;
        }

        // GET: api/system/oplog/company/{companyId:int}
        [Route(SystemApi.CompanyOperationLogsUrl)]
        public async Task<IActionResult> GetCompanyOperationLogsAsync(int companyId)
        {
            var operationLogs = await _repository.GetOperationLogsAsync(null, companyId, GridOptions);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/user/{userId:int}
        [Route(SystemApi.UserOperationLogsUrl)]
        public async Task<IActionResult> GetUserOperationLogsAsync(int userId)
        {
            var operationLogs = await _repository.GetOperationLogsAsync(userId, null, GridOptions);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/user/{userId:int}/company/{companyId:int}
        [Route(SystemApi.UserCompanyOperationLogsUrl)]
        public async Task<IActionResult> GetUserCompanyOperationLogsAsync(int userId, int companyId)
        {
            var operationLogs = await _repository.GetOperationLogsAsync(userId, companyId, GridOptions);
            return Json(operationLogs);
        }

        private readonly IOperationLogRepository _repository;
    }
}