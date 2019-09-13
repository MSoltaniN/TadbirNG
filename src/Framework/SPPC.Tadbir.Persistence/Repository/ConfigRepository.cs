using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.ViewModel.Config;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات برنامه را پیاده سازی می کند
    /// </summary>
    public class ConfigRepository : RepositoryBase, IConfigRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="fiscalRepository">امکان کار با اطلاعات دوره مالی را فراهم می کند</param>
        public ConfigRepository(IRepositoryContext context, IFiscalPeriodRepository fiscalRepository)
            : base(context)
        {
            _fiscalRepository = fiscalRepository;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// تمام تنظیمات موجود برای برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از تمام تنظیمات موجود برای برنامه</returns>
        public async Task<IList<SettingBriefViewModel>> GetAllConfigAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Setting>();
            var allConfig = await repository
                .GetAllAsync();
            return allConfig
                .Where(cfg => !(cfg.Type == 3 && cfg.ScopeType == 2))
                .Select(cfg => Mapper.Map<SettingBriefViewModel>(cfg))
                .ToList();
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

        /// <summary>
        /// محدوده تاریخی پیش فرض را با توجه به دوره مالی جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="start">پارامتر خروجی برای تنظیم تاریخ ابتدا در محدوده تاریخی پیش فرض</param>
        /// <param name="end">پارامتر خروجی برای تنظیم تاریخ انتها در محدوده تاریخی پیش فرض</param>
        public void GetCurrentFiscalDateRange(out DateTime start, out DateTime end)
        {
            var config = GetConfigByTypeAsync<DateRangeConfig>().Result;
            Verify.ArgumentNotNull(UserContext);
            if (config.DefaultDateRange == DateRangeOptions.CurrentToCurrent)
            {
                start = DateTime.Now;
                end = DateTime.Now;
            }
            else
            {
                var fp = _fiscalRepository.GetFiscalPeriodAsync(UserContext.FiscalPeriodId).Result;
                start = fp.StartDate;
                end = (config.DefaultDateRange == DateRangeOptions.FiscalStartToCurrent)
                    ? DateTime.Now
                    : fp.EndDate;
            }
        }

        /// <summary>
        /// تمام تنظیمات کاربری موجود برای فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>تنظیمات کاربری موجود برای فرم های لیستی</returns>
        public async Task<IList<ListFormViewConfig>> GetListViewConfigByUserAsync(int userId)
        {
            IList<ListFormViewConfig> configList = new List<ListFormViewConfig>();
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var viewRepository = UnitOfWork.GetAsyncRepository<View>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(ListFormViewConfig).Name
                    && cfg.User.Id == userId);
            var userViewIds = configItems.Select(cfg => cfg.ViewId.Value);
            var entityViews = await viewRepository.GetByCriteriaAsync(
                ev => !userViewIds.Contains(ev.Id),
                ev => ev.Columns);
            foreach (var view in entityViews)
            {
                var userConfig = new ListFormViewConfig() { ViewId = view.Id };
                foreach (var column in view.Columns)
                {
                    userConfig.ColumnViews.Add(Mapper.Map<ColumnViewConfig>(column));
                }

                configList.Add(userConfig);
            }

            foreach (var config in configItems)
            {
                configList.Add(Mapper.Map<ListFormViewConfig>(config));
            }

            return configList;
        }

        /// <summary>
        /// تنظیمات کاربری موجود برای یکی از فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات کاربری موجود برای یکی از فرم های لیستی</returns>
        public async Task<ListFormViewConfig> GetListViewConfigByUserAsync(int userId, int viewId)
        {
            var userConfig = default(ListFormViewConfig);
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var items = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(ListFormViewConfig).Name
                    && cfg.User.Id == userId
                    && cfg.View.Id == viewId);
            var config = items.SingleOrDefault();
            if (config == null)
            {
                var viewRepository = UnitOfWork.GetAsyncRepository<View>();
                var entityView = await viewRepository.GetByIDAsync(viewId, ev => ev.Columns);
                if (entityView != null)
                {
                    userConfig = new ListFormViewConfig() { ViewId = entityView.Id };
                    foreach (var column in entityView.Columns)
                    {
                        userConfig.ColumnViews.Add(Mapper.Map<ColumnViewConfig>(column));
                    }
                }
            }
            else
            {
                userConfig = Mapper.Map<ListFormViewConfig>(config);
            }

            return userConfig;
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری برای یکی از فرم های لیستی را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="userConfig">تنظیمات کاربری برای فرم لیستی</param>
        public async Task SaveUserListConfigAsync(int userId, ListFormViewConfig userConfig)
        {
            Verify.ArgumentNotNull(userConfig, "userConfig");
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var userRepository = UnitOfWork.GetAsyncRepository<User>();
            var existing = await repository
                .GetEntityWithTrackingQuery()
                .Where(cfg => cfg.User.Id == userId
                    && cfg.ViewId == userConfig.ViewId
                    && cfg.ModelType == typeof(ListFormViewConfig).Name)
                .SingleOrDefaultAsync();
            if (existing == null)
            {
                var newUserConfig = new UserSetting()
                {
                    SettingId = 4,      // TODO: Remove this hard-coded value
                    ViewId = userConfig.ViewId,
                    User = await userRepository.GetByIDAsync(userId),
                    ModelType = typeof(ListFormViewConfig).Name,
                    Values = JsonHelper.From(userConfig, false)
                };

                repository.Insert(newUserConfig);
            }
            else
            {
                existing.Values = JsonHelper.From(userConfig, false);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری موجود برای جستجوی سریع در یکی از فرم های لیستی را
        /// برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات کاربری موجود برای جستجوی سریع</returns>
        public async Task<QuickSearchConfig> GetQuickSearchConfigAsync(int userId, int viewId)
        {
            var userConfig = default(QuickSearchConfig);
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var items = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(QuickSearchConfig).Name
                    && cfg.User.Id == userId
                    && cfg.View.Id == viewId);
            var config = items.SingleOrDefault();
            if (config == null)
            {
                var viewRepository = UnitOfWork.GetAsyncRepository<View>();
                var entityView = await viewRepository.GetByIDAsync(viewId, ev => ev.Columns);
                if (entityView != null)
                {
                    userConfig = new QuickSearchConfig()
                    {
                        ViewId = entityView.Id,
                        SearchMode = TextSearchMode.Contains
                    };
                    foreach (var column in entityView.Columns
                        .Where(col => col.Visibility == ColumnVisibility.AlwaysVisible
                            || col.Visibility == null))
                    {
                        userConfig.Columns.Add(Mapper.Map<QuickSearchColumnConfig>(column));
                    }
                }
            }
            else
            {
                userConfig = Mapper.Map<QuickSearchConfig>(config);
            }

            return userConfig;
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری برای جستجوی سریع در یکی از فرم های لیستی را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="userConfig">تنظیمات کاربری برای جستجوی سریع</param>
        public async Task SaveQuickSearchConfigAsync(int userId, QuickSearchConfig userConfig)
        {
            Verify.ArgumentNotNull(userConfig, nameof(userConfig));
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var userRepository = UnitOfWork.GetAsyncRepository<User>();
            var existing = await repository
                .GetEntityWithTrackingQuery()
                .Where(cfg => cfg.User.Id == userId
                    && cfg.ViewId == userConfig.ViewId
                    && cfg.ModelType == typeof(QuickSearchConfig).Name)
                .SingleOrDefaultAsync();
            if (existing == null)
            {
                var newUserConfig = new UserSetting()
                {
                    SettingId = 6,      // TODO: Remove this hard-coded value
                    ViewId = userConfig.ViewId,
                    User = await userRepository.GetByIDAsync(userId),
                    ModelType = typeof(QuickSearchConfig).Name,
                    Values = JsonHelper.From(userConfig, false)
                };

                repository.Insert(newUserConfig);
            }
            else
            {
                existing.Values = JsonHelper.From(userConfig, false);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری موجود برای گزارش فوری برای یکی از فرم های لیستی را
        /// برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات کاربری موجود برای گزارش فوری</returns>
        public async Task<QuickReportConfig> GetQuickReportConfigAsync(int userId, int viewId)
        {
            var userConfig = default(QuickReportConfig);
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var items = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(QuickReportConfig).Name
                    && cfg.User.Id == userId
                    && cfg.View.Id == viewId);
            var config = items.SingleOrDefault();
            if (config != null)
            {
                userConfig = Mapper.Map<QuickReportConfig>(config);
            }

            return userConfig;
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری برای گزارش فوری برای یکی از فرم های لیستی را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <param name="userConfig">تنظیمات کاربری برای گزارش فوری</param>
        public async Task SaveQuickReportConfigAsync(int userId, QuickReportConfig userConfig)
        {
            Verify.ArgumentNotNull(userConfig, nameof(userConfig));
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var userRepository = UnitOfWork.GetAsyncRepository<User>();
            var existing = await repository
                .GetEntityWithTrackingQuery()
                .Where(cfg => cfg.User.Id == userId
                    && cfg.ViewId == userConfig.ViewId
                    && cfg.ModelType == typeof(QuickReportConfig).Name)
                .SingleOrDefaultAsync();
            if (existing == null)
            {
                var newUserConfig = new UserSetting()
                {
                    SettingId = 7,      // TODO: Remove this hard-coded value
                    ViewId = userConfig.ViewId,
                    User = await userRepository.GetByIDAsync(userId),
                    ModelType = typeof(QuickReportConfig).Name,
                    Values = JsonHelper.From(userConfig, false)
                };

                repository.Insert(newUserConfig);
            }
            else
            {
                existing.Values = JsonHelper.From(userConfig, false);
                repository.Update(existing);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای ساختار همه نماهای درختی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>تنظیمات موجود برای ساختار همه نماهای درختی</returns>
        public async Task<IList<ViewTreeFullConfig>> GetAllViewTreeConfigAsync()
        {
            var allConfig = new List<ViewTreeFullConfig>();
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<ViewSetting>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(ViewTreeConfig).Name);
            foreach (var config in configItems)
            {
                var treeConfig = Mapper.Map<ViewTreeFullConfig>(config);
                ClipUsableTreeLevels(treeConfig);
                allConfig.Add(treeConfig);
            }

            UnitOfWork.UseSystemContext();
            return allConfig;
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای ساختار نمای درختی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات موجود برای ساختار نمای درختی مشخص شده</returns>
        /// <remarks>بنا بر آخرین تحلیل انجام شده، این متد حداکثر 8 سطح درختی را در دسترس قرار می دهد</remarks>
        public async Task<ViewTreeFullConfig> GetViewTreeConfigByViewAsync(int viewId)
        {
            var viewConfig = default(ViewTreeFullConfig);
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<ViewSetting>();
            var config = await repository
                .GetSingleByCriteriaAsync(cfg => cfg.ViewId == viewId
                    && cfg.ModelType == typeof(ViewTreeConfig).Name);
            if (config != null)
            {
                viewConfig = Mapper.Map<ViewTreeFullConfig>(config);
                ClipUsableTreeLevels(viewConfig);
            }

            UnitOfWork.UseSystemContext();
            return viewConfig;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین تغییرات مجموعه ای از تنظیمات نماهای درختی را ذخیره می کند
        /// </summary>
        /// <param name="configItems">مجموعه ای از تنظیمات نماهای درختی</param>
        public async Task SaveViewTreeConfigAsync(List<ViewTreeFullConfig> configItems)
        {
            Verify.ArgumentNotNull(configItems, "configItems");
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<ViewSetting>();
            foreach (var configItem in configItems)
            {
                var existing = await repository.GetSingleByCriteriaAsync(
                    cfg => cfg.ViewId == configItem.Default.ViewId && cfg.SettingId == 5);
                if (existing != null)
                {
                    existing.Values = JsonHelper.From(configItem.Current, false);
                    repository.Update(existing);
                }
            }

            await UnitOfWork.CommitAsync();
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون،وضعیت استفاده از یک سطح از ساختار درختی را برای یکی از موجودیت های درختی بروزرسانی می کند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های درختی موجود</param>
        /// <param name="level">شماره سطحی که وضعیت استفاده از آن باید تغییر کند</param>
        /// <param name="itemCount">تعداد سطرهای اطلاعاتی موجود در سطح مورد نظر</param>
        public async Task SaveTreeLevelUsageAsync(int viewId, int level, int itemCount)
        {
            var config = await GetViewTreeConfigByViewAsync(viewId);
            config.Current.Levels[level].IsUsed = itemCount > 0;
            var configItems = new List<ViewTreeFullConfig> { config };
            await SaveViewTreeConfigAsync(configItems);
        }

        private static void ClipUsableTreeLevels(ViewTreeFullConfig fullConfig)
        {
            while (fullConfig.Default.Levels.Count > ConfigConstants.MaxUsableTreeDepth)
            {
                fullConfig.Default.Levels.RemoveAt(ConfigConstants.MaxUsableTreeDepth);
            }

            while (fullConfig.Current.Levels.Count > ConfigConstants.MaxUsableTreeDepth)
            {
                fullConfig.Current.Levels.RemoveAt(ConfigConstants.MaxUsableTreeDepth);
            }
        }

        private readonly IFiscalPeriodRepository _fiscalRepository;
    }
}
