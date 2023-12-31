﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات برنامه را پیاده سازی می کند
    /// </summary>
    public class ConfigRepository : BaseConfigRepository, IConfigRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی و سیستمی را در برنامه فراهم می کند</param>
        /// <param name="pathProvider">مسیرهای فایل های کاربردی مورد نیاز را فراهم می کند</param>
        public ConfigRepository(IRepositoryContext context, IOperationLogRepository log,
            IApiPathProvider pathProvider)
            : base(context, log)
        {
            _pathProvider = pathProvider;
        }

        /// <summary>
        /// محدوده تاریخی پیش فرض را با توجه به دوره مالی جاری برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="start">پارامتر خروجی برای تنظیم تاریخ ابتدا در محدوده تاریخی پیش فرض</param>
        /// <param name="end">پارامتر خروجی برای تنظیم تاریخ انتها در محدوده تاریخی پیش فرض</param>
        public void GetDefaultFiscalDateRange(out DateTime start, out DateTime end)
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
                var repository = UnitOfWork.GetRepository<FiscalPeriod>();
                var fiscalPeriod = repository.GetByID(UserContext.FiscalPeriodId);
                start = fiscalPeriod.StartDate;
                end = (config.DefaultDateRange == DateRangeOptions.FiscalStartToCurrent)
                    ? DateTime.Now
                    : fiscalPeriod.EndDate;
            }
        }

        /// <summary>
        /// محدوده تاریخی دوره مالی جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="start">پارامتر خروجی برای تنظیم تاریخ ابتدا در محدوده تاریخی</param>
        /// <param name="end">پارامتر خروجی برای تنظیم تاریخ انتها در محدوده تاریخی</param>
        public void GetCurrentFiscalDateRange(out DateTime start, out DateTime end)
        {
            var repository = UnitOfWork.GetRepository<FiscalPeriod>();
            var fiscalPeriod = repository.GetByID(UserContext.FiscalPeriodId);
            start = fiscalPeriod.StartDate;
            end = fiscalPeriod.EndDate;
        }

        /// <summary>
        /// به روش آسنکرون، نوع تقویم پیش فرض برای زبان جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مقدار عددی متناظر با نوع شمارشی موجود برای تقویم پیش فرض</returns>
        public async Task<CalendarType> GetCurrentCalendarTypeAsync()
        {
            var calendar = CalendarType.Jalali;
            var systemConfig = await GetConfigByTypeAsync<SystemConfig>();
            var config = systemConfig.DefaultCalendars
                .Where(cfg => cfg.Language == UserContext.Language)
                .SingleOrDefault();
            if (config != null)
            {
                calendar = (CalendarType)config.Calendar;
            }

            return calendar;
        }

        /// <summary>
        /// به روش آسنکرون، تقویم پیش فرض برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>پیاده سازی استاندارد موجود برای تقویم پیش فرض</returns>
        public async Task<Calendar> GetCurrentCalendarAsync()
        {
            var calendarType = await GetCurrentCalendarTypeAsync();
            return (calendarType == CalendarType.Jalali)
                ? new PersianCalendar()
                : new GregorianCalendar();
        }

        /// <summary>
        /// به روش آسنکرون، تاریخ داده شده را با توجه به تنظیمات تقویم پیش فرض به صورت رشته متنی برمی گرداند
        /// </summary>
        /// <param name="date">تاریخ مورد نظر برای نمایش متنی</param>
        /// <returns>تاریخ داده شده به صورت رشته متنی</returns>
        public async Task<string> GetDateDisplayAsync(DateTime date)
        {
            string dateDisplay = date.ToShortDateString(false);
            var calendar = await GetCurrentCalendarTypeAsync();
            if (calendar == CalendarType.Jalali)
            {
                dateDisplay = JalaliDateTime
                    .FromDateTime(date)
                    .ToShortDateString();
            }

            return dateDisplay;
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
                    && cfg.UserId == userId
                    && cfg.ViewId == viewId);
            var config = items.SingleOrDefault();
            if (config == null)
            {
                UnitOfWork.UseSystemContext();
                var viewRepository = UnitOfWork.GetAsyncRepository<View>();
                var entityView = await viewRepository.GetByIDAsync(viewId, ev => ev.Columns);
                UnitOfWork.UseCompanyContext();
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
            var existing = await repository
                .GetEntityWithTrackingQuery()
                .Where(cfg => cfg.UserId == userId
                    && cfg.ViewId == userConfig.ViewId
                    && cfg.ModelType == typeof(QuickSearchConfig).Name)
                .SingleOrDefaultAsync();
            if (existing == null)
            {
                UnitOfWork.UseSystemContext();
                var userRepository = UnitOfWork.GetAsyncRepository<User>();
                var user = await userRepository.GetByIDAsync(userId);
                UnitOfWork.UseCompanyContext();
                var newUserConfig = new UserSetting()
                {
                    SettingId = (int)SettingId.QuickSearch,
                    ViewId = userConfig.ViewId,
                    User = user,
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
        /// به روش آسنکرون، تنظیمات موجود برای ساختار همه نماهای درختی را خوانده و برمی گرداند
        /// </summary>
        /// <returns>تنظیمات موجود برای ساختار همه نماهای درختی</returns>
        public async Task<IList<ViewTreeFullConfig>> GetAllViewTreeConfigAsync()
        {
            var allConfig = new List<ViewTreeFullConfig>();
            var repository = UnitOfWork.GetAsyncRepository<ViewSetting>();
            var configItems = await repository
                .GetByCriteriaAsync(cfg => cfg.ModelType == typeof(ViewTreeConfig).Name);
            foreach (var config in configItems)
            {
                var treeConfig = Mapper.Map<ViewTreeFullConfig>(config);
                ClipUsableTreeLevels(treeConfig);
                allConfig.Add(treeConfig);
            }

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
            var repository = UnitOfWork.GetAsyncRepository<ViewSetting>();
            var config = await repository
                .GetSingleByCriteriaAsync(cfg => cfg.ViewId == viewId
                    && cfg.ModelType == typeof(ViewTreeConfig).Name);
            if (config != null)
            {
                viewConfig = Mapper.Map<ViewTreeFullConfig>(config);
                ClipUsableTreeLevels(viewConfig);
            }

            return viewConfig;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین تغییرات مجموعه ای از تنظیمات نماهای درختی را ذخیره می کند
        /// </summary>
        /// <param name="configItems">مجموعه ای از تنظیمات نماهای درختی</param>
        public async Task SaveViewTreeConfigAsync(List<ViewTreeFullConfig> configItems)
        {
            Verify.ArgumentNotNull(configItems, nameof(configItems));
            var repository = UnitOfWork.GetAsyncRepository<ViewSetting>();
            foreach (var configItem in configItems)
            {
                var existing = await repository.GetSingleByCriteriaAsync(
                    cfg => cfg.ViewId == configItem.Default.ViewId);
                if (existing != null)
                {
                    UnclipUsableTreeLevels(configItem);
                    existing.Values = JsonHelper.From(configItem.Current, false);
                    repository.Update(existing);
                }
            }

            await UnitOfWork.CommitAsync();
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

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت پیکربندی سیستم را ذخیره می کند
        /// </summary>
        /// <param name="configItem">تنظیمات پیکربندی سیستم</param>
        public async Task SaveSystemConfigAsync(SettingBriefViewModel configItem)
        {
            Verify.ArgumentNotNull(configItem, nameof(configItem));
            var repository = UnitOfWork.GetAsyncRepository<Setting>();

            var systemConfig = await repository
                .GetByIDWithTrackingAsync(configItem.Id);
            var oldValues = JsonHelper.To<SystemConfig>(systemConfig.Values);
            SetDefaultCalendar(configItem);
            systemConfig.Values = JsonHelper.From(configItem.Values, false);
            repository.Update(systemConfig);
            await UnitOfWork.CommitAsync();

            var newValues = JsonHelper.To<SystemConfig>(systemConfig.Values);
            await LogConfigChangeAsync(oldValues, newValues);
            if (newValues.UsesDefaultCoding && !await IsDefinedAccountAsync())
            {
                await InitializeDefaultAccounts();
            }
        }

        /// <summary>
        /// به روش آسنکرون، امکان تغییر سیستم ثبت دوره مالی جاری را بررسی می کند
        /// </summary>
        /// <returns>آیا تغییر سیستم ثبت دوره مالی جاری امکان دارد</returns>
        public async Task<bool> ValidateInventoryModeChangeAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            return await repository.GetCountByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId) == 0 ;
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات موجود برای عناوین سفارشی فرم گزارشی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="formId">شناسه دیتابیسی فرم گزارشی</param>
        /// <param name="localeId">شناسه دیتابیسی زبان مورد نظر برای محلی سازی متن عناوین</param>
        /// <returns>تنظیمات موجود برای عناوین سفارشی</returns>
        public async Task<FormLabelFullConfig> GetFormLabelConfigAsync(int formId, int localeId)
        {
            var labelConfig = default(FormLabelFullConfig);
            var repository = UnitOfWork.GetAsyncRepository<LabelSetting>();
            var config = await repository
                .GetSingleByCriteriaAsync(cfg => cfg.ModelType == typeof(FormLabelConfig).Name
                    && cfg.CustomForm.Id == formId
                    && cfg.LocaleId == localeId);
            if (config != null)
            {
                labelConfig = Mapper.Map<FormLabelFullConfig>(config);
            }

            return labelConfig;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت عناوین سفارشی یک فرم گزارشی را ذخیره می کند
        /// </summary>
        /// <param name="labelConfig">اطلاعات تنظیمات عناوین سفارشی</param>
        public async Task SaveFormLabelConfigAsync(FormLabelConfig labelConfig)
        {
            Verify.ArgumentNotNull(labelConfig, nameof(labelConfig));
            var repository = UnitOfWork.GetAsyncRepository<LabelSetting>();
            var existing = await repository.GetSingleByCriteriaAsync(
                cfg => cfg.CustomForm.Id == labelConfig.FormId && cfg.LocaleId == labelConfig.LocaleId);
            if (existing != null)
            {
                existing.Values = CorrectLabelConfigCasing(
                    JsonHelper.From(labelConfig, false, null, false));
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، تنظیمات کاربری موجود را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <returns>مقادیر جاری تنظیمات کاربری برای کاربر مورد نظر</returns>
        public async Task<UserProfileConfig> GetUserProfileConfigAsync(int userId)
        {
            var userProfile = default(UserProfileConfig);
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var existing = await repository
                .GetSingleByCriteriaAsync(
                    cfg => cfg.ModelType == typeof(UserProfileConfig).Name
                        && cfg.UserId == userId);
            if (existing == null)
            {
                userProfile = new UserProfileConfig();
            }
            else
            {
                userProfile = JsonHelper.To<UserProfileConfig>(existing.Values);
            }

            return userProfile;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت تنظیمات کاربری را ذخیره می کند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <param name="profile">آخرین وضعیت تنظیمات کاربری برای کاربر مورد نظر</param>
        public async Task SaveUserProfileConfigAsync(int userId, UserProfileConfig profile)
        {
            var repository = UnitOfWork.GetAsyncRepository<UserSetting>();
            var existing = await repository
                .GetSingleByCriteriaAsync(
                    cfg => cfg.ModelType == typeof(UserProfileConfig).Name
                        && cfg.UserId == userId);
            if (existing == null)
            {
                var userSetting = new UserSetting()
                {
                    SettingId = (int)SettingId.UserProfile,
                    ModelType = typeof(UserProfileConfig).Name,
                    UserId = userId,
                    Values = JsonHelper.From(profile, false)
                };
                repository.Insert(userSetting);
            }
            else
            {
                existing.Values = JsonHelper.From(profile, false);
                repository.Update(existing);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetLevelCodeLength(int level)
        {
            return GetLevelCodeLength(ViewId.Account, level);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetLevelCodeLength(int viewId, int level)
        {
            var fullConfig = GetViewTreeConfigByViewAsync(viewId).Result;
            var treeConfig = fullConfig.Current;
            int codeLength = treeConfig.Levels
                .Where(cfg => cfg.No <= level + 1)
                .Select(cfg => (int)cfg.CodeLength)
                .Sum();
            return codeLength;
        }

        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.EnvironmentParams; }
        }

        private static string CorrectLabelConfigCasing(string labelConfig)
        {
            return labelConfig
                .Replace("FormId", "formId")
                .Replace("LocaleId", "localeId")
                .Replace("LabelMap", "labelMap");
        }

        private static void ClipUsableTreeLevels(ViewTreeFullConfig fullConfig)
        {
            while (fullConfig.Current.Levels.Count > ConfigConstants.MaxUsableTreeDepth)
            {
                fullConfig.Current.Levels.RemoveAt(ConfigConstants.MaxUsableTreeDepth);
            }

            while (fullConfig.Default.Levels.Count > ConfigConstants.MaxUsableTreeDepth)
            {
                fullConfig.Default.Levels.RemoveAt(ConfigConstants.MaxUsableTreeDepth);
            }
        }

        private static void UnclipUsableTreeLevels(ViewTreeFullConfig fullConfig)
        {
            while (fullConfig.Current.Levels.Count < ConfigConstants.MaxTreeDepth)
            {
                var levelConfig = new ViewTreeLevelConfig()
                {
                    Name = ConfigConstants.DefaultLevelNameKey,
                    No = fullConfig.Current.Levels.Count + 1,
                    CodeLength = ConfigConstants.DefaultCodeLength
                };
                fullConfig.Current.Levels.Add(levelConfig);
            }
        }

        private static string GetYesNo(bool value)
        {
            return value ? AppStrings.BooleanYes : AppStrings.BooleanNo;
        }

        private static string GetNullableValue(int? nullable)
        {
            return nullable.HasValue
                ? nullable.Value.ToString()
                : "NULL";
        }

        private bool IsCalendarChanged(SystemConfig old, SystemConfig current)
        {
            var oldConfig = old.DefaultCalendars
                .Where(cfg => cfg.Language == UserContext.Language)
                .SingleOrDefault();
            var currentConfig = current.DefaultCalendars
                .Where(cfg => cfg.Language == UserContext.Language)
                .SingleOrDefault();
            return oldConfig.Calendar != currentConfig.Calendar;
        }

        private async Task<bool> IsDefinedAccountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            var query = repository.GetEntityQuery();
            return await query.AnyAsync();
        }

        private string GetCalendarName(SystemConfig config)
        {
            var calendarConfig = config.DefaultCalendars
                .Where(cfg => cfg.Language == UserContext.Language)
                .SingleOrDefault();
            return (calendarConfig.Calendar == (int)CalendarType.Gregorian)
                ? AppStrings.Gregorian
                : AppStrings.Persian;
        }

        private async Task InitializeDefaultAccounts()
        {
            var accountTreeConfig = await GetViewTreeConfigByViewAsync(ViewId.Account);
            var defaultAcc = JsonHelper.To<List<DefaultAccountViewModel>>(
                File.ReadAllText(_pathProvider.Accounts));
            UpdateDefaultAccountCodeRecursive(defaultAcc, accountTreeConfig, string.Empty);

            var accounts = defaultAcc.Select(f => Mapper.Map<Account>(f)).ToList();
            InsertDefaultAccounts(accounts);

            await UpdateTreeLevelUsage(accountTreeConfig);
        }

        private void UpdateDefaultAccountCodeRecursive(IList<DefaultAccountViewModel> defaultAccounts,
            ViewTreeFullConfig accountTreeConfig, string accParentFullCode, int? parentId = null)
        {
            foreach (var item in defaultAccounts.Where(f => f.ParentId == parentId))
            {
                int accCodeLength = item.Code.Length;
                int configCodeLength = accountTreeConfig.Current.Levels[item.Level].CodeLength;
                if (accCodeLength != configCodeLength)
                {
                    if (configCodeLength == 2)
                    {
                        item.Code = item.TwoDigitCode;
                    }
                    else
                    {
                        var diffLength = 0;
                        if (accCodeLength > configCodeLength)
                        {
                            diffLength = accCodeLength - configCodeLength;
                            item.Code = item.Code[diffLength..];
                        }
                        else
                        {
                            diffLength = configCodeLength - accCodeLength;
                            string format = String.Format("D{0}", configCodeLength);
                            item.Code = Convert.ToInt64(item.Code).ToString(format);
                        }
                    }
                }

                item.FullCode = string.Format("{0}{1}", accParentFullCode, item.Code);
                UpdateDefaultAccountCodeRecursive(defaultAccounts, accountTreeConfig, item.FullCode, item.Id);
            }
        }

        private void InsertDefaultAccounts(IList<Account> accounts)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.AppendLine("SET IDENTITY_INSERT [Finance].[Account] ON");
            foreach (var account in accounts)
            {
                scriptBuilder.AppendFormat(@"
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
VALUES({0}, {1}, {2}, {3}, {4}, {5}, N'{6}', N'{7}', N'{8}', {9})",
                    account.Id, GetNullableValue(account.ParentId), GetNullableValue(account.GroupId),
                    UserContext.FiscalPeriodId, UserContext.BranchId, account.BranchScope, account.Code,
                    account.FullCode, account.Name, account.Level);
                scriptBuilder.AppendLine();
            }

            scriptBuilder.AppendLine("SET IDENTITY_INSERT [Finance].[Account] OFF");
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            DbConsole.ExecuteNonQuery(scriptBuilder.ToString());

            scriptBuilder = new StringBuilder(File.ReadAllText(_pathProvider.AccountScript));
            scriptBuilder
                .Replace("%branchId%", UserContext.BranchId.ToString())
                .Replace("%fiscalPeriodId%", UserContext.FiscalPeriodId.ToString());
            DbConsole.ExecuteNonQuery(scriptBuilder.ToString());
        }

        private async Task UpdateTreeLevelUsage(ViewTreeFullConfig accountTreeConfig)
        {
            accountTreeConfig.Current.Levels[0].IsUsed = true;
            accountTreeConfig.Current.Levels[1].IsUsed = true;
            accountTreeConfig.Current.Levels[2].IsUsed = true;
            var configItems = new List<ViewTreeFullConfig> { accountTreeConfig };
            await SaveViewTreeConfigAsync(configItems);
        }

        private void SetDefaultCalendar(SettingBriefViewModel config)
        {
            // NOTE: config.Values that comes back from client is NOT SystemConfig, so it needs double conversion...
            var json = JsonHelper.From(config.Values);
            var systemConfig = JsonHelper.To<SystemConfig>(json);
            var calendarConfig = systemConfig.DefaultCalendars
                .Where(cfg => cfg.Language == UserContext.Language)
                .Single();
            calendarConfig.Calendar = systemConfig.DefaultCalendar;
            config.Values = systemConfig;
        }

        private async Task LogConfigChangeAsync(SystemConfig oldConfig, SystemConfig newConfig)
        {
            string description;
            if (IsCalendarChanged(oldConfig, newConfig))
            {
                description = String.Format("{0} : {1} , {2} : {3}",
                    AppStrings.Old, GetCalendarName(oldConfig),
                    AppStrings.New, GetCalendarName(newConfig));
                OnEntityAction(OperationId.CalendarChange);
                Log.Description = Context.Localize(description);
                await TrySaveLogAsync();
            }

            if (oldConfig.DefaultCurrencyNameKey != newConfig.DefaultCurrencyNameKey)
            {
                description = String.Format("{0} : {1} , {2} : {3}",
                    AppStrings.Old, oldConfig.DefaultCurrencyNameKey,
                    AppStrings.New, newConfig.DefaultCurrencyNameKey);
                OnEntityAction(OperationId.CurrencyChange);
                Log.Description = Context.Localize(description);
                await TrySaveLogAsync();
            }

            if (oldConfig.DefaultDecimalCount != newConfig.DefaultDecimalCount)
            {
                description = String.Format("{0} : {1} , {2} : {3}",
                    AppStrings.Old, oldConfig.DefaultDecimalCount,
                    AppStrings.New, newConfig.DefaultDecimalCount);
                OnEntityAction(OperationId.DecimalCountChange);
                Log.Description = Context.Localize(description);
                await TrySaveLogAsync();
            }

            if (oldConfig.UsesDefaultCoding != newConfig.UsesDefaultCoding)
            {
                description = String.Format("{0} : {1} , {2} : {3}",
                    AppStrings.Old, GetYesNo(oldConfig.UsesDefaultCoding),
                    AppStrings.New, GetYesNo(newConfig.UsesDefaultCoding));
                OnEntityAction(OperationId.DefaultCodingChange);
                Log.Description = Context.Localize(description);
                await TrySaveLogAsync();
            }
        }

        private readonly IApiPathProvider _pathProvider;
    }
}
