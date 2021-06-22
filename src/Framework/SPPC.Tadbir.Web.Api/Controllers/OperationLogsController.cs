using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class OperationLogsController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenService"></param>
        public OperationLogsController(IOperationLogRepository repository,
            IStringLocalizer<AppStrings> strings, ITokenService tokenService)
            : base(strings, tokenService)
        {
            _repository = repository;
        }

        #region Company Log Operations

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/system/oplog
        [HttpGet]
        [Route(OperationLogApi.OperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.View)]
        public async Task<IActionResult> GetOperationLogsAsync()
        {
            var operationLogs = await _repository.GetLogsAsync(GridOptions);
            Localize(operationLogs.Items);
            return JsonListResult(operationLogs);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/system/oplog/archive
        [HttpGet]
        [Route(OperationLogApi.OperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.ViewArchive)]
        public async Task<IActionResult> GetOperationLogArchiveAsync()
        {
            var logArchive = await _repository.GetLogsArchiveAsync(GridOptions);
            Localize(logArchive.Items);
            return JsonListResult(logArchive);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/system/oplog/all
        [HttpGet]
        [Route(OperationLogApi.AllOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog,
            (int)(OperationLogPermissions.View | OperationLogPermissions.ViewArchive))]
        public async Task<IActionResult> GetAllOperationLogsAsync()
        {
            var mergedLogs = await _repository.GetMergedLogsAsync(GridOptions);
            Localize(mergedLogs.Items);
            return JsonListResult(mergedLogs);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // POST: api/system/oplog/archive
        [HttpPost]
        [Route(OperationLogApi.OperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.Archive)]
        public async Task<IActionResult> PostSelectedLogsAsArchived(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequestResult(_strings[AppStrings.InvalidArchiveRange]);
            }

            var gridOptions = GridOptions ?? new GridOptions();
            await _repository.MoveLogsToArchiveAsync(from, to, gridOptions);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionDetail"></param>
        /// <returns></returns>
        // PUT: api/system/oplog
        [HttpPut]
        [Route(OperationLogApi.OperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.OperationLog, (int)OperationLogPermissions.Archive)]
        public async Task<IActionResult> PutSelectedLogsAsArchived(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            await _repository.MoveLogsToArchiveAsync(actionDetail.Items);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // POST: api/system/oplog
        [HttpPost]
        [Route(OperationLogApi.OperationLogsUrl)]
        public async Task<IActionResult> PostSelectedLogsAsRecovered(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequestResult(_strings[AppStrings.InvalidArchiveRange]);
            }

            await _repository.RecoverLogsFromArchive(from, to);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionDetail"></param>
        /// <returns></returns>
        // PUT: api/system/oplog/archive
        [HttpPut]
        [Route(OperationLogApi.OperationLogsArchiveUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutSelectedArchivedLogsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            await _repository.DeleteArchivedLogsAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #region System Log Operations

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/system/sys-oplog
        [HttpGet]
        [Route(OperationLogApi.SysOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.View)]
        public async Task<IActionResult> GetSysOperationLogsAsync()
        {
            var operationLogs = await _repository.GetSystemLogsAsync(GridOptions);
            Localize(operationLogs.Items);
            return JsonListResult(operationLogs);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/system/sys-oplog/archive
        [HttpGet]
        [Route(OperationLogApi.SysOperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.ViewArchive)]
        public async Task<IActionResult> GetSysOperationLogArchiveAsync()
        {
            var logArchive = await _repository.GetSystemLogsArchiveAsync(GridOptions);
            Localize(logArchive.Items);
            return JsonListResult(logArchive);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/system/sys-oplog/all
        [HttpGet]
        [Route(OperationLogApi.AllSysOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog,
            (int)(SysOperationLogPermissions.View | SysOperationLogPermissions.ViewArchive))]
        public async Task<IActionResult> GetAllSystemOperationLogsAsync()
        {
            var mergedLogs = await _repository.GetMergedSystemLogsAsync(GridOptions);
            Localize(mergedLogs.Items);
            return JsonListResult(mergedLogs);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // POST: api/system/sys-oplog/archive
        [HttpPost]
        [Route(OperationLogApi.SysOperationLogsArchiveUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.Archive)]
        public async Task<IActionResult> PostSelectedSysLogsAsArchived(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequestResult(_strings[AppStrings.InvalidArchiveRange]);
            }

            var gridOptions = GridOptions ?? new GridOptions();
            await _repository.MoveSystemLogsToArchiveAsync(from, to, gridOptions);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionDetail"></param>
        /// <returns></returns>
        // PUT: api/system/sys-oplog
        [HttpPut]
        [Route(OperationLogApi.SysOperationLogsUrl)]
        [AuthorizeRequest(SecureEntity.SysOperationLog, (int)SysOperationLogPermissions.Archive)]
        public async Task<IActionResult> PutSelectedSysLogsAsArchived(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            await _repository.MoveSystemLogsToArchiveAsync(actionDetail.Items);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        // POST: api/system/sys-oplog
        [HttpPost]
        [Route(OperationLogApi.SysOperationLogsUrl)]
        public async Task<IActionResult> PostSelectedSysLogsAsRecovered(DateTime from, DateTime to)
        {
            if (from == DateTime.MinValue || to == DateTime.MinValue)
            {
                return BadRequestResult(_strings[AppStrings.InvalidArchiveRange]);
            }

            await _repository.RecoverSystemLogsFromArchive(from, to);
            return Ok();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="actionDetail"></param>
        /// <returns></returns>
        // PUT: api/system/sys-oplog/archive
        [HttpPut]
        [Route(OperationLogApi.SysOperationLogsArchiveUrl)]
        [AuthorizeRequest]
        public async Task<IActionResult> PutSelectedArchivedSysLogsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            await _repository.DeleteArchivedSystemLogsAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        private void Localize(IList<OperationLogViewModel> logs)
        {
            foreach (var log in logs)
            {
                log.EntityTypeName = _strings[log.EntityTypeName ?? String.Empty];
                log.OperationName = _strings[log.OperationName];
                log.SourceListName = _strings[log.SourceListName ?? String.Empty];
                log.SourceName = _strings[log.SourceName ?? String.Empty];
                log.Description = _strings[log.Description ?? String.Empty];
            }
        }

        private readonly IOperationLogRepository _repository;
    }
}