using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Domain.Reporting;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public partial class DashboardRepository
    {
        #region Dashboard Management

        /// <summary>
        /// به روش آسنکرون، اطلاعات کامل داشبورد ایجاد شده توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات کامل برای داشبورد کاربر جاری برنامه یا رفرنس بدون مقدار
        /// در صورتی که داشبوردی برای کاربر جاری وجود نداشته باشد</returns>
        public DashboardViewModel GetCurrentUserDashboard()
        {
            var query = String.Format(DashboardQuery.CurrentDashboard, UserContext.Id);
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var dashboardResult = DbConsole.ExecuteQuery(query);
            return GetDashboard(dashboardResult);
        }

        /// <summary>
        /// به روش آسنکرون، داشبورد جدیدی برای کاربر جاری ایجاد کرده و اولین ویجت را به آن اضافه می کند
        /// </summary>
        /// <param name="tabWidget">اولین ویجت در داشبورد کاربر جاری که به برگه پیش فرض اضافه می شود</param>
        /// <returns>اطلاعات داشبورد ایجاد شده برای کاربر جاری</returns>
        public async Task<DashboardViewModel> CreateCurrentUserDashboardAsync(TabWidgetViewModel tabWidget)
        {
            Verify.ArgumentNotNull(tabWidget, nameof(tabWidget));
            if (await IsCurrentDashboardCreatedAsync())
            {
                return null;
            }

            var repository = UnitOfWork.GetAsyncRepository<Dashboard>();
            var dashboard = new Dashboard() { UserId = UserContext.Id };
            dashboard.Tabs.Add(new DashboardTab()
            {
                Index = 1,
                Title = AppStrings.NewDashboardTab
            });
            repository.Insert(dashboard, dbd => dbd.Tabs);
            await UnitOfWork.CommitAsync();
            tabWidget.TabId = dashboard.Tabs
                .Select(tab => tab.Id)
                .First();
            await SaveTabWidgetAsync(tabWidget);
            return GetCurrentUserDashboard();
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که داشبورد برای کاربر جاری ایجاد شده یا نه
        /// </summary>
        /// <returns>اگر برای کاربر جاری داشبورد ایجاد شده باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsCurrentDashboardCreatedAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Dashboard>();
            var dashboardId = await repository
                .GetEntityQuery()
                .Where(dbd => dbd.UserId == UserContext.Id)
                .Select(dbd => dbd.Id)
                .FirstOrDefaultAsync();
            return dashboardId > 0;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یکی از برگه های موجود در داشبورد کاربر جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه مورد نظر</param>
        /// <returns>اطلاعات نمایشی برگه مورد نظر یا رفرنس بدون مقدار در صورتی که برگه مورد نظر
        /// در داشبورد کاربر جاری نباشد</returns>
        public async Task<DashboardTabViewModel> GetDashboardTabAsync(int tabId)
        {
            var tab = default(DashboardTabViewModel);
            var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
            var existing = await repository
                .GetEntityQuery()
                .Where(tab => tab.Id == tabId && tab.Dashboard.UserId == UserContext.Id)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                tab = Mapper.Map<DashboardTabViewModel>(existing);
            }

            return tab;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک برگه داشبورد را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="tab">اطلاعات برگه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات برگه ایجاد یا اصلاح شده در دیتابیس</returns>
        public async Task<DashboardTabViewModel> SaveDashboardTabAsync(DashboardTabViewModel tab)
        {
            var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
            if (tab.Id == 0)
            {
                var newTab = Mapper.Map<DashboardTab>(tab);
                newTab.Index = GetNextTabIndex(tab.DashboardId);
                await InsertTabAsync(repository, newTab);
                tab.Id = newTab.Id;
                tab.Index = newTab.Index;
            }
            else
            {
                var existing = await repository.GetByIDAsync(tab.Id);
                if (existing != null)
                {
                    UpdateExisting(tab, existing);
                    repository.Update(existing);
                    await UnitOfWork.CommitAsync();
                }
            }

            return tab;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی چند برگه داشبورد را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="tabs">اطلاعات برگه های مورد نظر برای ایجاد یا اصلاح</param>
        public async Task SaveDashboardTabsAsync(IList<DashboardTabViewModel> tabs)
        {
            if (!tabs.Any())
            {
                return;
            }

            var tabIds = tabs.Select(tab => tab.Id);
            var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
            var existingTabs = await repository.GetByCriteriaAsync(tab => tabIds.Contains(tab.Id));
            foreach (var tab in existingTabs)
            {
                var tabView = tabs.FirstOrDefault(item => item.Id == tab.Id);
                if (tabView != null)
                {
                    UpdateExisting(tabView, tab);
                    repository.Update(tab);
                }
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، برگه مشخص شده را در دیتابیس حذف می کند
        /// </summary>
        /// <param name="tabId">شناسه دستابیسی برگه مورد نظر برای حذف</param>
        public async Task DeleteDashboardTabAsync(int tabId)
        {
            int dashboardId = await GetDashboardIdAsync(tabId);
            if (dashboardId > 0)
            {
                var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
                var tabs = await repository.GetByCriteriaAsync(tab => tab.DashboardId == dashboardId);
                var tabToDelete = tabs.Single(tab => tab.Id == tabId);
                foreach (var tab in tabs)
                {
                    if (tab.Id == tabId)
                    {
                        repository.Delete(tab);
                    }
                    else if (tab.Index > tabToDelete.Index)
                    {
                        tab.Index -= 1;
                        repository.Update(tab);
                    }
                }

                await DeleteTabAsync(tabToDelete);
            }
        }

        /// <summary>
        /// به روش آسنکرون، تعداد ویجت های اضافه شده به برگه مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی یکی از برگه های موجود</param>
        /// <returns>تعداد ویجت های اضافه شده به برگه</returns>
        public async Task<int> GetTabWidgetCountAsync(int tabId)
        {
            var repository = UnitOfWork.GetAsyncRepository<TabWidget>();
            return await repository.GetCountByCriteriaAsync(tw => tw.TabId == tabId);
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که برگه داده شده تنها برگه داشبورد است یا نه
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه مورد نظر</param>
        /// <returns>در صورتی که برگه مشخص شده تنها برگه داشبورد باشد، مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsSoleDashboardTab(int tabId)
        {
            bool isSoleTab = false;
            int dashboardId = await GetDashboardIdAsync(tabId);
            if (dashboardId > 0)
            {
                var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
                int count = await repository.GetCountByCriteriaAsync(tab => tab.DashboardId == dashboardId);
                isSoleTab = count == 1;
            }

            return isSoleTab;
        }

        /// <summary>
        /// به روش آسنکرون، یکی از ویجت های قابل دسترسی توسط کاربر جاری را در برگه تعیین شده اضافه می کند
        /// </summary>
        /// <param name="tabWidget">اطلاعات ویجت مورد نظر برای اضافه کردن به برگه داشبورد</param>
        /// <returns>آخرین اطلاعات ویجت اضافه شده در برگه داشبورد</returns>
        public async Task<TabWidgetViewModel> SaveTabWidgetAsync(TabWidgetViewModel tabWidget)
        {
            var repository = UnitOfWork.GetAsyncRepository<TabWidget>();
            if (tabWidget.Id == 0)
            {
                var widgetRepository = UnitOfWork.GetAsyncRepository<Widget>();
                var widgetInfo = await widgetRepository
                    .GetEntityQuery()
                    .Where(wgt => wgt.Id == tabWidget.WidgetId)
                    .Select(wgt => new
                    {
                        wgt.Title,
                        wgt.TypeId,
                        wgt.FunctionId,
                        wgt.DefaultSettings
                    })
                    .SingleOrDefaultAsync();
                if (widgetInfo == null)
                {
                    return tabWidget;
                }

                tabWidget.WidgetTitle = widgetInfo.Title;
                tabWidget.WidgetTypeId = widgetInfo.TypeId;
                tabWidget.WidgetFunctionId = widgetInfo.FunctionId;
                tabWidget.DefaultSettings = widgetInfo.DefaultSettings;
                tabWidget.Settings = widgetInfo.DefaultSettings;
                var saved = Mapper.Map<TabWidget>(tabWidget);
                repository.Insert(saved);
                await UnitOfWork.CommitAsync();
                tabWidget.Id = saved.Id;
                return tabWidget;
            }

            return tabWidget;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت های اضافه شده به برگه داشبورد را اصلاح می کند.
        /// </summary>
        /// <param name="tabWidgets">مجموعه ویجت های اضافه شده به داشبورد کاربر جاری</param>
        public async Task SaveTabWidgetsAsync(IList<TabWidgetViewModel> tabWidgets)
        {
            var tabWidgetIds = tabWidgets.Select(wgt => wgt.Id);
            var repository = UnitOfWork.GetAsyncRepository<TabWidget>();
            var existingItems = await repository.GetByCriteriaAsync(
                wgt => tabWidgetIds.Contains(wgt.Id));
            foreach (var item in existingItems)
            {
                var tabWidget = tabWidgets
                    .Where(twgt => twgt.Id == item.Id)
                    .FirstOrDefault();
                item.Settings = tabWidget.Settings;
                repository.Update(item);
            }

            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، ویجت داده شده را از برگه مورد نظر در داشبورد کاربر جاری حذف می کند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه ای که ویجت از آن حذف می شود</param>
        /// <param name="widgetId">شناسه دیتابیسی ویجتی که از برگه مورد نظر حذف می شود</param>
        public async Task DeleteTabWidgetAsync(int tabId, int widgetId)
        {
            var repository = UnitOfWork.GetAsyncRepository<TabWidget>();
            var existing = await repository.GetFirstByCriteriaAsync(
                tw => tw.TabId == tabId && tw.WidgetId == widgetId);
            if (existing != null)
            {
                repository.Delete(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        #endregion

        #region Widget Management

        /// <summary>
        /// به روش آسنکرون، فهرست ویجت های ایجادشده توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>ویجت های ایجادشده توسط کاربر جاری</returns>
        public async Task<PagedList<WidgetViewModel>> GetCurrentUserWidgetsAsync(
            GridOptions gridOptions = null)
        {
            return await GetWidgetsByCriteria(wgt => wgt.CreatedById == UserContext.Id, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، فهرست ویجت های قابل دسترسی توسط کاربر جاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>ویجت های قابل دسترسی توسط کاربر جاری</returns>
        public async Task<PagedList<WidgetViewModel>> GetAccessibleWidgetsAsync(
            GridOptions gridOptions = null)
        {
            Expression<Func<Widget, bool>> criteria = wgt => true;
            var roleWidgetRepository = UnitOfWork.GetAsyncRepository<RoleWidget>();
            var widgetIds = await roleWidgetRepository
                .GetEntityQuery()
                .Where(rw => UserContext.Roles.Contains(rw.RoleId))
                .Select(rw => rw.WidgetId)
                .Distinct()
                .ToListAsync();
            criteria = wgt => widgetIds.Contains(wgt.Id) || wgt.CreatedById == UserContext.Id;
            return await GetWidgetsByCriteria(criteria, gridOptions);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد موارد استفاده از ویجت داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <returns>تعداد موارد استفاده از ویجت</returns>
        public async Task<int> GetWidgetUsageCountAsync(int widgetId)
        {
            var repository = UnitOfWork.GetAsyncRepository<TabWidget>();
            int usageCount = await repository.GetCountByCriteriaAsync(
                tw => tw.WidgetId == widgetId);
            return usageCount;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت مشخص شده را با توجه به پارامترهای داده شده محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <param name="parameters">پارامترهای مورد نیاز برای تابع محاسباتی ویجت</param>
        /// <returns>اطلاعات مورد نیاز برای نمایش در ویجت</returns>
        public async Task<object> GetWidgetDataAsync(int widgetId, IList<ParameterSummary> parameters)
        {
            var data = default(object);
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var typeName = await repository
                .GetEntityQuery(wgt => wgt.Type)
                .Where(wgt => wgt.Id == widgetId)
                .Select(wgt => wgt.Type.Name)
                .SingleOrDefaultAsync();
            if ((bool)typeName?.StartsWith("Chart"))
            {
                data = await GetChartWidgetDataAsync(widgetId, parameters);
            }
            else if ((bool)typeName?.StartsWith("Gauge"))
            {
                data = await GetGaugeWidgetDataAsync(widgetId, parameters);
            }

            return data;
        }

        /// <summary>
        /// به روش آسنکرون اطلاعات نمایشی ویجت مورد نظر را خوانده و برمی گرداند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <returns>اطلاعات نمایشی ویجت مورد نظر</returns>
        public async Task<WidgetViewModel> GetWidgetAsync(int widgetId)
        {
            var existing = default(WidgetViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var widget = await repository
                .GetEntityQuery(wgt => wgt.Function, wgt => wgt.Type)
                .Include(wgt => wgt.Accounts)
                    .ThenInclude(acc => acc.Account)
                .Include(wgt => wgt.Accounts)
                    .ThenInclude(acc => acc.DetailAccount)
                .Include(wgt => wgt.Accounts)
                    .ThenInclude(acc => acc.CostCenter)
                .Include(wgt => wgt.Accounts)
                    .ThenInclude(acc => acc.Project)
                .Where(wgt => wgt.Id == widgetId)
                .SingleOrDefaultAsync();
            if (widget != null)
            {
                existing = Mapper.Map<WidgetViewModel>(widget);
            }

            return existing;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت داده شده را در دیتابیس ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="widget">ویجت مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات ویجت ایجاد یا اصلاح شده در دیتابیس</returns>
        public async Task<WidgetViewModel> SaveWidgetAsync(WidgetViewModel widget)
        {
            var savedWidget = default(WidgetViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            if (widget.Id == 0)
            {
                var newWidget = Mapper.Map<Widget>(widget);
                newWidget.CreatedById = UserContext.Id;
                await SetPropertyNamesAsync(newWidget);
                OnEntityAction(OperationId.Create);
                Log.Description = Context.Localize(GetState(newWidget));
                repository.Insert(newWidget, wgt => wgt.Accounts);
                await FinalizeActionAsync(newWidget);
                savedWidget = Mapper.Map<WidgetViewModel>(newWidget);
            }
            else
            {
                var existing = await repository.GetByIDWithTrackingAsync(
                    widget.Id, wgt => wgt.Accounts, wgt => wgt.Function, wgt => wgt.Type);
                if (existing != null)
                {
                    existing.Accounts.Clear();
                    string oldState = GetState(existing);
                    OnEntityAction(OperationId.Edit);
                    UpdateExisting(widget, existing);
                    repository.LoadReference(existing, wgt => wgt.Function);
                    repository.LoadReference(existing, wgt => wgt.Type);
                    Log.Description = Context.Localize(
                        String.Format("{0} : ({1}) , {2} : ({3})",
                        AppStrings.Old, Context.Localize(oldState),
                        AppStrings.New, Context.Localize(GetState(existing))));
                    Array.ForEach(widget.Accounts.ToArray(),
                        acc => existing.Accounts.Add(Mapper.Map<WidgetAccount>(acc)));
                    repository.Update(existing, wgt => wgt.Accounts);
                    await FinalizeActionAsync(existing);
                    savedWidget = Mapper.Map<WidgetViewModel>(existing);
                }
            }

            return savedWidget;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت داده شده را از دیتابیس حذف می کند
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر برای حذف</param>
        public async Task DeleteWidgetAsync(int widgetId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var widget = await repository.GetByIDWithTrackingAsync(widgetId, wgt => wgt.Accounts);
            if (widget != null)
            {
                var roleWidgetRepository = UnitOfWork.GetAsyncRepository<RoleWidget>();
                var roleWidgets = await roleWidgetRepository.GetByCriteriaAsync(
                    rw => rw.WidgetId == widgetId);
                Array.ForEach(roleWidgets.ToArray(), rw => roleWidgetRepository.Delete(rw));

                var tabWidgetRepository = UnitOfWork.GetAsyncRepository<TabWidget>();
                var tabWidgets = await tabWidgetRepository.GetByCriteriaAsync(
                    tw => tw.WidgetId == widgetId);
                Array.ForEach(tabWidgets.ToArray(), tw => tabWidgetRepository.Delete(tw));

                widget.Accounts.Clear();
                await SetPropertyNamesAsync(widget);
                await DeleteAsync(repository, widget);
            }
        }

        /// <summary>
        /// به روش آسنکرون، پارامترهای مورد نیاز تابع محاسباتی داده شده را برمی گرداند
        /// </summary>
        /// <param name="functionId">شناسه دیتابیسی تابع محاسباتی مورد نظر</param>
        /// <returns>پارامترهای مورد نیاز تابع محاسباتی</returns>
        public async Task<IList<ParameterSummary>> GetFunctionParametersAsync(int functionId)
        {
            var parameters = new List<ParameterSummary>();
            var repository = UnitOfWork.GetAsyncRepository<WidgetFunction>();
            var function = await repository
                .GetEntityQuery()
                .Include(func => func.Parameters)
                    .ThenInclude(param => param.Parameter)
                .Where(func => func.Id == functionId)
                .SingleOrDefaultAsync();
            if (function != null)
            {
                parameters.AddRange(function.Parameters
                    .Select(param => new ParameterSummary()
                    {
                        Name = param.Parameter.Name,
                        Type = param.Parameter.Type,
                        Value = GetParameterValue(param.Parameter.Name)
                    }));
            }

            return parameters;
        }

        /// <summary>
        /// به روش آسنکرون، نقش های دارای دسترسی به ویجت مورد نظر را خوانده و برمی گرداند 
        /// </summary>
        /// <param name="widgetId">شناسه دیتابیسی ویجت مورد نظر</param>
        /// <returns>نقش های دارای دسترسی به ویجت داده شده</returns>
        public async Task<RelatedItemsViewModel> GetWidgetRolesAsync(int widgetId)
        {
            RelatedItemsViewModel widgetRoles = null;
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var existing = await repository.GetByIDAsync(widgetId, br => br.RoleWidgets);
            if (existing != null)
            {
                UnitOfWork.UseSystemContext();
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                var enabledRoleIds = existing.RoleWidgets.Select(rw => rw.RoleId);
                var enabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                var disabledRoles = await roleRepository
                    .GetEntityQuery()
                    .Where(r => UserContext.Roles.Contains(r.Id) && !enabledRoleIds.Contains(r.Id))
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArrayAsync();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);
                UnitOfWork.UseCompanyContext();

                widgetRoles = new RelatedItemsViewModel() { Id = widgetId };
                Array.ForEach(enabledRoles
                    .Concat(disabledRoles)
                    .OrderBy(item => item.Name)
                    .ToArray(), item => widgetRoles.RelatedItems.Add(item));
            }

            return widgetRoles;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های دارای دسترسی به ویجت مورد نظر را ذخیره می کند
        /// </summary>
        /// <param name="widgetRoles">اطلاعات نمایشی نقش های دارای دسترسی</param>
        public async Task SaveWidgetRolesAsync(RelatedItemsViewModel widgetRoles)
        {
            Verify.ArgumentNotNull(widgetRoles, nameof(widgetRoles));
            var repository = UnitOfWork.GetAsyncRepository<RoleWidget>();
            var existing = await repository.GetByCriteriaAsync(rfp => rfp.WidgetId == widgetRoles.Id);
            if (AreRolesModified(existing, widgetRoles))
            {
                if (existing.Count > 0)
                {
                    RemoveUnassignedRoles(repository, existing, widgetRoles);
                }

                AddNewRoles(repository, existing, widgetRoles);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.RoleAccess);
                Log.Description = await GetWidgetRoleDescriptionAsync(widgetRoles.Id);
                await TrySaveLogAsync();
            }
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                   && left.All(value => right.Contains(value));
        }

        private static bool AreRolesModified(
           IList<RoleWidget> existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing
                .Select(rfp => rfp.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return !AreEqual(existingItems, enabledItems);
        }

        private static void RemoveUnassignedRoles(
            IRepository<RoleWidget> repository, IList<RoleWidget> existing,
            RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing
                .Select(rfp => rfp.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                var removed = existing
                    .Where(rfp => rfp.RoleId == id)
                    .Single();
                repository.Delete(removed);
            }
        }

        private static void AddNewRoles(
            IRepository<RoleWidget> repository, IList<RoleWidget> existing,
            RelatedItemsViewModel roleItems)
        {
            var currentItems = existing.Select(rfp => rfp.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var roleWidget = new RoleWidget()
                {
                    WidgetId = roleItems.Id,
                    RoleId = item.Id
                };
                repository.Insert(roleWidget);
            }
        }
        private async Task<string> GetWidgetRoleDescriptionAsync(int widgetId)
        {
            string description = String.Empty;
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var widget = await repository.GetByIDAsync(widgetId);
            if (widget != null)
            {
                string template = Context.Localize(AppStrings.RolesWithAccessToResource);
                string entity = Context.Localize(AppStrings.Widget).ToLower();
                description = String.Format(template, entity, widget.Title);
            }

            return description;
        }

        #endregion

        #region Data Lookup

        /// <summary>
        /// به روش آسنکرون، اطلاعات توابع محاسباتی قابل استفاده در ویجت ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات توابع محاسباتی</returns>
        public async Task<List<WidgetFunctionViewModel>> GetWidgetFunctionsLookupAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<WidgetFunction>();
            return await repository
                .GetEntityQuery()
                .Select(func => Mapper.Map<WidgetFunctionViewModel>(func))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات انواع نمودارهای قابل استفاده در ویجت ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات انواع نمودارها</returns>
        public async Task<List<WidgetTypeViewModel>> GetWidgetTypesLookupAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<WidgetType>();
            return await repository
                .GetEntityQuery()
                .Select(type => Mapper.Map<WidgetTypeViewModel>(type))
                .ToListAsync();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات ویجت های قابل دسترسی توسط کاربر جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات ویجت های قابل دسترسی</returns>
        public async Task<List<WidgetViewModel>> GetWidgetsLookupAsync()
        {
            var lookup = await GetAccessibleWidgetsAsync();
            return lookup.Items;
        }

        #endregion

        internal override int? EntityType => (int?)EntityTypeId.Widget;

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="widget">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="existing">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(WidgetViewModel widget, Widget existing)
        {
            existing.Title = widget.Title;
            existing.TypeId = widget.TypeId;
            existing.FunctionId = widget.FunctionId;
            existing.Description = widget.Description;
            existing.DefaultSettings = widget.DefaultSettings;
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="tab">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="existing">سطر اطلاعاتی موجود</param>
        protected static void UpdateExisting(DashboardTabViewModel tab, DashboardTab existing)
        {
            existing.Title = tab.Title;
            existing.Index = tab.Index;
        }

        /// <summary>
        /// اطلاعات خلاصه ویجت داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از ویجت های موجود</param>
        /// <returns>اطلاعات خلاصه ویجت داده شده به صورت رشته متنی</returns>
        protected override string GetState(Widget entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7}",
                    AppStrings.Title, entity.Title, AppStrings.FunctionName, entity.Function?.Name,
                    AppStrings.TypeName, entity.Type?.Name, AppStrings.Description, entity.Description)
                : null;
        }

        private class AccountByWidget
        {
            public int WidgetId { get; set; }

            public FullAccountViewModel FullAccount { get; set; }
        }

        private class ParameterByWidget
        {
            public int WidgetId { get; set; }

            public FunctionParameterViewModel Parameter { get; set; }
        }

        private class WidgetFunctionValues
        {
            public string XLabel { get; set; }

            public DateTime FromDate { get; set; }

            public DateTime ToDate { get; set; }
        }

        private enum ExpressionUsage
        {
            Select = 0,
            Where = 1,
            GroupBy = 2
        }

        private IConfigRepository Config { get; }

        private static string GetState(DashboardTab tab)
        {
            return (tab != null)
                ? $"{AppStrings.Title} : {tab.Title} , {AppStrings.No} : {tab.Index}"
                : null;
        }

        private object GetParameterValue(string name)
        {
            object value = null;
            if (name == "FromDate" || name == "ToDate")
            {
                Config.GetCurrentFiscalDateRange(out DateTime from, out DateTime to);
                value = name == "FromDate" ? from : to;
            }
            else if (name == "DateUnit")
            {
                value = (int)WidgetDateUnit.Monthly;
            }
            else if (name == "MinValue" || name == "MaxValue")
            {
                value = name == "MinValue" ? 0 : 100;
            }

            return value;
        }

        private async Task SetPropertyNamesAsync(Widget widget)
        {
            var functionRepository = UnitOfWork.GetAsyncRepository<WidgetFunction>();
            widget.Function = new WidgetFunction() { Id = widget.FunctionId };
            widget.Function.Name = await functionRepository
                .GetEntityQuery()
                .Where(func => func.Id == widget.FunctionId)
                .Select(func => func.Name)
                .SingleOrDefaultAsync();

            var typeRepository = UnitOfWork.GetAsyncRepository<WidgetType>();
            widget.Type = new WidgetType() { Id = widget.TypeId };
            widget.Type.Name = await typeRepository
                .GetEntityQuery()
                .Where(type => type.Id == widget.TypeId)
                .Select(type => type.Name)
                .SingleOrDefaultAsync();
        }

        private async Task<ChartSeriesViewModel> GetChartWidgetDataAsync(
            int widgetId, IList<ParameterSummary> parameters)
        {
            var dataSeries = default(ChartSeriesViewModel);
            GetChartWidgetParameters(parameters, out DateTime from, out DateTime to, out WidgetDateUnit unit);
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var widget = await repository
                .GetEntityWithTrackingQuery(wgt => wgt.Function, wgt => wgt.Accounts)
                .Where(wgt => wgt.Id == widgetId)
                .FirstOrDefaultAsync();
            if (widget != null)
            {
                var functionName = widget.Function.Name;
                DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
                if (functionName.StartsWith("Function_"))
                {
                    var accountRepository = UnitOfWork.GetAsyncRepository<WidgetAccount>();
                    var fullAccounts = GetFullAccounts(widget.Accounts, accountRepository);
                    var values = await GetTurnoverFunctionValuesAsync(from, to, unit);
                    dataSeries = GetBasicFunctionData(functionName, values, fullAccounts);
                }
                else
                {
                    var values = functionName.StartsWith("FunctionXT")
                        ? await GetTurnoverFunctionValuesAsync(from, to, unit)
                        : await GetBalanceFunctionValuesAsync(from, to, unit);
                    dataSeries = GetSpecialFunctionData(functionName, values);
                }
            }

            return dataSeries;
        }

        private void GetChartWidgetParameters(
            IList<ParameterSummary> parameters, out DateTime from, out DateTime to,
            out WidgetDateUnit unit)
        {
            Config.GetCurrentFiscalDateRange(out from, out to);
            unit = WidgetDateUnit.Monthly;
            var param = parameters
                .Where(p => p.Name == "FromDate")
                .FirstOrDefault();
            if (param != null)
            {
                from = Convert.ToDateTime(param.Value);
            }

            param = parameters
                .Where(p => p.Name == "ToDate")
                .FirstOrDefault();
            if (param != null)
            {
                to = Convert.ToDateTime(param.Value);
            }

            param = parameters
                .Where(p => p.Name == "DateUnit")
                .FirstOrDefault();
            if (param != null)
            {
                unit = (WidgetDateUnit)Convert.ToInt32(param.Value);
            }
        }

        private async Task<GaugeDataViewModel> GetGaugeWidgetDataAsync(
            int widgetId, IList<ParameterSummary> parameters)
        {
            var gaugeData = default(GaugeDataViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var widget = await repository.GetByIDAsync(widgetId, wgt => wgt.Function);
            if (widget != null)
            {
                GetGaugeWidgetParameters(
                    parameters, out DateTime from, out DateTime to, out decimal min, out decimal max);
                var evaluator = GetFunctionEvaluator(widget.Function.Name);
                gaugeData = new GaugeDataViewModel()
                {
                    MinValue = min,
                    MaxValue = max,
                    Value = evaluator(from, to)
                };
            }

            return gaugeData;
        }

        private void GetGaugeWidgetParameters(
            IList<ParameterSummary> parameters, out DateTime from, out DateTime to, out decimal min, out decimal max)
        {
            Config.GetCurrentFiscalDateRange(out from, out to);
            min = WidgetConstants.GaugeMinValue;
            max = WidgetConstants.GaugeMaxValue;
            var param = parameters
                .Where(p => p.Name == "FromDate")
                .FirstOrDefault();
            if (param != null)
            {
                from = Convert.ToDateTime(param.Value);
            }

            param = parameters
                .Where(p => p.Name == "ToDate")
                .FirstOrDefault();
            if (param != null)
            {
                to = Convert.ToDateTime(param.Value);
            }

            param = parameters
                .Where(p => p.Name == "MinValue")
                .FirstOrDefault();
            if (param != null)
            {
                min = Convert.ToDecimal(param.Value);
            }

            param = parameters
                .Where(p => p.Name == "MaxValue")
                .FirstOrDefault();
            if (param != null)
            {
                max = Convert.ToDecimal(param.Value);
            }
        }

        private DashboardViewModel GetDashboard(DataTable dashboardResult)
        {
            var dashboard = DashboardFromResult(dashboardResult);
            if (dashboard != null)
            {
                var query = String.Format(DashboardQuery.CurrentDashboardWidgets, dashboard.Id);
                var widgetResult = DbConsole.ExecuteQuery(query);
                var tabWidgets = widgetResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new TabWidgetViewModel()
                    {
                        Id = _report.ValueOrDefault<int>(row, "TabWidgetID"),
                        TabId = _report.ValueOrDefault<int>(row, "TabID"),
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        WidgetTitle = _report.ValueOrDefault(row, "Title"),
                        WidgetDescription = _report.ValueOrDefault(row, "Description"),
                        Settings = _report.ValueOrDefault(row, "Settings"),
                        DefaultSettings = _report.ValueOrDefault(row, "DefaultSettings")
                    })
                    .ToList();

                LoadWidgetDetails(tabWidgets);
                var widgetIds = tabWidgets
                    .Select(tw => tw.WidgetId)
                    .Distinct();
                var accounts = GetWidgetAccounts(widgetIds);
                var parameters = GetWidgetParameters(widgetIds);
                foreach (var tab in dashboard.Tabs)
                {
                    tab.Widgets.AddRange(tabWidgets.Where(tw => tw.TabId == tab.Id));
                    foreach (var widget in tab.Widgets)
                    {
                        widget.WidgetAccounts.AddRange(accounts
                            .Where(wacc => wacc.WidgetId == widget.WidgetId)
                            .Select(wacc => wacc.FullAccount));
                        widget.WidgetParameters.AddRange(parameters
                            .Where(wpara => wpara.WidgetId == widget.WidgetId)
                            .Select(wpara => wpara.Parameter));
                    }
                }
            }

            return dashboard;
        }

        private DashboardViewModel DashboardFromResult(DataTable dashboardResult)
        {
            var dashboard = default(DashboardViewModel);
            if (dashboardResult.Rows.Count > 0)
            {
                int dashboardId = _report.ValueOrDefault<int>(dashboardResult.Rows[0], "DashboardID");
                dashboard = new DashboardViewModel()
                {
                    Id = dashboardId,
                    UserId = UserContext.Id
                };
                dashboard.Tabs.AddRange(dashboardResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new DashboardTabViewModel()
                    {
                        Id = _report.ValueOrDefault<int>(row, "TabID"),
                        DashboardId = dashboardId,
                        Index = _report.ValueOrDefault<int>(row, "TabIndex"),
                        Title = _report.ValueOrDefault(row, "TabTitle")
                    }));
            }

            return dashboard;
        }

        private void LoadWidgetDetails(List<TabWidgetViewModel> tabWidgets)
        {
            var widgetIds = tabWidgets
                .Select(tw => tw.WidgetId)
                .Distinct();
            if (widgetIds.Any())
            {
                var query = String.Format(DashboardQuery.WidgetDetails, String.Join(',', widgetIds));
                var detailsResult = DbConsole.ExecuteQuery(query);
                var details = detailsResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new TabWidgetViewModel()
                    {
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        WidgetFunctionId = _report.ValueOrDefault<int>(row, "FunctionID"),
                        WidgetTypeId = _report.ValueOrDefault<int>(row, "TypeID"),
                        WidgetFunctionName = _report.ValueOrDefault(row, "FunctionName"),
                        WidgetTypeName = _report.ValueOrDefault(row, "TypeName")
                    });
                foreach (var group in details.GroupBy(w => w.WidgetId))
                {
                    var widget = group.First();
                    Array.ForEach(tabWidgets
                        .Where(item => item.WidgetId == group.Key)
                        .ToArray(), tw =>
                    {
                        tw.WidgetFunctionId = widget.WidgetFunctionId;
                        tw.WidgetFunctionName = widget.WidgetFunctionName;
                        tw.WidgetTypeId = widget.WidgetTypeId;
                        tw.WidgetTypeName = widget.WidgetTypeName;
                    });
                }
            }
        }

        private IEnumerable<AccountByWidget> GetWidgetAccounts(IEnumerable<int> widgetIds)
        {
            var accounts = new List<AccountByWidget>();
            if (widgetIds.Any())
            {
                var query = String.Format(DashboardQuery.WidgetsAccounts, String.Join(',', widgetIds));
                var accountsResult = DbConsole.ExecuteQuery(query);
                accounts.AddRange(accountsResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new AccountByWidget
                    {
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        FullAccount = new FullAccountViewModel()
                        {
                            Account = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "AccountID"),
                                Name = _report.ValueOrDefault(row, "AccountName"),
                                FullCode = _report.ValueOrDefault(row, "AccountFullCode")
                            },
                            DetailAccount = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "DetailAccountID"),
                                Name = _report.ValueOrDefault(row, "DetailAccountName"),
                                FullCode = _report.ValueOrDefault(row, "DetailAccountFullCode")
                            },
                            CostCenter = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "CostCenterID"),
                                Name = _report.ValueOrDefault(row, "CostCenterName"),
                                FullCode = _report.ValueOrDefault(row, "CostCenterFullCode")
                            },
                            Project = new AccountItemBriefViewModel()
                            {
                                Id = _report.ValueOrDefault<int>(row, "ProjectID"),
                                Name = _report.ValueOrDefault(row, "ProjectName"),
                                FullCode = _report.ValueOrDefault(row, "ProjectFullCode")
                            }
                        }
                    }));
            }

            return accounts;
        }

        private IEnumerable<ParameterByWidget> GetWidgetParameters(IEnumerable<int> widgetIds)
        {
            var parameters = new List<ParameterByWidget>();
            if (widgetIds.Any())
            {

                var query = String.Format(DashboardQuery.WidgetsParameters, String.Join(',', widgetIds));
                var parametersResult = DbConsole.ExecuteQuery(query);
                parameters.AddRange(parametersResult.Rows
                    .Cast<DataRow>()
                    .Select(row => new ParameterByWidget
                    {
                        WidgetId = _report.ValueOrDefault<int>(row, "WidgetID"),
                        Parameter = new FunctionParameterViewModel()
                        {
                            Name = _report.ValueOrDefault(row, "Name"),
                            Alias = _report.ValueOrDefault(row, "Alias"),
                            Type = _report.ValueOrDefault(row, "Type"),
                            DefaultValue = _report.ValueOrDefault(row, "DefaultValue"),
                            Description = _report.ValueOrDefault(row, "Description")
                        }
                    }));
            }

            return parameters;
        }

        private IList<FullAccountViewModel> GetFullAccounts(
            IList<WidgetAccount> accounts, IRepository<WidgetAccount> repository)
        {
            var fullAccounts = new List<FullAccountViewModel>();
            foreach (var account in accounts)
            {
                var fullAccount = new FullAccountViewModel();
                if (account.AccountId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.Account);
                    fullAccount.Account = Mapper.Map<AccountItemBriefViewModel>(account.Account);
                }

                if (account.DetailAccountId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.DetailAccount);
                    fullAccount.DetailAccount = Mapper.Map<AccountItemBriefViewModel>(account.DetailAccount);
                }

                if (account.CostCenterId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.CostCenter);
                    fullAccount.CostCenter = Mapper.Map<AccountItemBriefViewModel>(account.CostCenter);
                }

                if (account.ProjectId.HasValue)
                {
                    repository.LoadReference(account, acc => acc.Project);
                    fullAccount.Project = Mapper.Map<AccountItemBriefViewModel>(account.Project);
                }

                fullAccounts.Add(fullAccount);
            }

            return fullAccounts;
        }

        private async Task<IList<WidgetFunctionValues>> GetTurnoverFunctionValuesAsync(
            DateTime from, DateTime to, WidgetDateUnit unit)
        {
            var values = new List<WidgetFunctionValues>();
            var calendar = await Config.GetCurrentCalendarAsync();
            var enumerator = new DateSpanEnumerator(from, to, calendar);
            if (unit == WidgetDateUnit.Monthly)
            {
                values.AddRange(enumerator
                    .GetMonths()
                    .Select(month => new WidgetFunctionValues()
                    {
                        XLabel = Context.Localize(month.Name),
                        FromDate = month.Start,
                        ToDate = month.End
                    }));
            }
            else if (unit == WidgetDateUnit.Weekly)
            {
                values.AddRange(enumerator
                    .GetWeeks()
                    .Select(week => new WidgetFunctionValues()
                    {
                        XLabel = String.Format(Context.Localize(AppStrings.WeekX), week.Name),
                        FromDate = week.Start,
                        ToDate = week.End
                    }));
            }

            return values;
        }

        private async Task<IList<WidgetFunctionValues>> GetBalanceFunctionValuesAsync(
            DateTime from, DateTime to, WidgetDateUnit unit)
        {
            var values = new List<WidgetFunctionValues>();
            var calendar = await Config.GetCurrentCalendarAsync();
            Config.GetCurrentFiscalDateRange(out DateTime startDate, out DateTime _);
            var enumerator = new DateSpanEnumerator(from, to, calendar);
            if (unit == WidgetDateUnit.Monthly)
            {
                values.AddRange(enumerator
                    .GetMonths()
                    .Select(month => new WidgetFunctionValues()
                    {
                        XLabel = Context.Localize(month.Name),
                        FromDate = startDate,
                        ToDate = month.End
                    }));
            }
            else if (unit == WidgetDateUnit.Weekly)
            {
                values.AddRange(enumerator
                    .GetWeeks()
                    .Select(week => new WidgetFunctionValues()
                    {
                        XLabel = String.Format(Context.Localize(AppStrings.WeekX), week.Name),
                        FromDate = startDate,
                        ToDate = week.End
                    }));
            }

            return values;
        }

        private static string GetFullAccountLabel(FullAccountViewModel fullAccount)
        {
            return $"{fullAccount.Account.FullCode}-{fullAccount.DetailAccount.FullCode}-" +
                $"{fullAccount.CostCenter.FullCode}-{fullAccount.Project.FullCode}";
        }

        private string GetFullAccountExpressions(FullAccountViewModel fullAccount, ExpressionUsage usage)
        {
            var selectList = new List<string>();
            if (fullAccount.Account.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.Account, fullAccount.Account.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([acc].[FullCode], 1, {codeLength}) AS [Account]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([acc].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([acc].[FullCode], 1, {codeLength}) = '{fullAccount.Account.FullCode}'");
                selectList.Add(selectItem);
            }

            if (fullAccount.DetailAccount.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.DetailAccount, fullAccount.DetailAccount.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([facc].[FullCode], 1, {codeLength}) AS [DetailAccount]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([facc].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([facc].[FullCode], 1, {codeLength}) = '{fullAccount.DetailAccount.FullCode}'");
                selectList.Add(selectItem);
            }

            if (fullAccount.CostCenter.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.CostCenter, fullAccount.CostCenter.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([cc].[FullCode], 1, {codeLength}) AS [CostCenter]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([cc].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([cc].[FullCode], 1, {codeLength}) = '{fullAccount.CostCenter.FullCode}'");
                selectList.Add(selectItem);
            }

            if (fullAccount.Project.Id > 0)
            {
                var codeLength = Config.GetLevelCodeLength(ViewId.Project, fullAccount.Project.Level);
                var selectItem = usage == ExpressionUsage.Select
                    ? $"SUBSTRING([prj].[FullCode], 1, {codeLength}) AS [Project]"
                    : (usage == ExpressionUsage.GroupBy
                        ? $"SUBSTRING([prj].[FullCode], 1, {codeLength})"
                        : $"SUBSTRING([prj].[FullCode], 1, {codeLength}) = '{fullAccount.Project.FullCode}'");
                selectList.Add(selectItem);
            }

            return usage == ExpressionUsage.Where
                ? String.Join(" AND ", selectList)
                : String.Join(",", selectList);
        }

        private async Task<int> GetDashboardIdAsync(int tabId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
            return await repository
                .GetEntityQuery()
                .Where(tab => tab.Id == tabId)
                .Select(tab => tab.DashboardId)
                .SingleOrDefaultAsync();
        }

        private int GetNextTabIndex(int dashboardId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
            var lastIndex = repository
                .GetEntityQuery()
                .Where(tab => tab.DashboardId == dashboardId)
                .Max(tab => tab.Index);
            return lastIndex + 1;
        }

        private async Task InsertTabAsync(IRepository<DashboardTab> repository, DashboardTab tab)
        {
            OnEntityAction(OperationId.Create);
            Log.EntityTypeId = (int)EntityTypeId.DashboardTab;
            Log.Description = Context.Localize(GetState(tab));
            repository.Insert(tab);
            await UnitOfWork.CommitAsync();
            Log.EntityId = tab.Id;
            await TrySaveLogAsync();
        }

        private async Task DeleteTabAsync(DashboardTab tab)
        {
            OnEntityAction(OperationId.Delete);
            Log.Description = Context.Localize(GetState(tab));
            await UnitOfWork.CommitAsync();
            await TrySaveLogAsync();
        }

        private async Task<PagedList<WidgetViewModel>> GetWidgetsByCriteria(
            Expression<Func<Widget, bool>> criteria, GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            var userWidgets = await repository.GetByCriteriaAsync(
                criteria, wgt => wgt.Function, wgt => wgt.Type);
            var widgets = userWidgets
                .Select(wgt => Mapper.Map<WidgetViewModel>(wgt))
                .ToList();
            await SetUserFullNamesAsync(widgets);
            return new PagedList<WidgetViewModel>(widgets, gridOptions);
        }

        private async Task SetUserFullNamesAsync(List<WidgetViewModel> widgets)
        {
            UnitOfWork.UseSystemContext();
            var userIds = widgets
                .Select(wgt => wgt.CreatedById)
                .Distinct();
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var userMap = new Dictionary<int, string>(await repository
                .GetEntityQuery(usr => usr.Person)
                .Where(usr => userIds.Contains(usr.Id))
                .Select(usr => new KeyValuePair<int, string>(
                    usr.Id, $"{usr.Person.LastName}, {usr.Person.FirstName}"))
                .ToListAsync());
            Array.ForEach(widgets.ToArray(), widget =>
            {
                if (userMap.ContainsKey(widget.CreatedById))
                {
                    widget.CreatedByFullName = userMap[widget.CreatedById];
                }
            });
            UnitOfWork.UseCompanyContext();
        }
    }
}
