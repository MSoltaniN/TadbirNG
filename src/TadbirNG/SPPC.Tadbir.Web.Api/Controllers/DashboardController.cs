using System;
using System.Collections.Generic;
using System.Globalization;
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
using SPPC.Tadbir.ViewModel.Finance;
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
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> GetUserWidgetsAsync()
        {
            var userWidgets = await _repository.GetCurrentUserWidgetsAsync(GridOptions);
            Array.ForEach(userWidgets.Items.ToArray(), item =>
            {
                Localize(item);
            });
            return JsonListResult(userWidgets);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/widgets/all
        [HttpGet]
        [Route(DashboardApi.AllWidgetsUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageDashboard)]
        public async Task<IActionResult> GetAccessibleWidgetsAsync()
        {
            var allWidgets = await _repository.GetAccessibleWidgetsAsync(GridOptions);
            Array.ForEach(allWidgets.Items.ToArray(), item =>
            {
                Localize(item);
            });
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
        public async Task<string> QueryWidgetUsageAsync(int widgetId)
        {
            string confirmMessage = String.Empty;
            int usageCount = await _repository.GetWidgetUsageCountAsync(widgetId);
            if (usageCount > 0)
            {
                confirmMessage = _strings[AppStrings.ConfirmUsedWidgetDelete];
            }

            return confirmMessage;
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
        /// <returns></returns>
        // DELETE: api/dashboard/widgets/{widgetId:min(1)}
        [HttpDelete]
        [Route(DashboardApi.WidgetUrl)]
        [AuthorizeRequest(SecureEntity.Dashboard, (int)DashboardPermissions.ManageWidgets)]
        public async Task<IActionResult> DeleteExistingWidgetAsync(int widgetId)
        {
            var existing = await _repository.GetWidgetAsync(widgetId);
            var result = GetWidgetValidationResult(existing, widgetId);
            if (result is BadRequestObjectResult)
            {
                return result;
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
        // GET: api/dashboard/summaries
        [HttpGet]
        [Route(DashboardApi.SummariesUrl)]
        public async Task<IActionResult> GetSummariesAsync()
        {
            var summaries = await _repository.GetSummariesAsync(GetCurrentCalendar());
            Localize(summaries);
            return Json(summaries);
        }

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

        private Calendar GetCurrentCalendar()
        {
            string language = GetPrimaryRequestLanguage();
            return language == "fa"
                ? new PersianCalendar()
                : new GregorianCalendar() as Calendar;
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

        private void Localize(DashboardSummariesViewModel summaries)
        {
            summaries.GrossSales.Title = _strings[summaries.GrossSales.Title ?? String.Empty];
            summaries.GrossSales.Legend = _strings[summaries.GrossSales.Legend ?? String.Empty];
            summaries.NetSales.Title = _strings[summaries.NetSales.Title ?? String.Empty];
            summaries.NetSales.Legend = _strings[summaries.NetSales.Legend ?? String.Empty];
            Array.ForEach(summaries.NetSales.Points.ToArray(), point => point.XValue = _strings[point.XValue]);
            Array.ForEach(summaries.GrossSales.Points.ToArray(), point => point.XValue = _strings[point.XValue]);
        }

        private void Localize(DashboardViewModel dashboard)
        {
            if (dashboard != null)
            {
                Array.ForEach(dashboard.Tabs.ToArray(), tab =>
                {
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