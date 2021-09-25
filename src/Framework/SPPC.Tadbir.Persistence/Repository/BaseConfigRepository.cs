using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات پایه مورد نیاز برای ذخیره و بازیابی تنظیمات را پیاده سازی می کند
    /// </summary>
    public class BaseConfigRepository : EntityLoggingRepository<Setting, SettingBriefViewModel>, IBaseConfigRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی و سیستمی را در برنامه فراهم می کند</param>
        public BaseConfigRepository(IRepositoryContext context, IOperationLogRepository log)
            : base(context, log)
        {
        }

        /// <summary>
        /// تمام تنظیمات موجود برای برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از تمام تنظیمات موجود برای برنامه</returns>
        public async Task<IList<SettingBriefViewModel>> GetAllConfigAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Setting>();
            var allConfig = await repository
                .GetEntityQuery()
                .Where(cfg => cfg.IsStandalone
                    && !(cfg.Type == (short)ConfigType.User && cfg.ScopeType < (short)ConfigScopeType.Entity))
                .Select(cfg => Mapper.Map<SettingBriefViewModel>(cfg))
                .ToListAsync();
            SetDefaultCalendar(allConfig);
            await ReadAsync(null);
            return allConfig;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت داده شده برای تنظیمات را ذخیره می کند
        /// </summary>
        /// <param name="configItems">مجموعه ای از تنظیمات اصلاح شده</param>
        public async Task SaveConfigAsync(IList<SettingBriefViewModel> configItems)
        {
            Verify.ArgumentNotNull(configItems, "configItems");
            var repository = UnitOfWork.GetAsyncRepository<Setting>();
            var modifiedIds = configItems.Select(cfg => cfg.Id);
            var modified = await repository
                .GetEntityWithTrackingQuery()
                .Where(cfg => modifiedIds.Contains(cfg.Id))
                .ToListAsync();
            Array.ForEach(modified.ToArray(), cfg =>
            {
                var configItem = configItems
                    .Where(item => item.Id == cfg.Id)
                    .Single();
                cfg.Values = JsonHelper.From(configItem.Values, false);
                repository.Update(cfg);
            });
            await UnitOfWork.CommitAsync();
            OnEntityAction(OperationId.Save);
            await TrySaveLogAsync();
        }

        /// <summary>
        /// اطلاعات تنظیمات مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="settingId">شناسه دیتابیسی تنظیمات مورد نظر</param>
        /// <returns>اطلاعات نمایشی برای تنظیمات مورد نظر</returns>
        public async Task<SettingBriefViewModel> GetConfigByIdAsync(int settingId)
        {
            var config = default(SettingBriefViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Setting>();
            var configById = await repository.GetByIDAsync(settingId);
            if (configById != null)
            {
                config = Mapper.Map<SettingBriefViewModel>(configById);
            }

            return config;
        }

        /// <summary>
        /// تنظیمات موجود برای کلاس تنظیمات مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TConfig">نوع تنظیمات مورد نیاز</typeparam>
        /// <returns>تنظیمات موجود برای کلاس تنظیمات مشخص شده</returns>
        public async Task<TConfig> GetConfigByTypeAsync<TConfig>()
        {
            var configByType = default(TConfig);
            var repository = UnitOfWork.GetAsyncRepository<Setting>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(TConfig).Name);
            var config = configItems.SingleOrDefault();
            if (config != null)
            {
                configByType = Mapper.Map<TConfig>(config);
            }

            return configByType;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Setting; }
        }

        private void SetDefaultCalendar(List<SettingBriefViewModel> allConfig)
        {
            var sysConfig = allConfig
                .Where(cfg => cfg.ModelType == typeof(SystemConfig).Name)
                .FirstOrDefault();
            if (sysConfig != null)
            {
                var sysConfigValues = sysConfig.Values as SystemConfig;
                sysConfigValues.DefaultCalendar = sysConfigValues.DefaultCalendars
                    .Where(cfg => cfg.Language == UserContext.Language)
                    .Select(cfg => cfg.Calendar)
                    .Single();

                var sysConfigDefValues = sysConfig.DefaultValues as SystemConfig;
                sysConfigDefValues.DefaultCalendar = sysConfigDefValues.DefaultCalendars
                    .Where(cfg => cfg.Language == UserContext.Language)
                    .Select(cfg => cfg.Calendar)
                    .Single();
            }
        }
    }
}
