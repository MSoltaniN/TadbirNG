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
        public async Task<IActionResult> GetOperationLogsAsync()
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
        public async Task<IActionResult> GetSysOperationLogsAsync()
        {
            int itemCount = await _repository.GetSystemLogCountAsync(GridOptions);
            SetItemCount(itemCount);
            var operationLogs = await _repository.GetSystemLogsAsync(GridOptions);
            Localize(operationLogs);
            return Json(operationLogs);
        }

        // GET: api/system/oplog/archive
        [Route(OperationLogApi.OperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.ViewArchive)]
        public async Task<IActionResult> GetOperationLogArchiveAsync()
        {
            int itemCount = await _repository.GetLogArchiveCountAsync(GridOptions);
            SetItemCount(itemCount);
            var logArchive = await _repository.GetLogsArchiveAsync(GridOptions);
            Localize(logArchive);
            return Json(logArchive);
        }

        // GET: api/system/sys-oplog/archive
        [Route(OperationLogApi.SysOperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.ViewArchive)]
        public async Task<IActionResult> GetSysOperationLogArchiveAsync()
        {
            int itemCount = await _repository.GetSystemLogArchiveCountAsync(GridOptions);
            SetItemCount(itemCount);
            var logArchive = await _repository.GetSystemLogsArchiveAsync(GridOptions);
            Localize(logArchive);
            return Json(logArchive);
        }

        // PUT: api/system/oplog/archive
        [HttpPut]
        [Route(OperationLogApi.OperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.Archive)]
        public async Task<IActionResult> PutSelectedLogsAsArchived(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequest(_strings[AppStrings.InvalidArchiveRange]);
            }

            await _repository.MoveLogsToArchiveAsync(from, to);
            return Ok();
        }

        // PUT: api/system/oplog
        [HttpPut]
        [Route(OperationLogApi.AllOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.Recover)]
        public async Task<IActionResult> PutSelectedLogsAsRecovered(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequest(_strings[AppStrings.InvalidArchiveRange]);
            }

            await _repository.RecoverLogsFromArchive(from, to);
            return Ok();
        }

        // PUT: api/system/sys-oplog/archive
        [HttpPut]
        [Route(OperationLogApi.SysOperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.Archive)]
        public async Task<IActionResult> PutSelectedSysLogsAsArchived(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequest(_strings[AppStrings.InvalidArchiveRange]);
            }

            await _repository.MoveSystemLogsToArchiveAsync(from, to);
            return Ok();
        }

        // PUT: api/system/sys-oplog
        [HttpPut]
        [Route(OperationLogApi.AllSysOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.Recover)]
        public async Task<IActionResult> PutSelectedSysLogsAsRecovered(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequest(_strings[AppStrings.InvalidArchiveRange]);
            }

            await _repository.RecoverSystemLogsFromArchive(from, to);
            return Ok();
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