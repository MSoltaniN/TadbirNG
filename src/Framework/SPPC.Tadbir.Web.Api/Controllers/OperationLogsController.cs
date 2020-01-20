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
        [Route(OperationLogApi.AllOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.View)]
        public async Task<IActionResult> GetAllOperationLogsAsync()
        {
            int itemCount = await _repository.GetLogCountAsync(GridOptions);
            SetItemCount(itemCount);
            var operationLogs = await _repository.GetLogsAsync(GridOptions);
            Localize(operationLogs);
            return Json(operationLogs);
        }

        // GET: api/system/sys-oplog
        [Route(OperationLogApi.AllSysOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.View)]
        public async Task<IActionResult> GetAllSysOperationLogsAsync()
        {
            int itemCount = await _repository.GetSystemLogCountAsync(GridOptions);
            SetItemCount(itemCount);
            var operationLogs = await _repository.GetSystemLogsAsync(GridOptions);
            Localize(operationLogs);
            return Json(operationLogs);
        }

        private void Localize(IList<OperationLogViewModel> logs)
        {
            foreach (var log in logs)
            {
                log.EntityTypeName = _strings[log.EntityTypeName ?? String.Empty];
                log.OperationName = _strings[log.OperationName];
                log.SourceListName = _strings[log.SourceListName ?? String.Empty];
                log.SourceName = _strings[log.SourceName ?? String.Empty];
            }
        }

        private readonly IOperationLogRepository _repository;
    }
}