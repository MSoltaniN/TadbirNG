using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Mapper;
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
    public class ConfigRepository : IConfigRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="unitOfWork">پیاده سازی اینترفیس واحد کاری برای انجام عملیات دیتابیسی </param>
        /// <param name="mapper">نگاشت مورد استفاده برای تبدیل کلاس های مدل اطلاعاتی</param>
        public ConfigRepository(IAppUnitOfWork unitOfWork, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWork.UseSystemContext();
        }

        /// <summary>
        /// تمام تنظیمات موجود برای برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از تمام تنظیمات موجود برای برنامه</returns>
        public async Task<IList<SettingBriefViewModel>> GetAllConfigAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<Setting>();
            var allConfig = await repository
                .GetAllAsync();
            return allConfig
                .Where(cfg => !(cfg.Type == 3 && cfg.ScopeType == 2))
                .Select(cfg => _mapper.Map<SettingBriefViewModel>(cfg))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت داده شده برای تنظیمات را ذخیره می کند
        /// </summary>
        /// <param name="configItems">مجموعه ای از تنظیمات اصلاح شده</param>
        public async Task SaveConfigAsync(IList<SettingBriefViewModel> configItems)
        {
            Verify.ArgumentNotNull(configItems, "configItems");
            var repository = _unitOfWork.GetAsyncRepository<Setting>();
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
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// تنظیمات موجود برای کلاس تنظیمات مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TConfig">نوع تنظیمات مورد نیاز</typeparam>
        /// <returns>تنظیمات موجود برای کلاس تنظیمات مشخص شده</returns>
        public async Task<TConfig> GetConfigByTypeAsync<TConfig>()
        {
            var configByType = default(TConfig);
            var repository = _unitOfWork.GetAsyncRepository<Setting>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(TConfig).Name);
            var config = configItems.SingleOrDefault();
            if (config != null)
            {
                configByType = _mapper.Map<TConfig>(config);
            }

            return configByType;
        }

        /// <summary>
        /// تمام تنظیمات کاربری موجود برای فرم های لیستی را برای کاربر مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>تنظیمات کاربری موجود برای فرم های لیستی</returns>
        public async Task<IList<ListFormViewConfig>> GetListViewConfigByUserAsync(int userId)
        {
            IList<ListFormViewConfig> configList = new List<ListFormViewConfig>();
            var repository = _unitOfWork.GetAsyncRepository<UserSetting>();
            var viewRepository = _unitOfWork.GetAsyncRepository<View>();
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
                    userConfig.ColumnViews.Add(_mapper.Map<ColumnViewConfig>(column));
                }

                configList.Add(userConfig);
            }

            foreach (var config in configItems)
            {
                configList.Add(_mapper.Map<ListFormViewConfig>(config));
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
            var repository = _unitOfWork.GetAsyncRepository<UserSetting>();
            var items = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(ListFormViewConfig).Name
                    && cfg.User.Id == userId
                    && cfg.View.Id == viewId);
            var config = items.SingleOrDefault();
            if (config == null)
            {
                var viewRepository = _unitOfWork.GetAsyncRepository<View>();
                var entityView = await viewRepository.GetByIDAsync(viewId, ev => ev.Columns);
                if (entityView != null)
                {
                    userConfig = new ListFormViewConfig() { ViewId = entityView.Id };
                    foreach (var column in entityView.Columns)
                    {
                        userConfig.ColumnViews.Add(_mapper.Map<ColumnViewConfig>(column));
                    }
                }
            }
            else
            {
                userConfig = _mapper.Map<ListFormViewConfig>(config);
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
            var repository = _unitOfWork.GetAsyncRepository<UserSetting>();
            var userRepository = _unitOfWork.GetAsyncRepository<User>();
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

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای ساختار نمای درختی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی یکی از مدل های نمایشی موجود</param>
        /// <returns>تنظیمات موجود برای ساختار نمای درختی مشخص شده</returns>
        public async Task<ViewTreeFullConfig> GetViewTreeConfigByViewAsync(int viewId)
        {
            var viewConfig = default(ViewTreeFullConfig);
            var repository = _unitOfWork.GetAsyncRepository<ViewSetting>();
            var config = await repository
                .GetSingleByCriteriaAsync(cfg => cfg.ViewId == viewId
                    && cfg.ModelType == typeof(ViewTreeConfig).Name);
            if (config != null)
            {
                viewConfig = _mapper.Map<ViewTreeFullConfig>(config);
            }

            return viewConfig;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین تغییرات مجموعه ای از تنظیمات نماهای درختی را ذخیره می کند
        /// </summary>
        /// <param name="configItems">مجموعه ای از تنظیمات نماهای درختی</param>
        public async Task SaveViewTreeConfigAsync(List<ViewTreeFullConfig> configItems)
        {
            Verify.ArgumentNotNull(configItems, "configItems");
            var repository = _unitOfWork.GetAsyncRepository<ViewSetting>();
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

            await _unitOfWork.CommitAsync();
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

        private async Task InitDefaultColumnSettingsAsync()
        {
            InitDefaultColumns();
            var names = new string[] { "Id", "Name", "Level", "No", "UserName", "Description" };
            var repository = _unitOfWork.GetAsyncRepository<Column>();
            var items = await repository.GetByCriteriaAsync(prop => names.Contains(prop.Name), prop => prop.View);
            foreach (var item in items)
            {
                if (item.Name == "Name" || item.Name == "UserName" || item.Name == "No")
                {
                    _nameColumn.Name = item.Name;
                    item.Settings = JsonHelper.From(_nameColumn, false);
                }
                else if (item.Name == "Id")
                {
                    item.Settings = JsonHelper.From(_idColumn, false);
                }
                else if (item.Name == "Level")
                {
                    item.Settings = JsonHelper.From(_levelColumn, false);
                }
                else if (item.View.Id == 3)
                {
                    _nameColumn.Name = item.Name;
                    item.Settings = JsonHelper.From(_nameColumn, false);
                }

                repository.Update(item);
            }

            await _unitOfWork.CommitAsync();
        }

        private async Task InitSampleUserSettingsAsync()
        {
            var repository = _unitOfWork.GetAsyncRepository<UserSetting>();
            var userSetting = GetSampleUserSettings();
            await SaveUserListConfigAsync(1, userSetting);
        }

        private void InitDefaultColumns()
        {
            _idColumn = new ColumnViewConfig("Id");
            var idDeviceConfig = new ColumnViewDeviceConfig() { Width = 0, Index = -1, DesignIndex = 0, Visibility = ColumnVisibility.AlwaysHidden };
            _idColumn.Large = idDeviceConfig;
            _idColumn.Medium = idDeviceConfig;
            _idColumn.Small = idDeviceConfig;
            _idColumn.ExtraSmall = idDeviceConfig;
            _idColumn.ExtraLarge = idDeviceConfig;

            _nameColumn = new ColumnViewConfig("Name");
            var nameDeviceConfig = new ColumnViewDeviceConfig() { Index = 0, DesignIndex = 0, Visibility = ColumnVisibility.AlwaysVisible };
            _nameColumn.Large = nameDeviceConfig;
            _nameColumn.Medium = nameDeviceConfig;
            _nameColumn.Small = nameDeviceConfig;
            _nameColumn.ExtraSmall = nameDeviceConfig;
            _nameColumn.ExtraLarge = nameDeviceConfig;

            _levelColumn = new ColumnViewConfig("Level");
            var levelDeviceConfig = new ColumnViewDeviceConfig() { DesignIndex = 0, Visibility = ColumnVisibility.Hidden };
            _levelColumn.Large = levelDeviceConfig;
        }

        private ListFormViewConfig GetSampleUserSettings()
        {
            var listConfig = new ListFormViewConfig() { ViewId = 1, PageSize = 25 };
            var column = new ColumnViewConfig("Id");
            var deviceColumn = new ColumnViewDeviceConfig() { Width = 0, Index = -1, Visibility = ColumnVisibility.AlwaysHidden };
            column.Large = column.Medium = column.Small = column.ExtraSmall = column.ExtraLarge = deviceColumn;
            listConfig.ColumnViews.Add(column);

            column = new ColumnViewConfig("Code");
            deviceColumn = new ColumnViewDeviceConfig() { Width = 100, Index = 0 };
            column.Large = column.Medium = deviceColumn;
            deviceColumn = new ColumnViewDeviceConfig() { Visibility = ColumnVisibility.Hidden };
            column.Small = column.ExtraSmall = deviceColumn;
            listConfig.ColumnViews.Add(column);

            column = new ColumnViewConfig("FullCode");
            deviceColumn = new ColumnViewDeviceConfig() { Width = 150, Index = 1 };
            column.Large = column.Medium = deviceColumn;
            deviceColumn = new ColumnViewDeviceConfig() { Visibility = ColumnVisibility.Hidden };
            column.Small = column.ExtraSmall = deviceColumn;
            listConfig.ColumnViews.Add(column);

            column = new ColumnViewConfig("Name");
            deviceColumn = new ColumnViewDeviceConfig() { Width = 180, Index = 2, Visibility = ColumnVisibility.AlwaysVisible };
            column.Large = column.Medium = deviceColumn;
            deviceColumn = new ColumnViewDeviceConfig() { Width = 125, Index = 2, Visibility = ColumnVisibility.AlwaysVisible };
            column.Small = column.ExtraSmall = deviceColumn;
            listConfig.ColumnViews.Add(column);

            column = new ColumnViewConfig("Level");
            deviceColumn = new ColumnViewDeviceConfig() { Width = 50, Index = 4 };
            column.Large = deviceColumn;
            deviceColumn = new ColumnViewDeviceConfig() { Visibility = ColumnVisibility.Hidden };
            column.Medium = column.Small = column.ExtraSmall = deviceColumn;
            listConfig.ColumnViews.Add(column);

            column = new ColumnViewConfig("Description");
            deviceColumn = new ColumnViewDeviceConfig() { Width = 360, Index = 3, Visibility = ColumnVisibility.Visible };
            column.Large = column.Medium = deviceColumn;
            deviceColumn = new ColumnViewDeviceConfig() { Width = 180, Index = 3, Visibility = ColumnVisibility.Visible };
            column.Small = column.ExtraSmall = deviceColumn;
            listConfig.ColumnViews.Add(column);

            return listConfig;
        }

        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IDomainMapper _mapper;

        private ColumnViewConfig _idColumn;
        private ColumnViewConfig _nameColumn;
        private ColumnViewConfig _levelColumn;
    }
}
