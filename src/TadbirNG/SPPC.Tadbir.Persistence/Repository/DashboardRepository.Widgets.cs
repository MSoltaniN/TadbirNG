using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Domain.Reporting;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
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
        /// به روش آسنکرون، اطلاعات نمایشی یکی از برگه های موجود را خوانده و برمی گرداند
        /// </summary>
        /// <param name="tabId">شناسه دیتابیسی برگه مورد نظر</param>
        /// <returns>اطلاعات نمایشی برگه مورد نظر</returns>
        public async Task<DashboardTabViewModel> GetDashboardTabAsync(int tabId)
        {
            var tab = default(DashboardTabViewModel);
            var repository = UnitOfWork.GetAsyncRepository<Dashboard>();
            var existing = await repository
                .GetEntityQuery(dbd => dbd.Tabs)
                .Where(dbd => dbd.UserId == UserContext.Id)
                .Select(dbd => dbd.Tabs.Where(tab => tab.Id == tabId).FirstOrDefault())
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
                repository.Insert(newTab);
                await UnitOfWork.CommitAsync();
                tab.Id = newTab.Id;
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

            var repository = UnitOfWork.GetAsyncRepository<Dashboard>();
            int dashboardId = tabs
                .Select(tab => tab.DashboardId)
                .First();
            var dashboard = await repository.GetByIDWithTrackingAsync(dashboardId, dbd => dbd.Tabs);
            if (dashboard != null)
            {
                dashboard.Tabs.Clear();
                dashboard.Tabs.AddRange(tabs.Select(tab => Mapper.Map<DashboardTab>(tab)));
                repository.Update(dashboard);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، برگه مشخص شده را در دیتابیس حذف می کند
        /// </summary>
        /// <param name="tabId">شناسه دستابیسی برگه مورد نظر برای حذف</param>
        public async Task DeleteDashboardTabAsync(int tabId)
        {
            var repository = UnitOfWork.GetAsyncRepository<DashboardTab>();
            var existing = await repository.GetByIDAsync(tabId);
            if (existing != null)
            {
                repository.Delete(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، یکی از ویجت های قابل دسترسی توسط کاربر جاری را در برگه تعیین شده اضافه یا اصلاح می کند
        /// </summary>
        /// <param name="tabWidget">اطلاعات ویجت مورد نظر برای ایجاد یا اصلاح به برگه داشبورد</param>
        /// <returns>آخرین اطلاعات ویجت اضافه یا اصلاح شده در برگه داشبورد</returns>
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

            return null;
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
            if (!UserContext.Roles.Contains(AppConstants.AdminRoleId))
            {
                var roleWidgetRepository = UnitOfWork.GetAsyncRepository<RoleWidget>();
                var widgetIds = await roleWidgetRepository
                    .GetEntityQuery()
                    .Where(rw => UserContext.Roles.Contains(rw.RoleId))
                    .Select(rw => rw.WidgetId)
                    .Distinct()
                    .ToListAsync();
                criteria = wgt => widgetIds.Contains(wgt.Id) || wgt.CreatedById == UserContext.Id;
            }

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
                    await SetPropertyNamesAsync(existing);
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
            var repository = UnitOfWork.GetAsyncRepository<Widget>();
            return await repository
                .GetEntityQuery(widget => widget.Function, widget => widget.Type)
                .Select(type => Mapper.Map<WidgetViewModel>(type))
                .ToListAsync();
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
            if (unit == WidgetDateUnit.Monthly)
            {
                var enumerator = new MonthEnumerator(from, to, calendar);
                values.AddRange(enumerator
                    .GetMonths()
                    .Select(month => new WidgetFunctionValues()
                    {
                        XLabel = Context.Localize(month.Name),
                        FromDate = month.Start,
                        ToDate = month.End
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
            if (unit == WidgetDateUnit.Monthly)
            {
                var enumerator = new MonthEnumerator(from, to, calendar);
                values.AddRange(enumerator
                    .GetMonths()
                    .Select(month => new WidgetFunctionValues()
                    {
                        XLabel = Context.Localize(month.Name),
                        FromDate = startDate,
                        ToDate = month.End
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
