using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Core;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت لاگ های عملیاتی برنامه را پیاده سازی می کند
    /// </summary>
    public class OperationLogRepository : RepositoryBase, IOperationLogRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات جاری ایجاد لاگ را فراهم می کند</param>
        /// <param name="utility"></param>
        public OperationLogRepository(IRepositoryContext context, ILogConfigRepository config,
            IReportDirectUtility utility)
            : base(context)
        {
            _config = config;
            _utility = utility;
        }

        #region Company Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیاتی موجود را برای شرکت و کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های عملیاتی موجود</returns>
        public async Task<PagedList<OperationLogViewModel>> GetLogsAsync(GridOptions gridOptions)
        {
            return await FetchLogsFromSource(gridOptions, "OperationLog");
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های شرکتی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی بایگانی شده</returns>
        public async Task<PagedList<OperationLogViewModel>> GetLogsArchiveAsync(GridOptions gridOptions)
        {
            return await FetchLogsFromSource(gridOptions, "OperationLogArchive");
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات شرکتی موجود را به همراه لاگ های بایگانی شده
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی موجود و لاگ های بایگانی شده</returns>
        public async Task<PagedList<OperationLogViewModel>> GetMergedLogsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            IEnumerable<OperationLogViewModel> merged = new List<OperationLogViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var active = await GetLogsAsync(gridOptions);
                var archived = await GetLogsArchiveAsync(gridOptions);
                merged = active.Items
                    .Concat(archived.Items);
                if (gridOptions.SortColumns.Count == 0)
                {
                    merged = merged
                        .OrderByDescending(log => log.Date)
                        .ThenByDescending(log => log.Time);
                }
            }

            await LogOperationAsync<OperationLog>((int)EntityTypeId.OperationLog, gridOptions);
            return new PagedList<OperationLogViewModel>(merged, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات داده شده برای یک لاگ عملیاتی جدید را ذخیره می کند
        /// </summary>
        /// <param name="operationLog">اطلاعات لاگ عملیاتی جدید</param>
        public async Task SaveLogAsync(OperationLogViewModel operationLog)
        {
            Verify.ArgumentNotNull(operationLog, nameof(operationLog));
            var config = await _config.GetLogConfigAsync(operationLog);
            if (config != null && config.IsEnabled)
            {
                var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
                var newLog = Mapper.Map<OperationLog>(operationLog);
                repository.Insert(newLog);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای شرکتی ثبت شده در محدوده تاریخی داده شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بایگانی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بایگانی</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        public async Task MoveLogsToArchiveAsync(DateTime from, DateTime to, GridOptions gridOptions)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var logs = await repository.GetByCriteriaAsync(
                log => log.Date.Date >= from.Date && log.Date.Date <= to.Date);
            logs = logs
                .Apply(gridOptions, false)
                .ToList();
            foreach (var log in logs)
            {
                var archive = Mapper.Map<OperationLogArchive>(log);
                archiveRepository.Insert(archive);
                repository.Delete(log);
            }

            await UnitOfWork.CommitAsync();
            await LogOperationAsync<OperationLog>((int)EntityTypeId.OperationLog, OperationId.Archive);
        }

        /// <summary>
        /// به روش آسنکرون، رویدادهای شرکتی انتخاب شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="archivedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای بایگانی</param>
        public async Task MoveLogsToArchiveAsync(IEnumerable<int> archivedIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var archived = await repository.GetByCriteriaAsync(ar => archivedIds.Contains(ar.Id));
            foreach (var item in archived)
            {
                var archive = Mapper.Map<OperationLogArchive>(item);
                archiveRepository.Insert(archive);
                repository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
            await LogOperationAsync<OperationLog>((int)EntityTypeId.OperationLog, OperationId.Archive);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های شرکتی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        public async Task RecoverLogsFromArchive(DateTime from, DateTime to)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var archived = await archiveRepository.GetByCriteriaAsync(
                log => log.Date.Date >= from.Date && log.Date.Date <= to.Date);
            foreach (var item in archived)
            {
                var log = Mapper.Map<OperationLog>(item);
                log.Id = 0;
                repository.Insert(log);
                archiveRepository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// مجموعه ای از لاگ های شرکتی بایگانی شده را به صورت گروهی حذف می کند
        /// </summary>
        /// <param name="deletedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای حذف</param>
        public async Task DeleteArchivedLogsAsync(IEnumerable<int> deletedIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var archived = await repository.GetByCriteriaAsync(ar => deletedIds.Contains(ar.Id));
            foreach (var item in archived)
            {
                repository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
            await LogOperationAsync<OperationLog>((int)EntityTypeId.OperationLog, OperationId.Delete);
        }

        #endregion

        #region System Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات سیستمی موجود را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی موجود</returns>
        public async Task<PagedList<OperationLogViewModel>> GetSystemLogsAsync(GridOptions gridOptions)
        {
            return await FetchLogsFromSource(gridOptions, "SysOperationLog", true);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های سیستمی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی بایگانی شده</returns>
        public async Task<PagedList<OperationLogViewModel>> GetSystemLogsArchiveAsync(GridOptions gridOptions)
        {
            return await FetchLogsFromSource(gridOptions, "SysOperationLogArchive", true);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات سیستمی موجود را به همراه لاگ های بایگانی شده
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی موجود و لاگ های بایگانی شده</returns>
        public async Task<PagedList<OperationLogViewModel>> GetMergedSystemLogsAsync(GridOptions gridOptions)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            IEnumerable<OperationLogViewModel> merged = new List<OperationLogViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var active = await GetSystemLogsAsync(gridOptions);
                var archived = await GetSystemLogsArchiveAsync(gridOptions);
                merged = active.Items
                    .Concat(archived.Items);
                if (gridOptions.SortColumns.Count == 0)
                {
                    merged = merged
                        .OrderByDescending(log => log.Date)
                        .ThenByDescending(log => log.Time);
                }
            }

            await LogOperationAsync<SysOperationLog>((int)SysEntityTypeId.SysOperationLog, gridOptions);
            return new PagedList<OperationLogViewModel>(merged, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات داده شده برای یک لاگ عملیات سیستمی جدید را ذخیره می کند
        /// </summary>
        /// <param name="operationLog">اطلاعات لاگ عملیاتی جدید</param>
        public async Task SaveSystemLogAsync(OperationLogViewModel operationLog)
        {
            Verify.ArgumentNotNull(operationLog, nameof(operationLog));
            var config = await _config.GetSystemLogConfigAsync(operationLog);
            if (config != null && config.IsEnabled)
            {
                UnitOfWork.UseSystemContext();
                var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
                var newLog = Mapper.Map<SysOperationLog>(operationLog);
                repository.Insert(newLog);
                await UnitOfWork.CommitAsync();
                UnitOfWork.UseCompanyContext();
            }
        }

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای سیستمی ثبت شده در محدوده تاریخی داده شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بایگانی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بایگانی</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        public async Task MoveSystemLogsToArchiveAsync(DateTime from, DateTime to, GridOptions gridOptions)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var logs = await repository.GetByCriteriaAsync(
                log => log.Date.Date >= from.Date && log.Date.Date <= to.Date);
            logs = logs
                .Apply(gridOptions, false)
                .ToList();
            foreach (var log in logs)
            {
                var archive = Mapper.Map<SysOperationLogArchive>(log);
                archiveRepository.Insert(archive);
                repository.Delete(log);
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
            await LogOperationAsync<SysOperationLog>((int)SysEntityTypeId.SysOperationLog, OperationId.Archive);
        }

        /// <summary>
        /// به روش آسنکرون، رویدادهای سیستمی انتخاب شده را به بایگانی منتقل می کند
        /// </summary>
        /// <param name="archivedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای بایگانی</param>
        public async Task MoveSystemLogsToArchiveAsync(IEnumerable<int> archivedIds)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var archived = await repository.GetByCriteriaAsync(ar => archivedIds.Contains(ar.Id));
            foreach (var item in archived)
            {
                var archive = Mapper.Map<SysOperationLogArchive>(item);
                archiveRepository.Insert(archive);
                repository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
            await LogOperationAsync<SysOperationLog>((int)SysEntityTypeId.SysOperationLog, OperationId.Archive);
        }

        /// <summary>
        /// به روش آسنکرون، کلیه رویدادهای بایگانی شده در محدوده تاریخی داده شده را
        /// در لاگ های سیستمی بازیابی می کند
        /// </summary>
        /// <param name="from">ابتدای محدوده تاریخی برای بازیابی</param>
        /// <param name="to">انتهای محدوده تاریخی برای بازیابی</param>
        public async Task RecoverSystemLogsFromArchive(DateTime from, DateTime to)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var archiveRepository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var archived = await archiveRepository.GetByCriteriaAsync(
                log => log.Date.Date >= from.Date && log.Date.Date <= to.Date);
            foreach (var item in archived)
            {
                var log = Mapper.Map<SysOperationLog>(item);
                log.Id = 0;
                repository.Insert(log);
                archiveRepository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// مجموعه ای از لاگ های سیستمی بایگانی شده را به صورت گروهی حذف می کند
        /// </summary>
        /// <param name="deletedIds">مجموعه شناسه های دیتابیسی رکوردهای انتخاب شده برای حذف</param>
        public async Task DeleteArchivedSystemLogsAsync(IEnumerable<int> deletedIds)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var archived = await repository.GetByCriteriaAsync(ar => deletedIds.Contains(ar.Id));
            foreach (var item in archived)
            {
                repository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
            await LogOperationAsync<SysOperationLog>((int)SysEntityTypeId.SysOperationLog, OperationId.Delete);
        }

        #endregion

        private static string GetColumnSorting(GridOptions gridOptions)
        {
            string sorting = DefaultSorting;
            if (gridOptions.SortColumns.Count > 0)
            {
                sorting = String.Join(", ", gridOptions.SortColumns.Select(col => col.ToString()));
            }

            return sorting;
        }

        private async Task<PagedList<OperationLogViewModel>> FetchLogsFromSource(
            GridOptions gridOptions, string source, bool isSystem = false)
        {
            var options = gridOptions ?? new GridOptions() { ListChanged = false };
            var logs = new List<OperationLogViewModel>();
            if (options.Operation != (int)OperationId.Print)
            {
                logs.AddRange(GetLogItems(options, source, isSystem));
                Array.ForEach(logs.ToArray(), log => Localize(log));
                if (!isSystem)
                {
                    SetSystemValues(logs);
                }
            }

            var pagedList = new PagedList<OperationLogViewModel>(logs, options);
            await LogOperationAsync<OperationLog>((int)EntityTypeId.OperationLog, options);
            return pagedList;
        }

        private async Task LogOperationAsync<TModel>(int entity, GridOptions gridOptions)
            where TModel : class, IEntity
        {
            if (gridOptions == null)
            {
                return;
            }

            if (gridOptions.ListChanged)
            {
                await LogOperationAsync<TModel>(entity, (OperationId)gridOptions.Operation);
            }
        }

        private async Task LogOperationAsync<TModel>(int entity, OperationId operation)
            where TModel : class, IEntity
        {
            var log = new OperationLogViewModel()
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay,
                CompanyId = UserContext.CompanyId,
                BranchId = UserContext.BranchId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                EntityTypeId = entity,
                OperationId = (int)operation,
                UserId = UserContext.Id
            };
            if (typeof(TModel) == typeof(OperationLog))
            {
                await SaveLogAsync(log);
            }
            else
            {
                await SaveSystemLogAsync(log);
            }
        }

        private void SetSystemValues(List<OperationLogViewModel> logs)
        {
            if (!logs.Any())
            {
                return;
            }

            var ids = logs
                .Select(log => log.UserId)
                .Distinct();
            string query = String.Format(LogQuery.UserLookupQuery, String.Join(", ", ids));
            DbConsole.ConnectionString = DbConsole.BuildConnectionString("NGTadbirSys");
            var result = DbConsole.ExecuteQuery(query);
            var lookup = new Dictionary<int, string>();
            Array.ForEach(result.Rows
                .Cast<DataRow>()
                .Select(row => new KeyValuePair<int, string>(
                    _utility.ValueOrDefault<int>(row, "UserID"),
                    _utility.ValueOrDefault(row, "UserName")))
                .ToArray(), item => lookup[item.Key] = item.Value);
            Array.ForEach(logs.ToArray(), log => log.UserName = lookup[log.UserId.Value]);

            ids = logs
                .Select(log => log.CompanyId)
                .Distinct();
            query = String.Format(LogQuery.CompanyLookupQuery, String.Join(", ", ids));
            result = DbConsole.ExecuteQuery(query);
            lookup = new Dictionary<int, string>();
            Array.ForEach(result.Rows
                .Cast<DataRow>()
                .Select(row => new KeyValuePair<int, string>(
                    _utility.ValueOrDefault<int>(row, "CompanyID"),
                    _utility.ValueOrDefault(row, "Name")))
                .ToArray(), item => lookup[item.Key] = item.Value);
            Array.ForEach(logs.ToArray(), log => log.CompanyName = lookup[log.CompanyId.Value]);
        }

        private List<OperationLogViewModel> GetLogItems(
            GridOptions gridOptions, string table, bool isSystem = false)
        {
            string connection = isSystem
                ? DbConsole.BuildConnectionString("NGTadbirSys")
                : UnitOfWork.CompanyConnection;
            string queryName = isSystem ? LogQuery.SysOperationLogQuery : LogQuery.OperationLogQuery;

            DbConsole.ConnectionString = connection;
            string listQuery = String.Format(queryName, table, GetColumnSorting(gridOptions));
            var query = new ReportQuery(listQuery);
            var result = DbConsole.ExecuteQuery(query.Query);
            var logs = new List<OperationLogViewModel>();
            logs.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => GetLogItem(row)));
            return logs;
        }

        private OperationLogViewModel GetLogItem(DataRow row)
        {
            var voucherItem = new OperationLogViewModel()
            {
                Id = _utility.ValueOrDefault<int>(row, "Id"),
                UserName = _utility.ValueOrDefault(row, "UserName"),
                CompanyName = _utility.ValueOrDefault(row, "CompanyName"),
                BranchName = _utility.ValueOrDefault(row, "BranchName"),
                Date = _utility.ValueOrDefault<DateTime>(row, "Date"),
                Description = _utility.ValueOrDefault(row, "Description"),
                EntityAssociation = _utility.ValueOrDefault(row, "EntityAssociation"),
                EntityCode = _utility.ValueOrDefault(row, "EntityCode"),
                EntityDate = _utility.ValueOrDefault<DateTime>(row, "EntityDate"),
                EntityDescription = _utility.ValueOrDefault(row, "EntityDescription"),
                EntityName = _utility.ValueOrDefault(row, "EntityName"),
                EntityNo = _utility.ValueOrDefault<int>(row, "EntityNo"),
                EntityReference = _utility.ValueOrDefault(row, "EntityReference"),
                EntityTypeId = _utility.ValueOrDefault<int>(row, "EntityTypeID"),
                EntityTypeName = _utility.ValueOrDefault(row, "EntityTypeName"),
                FiscalPeriodName = _utility.ValueOrDefault(row, "FiscalPeriodName"),
                OperationName = _utility.ValueOrDefault(row, "OperationName"),
                SourceListName = _utility.ValueOrDefault(row, "SourceListName"),
                SourceName = _utility.ValueOrDefault(row, "SourceName"),
                Time = _utility.ValueOrDefault<TimeSpan>(row, "Time"),
                UserId = _utility.ValueOrDefault<int>(row, "UserID"),
                CompanyId = _utility.ValueOrDefault<int>(row, "CompanyID"),
                BranchId = _utility.ValueOrDefault<int>(row, "BranchID"),
                FiscalPeriodId = _utility.ValueOrDefault<int>(row, "FiscalPeriodID"),
                SourceId = _utility.ValueOrDefault<int>(row,"SourceID")
            };
            if (voucherItem.EntityDate == DateTime.MinValue)
            {
                voucherItem.EntityDate = null;
            }

            return voucherItem;
        }

        private void Localize(OperationLogViewModel log)
        {
            log.EntityTypeName = Context.Localize(log.EntityTypeName);
            log.OperationName = Context.Localize(log.OperationName);
            log.SourceListName = Context.Localize(log.SourceListName);
            log.SourceName = Context.Localize(log.SourceName);
            log.Description = Context.Localize(log.Description);
        }

        private const string DefaultSorting = "oplog.Date DESC, oplog.Time DESC";
        private readonly ILogConfigRepository _config;
        private readonly IReportDirectUtility _utility;
    }
}
