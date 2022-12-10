using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Filters;
using io = System.IO;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class DashboardController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="pathProvider">مسیرهای فایل های کاربردی مورد نیاز سرویس وب را فراهم می کند</param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public DashboardController(IDashboardRepository repository, IApiPathProvider pathProvider,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _pathProvider = pathProvider;
        }

        #region Dashboard Management

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/current
        [HttpGet]
        [Route(DashboardApi.CurrentDashboardUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public IActionResult GetCurrentDashboard()
        {
            var dashboard = _repository.GetCurrentUserDashboard();
            Localize(dashboard);
            return Json(dashboard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // POST: api/dashboard/new
        [HttpPost]
        [Route(DashboardApi.NewDashboardUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> PostNewDashboardAsync([FromBody] TabWidgetViewModel widget)
        {
            var result = GetTabWidgetValidationResult(widget, 0);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsCurrentDashboardCreatedAsync())
            {
                return BadRequestResult(_strings[AppStrings.OnlyOneDashboardAllowedPerUser]);
            }

            var dashboard = await _repository.CreateCurrentUserDashboardAsync(widget);
            Localize(dashboard);
            return Json(dashboard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        // GET: api/dashboard/tabs/{tabId:min(1)}
        [HttpGet]
        [Route(DashboardApi.DashboardTabUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> GetDashboardTabAsync(int tabId)
        {
            var tab = await _repository.GetDashboardTabAsync(tabId);
            return JsonReadResult(tab);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        // POST: api/dashboard/tabs
        [HttpPost]
        [Route(DashboardApi.DashboardTabsUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> PostNewDashboardTabAsync([FromBody] DashboardTabViewModel tab)
        {
            var result = GetDashboardTabValidationResult(tab);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var created = await _repository.SaveDashboardTabAsync(tab);
            return StatusCode(StatusCodes.Status201Created, created);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="tab"></param>
        /// <returns></returns>
        // PUT: api/dashboard/tabs/{tabId:min(1)}
        [HttpPut]
        [Route(DashboardApi.DashboardTabUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> PutModifiedDashboardTabAsync(
            int tabId, [FromBody] DashboardTabViewModel tab)
        {
            var result = GetDashboardTabValidationResult(tab, tabId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var edited = await _repository.SaveDashboardTabAsync(tab);
            return Ok(edited);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns></returns>
        // PUT: api/dashboard/tabs
        [HttpPut]
        [Route(DashboardApi.DashboardTabsUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> PutModifiedDashboardTabsAsync(
            [FromBody] IList<DashboardTabViewModel> tabs)
        {
            var result = GetDashboardTabsValidationResult(tabs);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveDashboardTabsAsync(tabs);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        // DELETE: api/dashboard/tabs/{tabId:min(1)}
        [HttpDelete]
        [Route(DashboardApi.DashboardTabUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> DeleteExistingDashboardTabAsync(int tabId)
        {
            var existing = await _repository.GetDashboardTabAsync(tabId);
            if (existing == null)
            {
                var message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.DashboardTab, tabId.ToString());
                return BadRequestResult(message);
            }

            int count = await _repository.GetTabWidgetCountAsync(tabId);
            if (count > 0)
            {
                string message = _strings[AppStrings.CannotDeleteUsedDashboardTab];
                return BadRequestResult(message);
            }

            if (await _repository.IsSoleDashboardTab(tabId))
            {
                string message = _strings[AppStrings.CannotDeleteLastDashboardTab];
                return BadRequestResult(message);
            }

            await _repository.DeleteDashboardTabAsync(tabId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="tabWidget"></param>
        /// <returns></returns>
        // POST: api/dashboard/tabs/{tabId:min(1)}/widgets
        [HttpPost]
        [Route(DashboardApi.TabWidgetsUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> PostNewTabWidgetAsync(
            int tabId, [FromBody] TabWidgetViewModel tabWidget)
        {
            var result = GetTabWidgetValidationResult(tabWidget, tabId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var posted = await _repository.SaveTabWidgetAsync(tabWidget);
            return StatusCode(StatusCodes.Status201Created, posted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabWidgets"></param>
        /// <returns></returns>
        // PUT: api/dashboard/tabs/widgets
        [HttpPut]
        [Route(DashboardApi.AllTabWidgetsUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> PutModofiedTabWidgetsAsync(
            [FromBody] IList<TabWidgetViewModel> tabWidgets)
        {
            if (tabWidgets == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Widget));
            }

            await _repository.SaveTabWidgetsAsync(tabWidgets);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <param name="widgetId"></param>
        /// <returns></returns>
        // DELETE: api/dashboard/tabs/{tabId:min(1)}/widgets/{widgetId:min(1)}
        [HttpDelete]
        [Route(DashboardApi.TabWidgetUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> DeleteExistingTabWidgetAsync(int tabId, int widgetId)
        {
            await _repository.DeleteTabWidgetAsync(tabId, widgetId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        #endregion

        #region Widget Management

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/widgets
        [HttpGet]
        [Route(DashboardApi.WidgetsUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageWidgets)]
        public async Task<IActionResult> GetUserWidgetsAsync()
        {
            var userWidgets = await _repository.GetCurrentUserWidgetsAsync(GridOptions);
            return JsonListResult(userWidgets);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/widgets/all
        [HttpGet]
        [Route(DashboardApi.AllWidgetsUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageWidgets)]
        public async Task<IActionResult> GetAccessibleWidgetsAsync()
        {
            var allWidgets = await _repository.GetAccessibleWidgetsAsync(GridOptions);
            return JsonListResult(allWidgets);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widgetId"></param>
        /// <returns></returns>
        // GET: api/dashboard/widgets/{widgetId:min(1)}/usage
        [HttpGet]
        [Route(DashboardApi.WidgetUsageUrl)]
        public async Task<int> QueryWidgetUsageAsync(int widgetId)
        {
            return await _repository.GetWidgetUsageCountAsync(widgetId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/widgets/{widgetId:min(1)}/data
        [HttpGet]
        [Route(DashboardApi.WidgetDataUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> GetWidgetDataAsync(int widgetId)
        {
            var parameters = GetParameters();
            var widgetData = await _repository.GetWidgetDataAsync(widgetId, parameters);
            if (widgetData is ChartSeriesViewModel chartData)
            {
                Array.ForEach(chartData.Datasets.ToArray(), dataset => dataset.Label = _strings[dataset.Label]);
                return Json(chartData);
            }

            return Json(widgetData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widgetId"></param>
        /// <returns></returns>
        // GET: api/dashboard/widgets/{widgetId:min(1)}
        [HttpGet]
        [Route(DashboardApi.WidgetUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageWidgets)]
        public async Task<IActionResult> GetWidgetAsync(int widgetId)
        {
            var widget = await _repository.GetWidgetAsync(widgetId);
            Localize(widget);
            return JsonReadResult(widget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widget"></param>
        /// <returns></returns>
        // POST: api/dashboard/widgets
        [HttpPost]
        [Route(DashboardApi.Widgets)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageWidgets)]
        public async Task<IActionResult> PostNewWidgetAsync([FromBody] WidgetViewModel widget)
        {
            var result = GetWidgetValidationResult(widget);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var savedWidget = await _repository.SaveWidgetAsync(widget);
            return StatusCode(StatusCodes.Status201Created, savedWidget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widgetId"></param>
        /// <param name="widget"></param>
        /// <returns></returns>
        // PUT: api/dashboard/widgets/{widgetId:min(1)}
        [HttpPut]
        [Route(DashboardApi.WidgetUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageWidgets)]
        public async Task<IActionResult> PutModifiedWidgetAsync(int widgetId, [FromBody] WidgetViewModel widget)
        {
            var result = GetWidgetValidationResult(widget, widgetId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var savedWidget = await _repository.SaveWidgetAsync(widget);
            return Ok(savedWidget);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widgetId"></param>
        /// <param name="confirmed">مشخص می کند که در صورت استفاده شدن از ویجت مورد نظر برای حذف، کاربر عمل حذف
        /// ویجت استفاده شده را تایید کرده یا نه؟</param>
        /// <returns></returns>
        // DELETE: api/dashboard/widgets/{widgetId:min(1)}
        [HttpDelete]
        [Route(DashboardApi.WidgetUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageWidgets)]
        public async Task<IActionResult> DeleteExistingWidgetAsync(int widgetId, bool confirmed = false)
        {
            var existing = await _repository.GetWidgetAsync(widgetId);
            if (existing == null)
            {
                var message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.Widget, widgetId.ToString());
                return BadRequestResult(message);
            }

            if (existing.CreatedById != SecurityContext.User.Id)
            {
                var message = _strings[AppStrings.CannotModifyOtherUserWidget];
                return BadRequestResult(message);
            }

            int usageCount = await _repository.GetWidgetUsageCountAsync(widgetId);
            if (usageCount > 0 && !confirmed)
            {
                string message = _strings[AppStrings.ConfirmUsedWidgetDelete];
                return Ok(message);
            }

            await _repository.DeleteWidgetAsync(widgetId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        // GET: api/dashboard/functions/{functionId:min(1)}/params
        [HttpGet]
        [Route(DashboardApi.FunctionParametersUrl)]
        public async Task<IActionResult> GetFunctionParametersAsync(int functionId)
        {
            var parameters = await _repository.GetFunctionParametersAsync(functionId);
            return Json(parameters);
        }

        #endregion

        #region Data Lookup

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/lookup/functions
        [HttpGet]
        [Route(DashboardApi.WidgetFunctionsLookupUrl)]
        public async Task<IActionResult> GetWidgetFunctionsLookupAsync()
        {
            var lookup = await _repository.GetWidgetFunctionsLookupAsync();
            Array.ForEach(lookup.ToArray(), item => item.Name = _strings[item.Name]);
            return Json(lookup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/lookup/wtypes
        [HttpGet]
        [Route(DashboardApi.WidgetTypesLookupUrl)]
        public async Task<IActionResult> GetWidgetTypesLookupAsync()
        {
            var lookup = await _repository.GetWidgetTypesLookupAsync();
            Array.ForEach(lookup.ToArray(), item => item.Name = _strings[item.Name]);
            return Json(lookup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/lookup/widgets
        [HttpGet]
        [Route(DashboardApi.WidgetsLookupUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> GetWidgetsLookupAsync()
        {
            var lookup = await _repository.GetWidgetsLookupAsync();
            Array.ForEach(lookup.ToArray(), widget =>
            {
                Localize(widget);
            });
            return Json(lookup);
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/license
        [HttpGet]
        [Route(DashboardApi.LicenseInfoUrl)]
        public IActionResult GetLicenseInfo()
        {
            string licenseData = io::File.ReadAllText(_pathProvider.License);
            var license = JsonHelper.To<LicenseFileModel>(licenseData);
            return Json(license);
        }

        private List<ParameterSummary> GetParameters()
        {
            var parameters = new List<ParameterSummary>();
            if (Request.Headers.ContainsKey(AppConstants.ParametersHeaderName))
            {
                var json = Encoding.UTF8.GetString(
                    Transform.FromBase64String(Request.Headers[AppConstants.ParametersHeaderName]));
                parameters = JsonHelper.To<List<ParameterSummary>>(json);
            }

            return parameters;
        }

        private IActionResult GetDashboardTabValidationResult(DashboardTabViewModel tab, int tabId = 0)
        {
            if (tab == null)
            {
                var message = _strings.Format(AppStrings.RequestFailedNoData, AppStrings.DashboardTab);
                return BadRequestResult(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            if (tab.Id != tabId)
            {
                var message = _strings.Format(AppStrings.RequestFailedConflict, AppStrings.DashboardTab);
                return BadRequestResult(message);
            }

            return Ok();
        }

        private IActionResult GetDashboardTabsValidationResult(IList<DashboardTabViewModel> tabs)
        {
            if (tabs == null)
            {
                var message = _strings.Format(AppStrings.RequestFailedNoData, AppStrings.DashboardTab);
                return BadRequestResult(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            var indices = String.Join(String.Empty, Enumerable.Range(1, tabs.Count));
            var tabIndices = String.Join(String.Empty,
                tabs.OrderBy(tab => tab.Index)
                    .Select(tab => tab.Index)
                    .ToArray());
            if (indices != tabIndices)
            {
                return BadRequestResult(_strings[AppStrings.IncorrectTabIndices]);
            }

            return Ok();
        }

        private IActionResult GetWidgetValidationResult(WidgetViewModel widget, int widgetId = 0)
        {
            if (widget == null)
            {
                var message = _strings.Format(AppStrings.RequestFailedNoData, AppStrings.Widget);
                return BadRequestResult(message);
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            if (widget.Id != widgetId)
            {
                var message = _strings.Format(AppStrings.RequestFailedConflict, AppStrings.Widget);
                return BadRequestResult(message);
            }

            if (widgetId > 0 && widget.CreatedById != SecurityContext.User.Id)
            {
                var message = _strings[AppStrings.CannotModifyOtherUserWidget];
                return BadRequestResult(message);
            }

            return Ok();
        }

        private IActionResult GetTabWidgetValidationResult(TabWidgetViewModel widget, int tabId)
        {
            if (widget == null)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.Widget));
            }

            if (!ModelState.IsValid)
            {
                return BadRequestResult(ModelState);
            }

            if (widget.TabId != tabId)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, AppStrings.Widget));
            }

            return Ok();
        }

        private void Localize(DashboardViewModel dashboard)
        {
            if (dashboard != null)
            {
                Array.ForEach(dashboard.Tabs.ToArray(), tab =>
                {
                    tab.Title = _strings[tab.Title];
                    Array.ForEach(tab.Widgets.ToArray(), widget =>
                    {
                        Localize(widget);
                    });
                });
            }
        }

        private void Localize(TabWidgetViewModel widget)
        {
            if (widget != null)
            {
                widget.WidgetFunctionName = _strings[widget.WidgetFunctionName];
                widget.WidgetTypeName = _strings[widget.WidgetTypeName];
            }
        }

        private void Localize(WidgetViewModel widget)
        {
            if (widget != null)
            {
                widget.FunctionName = _strings[widget.FunctionName];
                widget.TypeName = _strings[widget.TypeName];
            }
        }

        private readonly IDashboardRepository _repository;
        private readonly IApiPathProvider _pathProvider;
    }
}