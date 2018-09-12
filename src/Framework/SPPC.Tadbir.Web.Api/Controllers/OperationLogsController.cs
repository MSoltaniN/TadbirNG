using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Core;
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
        public async Task<IActionResult> GetAllOperationLogsAsync()
        {
            int itemCount = await _repository.GetLogCountAsync(GridOptions);
            SetItemCount(itemCount);
            var operationLogs = await _repository.GetLogsAsync(null, null, GridOptions);
            Localize(operationLogs);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/metadata
        [Route(SystemApi.OperationLogMetadataUrl)]
        public async Task<IActionResult> GetOperationLogMetadataAsync()
        {
            var metadata = await _repository.GetLogMetadataAsync();
            return JsonReadResult(metadata);
        }

        // GET: api/system/oplog/company/{companyId:int}
        [Route(SystemApi.CompanyOperationLogsUrl)]
        public async Task<IActionResult> GetCompanyOperationLogsAsync(int companyId)
        {
            var operationLogs = await _repository.GetLogsAsync(null, companyId, GridOptions);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/user/{userId:int}
        [Route(SystemApi.UserOperationLogsUrl)]
        public async Task<IActionResult> GetUserOperationLogsAsync(int userId)
        {
            var operationLogs = await _repository.GetLogsAsync(userId, null, GridOptions);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/user/{userId:int}/company/{companyId:int}
        [Route(SystemApi.UserCompanyOperationLogsUrl)]
        public async Task<IActionResult> GetUserCompanyOperationLogsAsync(int userId, int companyId)
        {
            var operationLogs = await _repository.GetLogsAsync(userId, companyId, GridOptions);
            return Json(operationLogs);
        }

        private void Localize(IList<OperationLogViewModel> logs)
        {
            Array.ForEach(logs.ToArray(), log =>
            {
                log.Action = _strings[log.Action];
                log.Entity = _strings[log.Entity];
                log.Result = _strings[log.Result];
            });
        }

        private readonly IOperationLogRepository _repository;
    }
}