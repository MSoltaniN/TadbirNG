using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت تنظیمات لاگ های عملیاتی را پیاده سازی می کند
    /// </summary>
    public class LogConfigRepository : RepositoryBase, ILogConfigRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public LogConfigRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تنظیمات لاگ های عملیاتی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه تنظیمات تعریف شده برای لاگ های عملیاتی</returns>
        public async Task<IList<LogSettingNodeViewModel>> GetAllConfigAsync()
        {
            var allConfig = new List<LogSettingNodeViewModel>();
            var repository = UnitOfWork.GetAsyncRepository<LogSetting>();
            var all = await repository.GetAllAsync(cfg => cfg.EntityType, cfg => cfg.Operation,
                cfg => cfg.Source, cfg => cfg.SourceType, cfg => cfg.Subsystem);

            int id = 1;
            foreach (var bySubsys in all
                .OrderBy(cfg => cfg.Subsystem.Id)
                .GroupBy(cfg => cfg.Subsystem.Id))
            {
                var first = bySubsys.First();
                var subsystem = new LogSettingNodeViewModel()
                {
                    Id = id++,
                    Name = first.Subsystem.Name,
                    ParentId = null
                };
                allConfig.Add(subsystem);
                foreach (var bySourceType in bySubsys
                    .OrderBy(cfg => cfg.SourceType.Id)
                    .GroupBy(cfg => cfg.SourceType.Id))
                {
                    first = bySourceType.First();
                    var sourceType = new LogSettingNodeViewModel()
                    {
                        Id = id++,
                        Name = first.SourceType.Name,
                        ParentId = subsystem.Id
                    };
                    allConfig.Add(sourceType);
                    foreach (var byEntity in bySourceType
                        .Where(cfg => cfg.EntityType != null)
                        .GroupBy(cfg => cfg.EntityType.Id))
                    {
                        first = byEntity.First();
                        var entityType = new LogSettingNodeViewModel()
                        {
                            Id = id++,
                            Name = first.EntityType.Name,
                            ParentId = sourceType.Id
                        };
                        entityType.Items.AddRange(byEntity
                            .OrderBy(cfg => cfg.Operation.Id)
                            .Select(cfg => Mapper.Map<LogSettingItemViewModel>(cfg)));
                        allConfig.Add(entityType);
                    }

                    foreach (var bySource in bySourceType
                        .Where(cfg => cfg.Source != null)
                        .GroupBy(cfg => cfg.Source.Id))
                    {
                        first = bySource.First();
                        var source = new LogSettingNodeViewModel()
                        {
                            Id = id++,
                            Name = first.Source.Name,
                            ParentId = sourceType.Id
                        };
                        source.Items.AddRange(bySource
                            .OrderBy(cfg => cfg.Operation.Id)
                            .Select(cfg => Mapper.Map<LogSettingItemViewModel>(cfg)));
                        allConfig.Add(source);
                    }
                }
            }

            return allConfig;
        }

        /// <summary>
        /// به روش آسنکرون، کلیه تنظیمات لاگ های سیستمی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه تنظیمات تعریف شده برای لاگ های سیستمی</returns>
        public async Task<IList<LogSettingNodeViewModel>> GetAllSystemConfigAsync()
        {
            var allConfig = new List<LogSettingNodeViewModel>();
            UnitOfWork.UseSystemContext();

            var repository = UnitOfWork.GetAsyncRepository<SysLogSetting>();
            var all = await repository.GetAllAsync(cfg => cfg.EntityType, cfg => cfg.Operation,
                cfg => cfg.Source);

            int id = 1;
            var rootNode = new LogSettingNodeViewModel()
            {
                Id = id++,
                Name = AppStrings.SystemLog,
                ParentId = null
            };
            allConfig.Add(rootNode);

            foreach (var byEntity in all
                .Where(cfg => cfg.EntityType != null)
                .GroupBy(cfg => cfg.EntityType.Id))
            {
                var first = byEntity.First();
                var entityType = new LogSettingNodeViewModel()
                {
                    Id = id++,
                    Name = first.EntityType.Name,
                    ParentId = rootNode.Id
                };
                entityType.Items.AddRange(byEntity
                    .Select(cfg => Mapper.Map<LogSettingItemViewModel>(cfg)));
                allConfig.Add(entityType);
            }

            foreach (var bySource in all
                .Where(cfg => cfg.Source != null)
                .GroupBy(cfg => cfg.Source.Id))
            {
                var first = bySource.First();
                var source = new LogSettingNodeViewModel()
                {
                    Id = id++,
                    Name = first.Source.Name,
                    ParentId = rootNode.Id
                };
                source.Items.AddRange(bySource
                    .Select(cfg => Mapper.Map<LogSettingItemViewModel>(cfg)));
                allConfig.Add(source);
            }

            UnitOfWork.UseCompanyContext();
            return allConfig;
        }

        /// <summary>
        /// به روش آسنکرون، تغییرات داده شده برای تنظیمات لاگ های عملیاتی را ذخیره می کند
        /// </summary>
        /// <param name="modified">تنظیمات تغییر یافته مورد نظر برای ذخیره</param>
        public async Task SaveModifiedConfigAsync(IList<LogSettingItemViewModel> modified)
        {
            var repository = UnitOfWork.GetAsyncRepository<LogSetting>();
            var modifiedConfig = await repository
                .GetEntityQuery()
                .Where(cfg => modified
                    .Select(item => item.Id)
                    .Contains(cfg.Id))
                .ToListAsync();
            foreach (var config in modifiedConfig)
            {
                var modifiedItem = modified
                    .Where(cfg => cfg.Id == config.Id)
                    .Single();
                config.IsEnabled = modifiedItem.IsEnabled;
                repository.Update(config);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تغییرات داده شده برای تنظیمات لاگ های سیستمی را ذخیره می کند
        /// </summary>
        /// <param name="modified">تنظیمات تغییر یافته مورد نظر برای ذخیره</param>
        public async Task SaveModifiedSystemConfigAsync(IList<LogSettingItemViewModel> modified)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysLogSetting>();
            var modifiedConfig = await repository
                .GetEntityQuery()
                .Where(cfg => modified
                    .Select(item => item.Id)
                    .Contains(cfg.Id))
                .ToListAsync();
            foreach (var config in modifiedConfig)
            {
                var modifiedItem = modified
                    .Where(cfg => cfg.Id == config.Id)
                    .Single();
                config.IsEnabled = modifiedItem.IsEnabled;
                repository.Update(config);
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseCompanyContext();
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات جاری ایجاد لاگ عملیاتی را برای رکورد لاگ داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="log">رکورد لاگ عملیات جاری که مشخصات عملیات موجودیت یا فرم عملیاتی را مشخص می کند</param>
        /// <returns>تنظیمات جاری برای ایجاد لاگ</returns>
        public async Task<LogSettingViewModel> GetLogConfigAsync(OperationLogViewModel log)
        {
            Verify.ArgumentNotNull(log, nameof(log));
            Expression<Func<LogSetting, bool>> criteria = null;
            if (log.EntityTypeId != null)
            {
                criteria = cfg => (cfg.Operation.Id == log.OperationId)
                    && (cfg.EntityType.Id == log.EntityTypeId);
            }
            else
            {
                criteria = cfg => (cfg.Operation.Id == log.OperationId)
                    && (cfg.Source.Id == log.SourceId);
            }

            var configResult = default(LogSettingViewModel);
            var repository = UnitOfWork.GetAsyncRepository<LogSetting>();
            var config = await repository.GetSingleByCriteriaAsync(criteria);
            if (config != null)
            {
                configResult = Mapper.Map<LogSettingViewModel>(config);
            }

            return configResult;
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات جاری ایجاد لاگ سیستمی را برای رکورد لاگ داده شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="log">رکورد لاگ عملیات جاری که مشخصات عملیات موجودیت یا فرم عملیاتی را مشخص می کند</param>
        /// <returns>تنظیمات جاری برای ایجاد لاگ</returns>
        public async Task<LogSettingViewModel> GetSystemLogConfigAsync(OperationLogViewModel log)
        {
            Verify.ArgumentNotNull(log, nameof(log));
            Expression<Func<SysLogSetting, bool>> criteria = null;
            if (log.EntityTypeId != null)
            {
                criteria = cfg => (cfg.OperationId == log.OperationId)
                    && (cfg.EntityTypeId == log.EntityTypeId);
            }
            else
            {
                criteria = cfg => (cfg.OperationId == log.OperationId)
                    && (cfg.SourceId == log.SourceId);
            }

            var configResult = default(LogSettingViewModel);
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<SysLogSetting>();
            var config = await repository.GetSingleByCriteriaAsync(criteria);
            if (config != null)
            {
                configResult = Mapper.Map<LogSettingViewModel>(config);
            }

            UnitOfWork.UseCompanyContext();
            return configResult;
        }
    }
}
