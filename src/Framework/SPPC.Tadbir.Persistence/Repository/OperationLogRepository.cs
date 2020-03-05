﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Domain;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Core;
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
        public OperationLogRepository(IRepositoryContext context, ILogConfigRepository config)
            : base(context)
        {
            _config = config;
        }

        #region Company Log Operations

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیاتی موجود را برای شرکت و کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های عملیاتی موجود</returns>
        public async Task<IList<OperationLogViewModel>> GetLogsAsync(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var list = await repository.GetEntityQuery(
                log => log.Branch, log => log.EntityType, log => log.FiscalPeriod,
                log => log.Operation, log => log.Source, log => log.SourceList)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .ToListAsync();
            list = list.Apply(options).ToList();
            foreach (var item in list)
            {
                await SetSystemValues(item);
            }

            await LogOperationAsync<OperationLog>((int)EntityTypeId.OperationLog, OperationId.View);
            return list;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های شرکتی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی بایگانی شده</returns>
        public async Task<IList<OperationLogViewModel>> GetLogsArchiveAsync(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var repository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var list = await repository.GetEntityQuery(
                log => log.Branch, log => log.EntityType, log => log.FiscalPeriod,
                log => log.Operation, log => log.Source, log => log.SourceList)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .ToListAsync();
            list = list.Apply(options).ToList();
            foreach (var item in list)
            {
                await SetSystemValues(item);
            }

            await LogOperationAsync<OperationLog>((int)EntityTypeId.OperationLog, OperationId.ViewArchive);
            return list;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات شرکتی موجود را به همراه لاگ های بایگانی شده
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های شرکتی موجود و لاگ های بایگانی شده</returns>
        public async Task<IList<OperationLogViewModel>> GetMergedLogsAsync(GridOptions gridOptions = null)
        {
            var active = await GetLogsAsync(null);
            var archived = await GetLogsArchiveAsync(null);
            return active
                .Concat(archived)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های عملیاتی</returns>
        public async Task<int> GetLogCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLog>();
            var items = await repository.GetAllAsync();
            return items
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های شرکتی بایگانی شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های شرکتی بایگانی شده</returns>
        public async Task<int> GetLogArchiveCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<OperationLogArchive>();
            var items = await repository.GetAllAsync();
            return items
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ عملیات شرکتی و بایگانی شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های شرکتی و بایگانی شده</returns>
        public async Task<int> GetMergedLogCountAsync(GridOptions gridOptions = null)
        {
            int activeCount = await GetLogCountAsync(gridOptions);
            int archiveCount = await GetLogArchiveCountAsync(gridOptions);
            return activeCount + archiveCount;
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
            var logs = await repository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
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
            var archived = await archiveRepository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
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
        public async Task<IList<OperationLogViewModel>> GetSystemLogsAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            var list = await repository.GetEntityQuery(
                    log => log.Operation, log => log.EntityType,
                    log => log.Source, log => log.SourceList,
                    log => log.Company, log => log.User)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions)
                .ToListAsync();
            UnitOfWork.UseCompanyContext();

            await LogOperationAsync<SysOperationLog>((int)SysEntityTypeId.SysOperationLog, OperationId.View);
            return list;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های سیستمی بایگانی شده را  خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی بایگانی شده</returns>
        public async Task<IList<OperationLogViewModel>> GetSystemLogsArchiveAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            var list = await repository.GetEntityQuery(
                    log => log.Operation, log => log.EntityType,
                    log => log.Source, log => log.SourceList,
                    log => log.Company, log => log.User)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions)
                .ToListAsync();
            UnitOfWork.UseCompanyContext();

            await LogOperationAsync<SysOperationLog>((int)SysEntityTypeId.SysOperationLog, OperationId.ViewArchive);
            return list;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه لاگ های عملیات سیستمی موجود را به همراه لاگ های بایگانی شده
        /// خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه لاگ های سیستمی موجود و لاگ های بایگانی شده</returns>
        public async Task<IList<OperationLogViewModel>> GetMergedSystemLogsAsync(GridOptions gridOptions = null)
        {
            var active = await GetSystemLogsAsync(null);
            var archived = await GetSystemLogsArchiveAsync(null);
            return active
                .Concat(archived)
                .OrderByDescending(log => log.Date)
                .ThenByDescending(log => log.Time)
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های عملیات سیستمی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های سیستمی</returns>
        public async Task<int> GetSystemLogCountAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLog>();
            int count = await repository.GetEntityQuery()
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .CountAsync();
            UnitOfWork.UseCompanyContext();
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ های سیستمی بایگانی شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های سیستمی بایگانی شده</returns>
        public async Task<int> GetSystemLogArchiveCountAsync(GridOptions gridOptions = null)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysOperationLogArchive>();
            int count = await repository.GetEntityQuery()
                .Select(log => Mapper.Map<OperationLogViewModel>(log))
                .Apply(gridOptions, false)
                .CountAsync();
            UnitOfWork.UseCompanyContext();
            return count;
        }

        /// <summary>
        /// به روش آسنکرون، تعداد سطرهای لاگ عملیات سیستمی و بایگانی شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد سطرهای لاگ های سیستمی و بایگانی شده</returns>
        public async Task<int> GetMergedSystemLogCountAsync(GridOptions gridOptions = null)
        {
            int activeCount = await GetSystemLogCountAsync(gridOptions);
            int archiveCount = await GetSystemLogArchiveCountAsync(gridOptions);
            return activeCount + archiveCount;
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
            var logs = await repository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
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
            var archived = await archiveRepository.GetByCriteriaAsync(log => log.Date.Date.IsBetween(from, to));
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

        private async Task SetSystemValues(OperationLogViewModel log)
        {
            UnitOfWork.UseSystemContext();
            var userRepository = UnitOfWork.GetAsyncRepository<User>();
            var user = await userRepository.GetByIDAsync(log.UserId ?? 0);
            log.UserName = user?.UserName;
            var companyRepository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await companyRepository.GetByIDAsync(log.CompanyId ?? 0);
            log.CompanyName = company?.Name;
            UnitOfWork.UseCompanyContext();
        }

        private readonly ILogConfigRepository _config;
    }
}
